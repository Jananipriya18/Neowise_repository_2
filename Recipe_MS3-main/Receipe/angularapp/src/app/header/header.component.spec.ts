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

    fit('HeaderComponent_should_navigate_to_Add_New_Property', () => {
        spyOn(router, 'navigate');
        component.navigateToAddProperty();
        expect(router.navigate).toHaveBeenCalledWith(['/addNewProperty']);
    });

    fit('HeaderComponent_should_navigate_to_View_Property', () => {
        spyOn(router, 'navigate');
        component.navigateToViewProperties();
        expect(router.navigate).toHaveBeenCalledWith(['/viewProperties']);
    });

    fit('HeaderComponent_should_have_a_link_with_text_View_property', () => {
        const navItems: NodeListOf<HTMLElement> = fixture.nativeElement.querySelectorAll('a');
        const viewRecipeLink: HTMLElement = navItems[navItems.length - 1];
        expect(viewRecipeLink.textContent).toContain('View Property');
    });
});
