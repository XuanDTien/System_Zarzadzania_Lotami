import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
}

export interface Flight {
  id: number,
  flightNumber: number,
  departureDate: Date,
  departureFrom: string,
  arrivalTo: string,
  planeType: string
}
