using System.Collections.Generic;
using dotnetapp.Models;
using dotnetapp.Services;
public class ProductServiceImpl : ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductServiceImpl(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Product AddProduct(Product product)
    {
        return _productRepository.addProduct(product);
    }

    public List<Product> GetAllProducts()
    {
        return _productRepository.getAllProducts();
    }

    public Product EditProduct(long productId, Product updatedProduct)
    {
        return _productRepository.editProduct(productId, updatedProduct);
    }

    public Product DeleteProduct(long productId)
    {
        return _productRepository.deleteProduct(productId);
    }
}
