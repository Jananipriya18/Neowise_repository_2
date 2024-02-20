import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Enquiry } from '../models/enquiry.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root',
})
export class EnquiryService {
  private apiUrl = "https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io"; // Adjust the API base URL

  constructor(private http: HttpClient) {}

  private createAuthorizationHeader(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  createEnquiry(enquiry: Enquiry): Observable<Enquiry> {
    const headers = this.createAuthorizationHeader();
    return this.http.post<Enquiry>(`${this.apiUrl}/api/Enquiry`, enquiry, { headers });
  }

  getAllEnquiries(): Observable<Enquiry[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Enquiry[]>(`${this.apiUrl}/api/Enquiry`, { headers });
  }

  getEnquiryById(enquiryId: number): Observable<Enquiry> {
    const headers = this.createAuthorizationHeader();
    const url = `${this.apiUrl}/${enquiryId}`;
    return this.http.get<Enquiry>(url, { headers });
  }


}
