import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiConfig';

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {

  constructor(private http: HttpClient) { }

  addAssignment(assignmentData: any): Observable<HttpResponse<any>> {
    console.log(assignmentData);
    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + localStorage.getItem('token'));
    return this.http.post(`${apiUrl}/api/assignment`, assignmentData, { headers, observe: 'response' });
  }
}
