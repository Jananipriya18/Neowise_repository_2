// import { Component, OnInit } from '@angular/core';
// import { AuthService } from 'src/app/services/auth.service';

// @Component({
//   selector: 'app-admin-dashboard',
//   templateUrl: './admin-dashboard.component.html',
//   styleUrls: ['./admin-dashboard.component.css']
// })
// export class AdminDashboardComponent implements OnInit {
//   isLoggedIn: boolean = false;
//   isAdmin: boolean = false;

//   constructor(private authService: AuthService) {
//     this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
//       this.isLoggedIn = authenticated;
//       if (this.isLoggedIn) {
//         this.isAdmin = this.authService.isAdmin();
//         console.log(this.isAdmin);
//       } else {
//         this.isAdmin = false;
//       }
//     });
//   }

//   ngOnInit(): void {
//     // Initialize the properties on component initialization
//     this.isLoggedIn = this.authService.isAuthenticated();
//     if (this.isLoggedIn) {
//       this.isAdmin = this.authService.isAdmin();
//     }
//   }
// }


import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit, OnDestroy {
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;
  private authSubscription: Subscription | undefined;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    // Initialize the properties on component initialization
    this.isLoggedIn = this.authService.isAuthenticated();
    if (this.isLoggedIn) {
      this.isAdmin = this.authService.isAdmin();
    }

    // Subscribe to isAuthenticated$ observable
    this.authSubscription = this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isAdmin = this.authService.isAdmin();
      } else {
        this.isAdmin = false;
      }
    });
  }

  ngOnDestroy(): void {
    // Unsubscribe to prevent memory leaks
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }
}
