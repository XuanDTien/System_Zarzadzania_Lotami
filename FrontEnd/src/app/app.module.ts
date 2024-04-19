import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppHeaderComponent } from './app-header/app-header.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { FlightModalComponent } from './flight-modal/flight-modal.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ConfirmModalComponent } from './confirm-modal/confirm-modal.component';





@NgModule({
  declarations: [
    AppComponent,
    AppHeaderComponent,
    HomeComponent,
    LoginComponent,
    FlightModalComponent,
    ConfirmModalComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    HttpClientModule,
    AppRoutingModule,
    MatSnackBarModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatButtonModule,
  ],
  providers: [
    provideAnimationsAsync('noop'),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
