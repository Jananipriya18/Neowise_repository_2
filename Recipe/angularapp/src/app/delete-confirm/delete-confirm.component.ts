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
  tutorId: number;
  recipe: Recipe; // Initialize recipe property with an empty object

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private recipeService: RecipeService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.tutorId = +params['id'];
      this.recipeService.getRecipe(this.tutorId).subscribe(
        (recipe: Recipe) => {
          this.recipe = recipe;
        },
        error => {
          console.error('Error fetching recipe:', error);
        }
      );
    });
  }

  confirmDelete(tutorId: number): void {
    this.recipeService.deleteRecipe(tutorId).subscribe(
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
