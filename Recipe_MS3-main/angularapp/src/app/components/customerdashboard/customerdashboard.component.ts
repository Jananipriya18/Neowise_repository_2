import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-customerdashboard',
  templateUrl: './customerdashboard.component.html',
  styleUrls: ['./customerdashboard.component.css']
})
export class CustomerdashboardComponent implements OnInit {

  constructor(private router: Router,private orderService: OrderService) { }

  ngOnInit(): void {
  }
  logout() {
    // Add your logout logic here (e.g., clearing session, redirecting to login page)
    // For now, let's navigate to the login page
    this.router.navigate(['/login']);
  }
  isActive(route: string): boolean {
    return this.router.isActive(route, true);
  }

}
