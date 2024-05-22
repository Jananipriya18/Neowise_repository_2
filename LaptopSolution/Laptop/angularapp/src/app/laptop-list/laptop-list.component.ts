// laptop-list.component.ts
import { Component, OnInit } from '@angular/core';
import { Laptop } from '../models/laptop.model';
import { LaptopService } from '../services/laptop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-laptop-list',
  templateUrl: './laptop-list.component.html',
  styleUrls: ['./laptop-list.component.css']
})
export class LaptopListComponent implements OnInit {
  laptops: Laptop[] = [];

  constructor(private laptopService: LaptopService, private router: Router) { }

  ngOnInit(): void {
    this.loadLaptops();
  }

  loadLaptops(): void {
    this.laptopService.getLaptops().subscribe(laptops => this.laptops = laptops);
  }

  Delete(laptopId: number): void {
    // Navigate to confirm delete page with the laptop ID as a parameter
    this.router.navigate(['/confirmDelete', laptopId]);
  }
}
