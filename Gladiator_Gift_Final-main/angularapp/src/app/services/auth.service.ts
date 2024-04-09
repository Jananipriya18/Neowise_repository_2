// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { BehaviorSubject, Observable, of } from 'rxjs';
// import { catchError, tap } from 'rxjs/operators';


// @Injectable({
//   providedIn: 'root'
// })
// export class AuthService {
//   private currentUserSubject: BehaviorSubject<string | null>;
//   public currentUser: Observable<string | null>;
//   public apiUrl = 'https://8080-bfabeabdaebdadfccdadbafeafdbeabddabfeee.premiumproject.examly.io';
//   private roleSubject = new BehaviorSubject<string>('');
//   role$: Observable<string> = this.roleSubject.asObservable();
//   private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.isAuthenticated());
//   isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

//   constructor(private http: HttpClient) {
//     this.currentUserSubject = new BehaviorSubject<string | null>(localStorage.getItem('currentUser'));
//     this.currentUser = this.currentUserSubject.asObservable();
//    }

//    register(username: string, password: string, role: string, email: string, mobileNumber:string): Observable<any> {
//     const body = { username, password, role, email, mobileNumber };
//     console.log(body);

//     return this.http.post<any>(`${this.apiUrl}/api/register`, body).pipe(
//       catchError(this.handleError<any>('register'))
//     );
//   }

//   isLoggedIn(): boolean {
//     console.log(localStorage.getItem('userId'));

//     return !!localStorage.getItem('userId');
//   }

//   getUserRole(): any {
//     const role = localStorage.getItem('role');
//     return role?.toString;
//   }

//   private handleError<T>(operation = 'operation', result?: T) {
//     return (error: any): Observable<T> => {
//       // Handle the error (you can log it or perform other actions)
//       console.error(error);

//       // Let the app keep running by returning an empty result.
//       return of(result as T);
//     };
//   }

//   login(email: string, password: string): Observable<any> {
//     const loginData = { email, password };
//     console.log(loginData);
//     return this.http.post<any>(`${this.apiUrl}/api/login`, loginData)
//       .pipe(
//         tap((response: any) => {
//           console.log(response);
//           // Store the token in localStorage or a more secure storage method
//           //localStorage.setItem('token', response.token);
//           localStorage.setItem('currentUser', response.username);
//           localStorage.setItem('role', response.role);
//           localStorage.setItem('userId', response.id);
          

//           console.log(localStorage.getItem('role'));
//           this.roleSubject.next(response.role);
//           this.isAuthenticatedSubject.next(true); // Notify observers that the user is authenticate

//         })
//       );
//   }

//   logout(): void {
//     // Remove the token from storage upon logout
//     //localStorage.removeItem('token');
//     localStorage.removeItem('currentUser');
//     localStorage.removeItem('role');
//     localStorage.removeItem('userId');
//     localStorage.removeItem('studentId');
//     localStorage.removeItem('admissionId');

//     this.currentUserSubject.next(null);
//   }

//   isAuthenticated(): boolean {
//     // Check if the user is authenticated by verifying the token
//     const token = localStorage.getItem('userId');
//     console.log(token);

//     return !!token; // Return true if the token exists
//   }

//   isAdmin(): boolean {
//     const role = localStorage.getItem('role');
//       if(role === 'admin' || role === 'ADMIN'){
//         return true;
//       }
    
//     return false; 
//   }

//   isCustomer(): boolean {

//     const role = localStorage.getItem('role');
    
//       if(role === 'customer' || role === 'CUSTOMER'){
//         return true;
//       }
//     return false; 
//   }

// }

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from '../models/user.model';
 
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<string | null>;
  public currentUser: Observable<string | null>;
  public apiUrl = 'https://8080-bfabeabdaebdadfccdadbafeaecccaaeccf.premiumproject.examly.io'; // Replace with your Spring Boot backend URL
  private userRoleSubject = new BehaviorSubject<string>('');
  private userIdSubject = new BehaviorSubject<string>('');
  userRole$: Observable<string> = this.userRoleSubject.asObservable();
  userId$: Observable<string> = this.userIdSubject.asObservable();
  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.isAuthenticated());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();
 
  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<string | null>(
      localStorage.getItem('currentUser')
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }
 
  register(user: User): Observable<any> {
    const body = user;
    console.log(body);
 
    return this.http.post<any>(`${this.apiUrl}/api/register`, body).pipe(
      tap((user) => this.storeUserData(user)),
      catchError(this.handleError<any>('register'))
    );
  }
 
  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };
    console.log(loginData);
    return this.http.post<any>(`${this.apiUrl}/api/login`, loginData)
      .pipe(
        tap(response => {
          if(response && response.token && response.token.token){
          // console.log(response.token.token);
          localStorage.setItem('token',response.token.token)
          const decodedToken = this.decodeToken(response.token.token);
          if(response.token.customerId==0)
          {
            localStorage.setItem("customerId","0")
          }
          else{
            localStorage.setItem("customerId",response.token.customerId)
            localStorage.setItem("cartId",response.token.cartId)

          }

          if (decodedToken) {
            localStorage.setItem('userId', decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
            localStorage.setItem('userRole', decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
            localStorage.setItem('currentUser', decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']);
            console.log(localStorage.getItem('userRole'))
            // Update BehaviorSubjects
            this.userRoleSubject.next(decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
            this.userIdSubject.next(decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
            this.isAuthenticatedSubject.next(true);
          } else {
            console.error('Unable to decode token or missing claims');
          }
        }
        })
      );
  }
 
  logout(): void {
    // Remove the token, role, and user ID from storage upon logout
    localStorage.removeItem('token');
    localStorage.removeItem('userRole');
    localStorage.removeItem('userId');
    localStorage.removeItem('customerId');
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
 
  isAuthenticated(): boolean {
    // Check if the user is authenticated by verifying the token
    const token = localStorage.getItem('token');
    console.log(token);
 
    return !!token; // Return true if the token exists
  }
 
  isAdmin(): boolean {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.decodeToken(token);
      console.log("decodedToken", decodedToken);
      return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'admin';
    }
    return false;
  }
 
  isCustomer(): boolean {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.decodeToken(token);
      return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'customer';
    }
    return false;
  }
 
  getCustomerName(): string {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.decodeToken(token);
      return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    }
    return '';
  }
 
  private storeUserData(user: any): void {
    localStorage.setItem('token', user.token);
    localStorage.setItem('userRole', user.role);
    localStorage.setItem('userId', user.userId);
  }
 
  private decodeToken(token: string): any {
    try {
      const decoded = JSON.parse(atob(token.split('.')[1]));
      console.log(decoded);
      return decoded;
    } catch (error) {
      return null;
      //added
    }
  }
 
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}