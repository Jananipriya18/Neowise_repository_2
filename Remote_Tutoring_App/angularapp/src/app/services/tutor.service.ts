// src/app/services/tutor.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tutor } from '../models/tutor.model';


@Injectable({
  providedIn: 'root'
})
export class TutorService {
  private apiUrl = 'https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/'; // Replace this with your API endpoint

  constructor(private http: HttpClient) { }

  addTutor(tutor: Tutor): Observable<Tutor> {
    return this.http.post<Tutor>(`${this.apiUrl}api/Tutor`, tutor);
  }

  getTutors(): Observable<Tutor[]> {
    return this.http.get<Tutor[]>(`${this.apiUrl}api/Tutor`);
  }

  deleteTutor(tutorId: number): Observable<void> {
    const url = `${this.apiUrl}api/Tutor/${tutorId}`; // Adjust the URL to match your API endpoint
    return this.http.delete<void>(url);
  }

  getTutor(tutorId: number): Observable<Tutor> {
    const url = `${this.apiUrl}api/Tutor/${tutorId}`;
    return this.http.get<Tutor>(url);
  }
}
