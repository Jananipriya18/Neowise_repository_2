import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Subscription } from 'rxjs';



@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  isLoggedIn: boolean = false;
  isAdmin: boolean = true;


  constructor(private authService: AuthService) {
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isAdmin = this.authService.isAdmin();
        console.log(this.isAdmin);
      } else {
        this.isAdmin = false;
      }
    });
  }

  ngOnInit(): void {
    // Initialize the properties on component initialization
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isAdmin = this.authService.isAdmin();
      }
    });
  }
  
}

