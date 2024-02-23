import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User } from '../models/user.model';
import { LoginModel } from '../models/loginModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public baseUrl = "https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io";
  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.isAuthenticUser());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();
  private userRoleSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  userRole$ = this.userRoleSubject.asObservable();

  constructor(private http: HttpClient) { }

  register(user: User): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/register`, user);
  }

  login(user: LoginModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/login`, user)
      .pipe(
        tap(res => {
          if (res && res.token) {
            const decodedToken = this.decodeToken(res.token);
            localStorage.setItem('token', res.token);
            localStorage.setItem('role', decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
            localStorage.setItem('userId', decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
            localStorage.setItem('name', decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
            
            this.isAuthenticatedSubject.next(true);
            this.userRoleSubject.next(decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
          }
        })
      );
  }

 

  isLoggedIn(): boolean {
    console.log(localStorage.getItem('token'));
    return !!localStorage.getItem('token');
  }

  isAdmin(): boolean {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('role');
    if (token && role.toLowerCase() === 'admin') {
      return true;
    }
    return false;
  }

  isStudent(): boolean {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('role');
    if (token && role.toLowerCase() === 'student') {
      return true;
    }
    return false;
  }

  isAuthenticUser(): boolean {
    const token = localStorage.getItem('token');
    console.log(token);
    return !!token;
  }


  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('userId');
    localStorage.removeItem('name');
    this.isAuthenticatedSubject.next(false);
  }

  private decodeToken(token: string): any {
    try {
        if (!token) {
            return null;
        }
        const decoded = JSON.parse(atob(token.split('.')[1]));
        console.log(decoded);
        return decoded;
    } catch (e) {
        console.log(e);
        return null;
    }
}

getCurrentUserId(): string {
  return localStorage.getItem('userId') || '';
}

}