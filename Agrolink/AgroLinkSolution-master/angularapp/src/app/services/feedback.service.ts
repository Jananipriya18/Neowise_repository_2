import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feedback } from '../models/feedback.model';
import { apiUrl } from 'src/apiconfig';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  public apiUrl = apiUrl;

  constructor(private http: HttpClient) { }

  sendFeedback(feedback: Feedback): Observable<Feedback> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<Feedback>(`${this.apiUrl}/feedback/addFeedback`, feedback, { headers });
  }

  getAllfeedbacksByUserId(userId: string): Observable<Feedback[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Feedback[]>(`${this.apiUrl}/feedback/getFeedbacksByUserId/${userId}`, { headers });
  }



  getFeedbacks(): Observable<Feedback[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Feedback[]>(`${this.apiUrl}/feedback/getAllFeedbacks`, { headers });
  }

}

