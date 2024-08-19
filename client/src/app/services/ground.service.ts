import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GroundService {
  private apiUrl = 'https://localhost:7149/api/Ground/available-grounds';

  constructor(private http: HttpClient) {}

  getAvailableGrounds(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
