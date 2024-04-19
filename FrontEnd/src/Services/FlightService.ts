import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  private tokenKey: string = "token";
  private apiUrl = 'https://localhost:7249/api/Flights';

  constructor(private http: HttpClient) { }

  getList(): Observable<Flight[]> {
    return this.http.get<Flight[]>(this.apiUrl);
  }

  getById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  addFlight(flight: Flight): Observable<any> {
    const headers = this.createAuthHeaders();
    return this.http.post(this.apiUrl, flight, { headers });
  }

  editFlight(flight: Flight): Observable<any> {
    const headers = this.createAuthHeaders();
    return this.http.put(`${this.apiUrl}/${flight.id}`, flight, { headers });
  }

  deleteFlight(flightId: number): Observable<any> {
    const headers = this.createAuthHeaders();
    return this.http.delete(`${this.apiUrl}/${flightId}`, { headers });
  }

  private createAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem(this.tokenKey);
    if (!token) {
      throw new Error('No authentication token found');
    }
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }
}

export interface Flight {
  id: number,
  flightNumber: number,
  departureDate: Date,
  departureFrom: string,
  arrivalTo: string,
  planeType: string
}
