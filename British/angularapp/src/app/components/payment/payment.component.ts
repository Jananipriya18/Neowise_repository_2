import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Payment } from '../../models/payment.model';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  payments: Payment[] = [];
  newPayment: Payment = {
    paymentID: 0,
    userId: 0, // Add userId property based on your authentication logic
    courseID: 0, // Add courseID property if applicable
    amountPaid: 0,
    paymentDate: new Date(),
    paymentMethod: '',
    transactionID: '',
  };

  constructor(private paymentService: PaymentService) {}

  ngOnInit(): void {
    this.fetchAllPayments();
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
