import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, async } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';

import { RecipeService } from '../services/recipe.service';
import { RecipeFormComponent } from './recipe-form.component';
import { Router } from '@angular/router';

describe('RecipeFormComponent', () => {
    let component: RecipeFormComponent;
    let fixture: ComponentFixture<RecipeFormComponent>;
    let recipeService: RecipeService;
    let router: Router;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [FormsModule, HttpClientTestingModule],
            declarations: [RecipeFormComponent],
            providers: [RecipeService, { provide: Router, useClass: class { navigate = jasmine.createSpy('navigate'); } }]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(RecipeFormComponent);
        component = fixture.componentInstance;
        recipeService = TestBed.inject(RecipeService);
        router = TestBed.inject(Router);
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
