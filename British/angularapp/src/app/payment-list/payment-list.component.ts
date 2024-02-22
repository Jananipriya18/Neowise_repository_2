// payment-list.component.ts

import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../services/payment.service';
import { Payment } from '../models/payment.model';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.css']
})
export class PaymentListComponent implements OnInit {
  payments: Payment[] = [];

  constructor(private paymentService: PaymentService) {}

  ngOnInit(): void {
    this.fetchAllPayments();
  }

  fetchAllPayments(): void {
    this.paymentService.getAllPayments().subscribe(
      (payments: Payment[]) => {
        this.payments = payments;
      },
      (error) => {
        console.error('Error fetching payments:', error);
        // Handle errors as needed
      }
    );
  }
}
