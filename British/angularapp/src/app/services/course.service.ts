// course.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../models/course.model';


@Injectable({
  providedIn: 'root',
})
export class CourseService {
  public apiUrl = "https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io"; 

  constructor(private http: HttpClient) {}

  private createAuthorizationHeader(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getAllCourses(): Observable<Course[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Course[]>(`${this.apiUrl}/api/Course`, { headers });
  }

  getCourseById(courseId: number): Observable<Course> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Course>(`${this.apiUrl}/api/Course/${courseId}`, { headers });
  }

  createCourse(course: Course): Observable<Course> {
    const headers = this.createAuthorizationHeader();
    return this.http.post<Course>(`${this.apiUrl}/api/Course`, course, { headers });
  }

  updateCourse(courseId: number, course: Course): Observable<Course> {
    const headers = this.createAuthorizationHeader();
    return this.http.put<Course>(`${this.apiUrl}/api/Course/${courseId}`, course, { headers });
  }

  deleteCourse(courseId: number): Observable<void> {
    const headers = this.createAuthorizationHeader();
    return this.http.delete<void>(`${this.apiUrl}/api/Course/${courseId}`, { headers });
  }
}
