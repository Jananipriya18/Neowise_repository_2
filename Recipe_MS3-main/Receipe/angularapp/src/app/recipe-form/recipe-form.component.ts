// src/app/recipe-form/recipe-form.component.ts
import { Component } from '@angular/core';
import { Recipe } from '../models/recipe.model';
import { RecipeService } from '../services/recipe.service'; // Corrected import statement
import { Router } from '@angular/router';


@Component({
  selector: 'app-recipe-form',
  templateUrl: './recipe-form.component.html',
  styleUrls: ['./recipe-form.component.css']
})
export class RecipeFormComponent {
  newRecipe: Recipe = {
    recipeId: 0,
    name: '',
    description: '',
    ingredients: '',
    instructions: '',
    author: ''
  };

  constructor(private recipeService: RecipeService, private router: Router) { }

  addRecipe(): void {
    this.recipeService.addRecipe(this.newRecipe).subscribe(() => {
      console.log('Recipe added successfully!');
      this.router.navigate(['/viewRecipes']);
    });
  }
}
