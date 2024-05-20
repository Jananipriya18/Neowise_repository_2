// header.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(private router: Router) {}

  navigateToAddTutor() {
    this.router.navigate(['/addNewTutor']);
  }

  navigateToViewTutors() {
    this.router.navigate(['/viewTutors']);
  }
}