using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;

public class ProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Product addProduct(Product product)
    {
        // product.CartId = ;
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }



    public List<Product> getAllProducts()
    {
        Console.WriteLine("product repo");
        return _context.Products.ToList();
    }

    public Product editProduct(long productId, Product updatedProduct)
{
    var existingProduct = _context.Products
        .Include(g => g.Cart) // Include the associated Cart
        .FirstOrDefault(g => g.ProductId == productId);

    if (existingProduct != null)
    {
        existingProduct.ProductType = updatedProduct.ProductType;
        existingProduct.ProductImageUrl = updatedProduct.ProductImageUrl;
        existingProduct.ProductDetails = updatedProduct.ProductDetails;
        existingProduct.ProductPrice = updatedProduct.ProductPrice;
        existingProduct.Quantity = updatedProduct.Quantity;

        // If the input provides a new CartId, update it
        if (updatedProduct.CartId != 0)
        {
            existingProduct.CartId = updatedProduct.CartId;
        }
        else
        {
            // Fetch the latest CartId directly from the Cart table
            var latestCartId = _context.Carts
                .Where(c => c.CustomerId == existingProduct.Cart.CustomerId)
                .OrderByDescending(c => c.CartId)
                .Select(c => c.CartId)
                .FirstOrDefault();

            // Update the CartId in the existingProduct
            existingProduct.CartId = latestCartId;
        }

        _context.SaveChanges();
        return existingProduct;
    }

    return null; // Product not found
}

    public Product deleteProduct(long productId)
    {
        var productToRemove = _context.Products.Find(productId);

        if (productToRemove != null)
        {
            _context.Products.Remove(productToRemove);
            _context.SaveChanges();
            return productToRemove;
        }

        return null; // Product not found
    }
}
