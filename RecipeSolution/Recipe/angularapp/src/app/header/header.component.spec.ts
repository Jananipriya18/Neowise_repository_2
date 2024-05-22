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

    fit('HeaderComponent_should_navigate_to_Add_New_Laptop', () => {
        spyOn(router, 'navigate');
        component.navigateToAddLaptop();
        expect(router.navigate).toHaveBeenCalledWith(['/addNewLaptop']);
    });

    fit('HeaderComponent_should_navigate_to_View_Recipe', () => {
        spyOn(router, 'navigate');
        component.navigateToViewLaptop();
        expect(router.navigate).toHaveBeenCalledWith(['/viewLaptops']);
    });
});
