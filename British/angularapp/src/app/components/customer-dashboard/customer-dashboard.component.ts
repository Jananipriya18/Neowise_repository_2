import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent implements OnInit {
  isLoggedIn: boolean = false;
  isstudent: boolean = false;

  constructor(private authService: AuthService) {
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isstudent = this.authService.isstudent();
        console.log(this.isstudent);

      } else {
        this.isstudent = false;
      }
    });
  }

  
  
  ngOnInit(): void {
    // Initialize the properties on component initialization
    this.isLoggedIn = this.authService.isAuthenticated();
    if (this.isLoggedIn) {
      this.isstudent = this.authService.istudent();
    }
  }
}
