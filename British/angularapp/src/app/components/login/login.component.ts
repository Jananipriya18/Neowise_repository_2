import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/loginModel';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email: string="";
  password: string="";
  error: string = '';
  user: LoginModel={
    Email:'',
    Password:''
  };
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  login(): void {
    this.user={
      Email:this.email,
      Password:this.password
    }
    this.authService.login(this.user).subscribe(
      (user) => {
        console.log(user);
        if (this.authService.isAdmin() || this.authService.isStudent()){
        window.alert('login successful!');
          this.router.navigate(['/']);
        }
        else {
          console.log('Not Admin logged in');
          alert('You are not authorized to access this page');
        }
      },
      (error) => {
        console.log(error.error);
        if (error.error.Message){
        alert(error.error.Message);
        }

        this.error = error.error;
      }
    );
  }

}
