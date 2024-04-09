import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class GiftService {

  public apiUrl='https://8080-bfabeabdaebdadfccdadbafeaecccaaeccf.premiumproject.examly.io'

  constructor(private http: HttpClient) { }

  addGift(gift: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    console.log(gift);
    return this.http.post(`${this.apiUrl}/api/gift`, gift, {headers});
  }

  viewAllGifts(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get<any[]>(`${this.apiUrl}/api/gift`, {headers});
  }

  // updateGift(giftId: any, updatedGift: any): any {
  //   const token = localStorage.getItem('token');
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  //   });
  //   const cartId = localStorage.getItem('cartId'); // Get the cartId from localStorage
  //   updatedGift.cartId = cartId; 
  //   console.log(giftId,updatedGift);
  //   return this.http.put(`${this.apiUrl}/api/gift/${giftId}`, updatedGift, {headers});
  // }

  updateGift(giftId: any, updatedGift: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
  
    // Check if customerId is present in localStorage
    const customerId = localStorage.getItem('customerId');
    if (customerId) {
      const cartId = localStorage.getItem('cartId'); // Get the cartId from localStorage
      updatedGift.cartId = cartId;
    }
  
    console.log(giftId, updatedGift);
    return this.http.put(`${this.apiUrl}/api/gift/${giftId}`, updatedGift, { headers });
  }
  


  deleteGift(giftId: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.delete(`${this.apiUrl}/api/gift/${giftId}`,{headers});
  }

}
