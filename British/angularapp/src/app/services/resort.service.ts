import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiUrl } from 'src/apiconfig';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResortService {
  public apiUrl =apiUrl;

  constructor(private http: HttpClient) {}

  // addResort(resort: any): Observable<any> {
  //   const token = localStorage.getItem('token');
  //   console.log(token)
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  //   });

  //   return this.http.post(`${this.apiUrl}/api/Resort`, resort, { headers });
  // }

  // getAllResorts() {
  //   const token = localStorage.getItem('token');
  //   // console.log(token)
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  //   });
  //   return this.http.get(`${this.apiUrl}/api/Resort`, { headers });
  // }

  // updateResort(resortDetails: any) {
  //   console.log(resortDetails);
  //   const token = localStorage.getItem('token');
  //   console.log(token)
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  //   });
  //   return this.http.put(`${this.apiUrl}/api/resort/${resortDetails.resortId}`, { headers, resortDetails });
  // }

  // updateResort(resortDetails: any): Observable<any> {
  //   console.log(resortDetails.resortId)
  //   const token = localStorage.getItem('token');
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  //   });

  //   return this.http.put(`${this.apiUrl}/api/resort/${resortDetails.resortId}`, resortDetails, { headers });
  // }

  // deleteResort(resortId: string): Observable<any> {
  //   const token = localStorage.getItem('token');
  //   const headers = new HttpHeaders({
  //     'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  //   });

  //   return this.http.delete(`${this.apiUrl}/api/resort/${resortId}`, { headers });
  // }

  addReview(review: any): Observable<any> {
    const token = localStorage.getItem('token');
    // console.log(token)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });

    return this.http.post(`${this.apiUrl}/api/review`, review, { headers });
  }
  
  getAllReviews(){
    const token = localStorage.getItem('token');
    // console.log(token)
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
    });
    return this.http.get(`${this.apiUrl}/api/Review`, { headers });
  }
}
