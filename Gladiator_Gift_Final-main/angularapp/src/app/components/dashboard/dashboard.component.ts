import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit(): void {
  }
  logout() {
    // Add your logout logic here (e.g., clearing session, redirecting to login page)
    // For now, let's navigate to the login page
    this.router.navigate(['/login']);
  }
  isActive(route: string): boolean {
    return this.router.url === route;
  }

}
