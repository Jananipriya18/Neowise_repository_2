import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  showContainerDropdown = false;
  showAssignmentDropdown = false;
  showSidebar = true;
  showCountboard = false; // This property tracks whether the countboard should be displayed or not
  containerCount = 0;
  operatorCount = 0;

  constructor(private router: Router, private dataService: DataService) { }

  ngOnInit(): void {
    
  }

  logout(){
    localStorage.clear();
    this.router.navigate(['/login']);
    
  }


  displayDashboard() {
    this.router.navigate(['admin-dashboard']);
    this.showCountboard = true; // Show the countboard when the Dashboard link is clicked
    console.log(localStorage.getItem('token'));
    this.dataService.getContainers().subscribe(data => {
      this.containerCount = data.length;
    });

    this.dataService.getUsers().subscribe((data: { Users: any[] }) => {
      console.log(data);
      this.operatorCount = data.Users.filter(user => user.UserRole === 'Operator').length;
    });
  }
  
  
  toggleSidebar() {
    this.showSidebar = !this.showSidebar;
  }

 

  toggleContainerDropdown() {
    this.showContainerDropdown = !this.showContainerDropdown;
  }

  toggleAssignmentDropdown() {
    this.showAssignmentDropdown = !this.showAssignmentDropdown;
  }

  navigateTo(route: string) {
    this.router.navigate([`admin-dashboard/${route}`]);
    this.showCountboard = false;
  }



}
