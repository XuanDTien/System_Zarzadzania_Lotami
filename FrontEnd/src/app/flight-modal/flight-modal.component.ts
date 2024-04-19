import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { FlightService } from '../../Services/FlightService';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RedirectService } from '../../Services/RedirectService';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

@Component({
  selector: 'app-flight-modal',
  templateUrl: './flight-modal.component.html',
  styleUrl: './flight-modal.component.css'
})
export class FlightModalComponent implements OnInit {
  flightForm!: FormGroup;
  isEditMode: boolean = false;
  serverError: string | null = null;
  todate = new Date();


  constructor(
    private dialogRef: MatDialogRef<FlightModalComponent>,
    @Inject(MAT_DIALOG_DATA) public flight: any,
    private redirectService: RedirectService,
    private flightService: FlightService,
    private snackBar: MatSnackBar,
    private fb: FormBuilder,
    private router: Router

  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm(): void {
    this.isEditMode = !!this.flight;

    this.flightForm = this.fb.group({
      flightNumber: ['', Validators.required],
      departureDate: ['', Validators.required],
      departureFrom: ['', [Validators.required, Validators.pattern(/^[A-Za-z\s]*$/)]],
      arrivalTo: ['', [Validators.required, Validators.pattern(/^[A-Za-z\s]*$/)]],
      planeType: ['']
    });

    if (this.isEditMode) {
      this.flightForm.patchValue({
        flightNumber: this.flight.flightNumber,
        departureDate: this.flight.departureDate,
        departureFrom: this.flight.departureFrom,
        arrivalTo: this.flight.arrivalTo,
        planeType: this.flight.planeType
      });
    }
  }

  close(): void {
    this.dialogRef.close();
  }

  onSubmit(form: FormGroup): void {
    if (this.isEditMode) {
      form.addControl('id', new FormControl(this.flight.id));

      this.flightService.editFlight(form.value).subscribe({
        next: (result) => {
          this.snackBar.open('Successful saved.', '', {
            duration: 5000,
            panelClass: ['green-snackbar'],
          });
          this.dialogRef.close();
          this.redirectService.redirectTo(this.router.url);
        },
        error: (error) => {
          console.error('Error:', error);
          this.serverError = error.error || 'An error occurred.';
        }
      });
    } else {
      this.flightService.addFlight(form.value).subscribe({
        next: (result) => {
          this.snackBar.open('Successful saved.', '', {
            duration: 5000,
            panelClass: ['green-snackbar'],
          });
          this.dialogRef.close();
          this.redirectService.redirectTo(this.router.url);
        },
        error: (error) => {
          console.error('Error:', error);
          this.serverError = error.error.title || error.error || 'An error occurred.';
        }
      });
    }
  }
}
