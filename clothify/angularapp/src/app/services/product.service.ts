import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  public apiUrl='https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'

  constructor(private http: HttpClient) { }

  addProduct(product: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    console.log(product);
    return this.http.post(`${this.apiUrl}/api/product`, product, {headers});
  }

  viewAllProducts(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get<any[]>(`${this.apiUrl}/api/product`, {headers});
  }

  // updateProduct(productId: any, updatedProduct: any): any {
  //   const token = localStorage.getItem('token');
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  //   });
  //   const cartId = localStorage.getItem('cartId'); // Get the cartId from localStorage
  //   updatedProduct.cartId = cartId; 
  //   console.log(productId,updatedProduct);
  //   return this.http.put(`${this.apiUrl}/api/product/${productId}`, updatedProduct, {headers});
  // }

  updateProduct(productId: any, updatedProduct: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
  
    // Check if customerId is present in localStorage
    const customerId = localStorage.getItem('customerId');
    if (customerId) {
      const cartId = localStorage.getItem('cartId'); // Get the cartId from localStorage
      updatedProduct.cartId = cartId;
    }
  
    console.log(productId, updatedProduct);
    return this.http.put(`${this.apiUrl}/api/product/${productId}`, updatedProduct, { headers });
  }
  


  deleteProduct(productId: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.delete(`${this.apiUrl}/api/product/${productId}`,{headers});
  }

}
