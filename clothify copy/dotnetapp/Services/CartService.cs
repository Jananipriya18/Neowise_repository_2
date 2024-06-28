using dotnetapp.Models;
 
namespace dotnetapp.Services
{
    public interface CartService
    {
 
        Cart updateCart(Cart updatedCart);
        Cart getCartByCustomerId(long customerId);
        double CalculateTotalAmount(long customerId);
        bool DeleteProductFromCartById(int cartId, int productId);
    }
}