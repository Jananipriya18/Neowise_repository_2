// src/app/services/recipe.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Recipe } from '../models/recipe.model';


@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private apiUrl = 'https://8080-bfdeeddcedfa312011766dbabbecfebfefftwo.premiumproject.examly.io/'; // Replace this with your API endpoint

  constructor(private http: HttpClient) { }

  addRecipe(recipe: Recipe): Observable<Recipe> {
    return this.http.post<Recipe>(`${this.apiUrl}api/Recipe`, recipe);
  }

  getRecipes(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(`${this.apiUrl}api/Recipe`);
  }

  deleteRecipe(recipeId: number): Observable<void> {
    const url = `${this.apiUrl}api/Recipe/${recipeId}`; // Adjust the URL to match your API endpoint
    return this.http.delete<void>(url);
  }

  getRecipe(recipeId: number): Observable<Recipe> {
    const url = `${this.apiUrl}api/Recipe/${recipeId}`;
    return this.http.get<Recipe>(url);
  }
}
