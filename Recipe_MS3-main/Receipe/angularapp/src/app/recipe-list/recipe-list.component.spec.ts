import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RecipeService } from '../services/recipe.service';
import { RecipeListComponent } from './recipe-list.component';
import { of } from 'rxjs';

describe('RecipeListComponent', () => {
    let component: RecipeListComponent;
    let fixture: ComponentFixture<RecipeListComponent>;
    let mockRecipeService;

    beforeEach(async () => {
        mockRecipeService = jasmine.createSpyObj(['getRecipes']);

        await TestBed.configureTestingModule({
            declarations: [RecipeListComponent],
            providers: [
                { provide: RecipeService, useValue: mockRecipeService }
            ]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(RecipeListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_recipe_listComponent', () => {
        mockRecipeService.getRecipes.and.returnValue(of([]));
        fixture.detectChanges();
        expect(component).toBeTruthy();
    });

    fit('recipe_listComponent_should_call_loadRecipes_on_ngOnInit', () => {
        spyOn(component, 'loadRecipes');
        fixture.detectChanges();
        expect(component.loadRecipes).toHaveBeenCalled();
    });

});
