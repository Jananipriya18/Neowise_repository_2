// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { AuthService } from '../../services/auth.service';

// @Component({
//   selector: 'app-registration',
//   templateUrl: './registration.component.html',
//   styleUrls: ['./registration.component.css']
// })
// export class RegistrationComponent {
//   userName: string = "";
//   password: string = "";
//   confirmPassword: string = "";
//   mobileNumber: string = "";
//   role: string = "";
//   emailID: string;
//   passwordMismatch: boolean = false; // New property to track password mismatch

//   constructor(private authService: AuthService, private router: Router) { }

//   register(): void {
//     if (this.password !== this.confirmPassword) {
//       this.passwordMismatch = true;
//       return;
//     }

//     this.passwordMismatch = false;

//     if (!this.isPasswordComplex(this.password)) {
//       return; // Password complexity check failed
//     }

//     this.authService.register(this.userName, this.password, this.role, this.emailID, this.mobileNumber).subscribe(
//       (user) => {
//         console.log(user);

//         console.log(this.role)
//         if (user == true && this.role === 'ADMIN') {
//           alert('Registration Successful');
//           this.router.navigate(['/login']);
//         } else if ( user == true && this.role === 'STUDENT') {
//           alert('Registration Successful');
//           this.router.navigate(['/login']);
//         } else{
//           alert('Registration failed. User with that Email already exists or an error occurred. Please try again.');

//         }
//       },
//       (error) => {
//         console.log(error);
//       }
//     );
//   }
//   isPasswordComplex(password: string): boolean {
//     const hasUppercase = /[A-Z]/.test(password);
//     const hasLowercase = /[a-z]/.test(password);
//     const hasDigit = /\d/.test(password);
//     const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\-]/.test(password);

//     return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
//   }
// }


import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registrationForm: FormGroup;
  passwordMismatch: boolean = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(): void {
    this.registrationForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', [Validators.required, this.passwordComplexityValidator]],
      confirmPassword: ['', Validators.required],
      mobileNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      role: ['', Validators.required],
      emailID: ['', [Validators.required, Validators.email]],
    });
  }

  register(): void {
    if (this.registrationForm.invalid) {
      this.passwordMismatch = true;
      return;
    }

    this.passwordMismatch = false;

    this.authService.register(this.registrationForm.value).subscribe(
      (user) => {
        if (user == true && (this.role === 'ADMIN' || this.role === 'STUDENT')) {
          alert('Registration Successful');
          this.router.navigate(['/login']);
        } else {
          alert('Registration failed. User with that Email already exists or an error occurred. Please try again.');
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  passwordComplexityValidator(control): { [key: string]: boolean } | null {
    const password = control.value;

    if (!/[A-Z]/.test(password) || !/[a-z]/.test(password) || !/\d/.test(password) || !/[!@#$%^&*()_+{}\[\]:;<>,.?~\-]/.test(password)) {
      return { 'passwordComplexity': true };
    }

    return null;
  }
}
