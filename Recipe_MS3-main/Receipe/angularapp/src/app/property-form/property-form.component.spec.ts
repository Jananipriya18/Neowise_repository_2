import { ComponentFixture, TestBed, async } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { PropertyService } from '../services/property.service';
import { PropertyFormComponent } from './property-form.component';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';

describe('PropertyFormComponent', () => {
  let component: PropertyFormComponent;
  let fixture: ComponentFixture<PropertyFormComponent>;
  let propertyService: PropertyService;
  let router: Router;// Use any type for router spy

  beforeEach(async(() => {
    TestBed.configureTestingModule({
        imports: [FormsModule, HttpClientTestingModule],
        declarations: [PropertyFormComponent],
        providers: [PropertyService, { provide: Router, useClass: class { navigate = jasmine.createSpy('navigate'); } }]
    }).compileComponents();
}));


  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyFormComponent);
    component = fixture.componentInstance;
    propertyService = TestBed.inject(PropertyService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  fit('should_create_PropertyFormComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('PropertyFormComponent_should_have_a_form_for_adding_a_property', () => {
    const formElement: HTMLFormElement = fixture.nativeElement.querySelector('form');
    expect(formElement).toBeTruthy();
  });

  fit('PropertyFormComponent_should_have_form_controls_for_property_details_description_ingredients_instructions_and_author', () => {
    const formElement: HTMLFormElement = fixture.nativeElement.querySelector('form');
    expect(formElement.querySelector('input[name="name"]')).toBeTruthy(); // Check for name input
    expect(formElement.querySelector('textarea[name="description"]')).toBeTruthy(); // Check for description textarea
    expect(formElement.querySelector('input[name="address"]')).toBeTruthy(); // Check for address input
    expect(formElement.querySelector('input[name="propertyType"]')).toBeTruthy(); // Check for propertyType input
    expect(formElement.querySelector('input[name="bedrooms"]')).toBeTruthy(); // Check for bedrooms input
    expect(formElement.querySelector('input[name="bathrooms"]')).toBeTruthy(); // Check for bathrooms input
    expect(formElement.querySelector('input[name="monthlyRent"]')).toBeTruthy(); // Check for monthlyRent input
    expect(formElement.querySelector('button[type="submit"]')).toBeTruthy(); // Check for submit button
  });

  fit('PropertyFormComponent_should_have_a_button_for_adding_a_property', () => {
    const buttonElement: HTMLButtonElement = fixture.nativeElement.querySelector('button');
    expect(buttonElement).toBeTruthy();
    expect(buttonElement.textContent).toContain('Add Property');
  });

  fit('PropertyFormComponent_should_have_addProperty_method', () => {
    expect(component.onSubmit).toBeTruthy(); // Check for onSubmit method
  });
});
