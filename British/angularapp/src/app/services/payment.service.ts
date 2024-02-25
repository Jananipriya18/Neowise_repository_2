import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
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
    return this.http.get<Payment[]>(`${this.apiUrl}/api/admin/payment`, { headers });
  }

  getPaymentById(id: number): Observable<Payment> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Payment>(`${this.apiUrl}/api/Payment/${id}`, { headers });
  }

//   createPayment(payment: Payment): Observable<Payment> {
//     const headers = this.createAuthorizationHeader();
//     console.log("payload test", payment);
//     return this.http.post<Payment>(`${this.apiUrl}/api/student/payment`, payment, { headers });
//   }

createPayment(payment: Payment): Observable<Payment> {
  const headers = this.createAuthorizationHeader();
  console.log("payload test", payment);
  return this.http.post<Payment>(`${this.apiUrl}/api/student`, payment, { headers })
    .pipe(
      tap((response: Payment) => {
        console.log('Backend Response:', response);
      })
    );
}
}
