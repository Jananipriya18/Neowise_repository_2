import { Component } from '@angular/core';
import { Recipe } from '../models/tutor.model';
import { RecipeService } from '../services/tutor.service'; // Corrected import statement
import { Router } from '@angular/router';


@Component({
  selector: 'app-recipe-form',
  templateUrl: './recipe-form.component.html',
  styleUrls: ['./recipe-form.component.css']
})

export class RecipeFormComponent {
  newRecipe: Recipe = {
    tutorId: 0,
    name: '',
    email: '',
    subjectsOffered: '',
    contactNumber: '',
    availability: ''
  };
  
  formSubmitted = false; // Track form submission

  constructor(private recipeService: RecipeService, private router: Router) { }

  addRecipe(): void {
    this.formSubmitted = true; // Set formSubmitted to true on form submission
    if (this.isFormValid()) {
      this.recipeService.addRecipe(this.newRecipe).subscribe(() => {
        console.log('Recipe added successfully!');
        this.router.navigate(['/viewRecipes']);
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.newRecipe[fieldName];
    return !field && (this.formSubmitted || this.newRecipe[fieldName].touched);
  }

  isFormValid(): boolean {
    return !this.isFieldInvalid('name') && !this.isFieldInvalid('email') &&
      !this.isFieldInvalid('subjectsOffered') && !this.isFieldInvalid('contactNumber') &&
      !this.isFieldInvalid('availability');
  }
}

