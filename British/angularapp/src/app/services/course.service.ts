// course.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../models/course.model';


@Injectable({
  providedIn: 'root',
})
export class CourseService {
  public apiUrl = "https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io"; 

  // private apiUrl = '/api/Course';

  constructor(private http: HttpClient) {}

  getAllCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.apiUrl}/api/Course`);
  }

  getCourseById(courseId: number): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/api/Course/${courseId}`);
  }

  createCourse(course: Course): Observable<Course> {
    return this.http.post<Course>(`${this.apiUrl}/api/Course`, course);
  }

  updateCourse(courseId: number, course: Course): Observable<Course> {
    return this.http.put<Course>(`${this.apiUrl}/api/Course/${courseId}`, course);
  }

  deleteCourse(courseId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/Course/${courseId}`);
  }
}
