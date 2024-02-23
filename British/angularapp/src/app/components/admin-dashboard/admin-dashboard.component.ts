import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  isLoggedIn: boolean = false;
  isadmin: boolean = false;

  constructor(private authService: AuthService) {
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isadmin = this.authService.isAdmin();
        console.log(this.isadmin);
      } else {
        this.isadmin = false;
      }
    });
  }

  ngOnInit(): void {
    // Initialize the properties on component initialization
    this.isLoggedIn = this.authService.isAuthenticated$();
    if (this.isLoggedIn) {
      this.isadmin = this.authService.isAdmin();
    }
  }
}

