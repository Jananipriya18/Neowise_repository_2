import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiConfig';

@Injectable({
  providedIn: 'root'
})
export class ContainerService {

  constructor(private http: HttpClient) { }

  addContainer(containerData: any): Observable<HttpResponse<any>> {
    console.log(containerData);
    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'));
    return this.http.post(`${apiUrl}/api/container`, containerData, { headers, observe: 'response' });
  }

  getContainers(): Observable<any[]> {
    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'));
    return this.http.get<any[]>(`${apiUrl}/api/container`, { headers });
  }

  deleteContainer(containerId: number) {
    console.log(containerId);
    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'));
    return this.http.delete(`${apiUrl}/api/container/${containerId}`, { headers });
  }

  getContainerById(containerId: number): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${apiUrl}/api/container/${containerId}`, { headers });
}

updateContainer(containerId: number, data: any): Observable<HttpResponse<any>>{
    console.log(data , containerId);
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put<any>(`${apiUrl}/api/container/${containerId}`, data, { headers , observe : 'response'});
}
}
