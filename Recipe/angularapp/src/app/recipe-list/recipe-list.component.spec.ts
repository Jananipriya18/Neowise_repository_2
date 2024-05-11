// import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { RecipeService } from '../services/recipe.service';
// import { RecipeListComponent } from './recipe-list.component';
// import { of } from 'rxjs';
// import { RouterTestingModule } from '@angular/router/testing';
// import { HttpClientTestingModule } from '@angular/common/http/testing';
// import { Recipe } from '../models/recipe.model';

// describe('RecipeListComponent', () => {
//     let component: RecipeListComponent;
//     let fixture: ComponentFixture<RecipeListComponent>;
//     let mockRecipeService;

//     beforeEach(async () => {
//         mockRecipeService = jasmine.createSpyObj(['getRecipes']);

//         await TestBed.configureTestingModule({
//             declarations: [RecipeListComponent],
//             imports: [RouterTestingModule, HttpClientTestingModule],
//             providers: [RecipeService]
//         }).compileComponents();
//     });

//     beforeEach(() => {
//         fixture = TestBed.createComponent(RecipeListComponent);
//         component = fixture.componentInstance;
//     });

//     fit('should_create_recipe_listComponent', () => {
//         // mockRecipeService.getRecipes.and.returnValue(of([]));
//         // fixture.detectChanges();
//         expect(component).toBeTruthy();
//     });

//     fit('recipe_listComponent_should_call_loadRecipes_on_ngOnInit', () => {
//         spyOn(component, 'loadRecipes');
//         fixture.detectChanges();
//         expect(component.loadRecipes).toHaveBeenCalled();
//     });

// });


import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RecipeService } from '../services/recipe.service';
import { RecipeListComponent } from './recipe-list.component';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Recipe } from '../models/recipe.model';

describe('RecipeListComponent', () => {
    let component: RecipeListComponent;
    let fixture: ComponentFixture<RecipeListComponent>;
    let mockRecipeService: jasmine.SpyObj<RecipeService>; // Specify the type of mock

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockRecipeService = jasmine.createSpyObj<RecipeService>('RecipeService', ['getRecipes', 'addRecipe']);

        TestBed.configureTestingModule({
            declarations: [RecipeListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: RecipeService, useValue: mockRecipeService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(RecipeListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_recipe_listComponent', () => {
        expect(component).toBeTruthy();
    });

    fit('recipe_listComponent_should_call_loadRecipes_on_ngOnInit', () => {
        spyOn(component, 'loadRecipes');
        fixture.detectChanges();
        expect(component.loadRecipes).toHaveBeenCalled();
    });

    // fit('should_add_a_recipe_to_the_database', () => {
    //     const newRecipe: Recipe = { tutorId: 1, name: 'New Recipe', email: 'Email', subjectsOffered: 'SubjectsOffered', instructions: 'Instructions', author: 'Author' };
    //     const initialDatabaseLength = 0; // Assuming there are initially 2 recipes in the database
    
    //     mockRecipeService.addRecipe.and.returnValue(of()); // Mock addRecipe method to return a successful response
    //     mockRecipeService.getRecipes.and.returnValue(of([...component.recipes, newRecipe])); // Mock getRecipes to return the updated list with the new recipe
    
    //     component.recipes = []; // Clear recipes array for testing
    //     component.loadRecipes(); // Load recipes
    
    //     expect(mockRecipeService.addRecipe).toHaveBeenCalledWith(newRecipe); // Verify addRecipe method is called with new recipe
    //     expect(component.recipes.length).toBe(initialDatabaseLength + 1); // Check if a recipe is added by checking the length
    // });

});
