import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  public apiUrl =
    'https://8080-bfabeabdaebdadfccdadbafeaecccaaeccf.premiumproject.examly.io';

  constructor(private http: HttpClient) {}


  registerCustomer(customer: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    console.log(customer);

    return this.http.post(`${this.apiUrl}/api/customer`,customer,{headers});
  }

  viewCustomerById(customerId: any): any {
  const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    console.log(customerId);
    return this.http.get(`${this.apiUrl}/api/customer/${customerId}`, {headers});
  }

}
