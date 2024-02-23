import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  username: string = "";
  password: string = "";
  confirmPassword: string = "";
  mobileNumber: string = "";
  role: string = "";
  email: string = "";
  passwordMismatch: boolean = false; // New property to track password mismatch

  constructor(private authService: AuthService, private router: Router) { }

  register(): void {
    if (this.password !== this.confirmPassword) {
      this.passwordMismatch = true;
      return;
    }

    this.passwordMismatch = false;

    if (!this.isPasswordComplex(this.password)) {
      return; // Password complexity check failed
    }
    const user: User = {
      Username: this.username,
      Password: this.password,
      MobileNumber: this.mobileNumber,
      UserRole: this.role,
      Email: this.email
    };

    this.authService.register(user).subscribe(
      (user) => {
        console.log(user.Status);

        console.log(this.role)
        if (user.Status == "Success") {
        window.alert('Register successful!');
          this.router.navigate(['/login']);
        }
        // else if ( user == true && this.role === 'ORGANIZER') {
        //   this.router.navigate(['/login']);
        // }
      },
      (error) => {
        console.log(error.error);
        if (error.error.Status === "Error") {
        window.alert('Registration is not successful!');
          // Username already exists
          alert(error.error.Message);
        }

        // Handle registration error, display a message, etc.
      }
    );
  }
  isPasswordComplex(password: string): boolean {
    const hasUppercase = /[A-Z]/.test(password);
    const hasLowercase = /[a-z]/.test(password);
    const hasDigit = /\d/.test(password);
    const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\-]/.test(password);

    return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
  }

  ngOnInit(): void {

  }

}