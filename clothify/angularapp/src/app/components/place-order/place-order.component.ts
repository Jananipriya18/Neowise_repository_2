import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from 'src/app/services/cart.service';
import { CustomerService } from 'src/app/services/customer.service';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css']
})

export class PlaceOrderComponent implements OnInit {
  showSuccessPopup: boolean = false;
  totalAmount : number;
 customerId = Number(localStorage.getItem('customerId'));
//  currentUser = localStorage.getItem('currentUser');

 customerData: any;
  constructor(private cartService: CartService, private customerService: CustomerService, private router: Router, private route: ActivatedRoute, private orderService: OrderService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.totalAmount = params['totalAmount'];
    });
    this.getAllProductsFromCart()
  }

  getAllProductsFromCart() {
    this.cartService.getAllProductsFromCart().subscribe(
      response => {
        console.log(response);
        this.customerData = response;
      },
      error => {
        console.error(error);
      }
    );
  }
  submitOrder() {
    if (!this.customerData || !this.customerData.products || !this.customerData.products.$values || this.customerData.products.$values.length === 0) {
      console.error('Invalid customer data');
      return;
    }

    
   
    const orderData = {
      customerId: this.customerId,
      quantity: this.customerData.products.$values.length,
      orderPrice:this.totalAmount,
      products: this.customerData.products.$values.map(product => ({
        // productId: product.productId,
        productType: product.productType,
        productImageUrl: product.productImageUrl,
        productDetails: product.productDetails,
        productPrice: product.productPrice,
        quantity: product.quantity,
        cartId: product.cartId
      }))
    };
  
    console.log(orderData);
  
    this.orderService.addOrder(orderData).subscribe(
      response => {
        console.log("Order added successfully", response);
      },
      error => {
        console.error("Error adding Order",error);
      }
    );
    
  
      setTimeout(() => {
        this.showSuccessPopup = true;
      }, 2000);
    }

    navigateToDashboard() {
      this.router.navigate(['/customer/myorders']);
  }
 
  }