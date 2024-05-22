import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DeleteConfirmComponent } from './delete-confirm.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { LaptopService } from '../services/laptop.service'; // Import LaptopService
import { Laptop } from '../models/laptop.model';

describe('DeleteConfirmComponent', () => {
    let component: DeleteConfirmComponent;
    let fixture: ComponentFixture<DeleteConfirmComponent>;
    let router: Router;
    let activatedRoute: ActivatedRoute;
    let mockLaptopService: jasmine.SpyObj<LaptopService>; // Declare mockLaptopService

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockLaptopService = jasmine.createSpyObj<LaptopService>('LaptopService', ['getLaptop', 'deleteLaptop'] as any);

        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientModule, FormsModule, HttpClientTestingModule], // Add HttpClientModule and HttpClientTestingModule to imports
            declarations: [DeleteConfirmComponent],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: LaptopService, useValue: mockLaptopService }
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

    // fit('DeleteConfirmComponent_should_navigate_to_viewLaptops_after_cancelDelete', () => {
    //     spyOn(router, 'navigate').and.stub(); // Spy on router.navigate method
    //     component.cancelDelete();
    //     expect(router.navigate).toHaveBeenCalledWith(['/viewLaptops']); // Verify router.navigate is called with correct argument
    // });

    fit('DeleteConfirmComponent_should_call_deleteLaptop_method_when_confirmDelete_is_called', () => {
        const laptopId = 1;
        
        // Spy on the deleteLaptop method of the LaptopService
        mockLaptopService.deleteLaptop.and.returnValue(of(null));

        // Call the confirmDelete method
        component.confirmDelete(laptopId);

        // Expect the deleteLaptop method to have been called with the laptopId
        expect(mockLaptopService.deleteLaptop).toHaveBeenCalledWith(laptopId);
    });
});
