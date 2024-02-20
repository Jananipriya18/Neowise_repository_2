import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiUrl } from '../../apiConfig';
import { catchError, map } from 'rxjs/operators';
import { Observable  , throwError} from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  
  private decodeToken(token: string): any {
    try {
      const decoded = JSON.parse(atob(token.split('.')[1]));
      console.log(decoded);
      return decoded;
    } catch (error) {
      return null;
    }
  }



  login(formData: any): Observable<any> {
    const loginUrl = `${apiUrl}/api/login`;
    return this.http.post<any>(loginUrl, formData, { observe: 'response' })
      .pipe(
        map((response: any) => {
          console.log(response);
          if (response.status === 200 && response.body.Status === 'Success') {
            console.log("eeee");
            const token = response.body.token;

            localStorage.setItem('token', token);
            
            
            try {
              const decodedToken = this.decodeToken(token);
              return { decodedToken , token};
            } catch (error) {
              console.error('Error decoding token', error);
              throw new Error('Error decoding token');
            }
          } else {
            throw new Error('Login failed');
          }
        }),
        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          if (error.status === 500) {
            errorMessage = '*Account not found. Please check your email and password.';
          } else {
            errorMessage = '*Invalid email or password';
          }
          return throwError(new HttpErrorResponse({ status: error.status, statusText: errorMessage }));
        })
      );
  }

  register(userData: any): Observable<any> {
    const registerUrl = `${apiUrl}/api/auth/register`;
    return this.http.post(registerUrl, userData);
  }
}
