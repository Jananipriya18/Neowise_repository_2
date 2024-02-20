import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
  error: string = '';
 

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit(): void {
    localStorage.clear();
  }

  onSubmit() {
    this.submitted = true;
    
    if (this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.loginForm.value)
      .subscribe(
        (response: any) => {
          console.log(response.token);  // Logs the actual token
         console.log(response.decodedToken);
         localStorage.setItem("token", response.token);
        console.log(localStorage.getItem("token"));
          
          const userData = {
            role: response.decodedToken.role,
            userId: response.decodedToken.nameid,
            userName:response.decodedToken.name,
          };

          console.log("userData", userData);
         

          localStorage.setItem("userData", JSON.stringify(userData));

          if (response.decodedToken.role === "Admin") {
            this.router.navigate(['/admin-dashboard']);
          } else {
            this.router.navigate(['/opertor-dashbaord']);
          }
        },
        (error: HttpErrorResponse) => {
          // The login failed, the response is an HttpErrorResponse
          this.error = error.statusText; // Set the error message here
        }
      );
  }
}
