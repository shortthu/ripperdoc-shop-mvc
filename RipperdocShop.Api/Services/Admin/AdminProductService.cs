using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Api.Data;
using RipperdocShop.Shared.DTOs;
using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Api.Services.Core;

namespace RipperdocShop.Api.Services.Admin;

public class AdminProductService(
    ApplicationDbContext context,
    IProductCoreService productCoreService)
    : IAdminProductService
{
    public async Task<Product> CreateAsync(ProductDto dto, Category category, Brand? brand)
    {
        var product = new Product(
            dto.Name,
            dto.Description,
            dto.ImageUrl,
            dto.Price,
            category,
            dto.IsFeatured,
            brand
        );

        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(Guid id, ProductDto dto, Category category, Brand? brand)
    {
        var product = await productCoreService.GetByIdAsync(id);
        if (product == null)
            return null;

        if (product.DeletedAt != null)
            throw new InvalidOperationException("Cannot update a deleted product. Restore it first, choom.");

        product.UpdateDetails(
            dto.Name,
            dto.Description,
            dto.ImageUrl,
            dto.Price,
            category,
            brand
        );

        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> SetFeaturedAsync(Guid id)
    {
        var product = await productCoreService.GetByIdAsync(id);
        if (product == null)
            return null;

        product.SetFeatured();
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> RemoveFeaturedAsync(Guid id)
    {
        var product = await productCoreService.GetByIdAsync(id);
        if (product == null)
            return null;

        product.RemoveFeatured();
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> SoftDeleteAsync(Guid id)
    {
        var product = await productCoreService.GetByIdAsync(id);
        if (product == null)
            return null;

        product.SoftDelete();
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> RestoreAsync(Guid id)
    {
        var product = await productCoreService.GetByIdAsync(id);
        if (product == null)
            return null;

        product.Restore();
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> DeletePermanentlyAsync(Guid id)
    {
        var product = await context.Products
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return product;
    }
}
