// import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
// import { RecipeService } from '../services/recipe.service';
// import { RecipeListComponent } from './laptop-list.component';
// import { of } from 'rxjs';
// import { RouterTestingModule } from '@angular/router/testing';
// import { HttpClientTestingModule } from '@angular/common/http/testing';
// import { Recipe } from '../models/recipe.model';

// describe('RecipeListComponent', () => {
//     let component: RecipeListComponent;
//     let fixture: ComponentFixture<RecipeListComponent>;
//     let mockRecipeService: jasmine.SpyObj<RecipeService>; // Specify the type of mock

//     beforeEach(waitForAsync(() => {
//         // Create a spy object with the methods you want to mock
//         mockRecipeService = jasmine.createSpyObj<RecipeService>('RecipeService', ['getRecipes', 'addRecipe']);

//         TestBed.configureTestingModule({
//             declarations: [RecipeListComponent],
//             imports: [RouterTestingModule, HttpClientTestingModule],
//             providers: [
//                 // Provide the mock service instead of the actual service
//                 { provide: RecipeService, useValue: mockRecipeService }
//             ]
//         }).compileComponents();
//     }));

//     beforeEach(() => {
//         fixture = TestBed.createComponent(RecipeListComponent);
//         component = fixture.componentInstance;
//     });

//     fit('should_create_recipe_listComponent', () => {
//         expect(component).toBeTruthy();
//     });

//     fit('recipe_listComponent_should_call_loadRecipes_on_ngOnInit', () => {
//         spyOn(component, 'loadRecipes');
//         fixture.detectChanges();
//         expect(component.loadRecipes).toHaveBeenCalled();
//     });

//     // fit('should_add_a_recipe_to_the_database', () => {
//     //     const newRecipe: Recipe = { recipeId: 1, name: 'New Recipe', description: 'Description', ingredients: 'Ingredients', instructions: 'Instructions', author: 'Author' };
//     //     const initialDatabaseLength = 0; // Assuming there are initially 2 recipes in the database
    
//     //     mockRecipeService.addRecipe.and.returnValue(of()); // Mock addRecipe method to return a successful response
//     //     mockRecipeService.getRecipes.and.returnValue(of([...component.recipes, newRecipe])); // Mock getRecipes to return the updated list with the new recipe
    
//     //     component.recipes = []; // Clear recipes array for testing
//     //     component.loadRecipes(); // Load recipes
    
//     //     expect(mockRecipeService.addRecipe).toHaveBeenCalledWith(newRecipe); // Verify addRecipe method is called with new recipe
//     //     expect(component.recipes.length).toBe(initialDatabaseLength + 1); // Check if a recipe is added by checking the length
//     // });

// });


import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { LaptopService } from '../services/laptop.service';
import { LaptopListComponent } from './laptop-list.component';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Laptop } from '../models/laptop.model';

describe('LaptopListComponent', () => {
    let component: LaptopListComponent;
    let fixture: ComponentFixture<LaptopListComponent>;
    let mockLaptopService: jasmine.SpyObj<LaptopService>;

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockLaptopService = jasmine.createSpyObj<LaptopService>('LaptopService', ['getLaptops', 'addLaptop']);

        TestBed.configureTestingModule({
            declarations: [LaptopListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: LaptopService, useValue: mockLaptopService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LaptopListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_laptop_listComponent', () => {
        expect(component).toBeTruthy();
    });

    fit('laptop_listComponent_should_call_loadLaptops_on_ngOnInit', () => {
        spyOn(component, 'loadLaptops');
        fixture.detectChanges();
        expect(component.loadLaptops).toHaveBeenCalled();
    });

    // fit('should_add_a_laptop_to_the_database', () => {
    //     const newLaptop: Laptop = {
    //         laptopId: 1, brand: 'New Brand', model: 'New Model', description: 'New Description',
    //         processor: 'New Processor', storage: 'New Storage', price: 1500
    //     };
    //     const initialDatabaseLength = 0; // Assuming there are initially 0 laptops in the database
    
    //     mockLaptopService.addLaptop.and.returnValue(of()); // Mock addLaptop method to return a successful response
    //     mockLaptopService.getLaptops.and.returnValue(of([...component.laptops, newLaptop])); // Mock getLaptops to return the updated list with the new laptop
    
    //     component.laptops = []; // Clear laptops array for testing
    //     component.loadLaptops(); // Load laptops
    
    //     expect(mockLaptopService.addLaptop).toHaveBeenCalledWith(newLaptop); // Verify addLaptop method is called with new laptop
    //     expect(component.laptops.length).toBe(initialDatabaseLength + 1); // Check if a laptop is added by checking the length
    // });
});
