import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyListComponent } from './property-list.component';

describe('PropertyListComponent', () => {
  let component: PropertyListComponent;
  let fixture: ComponentFixture<PropertyListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PropertyListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('should create', () => {
    expect(component).toBeTruthy();
  });
  fit('should_create_RecipeFormComponent', () => {
    expect(component).toBeTruthy();
    });

    fit('RecipeFormComponent_should_have_a_form_for_adding_a_recipe', () => {
        const formElement: HTMLFormElement = fixture.nativeElement.querySelector('form');
        expect(formElement).toBeTruthy();
    });

    fit('RecipeFormComponent_should_have_form_controls_for_recipe_details_description_ingredients_instructions_and_author', () => {
        const formElement: HTMLFormElement = fixture.nativeElement.querySelector('form');
        expect(formElement.querySelector('#name')).toBeTruthy();
        expect(formElement.querySelector('#description')).toBeTruthy();
        expect(formElement.querySelector('#ingredients')).toBeTruthy();
        expect(formElement.querySelector('#instructions')).toBeTruthy();
        expect(formElement.querySelector('#author')).toBeTruthy();
    });

    fit('RecipeFormComponent_should_have_a_button_for_adding_a_recipe', () => {
        const buttonElement: HTMLButtonElement = fixture.nativeElement.querySelector('button');
        expect(buttonElement).toBeTruthy();
        expect(buttonElement.textContent).toContain('Add Recipe');
    });

    fit('RecipeFormComponent_should_have_addRecipe_method', () => {
        expect(component['addRecipe']).toBeTruthy();
    });
});
