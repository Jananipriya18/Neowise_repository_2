import { Component } from '@angular/core';
import { Laptop } from '../models/laptop.model';
import { LaptopService } from '../services/laptop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-laptop-form',
  templateUrl: './laptop-form.component.html',
  styleUrls: ['./laptop-form.component.css']
})
export class LaptopFormComponent {
  newLaptop: Laptop = {
    laptopId: 0,
    brand: '',
    model: '',
    description: '',
    processor: '',
    storage: '',
    price: 0
  };

  formSubmitted = false; // Track form submission

  constructor(private laptopService: LaptopService, private router: Router) { }

  addLaptop(): void {
    this.formSubmitted = true; // Set formSubmitted to true on form submission
    if (this.isFormValid()) {
      this.laptopService.addLaptop(this.newLaptop).subscribe(() => {
        console.log('Laptop added successfully!');
        this.router.navigate(['/viewLaptops']);
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.newLaptop[fieldName];
    return !field && (this.formSubmitted || this.newLaptop[fieldName].touched);
  }

  isFormValid(): boolean {
    return !this.isFieldInvalid('brand') && !this.isFieldInvalid('model') &&
      !this.isFieldInvalid('description') && !this.isFieldInvalid('processor') &&
      !this.isFieldInvalid('storage') && !this.isFieldInvalid('price');
  }
}
