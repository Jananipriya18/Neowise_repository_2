//new
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
 
public class CartRepository
{
    private readonly ApplicationDbContext _context;
 
    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }
 
public Cart updateCart(Cart updatedCart)
{
    var existingCart = _context.Carts
        .Include(c => c.Products) // Include the Products collection
        .FirstOrDefault(c => c.CartId == updatedCart.CartId);
 
    if (existingCart != null)
    {
        // Update individual properties of Cart
        existingCart.CustomerId = updatedCart.CustomerId;
 
        // Update existing products based on the updated ones
        foreach (var updatedProduct in updatedCart.Products)
        {
            var existingProduct = existingCart.Products.FirstOrDefault(g => g.ProductId == updatedProduct.ProductId);
 
            if (existingProduct != null)
            {
                existingProduct.ProductType = updatedProduct.ProductType;
                existingProduct.ProductImageUrl = updatedProduct.ProductImageUrl;
                existingProduct.ProductDetails = updatedProduct.ProductDetails;
                existingProduct.ProductPrice = updatedProduct.ProductPrice;
                existingProduct.Quantity = updatedProduct.Quantity;
            }
            else
            {
                // Handle case where the product is not found in the existingCart
                // You may choose to add it or skip it depending on your requirements
            }
        }
 
        _context.SaveChanges();
 
        return existingCart;
    }
 
    return null;
}
 
 
 
   
    public Cart getCartByCustomerId(long customerId)
    {
        // return _context.Carts.FirstOrDefault(c => c.CustomerId == customerId);
        return _context.Carts
        .Include(c => c.Customer)  // Include the associated customer details
        .Include(c => c.Products)     // Include the associated products
        .FirstOrDefault(c => c.CustomerId == customerId);
    }
 
 
    // public List<Product> getAllProductsByCustomerId(long customerId)
    // {
    //     var cart = getCartByCustomerId(customerId);
 
    //     if (cart != null)
    //     {
    //         if (cart.Products != null)
    //         {
    //             return cart.Products.ToList();
    //         }
    //         else
    //         {
    //             Console.WriteLine("Cart has no associated products.");
    //         }
    //     }
    //     else
    //     {
    //         Console.WriteLine($"Cart not found for customer ID: {customerId}");
    //     }
 
    //     return new List<Product>();
    // }
 
    public double CalculateTotalAmount(long customerId)
{
    var cart = getCartByCustomerId(customerId);
 
    if (cart != null)
    {
        if (cart.Products != null)
        {
            return cart.Products.Sum(g => g.ProductPrice * g.Quantity);
        }
        else
        {
            Console.WriteLine("Cart has no associated products.");
        }
    }
    else
    {
        Console.WriteLine($"Cart not found for customer ID: {customerId}");
    }
 
    return 0;
}
 
public bool DeleteProductFromCartById(int cartId, int productId)
{
    var cartToUpdate = _context.Carts.Include(c => c.Products).FirstOrDefault(c => c.CartId == cartId);
 
    if (cartToUpdate != null)
    {
        var productToDelete = cartToUpdate.Products.FirstOrDefault(g => g.ProductId == productId);
 
        if (productToDelete != null)
        {
            cartToUpdate.Products.Remove(productToDelete);
            _context.SaveChanges();
            return true; // Indicate successful deletion
        }
 
        // Product with specified ID not found in the cart
        return false;
    }
 
    // Cart with specified ID not found
    return false;
}
 
 
 
}