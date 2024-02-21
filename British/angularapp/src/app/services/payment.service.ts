import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Payment } from '../models/payment.model';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  private apiUrl = 'https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; 
  
  constructor(private http: HttpClient) {}

  private createAuthorizationHeader(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getAllPayments(): Observable<Payment[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Payment[]>(`${this.apiUrl}/api/Payment`, { headers });
  }

  getPaymentById(id: number): Observable<Payment> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Payment>(`${this.apiUrl}/api/Payment/${id}`, { headers });
  }

  createPayment(payment: Payment): Observable<Payment> {
    const headers = this.createAuthorizationHeader();
    return this.http.post<Payment>(`${this.apiUrl}/api/Payment`, payment, { headers });
  }
}
