import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm/delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RecipeService } from './services/recipe.service'; // Import RecipeService
import { Recipe } from './models/recipe.model';

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockRecipeService: jasmine.SpyObj<RecipeService>; // Declare mockRecipeService

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockRecipeService = jasmine.createSpyObj<RecipeService>('RecipeService', ['getRecipe', 'deleteRecipe'] as any);

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule], // Add HttpClientModule and HttpClientTestingModule to imports
            declarations: [DeleteConfirmComponent],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: RecipeService, useValue: mockRecipeService }
            ]
        }).compileComponents();
        router = TestBed.inject(Router);
        activatedRoute = TestBed.inject(ActivatedRoute);
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(DeleteConfirmComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    fit('should_create_DeleteConfirmComponent', () => {
        expect(component).toBeTruthy();
    });

    // fit('DeleteConfirmComponent_should_navigate_to_viewRecipes_after_cancelDelete', () => {
    //     spyOn(router, 'navigate').and.stub(); // Spy on router.navigate method
    //     component.cancelDelete();
    //     expect(router.navigate).toHaveBeenCalledWith(['/viewRecipes']); // Verify router.navigate is called with correct argument
    // });

    fit('DeleteConfirmComponent_should_call_deleteRecipe_method_when_confirmDelete_is_called', () => {
        const recipeId = 1;
        
        // Spy on the deleteRecipe method of the RecipeService
        mockRecipeService.deleteRecipe.and.returnValue(of(null));

        // Call the confirmDelete method
        component.confirmDelete(recipeId);

        // Expect the deleteRecipe method to have been called with the recipeId
        expect(mockRecipeService.deleteRecipe).toHaveBeenCalledWith(recipeId);
    });
});

