import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../models/student.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root',
})
export class StudentService {
  private apiUrl = "https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io"; 

  constructor(private http: HttpClient) {}

  private createAuthorizationHeader(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  createStudent(student: any): Observable<any> {
    const headers = this.createAuthorizationHeader();
    return this.http.post(`${this.apiUrl}/api/student`, student, { headers });
  }
  
  deleteStudent(studentId: number): Observable<any> {
    const headers = this.createAuthorizationHeader();
    return this.http.delete(`${this.apiUrl}/api/student/${studentId}`, { headers });
  }

  // getStudent(userId: number): Observable<any> {
  //   const headers = this.createAuthorizationHeader();
  //   return this.http.get(`${this.apiUrl}/api/student/user/${userId}`, { headers });
  // }

  getStudent(userId: number): Observable<any> {
    const headers = this.createAuthorizationHeader();
    return this.http.get(`${this.apiUrl}/api/${userId}`, { headers });
  }
  
  getStudents(): Observable<any[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<any[]>(`${this.apiUrl}/api/students`, { headers });
  }
  
}
