import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-cart',
  templateUrl: './my-cart.component.html',
  styleUrls: ['./my-cart.component.css'],
})
export class MyCartComponent implements OnInit {
  maxQuantity: number; // Define maxQuantity
  customerData = { totalAmount: 0 };
  products = [];
  totalAmount = 0;
  productsCart: any;
  
  cartId = Number(localStorage.getItem('cartId'));
  userQuantity: number = 1;
  
  constructor(private cartService: CartService, private router: Router) {}
  
  // productsCart = {};
  ngOnInit(): void {
    this.updateTotalAmount();
    this.getAllProductsFromCart();
  }

  getAllProductsFromCart() {
    this.cartService.getAllProductsFromCart().subscribe(
      (response) => {
        console.log(response);
        if (response && response.products) {
          // Check if the products property is an object with $values array
          if (Array.isArray(response.products.$values)) {
            // Extract the array from the nested object
            this.products = response.products.$values;
          } else {
            // If $values array is not present, consider products itself as the array
            this.products = response.products;
          }
          this.totalAmount = response.totalAmount;
          this.products.forEach(product => {
            product.userQuantity = 1;
          });
        } else {
          console.error('Invalid response format:', response);
        }
      },
      (error) => {
        console.error(error);
      }
    );
    //new
  }
  
  calculateTotalAmount(): number {
    let totalAmount = 0;
    for (const product of this.products) {
      totalAmount += product.userQuantity * product.productPrice;
    }
    return totalAmount;
  }
  validateQuantity(productData: any): void {
    // Your validation logic here, using productData.userQuantity
  }
  
  // initializeQuantity() {
  //   this.products.forEach(productData => {
  //     productData.maxQuantity = 1;
  //   });
  // }


  placeOrder() {
    const totalAmount = this.calculateTotalAmount();
    this.router.navigate(['/customer/placeorder'], { queryParams: { totalAmount } });
  }


  updateQuantity(productData: any): void {
    if (productData.quantity > productData.userQuantity) {
      productData.quantity = productData.userQuantity;
    }
    
    productData.totalAmount = productData.userQuantity * productData.productPrice; // Update the total amount for the specific product
    
    const customerId = localStorage.getItem('customerId'); // Get the customerId from local storage
    
    // Call getTotalAmount method from CartService with customerId as argument
    this.cartService.getTotalAmount(customerId).subscribe(
      response => {
        console.log(response); // Handle the response as per your requirement
      },
      error => {
        console.error('Error:', error); 
      }
    );
    this.updateTotalAmount();
    console.log('Total amount updated:', this.totalAmount);
  }
  updateTotalAmount(): void {
    this.totalAmount = this.products.reduce((total, product) => total + (this.userQuantity * product.productPrice), 0);
    console.log('Total amount:', this.totalAmount);
  }

  // removeFromCart(productId: number) {
  //   this.products = this.products.filter(product => product.productId !== productId);
  //   this.productsCart = {
  //     cartId: Number(localStorage.getItem('cartId')),
  //     customer: { customerId: Number(localStorage.getItem('customerId')) },
  //     products: [...this.products],
  //     totalAmount: this.totalAmount - this.products.filter(product => product.productId === productId)[0].productPrice
  //   } 
  //   console.log(this.productsCart);
  //   this.cartService.updateCart(this.productsCart).subscribe(
  //     response => {
  //       console.log(response);
  //       console.log('Product removed from cart successfully');
  //       this.getAllProductsFromCart();
  //     },
  //     error => {
  //       console.error(error);
  //     }
  //   );
  // }
  removeFromCart(productId: number) {
    const index = this.products.findIndex(product => product.productId === productId);
    if (index !== -1) {
      const removedProduct = this.products.splice(index, 1)[0]; // Remove the product from the array
      this.totalAmount -= removedProduct.productPrice; // Adjust the total amount
      const productsCart = {
        cartId: this.cartId,
        // productId:this.productId,
        customer: { customerId: Number(localStorage.getItem('customerId')) },
        products: [...this.products],
        totalAmount: this.totalAmount,
        updatedCart: true
      }; 
      console.log(productsCart);
      this.cartService.removeProductsFromCart(productsCart, productId).subscribe(
        response => {
          console.log(response);
          console.log('Product removed from cart successfully');
          this.getAllProductsFromCart();
        },
        error => {
          console.error(error);
        }
      );
    } else {
      console.error('Product not found in the cart');
    }
  }

}
