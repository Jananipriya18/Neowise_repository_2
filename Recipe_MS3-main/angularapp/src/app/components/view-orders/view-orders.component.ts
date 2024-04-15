import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order.service';
import { Order } from 'src/app/models/order.model';
import { CustomerService } from 'src/app/services/customer.service';
import { Customer } from 'src/app/models/customer.model';
import { forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-view-orders',
  templateUrl: './view-orders.component.html',
  styleUrls: ['./view-orders.component.css']
})
export class ViewOrdersComponent implements OnInit {
  orders: any;
  message: string = '';

  constructor(private orderService: OrderService, private customerService: CustomerService) { }

  ngOnInit(): void {
    this.viewAllOrders();
  }
  viewAllOrders() {
    // this.orderService.viewAllOrders().subscribe((response: any) => {
    //   this.orders = response;
    //   console.log(this.orders);
    //   if (this.orders.length === 0) {
    //     this.message = 'No orders placed';
    //   } else {
    //     this.message = '';
    //   }
    // }, error => {
    //   console.error('Error:', error);
    // });
    // this.orderService.viewAllOrders().subscribe((response: any) => {
    //   this.orders = Array.isArray(response) ? response : Object.values(response);
    //   console.log(this.orders);
    //   if (this.orders.length === 0) {
    //     this.message = 'No orders placed';
    //   } else {
    //     this.message = '';
    //   }
    // }, error => {
    //   console.error('Error:', error);
    // });
    // this.orderService.viewAllOrders().subscribe((response: any) => {
    //   console.log(response);
    //   this.orders = response;
    //   console.log(this.orders);
    //   if (!this.orders || this.orders.length === 0) {
    //     this.message = 'No orders placed';
    //   } else {
    //     this.message = '';
    //   }
    // }, error => {
    //   console.error('Error:', error);
    // });
    // this.orderService.viewAllOrders().subscribe((response: any) => {
    //   console.log(response);
    //   this.orders = response.$values;
    //   console.log(this.orders);
    //   if (!this.orders || this.orders.length === 0) {
    //     this.message = 'No orders placed';
    //   } else {
    //     this.message = '';
    //   }
    // }, error => {
    //   console.error('Error:', error);
    // });
    // this.orderService.viewAllOrders().subscribe((response: any) => {
    //   console.log(response);
    //   if (response.$values && Array.isArray(response.$values)) {
    //     this.orders = response.$values;
    //   } else if (response.$values && typeof response.$values === 'object') {
    //     this.orders = Object.values(response.$values);
    //   } else {
    //     this.orders = [];
    //   }
    //   console.log(this.orders);
    //   if (!this.orders || this.orders.length === 0) {
    //     this.message = 'No orders placed';
    //   } else {
    //     this.message = '';
    //   }
    // }, error => {
    //   console.error('Error:', error);
    // });
  //   this.orderService.viewAllOrders().subscribe((response: any) => {
  //     console.log(response);
  //     if (response.$values && Array.isArray(response.$values)) {
  //       this.orders = response.$values;
  //     } else if (response.$values && typeof response.$values === 'object') {
  //       this.orders = Object.values(response.$values);
  //     } else {
  //       this.orders = [];
  //     }

  //     // Fetch the customer details for each order
  //     this.orders.forEach((order: any) => {
  //       this.customerService.viewCustomerById(order.customerId).subscribe((customer: any) => {
  //         order.customer = customer; // Add the customer details to the order
  //       });
  //     });

  //     console.log(this.orders);
  //     if (!this.orders || this.orders.length === 0) {
  //       this.message = 'No orders placed';
  //     } else {
  //       this.message = '';
  //     }
  //   }, error => {
  //     console.error('Error:', error);
  //   });
  // }
  // }

//   this.orderService.viewAllOrders().subscribe((response: any) => {
//     console.log(response);
//     let orders = [];
//     if (response.$values && Array.isArray(response.$values)) {
//       orders = response.$values;
//     } else if (response.$values && typeof response.$values === 'object') {
//       orders = Object.values(response.$values);
//     }

//     const observables = orders.map((order: any) => {
//       return this.customerService.viewCustomerById(order.customerId).pipe(
//         map((customer: any) => {
//           order.customer = customer;
//           return order;
//         })

//       );
//     });

//     forkJoin(observables).subscribe((completedOrders: any) => {
//       this.orders = completedOrders;
//       console.log(this.orders);
//       if (!this.orders || this.orders.length === 0) {
//         this.message = 'No orders placed';
//       } else {
//         this.message = '';
//       }
//     }, error => {
//       console.error('Error:', error);
//     });
//   }, error => {
//     console.error('Error:', error);
//   });
// }
// }

this.orderService.viewAllOrders().subscribe((response: any) => {
      console.log(response);
      if (response.$values && Array.isArray(response.$values)) {
        this.orders = response.$values;
      } else if (response.$values && typeof response.$values === 'object') {
        this.orders = Object.values(response.$values);
      } else {
        this.orders = [];
      }
      console.log('Orders:', this.orders);
    // Fetch the customer details for each order
    this.orders.forEach((order: any) => {
      this.customerService.viewCustomerById(order.customerId).subscribe((customer: any) => {
        order.customer = customer; // Add the customer details to the order
        if (order.gifts && typeof order.gifts === 'object') {
          // Convert gifts to an array if it's an object
          order.gifts = Object.values(order.gifts);
        }
      });
    });

    console.log(this.orders);
    if (!this.orders || this.orders.length === 0) {
      this.message = 'No orders placed';
    } else {
      this.message = '';
    }
  }, error => {
    console.error('Error:', error);
  });
}
}

  
