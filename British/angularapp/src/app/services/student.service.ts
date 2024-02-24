// student.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private apiUrl = 'https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  createStudent(student: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/api/student`, student);
  }

  deleteStudent(studentId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/api/student/${studentId}`);
  }

  getStudent(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/api/student/user/${userId}`);
  }
}
