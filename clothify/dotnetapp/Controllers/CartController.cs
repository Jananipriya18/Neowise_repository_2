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
    private readonly ProductService _productService;
 
    public CartController(CartService cartService, ProductService productService)
    {
        _cartService = cartService;
        _productService = productService;
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
    public IActionResult DeleteProductFromCartById(int cartId, int productId)
    {
        var success = _cartService.DeleteProductFromCartById(cartId, productId);
 
        if (success)
        {
            return Ok("Product deleted successfully from the cart.");
        }
        else
        {
            return NotFound("Product or Cart not found for the specified IDs.");
        }
    }
}