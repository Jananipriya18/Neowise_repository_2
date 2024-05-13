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
import { RecipeListComponent } from '../recipe-list/recipe-list.component';
 
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
 
  fit('should_create_RecipeFormComponent', () => {
    expect(component).toBeTruthy();
});
 
 
  fit('RecipeFormComponent_should_render_error_messages_when_required_fields_are_empty_on_submit', () => {
  // Set all fields to empty strings
  component.newRecipe = {
    recipeId: null,
    name: '',
    email: '',
    subjectsOffered: '',
    contactNumber: '',
    availability: ''
  } as any;
 
  // Manually trigger form submission
  component.formSubmitted = true;
 
  fixture.detectChanges();
 
  // Find the form element
  const form = fixture.debugElement.query(By.css('form')).nativeElement;
 
  // Submit the form
  form.dispatchEvent(new Event('submit'));
 
  fixture.detectChanges();
 
  // Check if error messages are rendered for each field
  expect(fixture.debugElement.query(By.css('#name + .error-message'))).toBeTruthy();
  expect(fixture.debugElement.query(By.css('#email + .error-message'))).toBeTruthy();
  expect(fixture.debugElement.query(By.css('#subjectsOffered + .error-message'))).toBeTruthy();
  expect(fixture.debugElement.query(By.css('#contactNumber + .error-message'))).toBeTruthy();
  expect(fixture.debugElement.query(By.css('#availability + .error-message'))).toBeTruthy();
});
 
  fit('RecipeFormComponent_should_call_add_recipe_method_while_adding_the_recipe', () => {
    // Create a mock Recipe object with all required properties
    const recipe: Recipe = { 
      recipeId: 1, 
      name: 'Test Recipe', 
      email: 'Test Recipe Email', 
      subjectsOffered: ' Test subjects Offered', 
      contactNumber: 'Test Contact Number', 
      availability: 'Test Availability'
    } as any;
    const addRecipeSpy = spyOn(component, 'addTutor').and.callThrough();
    component.addTutor();
    expect(addTutorSpy).toHaveBeenCalled();
  });
});
 