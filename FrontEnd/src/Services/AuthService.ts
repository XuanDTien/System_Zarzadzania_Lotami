import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private tokenKey: string = "token";
  private apiUrl = 'https://localhost:7249/Auth/authenticate';

  constructor(private http: HttpClient) { }

  login(user: { username: string; password: string }): Observable<any> {
    return this.http.post<any>(this.apiUrl, user)
      .pipe(tap((response: { token: string }) => {
        if (response) {
          localStorage.setItem(this.tokenKey, response.token);
        }
      }));
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
  }
}
