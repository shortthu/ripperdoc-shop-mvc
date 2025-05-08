using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using RipperdocShop.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("CustomerApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CustomerApi:URL"] ?? string.Empty);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IProductService, ProductService>()
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<IBrandService, BrandService>()
    .AddScoped<IProductRatingService, ProductRatingService>()
    .AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        // options.AccessDeniedPath = "/AccessDenied";
        options.Cookie.HttpOnly = true;
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{slug?}");

app.Run();
