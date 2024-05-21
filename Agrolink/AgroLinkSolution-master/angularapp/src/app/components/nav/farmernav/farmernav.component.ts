import { Component, OnInit } from '@angular/core';

import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-farmernav',
  templateUrl: './farmernav.component.html',
  styleUrls: ['./farmernav.component.css']
})

export class FarmernavComponent implements OnInit {
  isLoggedIn: boolean = true;
  isFarmer: boolean = true;

  constructor(private authService: AuthService) {
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      // if (this.isLoggedIn) {
      //   this.isFarmer = this.authService.isFarmer();

      //   console.log(this.isFarmer);
      // } else {
         this.isFarmer = true;
      // }
    });
  }

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthenticated();
    console.log(this.isLoggedIn);
    // if (this.isLoggedIn) {
    //   this.isFarmer = this.authService.isFarmer();
    // }
  }

}
