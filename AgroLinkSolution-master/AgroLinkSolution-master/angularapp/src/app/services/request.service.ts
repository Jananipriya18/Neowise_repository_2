import { Injectable } from '@angular/core';
import { apiUrl } from 'src/apiconfig';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AgroChemicals } from '../models/agrochemicals';
import { Observable } from 'rxjs';
import { Request } from '../models/request.model';
@Injectable({
    providedIn: 'root'
})
export class RequestService {

    public apiUrl = apiUrl;

    constructor(private http: HttpClient) {

    }

    addRequest(requestObject: Request): Observable<Request> {
        const headers = new HttpHeaders({
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        });
        return this.http.post<Request>(`${this.apiUrl}/request/addRequest`, requestObject, { headers });
    }

    getRequestByRequestId(requestId: number): Observable<Request> {
        const headers = new HttpHeaders({
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        });
        return this.http.get<Request>(`${this.apiUrl}/request/getRequestByRequestId${requestId}`, { headers });
    }
    getAllRequest(): Observable<Request[]> {

        const headers = new HttpHeaders({
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        });
        return this.http.get<Request[]>(`${this.apiUrl}/agrochemical/getAllCrop`, { headers });
    }
    deleteRequestByRequestId(requestId: number): Observable<void> {
        const headers = new HttpHeaders({
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        });
        return this.http.delete<void>(`${this.apiUrl}/request/deleteRequestByRequestId/${requestId}`, { headers });
    }

    updateRequestByRequestId(requestId: string, requestObject: Request): Observable<Request> {
        const headers = new HttpHeaders({
            'Authorization': 'Bearer ' + localStorage.getItem('token')
        });
        return this.http.put<Request>(`${this.apiUrl}/request/updateRequestByRequestId/${requestId}`, requestObject, { headers });
    
    }

}
