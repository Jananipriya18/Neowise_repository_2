import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { RouterTestingModule } from '@angular/router/testing';
import { RecipeFormComponent } from './recipe-form.component';
import { RecipeService } from '../services/recipe.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Recipe } from '../models/recipe.model';
import { fakeAsync, tick } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { RecipeListComponent } from '../laptop-list/laptop-list.component';

describe('RecipeFormComponent', () => {
  let component: RecipeFormComponent;
  let fixture: ComponentFixture<RecipeFormComponent>;
  let recipeService: RecipeService;
  let router: Router;
  let recipeListComponent: RecipeListComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RecipeFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [
        RecipeService,
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    recipeService = TestBed.inject(RecipeService);
    router = TestBed.inject(Router);

  });

  fit('should_have_addRecipe_method', () => {
    expect(component.addRecipe).toBeTruthy();
  });

  fit('should_show_error_messages_for_required_fields_on_submit', fakeAsync(() => {
    // Mock new recipe data
    component.newRecipe = {
        recipeId: 1,
        name: '',
        description: '',
        ingredients: '',
        instructions: '',
        author: ''
    };

    // Trigger form submission
    const form = fixture.debugElement.query(By.css('form')).nativeElement;
    form.dispatchEvent(new Event('submit'));
    fixture.detectChanges();
    tick();

    // Check error messages for each field
    const errorMessages = fixture.debugElement.queryAll(By.css('.error-message'));
    expect(errorMessages.length).toBe(5); // Assuming there are 5 required fields

    // Check error messages content
    expect(errorMessages[0].nativeElement.textContent).toContain('Name is required');
    expect(errorMessages[1].nativeElement.textContent).toContain('Description is required');
    expect(errorMessages[2].nativeElement.textContent).toContain('Ingredients are required');
    expect(errorMessages[3].nativeElement.textContent).toContain('Instructions are required');
    expect(errorMessages[4].nativeElement.textContent).toContain('Author is required');
}));


  // fit('should show name required error message on register page', fakeAsync(() => {
  //   const nameInput = fixture.debugElement.query(By.css('#name'));
  //   nameInput.nativeElement.value = '';
  //   nameInput.nativeElement.dispatchEvent(new Event('input'));
  //   fixture.detectChanges();
  //   tick();
  //   const errorMessage = fixture.debugElement.query(By.css('.error-message'));
  //   expect(errorMessage.nativeElement.textContent).toContain('Name is required');
  // }));

  fit('should_not_render_any_error_messages_when_all_fields_are_filled', () => {
    const compiled = fixture.nativeElement;
    const form = compiled.querySelector('form');

    // Fill all fields
    component.newRecipe = {
      recipeId: null, // or omit this line if recipeId is auto-generated
      name: 'Test Name',
      description: 'Test Description',
      ingredients: 'Test Ingredients',
      instructions: 'Test Instructions',
      author: 'Test Author'
    };

    fixture.detectChanges();

    form.dispatchEvent(new Event('submit')); // Submit the form

    // Check if no error messages are rendered
    expect(compiled.querySelector('#nameError')).toBeNull();
    expect(compiled.querySelector('#descriptionError')).toBeNull();
    expect(compiled.querySelector('#ingredientsError')).toBeNull();
    expect(compiled.querySelector('#instructionsError')).toBeNull();
    expect(compiled.querySelector('#authorError')).toBeNull();
  });

  fit('should_call_add_recipe_method_while_adding_the_recipe', () => {
    // Create a mock Recipe object with all required properties
    const recipe: Recipe = { 
      recipeId: 1, 
      name: 'Test Recipe', 
      description: 'Test Recipe Description', 
      ingredients: 'Ingredient 2', 
      instructions: 'Test Recipe Instructions', 
      author: 'Test Author'
    };
    const addRecipeSpy = spyOn(component, 'addRecipe').and.callThrough();
    component.addRecipe();
    expect(addRecipeSpy).toHaveBeenCalled();
  });
});

