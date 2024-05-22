// src/app/services/laptop.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Laptop } from '../models/laptop.model';

@Injectable({
  providedIn: 'root'
})
export class LaptopService {
  private apiUrl = 'https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/'; // Replace this with your API endpoint

  constructor(private http: HttpClient) { }

  addLaptop(laptop: Laptop): Observable<Laptop> {
    return this.http.post<Laptop>(`${this.apiUrl}api/Laptop`, laptop);
  }

  getLaptops(): Observable<Laptop[]> {
    return this.http.get<Laptop[]>(`${this.apiUrl}api/Laptop`);
  }

  deleteLaptop(laptopId: number): Observable<void> {
    const url = `${this.apiUrl}api/Laptop/${laptopId}`; // Adjust the URL to match your API endpoint
    return this.http.delete<void>(url);
  }

  getLaptop(laptopId: number): Observable<Laptop> {
    const url = `${this.apiUrl}api/Laptop/${laptopId}`;
    return this.http.get<Laptop>(url);
  }
}
