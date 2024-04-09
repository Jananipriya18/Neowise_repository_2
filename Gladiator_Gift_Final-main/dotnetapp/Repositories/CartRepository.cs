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
        .Include(c => c.Gifts) // Include the Gifts collection
        .FirstOrDefault(c => c.CartId == updatedCart.CartId);
 
    if (existingCart != null)
    {
        // Update individual properties of Cart
        existingCart.CustomerId = updatedCart.CustomerId;
 
        // Update existing gifts based on the updated ones
        foreach (var updatedGift in updatedCart.Gifts)
        {
            var existingGift = existingCart.Gifts.FirstOrDefault(g => g.GiftId == updatedGift.GiftId);
 
            if (existingGift != null)
            {
                existingGift.GiftType = updatedGift.GiftType;
                existingGift.GiftImageUrl = updatedGift.GiftImageUrl;
                existingGift.GiftDetails = updatedGift.GiftDetails;
                existingGift.GiftPrice = updatedGift.GiftPrice;
                existingGift.Quantity = updatedGift.Quantity;
            }
            else
            {
                // Handle case where the gift is not found in the existingCart
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
        .Include(c => c.Gifts)     // Include the associated gifts
        .FirstOrDefault(c => c.CustomerId == customerId);
    }
 
 
    // public List<Gift> getAllGiftsByCustomerId(long customerId)
    // {
    //     var cart = getCartByCustomerId(customerId);
 
    //     if (cart != null)
    //     {
    //         if (cart.Gifts != null)
    //         {
    //             return cart.Gifts.ToList();
    //         }
    //         else
    //         {
    //             Console.WriteLine("Cart has no associated gifts.");
    //         }
    //     }
    //     else
    //     {
    //         Console.WriteLine($"Cart not found for customer ID: {customerId}");
    //     }
 
    //     return new List<Gift>();
    // }
 
    public double CalculateTotalAmount(long customerId)
{
    var cart = getCartByCustomerId(customerId);
 
    if (cart != null)
    {
        if (cart.Gifts != null)
        {
            return cart.Gifts.Sum(g => g.GiftPrice * g.Quantity);
        }
        else
        {
            Console.WriteLine("Cart has no associated gifts.");
        }
    }
    else
    {
        Console.WriteLine($"Cart not found for customer ID: {customerId}");
    }
 
    return 0;
}
 
public bool DeleteGiftFromCartById(int cartId, int giftId)
{
    var cartToUpdate = _context.Carts.Include(c => c.Gifts).FirstOrDefault(c => c.CartId == cartId);
 
    if (cartToUpdate != null)
    {
        var giftToDelete = cartToUpdate.Gifts.FirstOrDefault(g => g.GiftId == giftId);
 
        if (giftToDelete != null)
        {
            cartToUpdate.Gifts.Remove(giftToDelete);
            _context.SaveChanges();
            return true; // Indicate successful deletion
        }
 
        // Gift with specified ID not found in the cart
        return false;
    }
 
    // Cart with specified ID not found
    return false;
}
 
 
 
}