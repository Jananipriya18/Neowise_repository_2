import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { HeaderComponent } from './header.component';

describe('HeaderComponent', () => {
    let component: HeaderComponent;
    let fixture: ComponentFixture<HeaderComponent>;
    let router: Router;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [HeaderComponent],
            imports: [RouterTestingModule],
        }).compileComponents();

        fixture = TestBed.createComponent(HeaderComponent);
        component = fixture.componentInstance;
        router = TestBed.inject(Router);
    });

    fit('should_create_HeaderComponent', () => {
        expect(component).toBeTruthy();
    });

    fit('HeaderComponent_should_navigate_to_Add_New_Recipe', () => {
        spyOn(router, 'navigate');
        component.navigateToAddRecipe();
        expect(router.navigate).toHaveBeenCalledWith(['/addNewRecipe']);
    });

    fit('HeaderComponent_should_navigate_to_View_Recipe', () => {
        spyOn(router, 'navigate');
        component.navigateToViewRecipes();
        expect(router.navigate).toHaveBeenCalledWith(['/viewRecipes']);
    });

    fit('HeaderComponent_should_have_a_link_with_text_View_Recipe', () => {
        const navItems: NodeListOf<HTMLElement> = fixture.nativeElement.querySelectorAll('a');
        const viewRecipeLink: HTMLElement = navItems[navItems.length - 1];
        expect(viewRecipeLink.textContent).toContain('View Recipe');
    });
});
