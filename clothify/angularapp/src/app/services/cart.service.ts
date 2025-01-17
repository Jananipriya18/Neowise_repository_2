import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class CartService {
  public apiUrl =
    'https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io';

  constructor(private http: HttpClient) { }

  updateCart(cartDetails: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    console.log(cartDetails);
    const cartId = localStorage.getItem('cartId');
    return this.http.put(`${this.apiUrl}/api/cart/update/${cartId}`, cartDetails, { headers });
  }

  removeProductsFromCart(cartDetails: any, productId: any): any {
    console.log("cartId", cartDetails.cartId)
    console.log("cartDetails", cartDetails)
    console.log("Product Details", cartDetails.products)
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.delete(`${this.apiUrl}/api/cart/${cartDetails.cartId}?productId=${productId}`, { headers });
  }

  getProductsFromCart(cartId: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get(`${this.apiUrl}/api/cart/${cartId}`, { headers });
  }


  getAllProductsFromCart(): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    const customerId = localStorage.getItem('customerId');
    return this.http.get(`${this.apiUrl}/api/cart/customer/${customerId}`, { headers });
  }

  addReview(review: any): Observable<any> {
    const token = localStorage.getItem('token');
    // console.log(token)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });

    return this.http.post(`${this.apiUrl}/api/review`, review, { headers });
  }

  getAllReviews() {
    const token = localStorage.getItem('token');
    // console.log(token)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get(`${this.apiUrl}/api/review`, { headers });
  }

  getTotalAmount(customerId: string): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get(`${this.apiUrl}/api/cart/customer/${customerId}/total`, { headers });
  }

}
