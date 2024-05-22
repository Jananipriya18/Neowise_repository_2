import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { LaptopService } from '../services/laptop.service';
import { LaptopListComponent } from './laptop-list.component';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Laptop } from '../models/laptop.model';

describe('LaptopListComponent', () => {
    let component: LaptopListComponent;
    let fixture: ComponentFixture<LaptopListComponent>;
    let mockLaptopService: jasmine.SpyObj<LaptopService>;

    beforeEach(waitForAsync(() => {
        // Create a spy object with the methods you want to mock
        mockLaptopService = jasmine.createSpyObj<LaptopService>('LaptopService', ['getLaptops', 'addLaptop']);

        TestBed.configureTestingModule({
            declarations: [LaptopListComponent],
            imports: [RouterTestingModule, HttpClientTestingModule],
            providers: [
                // Provide the mock service instead of the actual service
                { provide: LaptopService, useValue: mockLaptopService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LaptopListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_laptop_listComponent', () => {
        expect(component).toBeTruthy();
    });

    fit('laptop_listComponent_should_call_loadLaptops_on_ngOnInit', () => {
        spyOn(component, 'loadLaptops');
        fixture.detectChanges();
        expect(component.loadLaptops).toHaveBeenCalled();
    });

});
