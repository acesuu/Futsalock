import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7149/api/Auth';

  constructor(private http: HttpClient) {}

  signUp(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/signup`, data);
  }

  signIn(credentials: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/signin`, credentials);
  }

  signOut(): Observable<any> {
    return this.http.post(`${this.baseUrl}/signout`, {});
  }
}
