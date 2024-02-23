import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent implements OnInit {
  isLoggedIn: boolean = false;
  isStudent: boolean = true;

  constructor(private authService: AuthService) {
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isStudent = this.authService.isStudent();
        console.log(this.isStudent);

      } else {
        this.isStudent = false;
      }
    });
  }

  
  
  ngOnInit(): void {
    // Initialize the properties on component initialization
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isStudent = this.authService.isStudent();
      }
    });
  }
  
}
