import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Crop } from '../models/crop.model';
import { Observable } from 'rxjs';
import { apiUrl } from 'src/apiconfig';

@Injectable({
  providedIn: 'root'
})
export class CropService {

  public apiUrl = apiUrl;

  constructor(private http: HttpClient) {

  }

  addCrop(requestObject: Crop): Observable<Crop> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<Crop>(`${this.apiUrl}/crop/addcrop`, requestObject, { headers });
  }

  getCropById(id: number): Observable<Crop> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Crop>(`${this.apiUrl}/crop/getCropByCropID${id}`, { headers });
  }
  getAllCrops(): Observable<Crop[]> {

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Crop[]>(`${this.apiUrl}/crop/getAllCrop`, { headers });
  }
  deleteCrop(cropId: number): Observable<void> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<void>(`${this.apiUrl}/crop/deleteCropByCropID/${cropId}`, { headers });
}

updateCrop(id: number, requestObject: Crop): Observable<Crop> {
  const headers = new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  });
  return this.http.put<Crop>(`${this.apiUrl}/crop/updateCropByCropID/${id}`, requestObject, { headers });
}


}
