import { Component } from '@angular/core';
import { Property } from '../models/property.model';
import { PropertyService } from '../services/property.service'; // Corrected import statement
import { Router } from '@angular/router';

@Component({
  selector: 'app-property-form',
  templateUrl: './property-form.component.html',
  styleUrls: ['./property-form.component.css']
})
export class PropertyFormComponent {
  newProperty: Property = {
    propertyId: 0,
    name: '',
    description: '',
    address: '',
    propertyType: '',
    bedrooms: 2,
    bathrooms: 1,
    monthlyRent: 1500,
    available: true,
    createdAt: new Date()
  };

  constructor(private propertyService: PropertyService, private router: Router) { }

  onSubmit(): void {
    this.propertyService.addProperty(this.newProperty).subscribe(() => {
      console.log('Property added successfully!');
      this.router.navigate(['/viewProperties']);
    });
  }
}
