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
    this.getAllGiftsFromCart()
  }

  getAllGiftsFromCart() {
    this.cartService.getAllGiftsFromCart().subscribe(
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
    if (!this.customerData || !this.customerData.gifts || !this.customerData.gifts.$values || this.customerData.gifts.$values.length === 0) {
      console.error('Invalid customer data');
      return;
    }

    
   
    const orderData = {
      customerId: this.customerId,
      quantity: this.customerData.gifts.$values.length,
      orderPrice:this.totalAmount,
      gifts: this.customerData.gifts.$values.map(gift => ({
        // giftId: gift.giftId,
        giftType: gift.giftType,
        giftImageUrl: gift.giftImageUrl,
        giftDetails: gift.giftDetails,
        giftPrice: gift.giftPrice,
        quantity: gift.quantity,
        cartId: gift.cartId
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