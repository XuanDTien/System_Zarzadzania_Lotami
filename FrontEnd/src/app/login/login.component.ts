import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthService } from '../../Services/AuthService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      const user = {
        username: form.value.username,
        password: form.value.password
      };
      this.authService.login(user).subscribe(
        response => {
          console.log('Login successful', response);
          this.router.navigate(['/']);
        }
      );
    } else {
      console.log("Invalid form");
    };
  }
}
