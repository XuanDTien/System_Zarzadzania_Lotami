import { Component, OnInit } from '@angular/core';
import { Flight, FlightService } from '../../Services/FlightService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  flightsList: Flight[] = [];

  constructor(
    private flightService: FlightService,
  ) { }

  ngOnInit(): void {
    this.flightService.getList().subscribe({
      next: (result) => {
        this.flightsList = result;
        console.log(this.flightsList[0]);
      },
      error: (error) => {
        console.error('Error fetching news:', error);
      },
    });
  }
}
