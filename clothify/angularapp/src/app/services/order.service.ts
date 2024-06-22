import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  public apiUrl='https://8080-bfabeabdaebdadfccdadbafeaecccaaeccf.premiumproject.examly.io'

  constructor(private http: HttpClient) { }
  
  addOrder(orderData: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    console.log(orderData);
    return this.http.post(`${this.apiUrl}/api/order`, orderData);
  }

  cancelOrder(orderId: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.delete(`${this.apiUrl}/api/order/${orderId}`);
  }

  // viewAllOrders(orderData: any): any {
  //   const token = localStorage.getItem('token');
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  //   });
  //   return this.http.get(`${this.apiUrl}/api/order`, orderData);
  // }

  viewAllOrders(): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get(`${this.apiUrl}/api/order`, { headers: headers });
  }

  viewOrderByUserId(): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    const customerId = localStorage.getItem('customerId');
    console.log(customerId,"customerId");
    return this.http.get(`${this.apiUrl}/api/order/customer/${customerId}`);
  }

  // viewOrderByCustomerId(customerId: any): any {
  //   return this.http.get(`${this.apiUrl}/api/order/customer/${customerId}`);
  // }

  viewOrderById(orderId: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get(`${this.apiUrl}/api/order/${orderId}`);
  }

  deleteOrder(orderId: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.delete(`${this.apiUrl}/api/order/${orderId}`, { headers });
  }

}
