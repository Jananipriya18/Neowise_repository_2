import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sellernav',
  templateUrl: './sellernav.component.html',
  styleUrls: ['./sellernav.component.css']
})
export class SellernavComponent implements OnInit {

  isLoggedIn: boolean = false;
  isSeller: boolean = false;
  
  constructor(private authService: AuthService) {
    this.authService.isAuthenticated$.subscribe((authenticated: boolean) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.isSeller = this.authService.isSeller();
        console.log(this.isSeller);
      } else {
        this.isSeller = false;
      }
    });
  }

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthenticated();
    if (this.isLoggedIn) {
      this.isSeller = this.authService.isSeller();
    }
    console.log(this.isSeller);
  }

}
