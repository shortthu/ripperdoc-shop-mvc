using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using RipperdocShop.Data;
using RipperdocShop.Models.Identities;
using RipperdocShop.Interceptors;
using RipperdocShop.Services;
using RipperdocShop.Services.Admin;
using RipperdocShop.Services.Core;

var builder = WebApplication.CreateBuilder(args);

var jwtSecret = builder.Configuration["Jwt:Secret"];

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<TimestampInterceptor>();

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IBrandCoreService, BrandCoreService>();
builder.Services.AddScoped<IAdminBrandService, AdminBrandService>();
builder.Services.AddScoped<ICategoryCoreService, CategoryCoreService>();
builder.Services.AddScoped<IAdminCategoryService, AdminCategoryService>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
     // Jwt bearer for API (Admin)
     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
     {
         options.RequireHttpsMetadata = false; // for dev
         options.SaveToken = true;
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidIssuer = builder.Configuration["Jwt:Issuer"],
             ValidAudience = builder.Configuration["Jwt:Audience"],
             ValidateIssuerSigningKey = true,
             ValidateLifetime = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                 jwtSecret ?? throw new InvalidOperationException())),
             ClockSkew = TimeSpan.Zero
         };
         options.Events = new JwtBearerEvents
         {
             OnMessageReceived = context =>
             {
                 if (context.Request.Cookies.TryGetValue("AdminAccessToken", out var token))
                     context.Token = token;
                 return Task.CompletedTask;
             }
         };
     });

builder.Services.AddScoped<JwtService>();

builder.Services.AddSwaggerGen(options =>
{
    // JWT Bearer
    var jwtScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Put only your JWT Bearer token here.",
        
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    
    options.AddSecurityDefinition(jwtScheme.Reference.Id, jwtScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtScheme,  [] }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Seed Roles & an Admin User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var adminEmail = builder.Configuration["Admin:Email"] ?? Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        var adminPassword = builder.Configuration["Admin:Password"] ?? Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            throw new Exception("Admin:Email or Admin:Password is not set. Please set them in the appsettings.json file or in the environment variables ADMIN_EMAIL and ADMIN_PASSWORD.");
        }
        await IdentitySeeder.SeedAsync(services, adminEmail, adminPassword);;
        logger.LogInformation("Identity seeding complete.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding identity roles and users.");
        throw;
    }
}

app.Run();
