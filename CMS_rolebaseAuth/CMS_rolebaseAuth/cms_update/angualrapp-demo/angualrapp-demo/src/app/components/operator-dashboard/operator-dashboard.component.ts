import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-operator-dashboard',
  templateUrl: './operator-dashboard.component.html',
  styleUrls: ['./operator-dashboard.component.css']
})
export class OperatorDashboardComponent implements OnInit {

  constructor(public router: Router) { }

  ngOnInit(): void {
  }

  logout(){
    localStorage.clear();
    this.router.navigate(['/login']);
    
  }

}
