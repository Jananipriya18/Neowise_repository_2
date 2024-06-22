  import { Component, OnInit } from '@angular/core';
  import { OrderService } from 'src/app/services/order.service';
  import { CustomerService } from 'src/app/services/customer.service';
  import { GiftService } from 'src/app/services/gift.service';
  import { Router } from '@angular/router';

  @Component({
    selector: 'app-my-orders',
    templateUrl: './my-orders.component.html',
    styleUrls: ['./my-orders.component.css']
  })
  export class MyOrdersComponent implements OnInit {
    orders = [];
    customers = {};
    giftTypes: string[] = [];

    disableAddReview = true;
    constructor(private orderService: OrderService, private customerService: CustomerService, private giftService:GiftService, private router: Router) { }

    ngOnInit(): void {
      this.viewOrderByUserId();
    }

    // viewOrderByUserId() {
    //   this.orderService.viewOrderByUserId().subscribe(
    //     response => {
    //       console.log(response);
    //       this.orders = response.$values;
    //     },
    //     error => {
    //       console.error(error);
    //     }
    //   );
    // }
    viewOrderByUserId() {
      this.orderService.viewOrderByUserId().subscribe(
        response => {
          console.log(response);
          this.orders = response.$values;
          this.orders.forEach(order => {
            if (order.gifts && order.gifts.$values) {
              order.gifts = order.gifts.$values;
              
            } else {
              console.error('order.gifts.$values is not defined:', order.gifts);
            }
          });
          this.disableAddReview = this.orders.length === 0;
        },
        error => {
          console.error(error);
        }
      );
    }
    cancelOrder(orderId: string): void {
      this.orderService.deleteOrder(orderId).subscribe(
        response => {
          console.log(response);
          // Remove the cancelled order from the orders array
          this.orders = this.orders.filter(order => order.orderId !== orderId);
        },
        error => {
          console.error(error);
        }
      );
    }
    navigateToAddReview(){
        // Only navigate if Add Review is not disabled
        this.router.navigate(['/customer/addreview']);
    }
  }
