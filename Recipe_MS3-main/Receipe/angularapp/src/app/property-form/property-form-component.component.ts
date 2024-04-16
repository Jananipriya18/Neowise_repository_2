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
    // Include other properties from the Property model as needed
    // For example: name: '', address: '', etc.
  };

  constructor(private propertyService: PropertyService, private router: Router) { }

  onSubmit(): void {
    this.propertyService.addProperty(this.newProperty).subscribe(() => {
      console.log('Property added successfully!');
      this.router.navigate(['/viewProperties']);
    });
  }
}
