import { Component, OnInit } from '@angular/core';
import { Flight, FlightService } from '../../Services/FlightService';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FlightModalComponent } from '../flight-modal/flight-modal.component';
import { ConfirmModalComponent } from '../confirm-modal/confirm-modal.component';
import { RedirectService } from '../../Services/RedirectService';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  flightsList: Flight[] = [];

  constructor(
    private flightService: FlightService,
    public dialog: MatDialog,
    private redirectService: RedirectService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit(): void {
    this.loadFlightsList();
  }

  private loadFlightsList(retryCount: number = 0): void {
    this.flightService.getList().subscribe({
      next: (result) => {
        this.flightsList = result;
      },
      error: (error) => {
        console.error('Error fetching flights:', error);
        if (retryCount < 3) { 
          console.log(`Retrying to fetch flights list... Attempt ${retryCount + 1}`);
          setTimeout(() => {
            this.loadFlightsList(retryCount + 1); 
          }, 2000); 
        } else {
          console.log('Failed to fetch flights list after 3 attempts.');
        }
      },
    });
  }
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  addFlight(): void {
    this.editFlight();
  }

  editFlight(flight?: Flight): void {
    if (flight?.id) {
        const dialogRef = this.dialog.open(FlightModalComponent, {
          width: '60vw',
          data: flight,
        });
      dialogRef.afterClosed().subscribe(() => {
        this.loadFlightsList();
      });
    } else {
      const dialogRef = this.dialog.open(FlightModalComponent, {
        width: '60vw',
        data: undefined,
      });
      dialogRef.afterClosed().subscribe(() => {
        this.loadFlightsList();
      });
    }
  }


  deleteFlight(flightId: number): void {
    const dialogRef = this.dialog.open(ConfirmModalComponent, {
      data: {
        buttonText: {
          ok: 'Yes',
          cancel: 'No',
        },
      },
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.flightService.deleteFlight(flightId).subscribe({
          next: () => {
            this.snackBar.open('Deleted successfully.', 'Close', {
              duration: 3000,
            });
            this.redirectService.redirectTo(this.router.url);
            this.loadFlightsList();
          },
          error: (error) => {
            console.error('Error deleting:', error);
          },
        });
      }
    });
  }
}
