import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { Course } from 'src/app/models/course.model';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  public apiUrl = 'https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const authToken = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${authToken}`
    });
  }

  getAllCourses(): Observable<Course[]> {
    const role = localStorage.getItem('userRole');

    let endpoint;
    if (role && role.toUpperCase() === 'ADMIN') {
      endpoint = `${this.apiUrl}/api/course`;
    } else if (role && role.toUpperCase() === 'STUDENT') {
      endpoint = `${this.apiUrl}/api/student/course`;
    } else {
      console.error('Access denied. Invalid role.');
      return of([]); // Return an empty observable using RxJS 'of'
    }

    const headers = this.getHeaders();

    return this.http.get<Course[]>(endpoint, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  saveCourseByAdmin(course: Course): Observable<Course> {
    const role = localStorage.getItem('userRole');

    if (role !== 'ADMIN' && role !== 'admin') {
      console.error('Access denied. Only admins can add courses.');
      return throwError({ message: 'Access denied. Only admins can add courses.' });
    }
    const endpoint = `${this.apiUrl}/api/course`;
    const headers = this.getHeaders();

    return this.http.post<Course>(endpoint, course, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  updateCourseByAdmin(courseId: number, updatedCourseData: Course): Observable<Course> {
    const role = localStorage.getItem('userRole');

    if (role !== 'ADMIN' && role !== 'admin') {
      console.error('Access denied. Only admins can update courses.');
      return throwError({ message: 'Access denied. Only admins can update courses.' });
    }
    const endpoint = `${this.apiUrl}/api/course/${courseId}`;
    const headers = this.getHeaders();

    return this.http.put<Course>(endpoint, updatedCourseData, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  deleteCourseByAdmin(courseId: number): Observable<Course> {
    const role = localStorage.getItem('userRole');

    if (role !== 'ADMIN' && role !== 'admin') {
      console.error('Access denied. Only admins can delete courses.');
      return throwError({ message: 'Access denied. Only admins can delete courses.' });
    }
    const endpoint = `${this.apiUrl}/api/course/${courseId}`;
    const headers = this.getHeaders();

    return this.http.delete<Course>(endpoint, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  getStudentCourses(): Observable<Course[]> {
    const role = localStorage.getItem('userRole');

    let endpoint;
    if (role === 'ADMIN' || role === 'admin') {
      endpoint = `${this.apiUrl}/api/course`;
    } else if (role === 'STUDENT' || role === 'student') {
      endpoint = `${this.apiUrl}/api/student/course`;
    } else {
      console.error('Access denied. Invalid role.');
      return of([]); // Return an empty observable using RxJS 'of'
    }

    const headers = this.getHeaders();

    return this.http.get<Course[]>(endpoint, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }
}
