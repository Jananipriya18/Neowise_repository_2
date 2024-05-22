import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LaptopService } from '../services/laptop.service';
import { Laptop } from '../models/laptop.model'; // Import Laptop interface

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  laptopId: number;
  laptop: Laptop; // Initialize laptop property

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private laptopService: LaptopService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.laptopId = +params['id'];
      this.laptopService.getLaptop(this.laptopId).subscribe(
        (laptop: Laptop) => {
          this.laptop = laptop;
        },
        error => {
          console.error('Error fetching laptop:', error);
        }
      );
    });
  }

  confirmDelete(laptopId: number): void {
    this.laptopService.deleteLaptop(laptopId).subscribe(
      () => {
        console.log('Laptop deleted successfully.');
        this.router.navigate(['/viewLaptops']);
      },
      (error) => {
        console.error('Error deleting laptop:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewLaptops']);
  }
}
