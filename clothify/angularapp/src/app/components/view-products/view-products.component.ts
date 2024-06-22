import { Component } from '@angular/core';
import { Router } from '@angular/router';
// import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';

interface Product {
  productId: string;
}

@Component({
  selector: 'app-view-products',
  templateUrl: './view-products.component.html',
  styleUrls: ['./view-products.component.css']
})
export class ViewProductsComponent {
  products: Product[] = [];
  constructor(private router: Router,private productService: ProductService) { }
  
  // ngOnInit(): void {
  //   this.productService.viewAllProducts().subscribe((products: Product[]) => {
  //     console.log(products); // Add this line
  //     this.products = products;
  //   }, (error) => {
  //     console.error(error);
  //   });
  // }

  ngOnInit(): void {
    this.productService.viewAllProducts().subscribe((response: any) => {
      this.products = response.$values; // assuming $values is the array of products
    }, (error) => {
      console.error(error);
    });
  }

  editProduct(product: Product): void {
    // Serialize the product object and pass it as a query parameter
    const queryParams = {
      product: JSON.stringify(product)
    };

    // Navigate to the edit page with the serialized product object
    this.router.navigate(['/admin/editproduct', product.productId], { queryParams: queryParams });
  }

  // deleteProduct(product: Product): void {
  //   this.productService.deleteProduct(product.productId).subscribe(() => {
  //     console.log('Product deleted successfully');
  //     this.products = this.products.filter(g => g.productId !== product.productId);
  //   }, (error) => {
  //     console.error(error);
  //   });
  // }
  deleteProduct(product: any): void {
    const userConfirmed = window.confirm(`Are you sure you want to delete the product '${product.productType}'?`);

    if (userConfirmed) {
      this.productService.deleteProduct(product.productId).subscribe(() => {
            console.log('Product deleted successfully');
            this.products = this.products.filter(g => g.productId !== product.productId);
          }, (error) => {
            console.error(error);
          });
    }
  }

}
