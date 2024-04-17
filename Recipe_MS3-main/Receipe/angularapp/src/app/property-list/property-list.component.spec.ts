import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertyService } from '../services/property.service';
import { PropertyListComponent } from './property-list.component';
import { of } from 'rxjs';

describe('PropertyListComponent', () => {
    let component: PropertyListComponent;
    let fixture: ComponentFixture<PropertyListComponent>;
    let mockPropertyService;

    beforeEach(async () => {
        mockPropertyService = jasmine.createSpyObj(['getProperties']);

        await TestBed.configureTestingModule({
            declarations: [PropertyListComponent],
            providers: [
                { provide: PropertyService, useValue: mockPropertyService }
            ]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(PropertyListComponent);
        component = fixture.componentInstance;
    });

    fit('should_create_property_listComponent', () => {
        mockPropertyService.getProperties.and.returnValue(of([]));
        fixture.detectChanges();
        expect(component).toBeTruthy();
    });

    fit('property_listComponent_should_call_properties_on_ngOnInit', () => {
        spyOn(component, 'loadProperties');
        fixture.detectChanges();
        expect(component.loadProperties).toHaveBeenCalled();
    });

});