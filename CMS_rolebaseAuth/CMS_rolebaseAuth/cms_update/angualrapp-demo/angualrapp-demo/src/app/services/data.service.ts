// data.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from '../../apiConfig';


interface User {
  Email: string;
  MobileNumber: string;
  Password: string;
  UserId: number;
  UserRole: string;
  Username: string;
}

interface UsersResponse {
  Users: User[];
}


@Injectable({
  providedIn: 'root'
})
export class DataService {
  constructor(private http: HttpClient) { }



  getContainers(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any[]>(`${apiUrl}/api/container`, { headers });
  }

  getUsers(): Observable<UsersResponse> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<UsersResponse>(`${apiUrl}/api/users`, { headers });
  }
}