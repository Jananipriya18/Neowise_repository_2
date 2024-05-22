import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeService } from '../services/recipe.service';
import { Recipe } from '../models/recipe.model'; // Import Recipe interface

@Component({
  selector: 'app-delete-confirm',
  templateUrl: './delete-confirm.component.html',
  styleUrls: ['./delete-confirm.component.css']
})
export class DeleteConfirmComponent implements OnInit {
  recipeId: number;
  recipe: Recipe; // Initialize recipe property with an empty object

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private recipeService: RecipeService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.recipeId = +params['id'];
      this.recipeService.getRecipe(this.recipeId).subscribe(
        (recipe: Recipe) => {
          this.recipe = recipe;
        },
        error => {
          console.error('Error fetching recipe:', error);
        }
      );
    });
  }

  confirmDelete(recipeId: number): void {
    this.recipeService.deleteRecipe(recipeId).subscribe(
      () => {
        console.log('Recipe deleted successfully.');
        this.router.navigate(['/viewRecipes']);
      },
      (error) => {
        console.error('Error deleting recipe:', error);
      }
    );
  }

  cancelDelete(): void {
    this.router.navigate(['/viewRecipes']);
  }
}
