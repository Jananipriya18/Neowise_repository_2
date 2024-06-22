using System.Collections.Generic;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Collections.Generic;
using dotnetapp.Models;
using dotnetapp.Services;
using dotnetapp.Data;
using dotnetapp.Repositories;
using Microsoft.EntityFrameworkCore;
 
 
public class CartServiceImpl : CartService
{
    private readonly CartRepository _cartRepository;
    private readonly ApplicationDbContext _context;
 
    public CartServiceImpl(CartRepository cartRepository, ApplicationDbContext context)
    {
        _cartRepository = cartRepository;
        _context = context;
    }
 
 
   
    public Cart updateCart(Cart updatedCart)
    {
        return _cartRepository.updateCart(updatedCart);
    }
 
    public Cart getCartByCustomerId(long customerId)
    {
        return _cartRepository.getCartByCustomerId(customerId);
    }
 
    public double CalculateTotalAmount(long customerId)
    {
        return _cartRepository.CalculateTotalAmount(customerId);
    }
    public bool DeleteGiftFromCartById(int cartId, int productId)
    {
        return _cartRepository.DeleteGiftFromCartById(cartId, productId);
    }
}