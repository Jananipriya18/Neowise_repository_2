//new
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
 
 
 
[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;
    private readonly GiftService _giftService;
 
    public CartController(CartService cartService, GiftService giftService)
    {
        _cartService = cartService;
        _giftService = giftService;
    }
 
    [Authorize(Roles = "customer")]      
    [HttpPut("update/{cartId}")]
    public IActionResult UpdateCart([FromBody] Cart updatedCart)
    {
       
        var updated = _cartService.updateCart(updatedCart);
 
        if (updated != null)
        {
            return Ok(updated);
        }
        return BadRequest("Cart not found");
    }
 
    [Authorize(Roles = "customer")]      
    [HttpGet("customer/{customerId}")]
    public IActionResult GetCartByCustomerId(long customerId)
    {
        var cart = _cartService.getCartByCustomerId(customerId);
        if (cart != null)
        {
            return Ok(cart);
        }
        return NotFound($"Cart not found for customer ID: {customerId}");
    }
 
    [Authorize(Roles = "admin,customer")]
    [HttpGet("customer/{customerId}/total")]
    public IActionResult GetCustomerCartTotalAmount(long customerId)
    {
        double totalAmount = _cartService.CalculateTotalAmount(customerId);
 
        return Ok(new { TotalAmount = totalAmount });
    }
 
    [Authorize(Roles = "customer")]
    [HttpDelete("{cartId}")]
    public IActionResult DeleteGiftFromCartById(int cartId, int giftId)
    {
        var success = _cartService.DeleteGiftFromCartById(cartId, giftId);
 
        if (success)
        {
            return Ok("Gift deleted successfully from the cart.");
        }
        else
        {
            return NotFound("Gift or Cart not found for the specified IDs.");
        }
    }
}