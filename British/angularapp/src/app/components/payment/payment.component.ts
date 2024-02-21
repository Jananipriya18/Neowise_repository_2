// // import { Component, OnInit } from '@angular/core';
// // import { PaymentService } from '../../services/payment.service';
// // import { Payment } from '../../models/payment.model';
// // import { User } from 'src/app/models/user.model';
// // import { ActivatedRoute } from '@angular/router';
// // import { AuthService } from '../../services/auth.service';

// // @Component({
// //   selector: 'app-payment-list',
// //   templateUrl: './payment.component.html',
// //   styleUrls: ['./payment.component.css']
// // })
// // export class PaymentComponent implements OnInit {
// //   payments: Payment[] = [];
// //   newPayment: Payment = {
// //     paymentID: 0,
// //     userId: '', // Add userId property based on your authentication logic
// //     courseID: 0, // Add courseID property if applicable
// //     amountPaid: 0,
// //     paymentDate: new Date(),
// //     paymentMethod: '',
// //     transactionID: '',
// //   };

// //   constructor(private paymentService: PaymentService,
// //     private route: ActivatedRoute,
// //     private authService: AuthService) {}

// //   ngOnInit(): void {
// //     this.fetchAllPayments();
// //     this.courseID = +this.route.snapshot.paramMap.get('courseId');
// //     this.courseName = this.route.snapshot.queryParamMap.get('courseName');
// //     this.userId = this.route.snapshot.queryParamMap.get('userId');     
// //   }

// //   fetchAllPayments(): void {
// //     this.paymentService.getAllPayments().subscribe(
// //       (payments: Payment[]) => {
// //         this.payments = payments;
// //         console.log('Payments:', this.payments);
// //       },
// //       (error) => {
// //         console.error('Error fetching payments:', error);
// //         // Handle errors as needed
// //       }
// //     );
// //   }

// //   makePayment(): void {
// //     this.paymentService.createPayment(this.newPayment).subscribe(
// //       (createdPayment: Payment) => {
// //         console.log('Payment successful:', createdPayment);
// //         // Optionally, you can refresh the payments list after a successful payment
// //         this.fetchAllPayments();
// //       },
// //       (error) => {
// //         console.error('Error making payment:', error);
// //         // Handle errors as needed
// //       }
// //     );
// //   }
// // }


// import { Component, OnInit } from '@angular/core';
// import { PaymentService } from '../../services/payment.service';
// import { Payment } from '../../models/payment.model';
// import { ActivatedRoute } from '@angular/router';
// import { AuthService } from '../../services/auth.service';

// @Component({
//   selector: 'app-payment-list',
//   templateUrl: './payment.component.html',
//   styleUrls: ['./payment.component.css']
// })
// export class PaymentComponent implements OnInit {
//   payments: Payment[] = [];
//   newPayment: Payment = {
//     paymentID: 0,
//     userId: '', // Use string type for userId
//     courseID: 0,
//     amountPaid: 0,
//     paymentDate: new Date(),
//     paymentMethod: '',
//     transactionID: '',
//   };

//   constructor(
//     private paymentService: PaymentService,
//     private route: ActivatedRoute,
//     private authService: AuthService // Inject AuthService
//   ) {}

//   ngOnInit(): void {
//     this.fetchAllPayments();
//     this.newPayment.userId = this.authService.getCurrentUserId(); // Use AuthService to get userId
//   }

//   fetchAllPayments(): void {
//     this.paymentService.getAllPayments().subscribe(
//       (payments: Payment[]) => {
//         this.payments = payments;
//         console.log('Payments:', this.payments);
//       },
//       (error) => {
//         console.error('Error fetching payments:', error);
//         // Handle errors as needed
//       }
//     );
//   }

//   makePayment(): void {
//     this.paymentService.createPayment(this.newPayment).subscribe(
//       (createdPayment: Payment) => {
//         console.log('Payment successful:', createdPayment);
//         // Optionally, you can refresh the payments list after a successful payment
//         this.fetchAllPayments();
//       },
//       (error) => {
//         console.error('Error making payment:', error);
//         // Handle errors as needed
//       }
//     );
//   }
// }


// payment.component.ts

import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Payment } from '../../models/payment.model';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  payments: Payment[] = [];
  newPayment: Payment = {
    paymentID: 0,
    userId: 0, // Use number type for userId
    courseID: 0,
    amountPaid: 0,
    paymentDate: new Date(),
    paymentMethod: '',
    transactionID: '',
  };

  constructor(
    private paymentService: PaymentService,
    private route: ActivatedRoute,
    private authService: AuthService // Inject AuthService
  ) {}

  ngOnInit(): void {
    this.fetchAllPayments();
    // Convert the string to a number before assigning it to userId
    this.newPayment.userId = Number(this.authService.getCurrentUserId());
  }

  fetchAllPayments(): void {
    this.paymentService.getAllPayments().subscribe(
      (payments: Payment[]) => {
        this.payments = payments;
        console.log('Payments:', this.payments);
      },
      (error) => {
        console.error('Error fetching payments:', error);
        // Handle errors as needed
      }
    );
  }

  makePayment(): void {
    this.paymentService.createPayment(this.newPayment).subscribe(
      (createdPayment: Payment) => {
        console.log('Payment successful:', createdPayment);
        // Optionally, you can refresh the payments list after a successful payment
        this.fetchAllPayments();
      },
      (error) => {
        console.error('Error making payment:', error);
        // Handle errors as needed
      }
    );
  }
}

