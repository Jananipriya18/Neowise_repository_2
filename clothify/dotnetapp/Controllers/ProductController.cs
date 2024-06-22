using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;

// [Authorize]

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [Authorize(Roles = "admin")] 
    [HttpPost]
    public IActionResult AddProduct([FromBody] Product product)
    {
        var addedProduct = _productService.AddProduct(product);
        return Ok(addedProduct);
    }

    [Authorize(Roles = "admin,customer")] 
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var allProducts = _productService.GetAllProducts();
        return Ok(allProducts);
    }

    [Authorize(Roles = "admin,customer")] 
    [HttpPut("{productId}")]
    public IActionResult EditProduct(long productId, [FromBody] Product updatedProduct)
    {
        var editedProduct = _productService.EditProduct(productId, updatedProduct);
        if (editedProduct != null)
        {
            return Ok(editedProduct);
        }
        return NotFound("Product not found");
    }

    [Authorize(Roles = "admin")] 
    [HttpDelete("{productId}")]
    public IActionResult DeleteProduct(long productId)
    {
        var deletedProduct = _productService.DeleteProduct(productId);
        if (deletedProduct != null)
        {
            return Ok(deletedProduct);
        }
        return NotFound("Product not found");
    }
}
