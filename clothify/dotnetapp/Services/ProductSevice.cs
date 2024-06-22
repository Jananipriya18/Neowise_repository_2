// Services/ProductService.cs
using System.Collections.Generic;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public interface ProductService
    {
        Product AddProduct(Product product);
        List<Product> GetAllProducts();
        Product EditProduct(long productId, Product updatedProduct);
        Product DeleteProduct(long productId);
    }
}

