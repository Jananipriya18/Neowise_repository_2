import { ComponentFixture, TestBed, waitForAsync, fakeAsync, tick } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { LaptopFormComponent } from './laptop-form.component';
import { LaptopService } from '../services/laptop.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Laptop } from '../models/laptop.model';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { LaptopListComponent } from '../laptop-list/laptop-list.component';

describe('LaptopFormComponent', () => {
  let component: LaptopFormComponent;
  let fixture: ComponentFixture<LaptopFormComponent>;
  let laptopService: LaptopService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LaptopFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [
        LaptopService,
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LaptopFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    laptopService = TestBed.inject(LaptopService);
    router = TestBed.inject(Router);
  });

  fit('should_create_LaptopFormComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('LaptopFormComponent_should_render_error_messages_when_required_fields_are_empty_on_submit', fakeAsync(() => {
    // Set all fields to empty values
    component.newLaptop = {
      laptopId: 0,
      brand: '',
      model: '',
      description: '',
      processor: '',
      storage: '',
      price: 0
    };

    // Trigger form submission
    const form = fixture.debugElement.query(By.css('form')).nativeElement;
    form.dispatchEvent(new Event('submit'));
    fixture.detectChanges();
    tick();

    // Check error messages for each field
    const errorMessages = fixture.debugElement.queryAll(By.css('.error-message'));
    expect(errorMessages.length).toBe(6); // Assuming there are 6 required fields

    // Check error messages content
    expect(errorMessages[0].nativeElement.textContent).toContain('Brand is required');
    expect(errorMessages[1].nativeElement.textContent).toContain('Model is required');
    expect(errorMessages[2].nativeElement.textContent).toContain('Description is required');
    expect(errorMessages[3].nativeElement.textContent).toContain('Processor is required');
    expect(errorMessages[4].nativeElement.textContent).toContain('Storage is required');
    expect(errorMessages[5].nativeElement.textContent).toContain('Price is required');
  }));

  // fit('should_not_render_any_error_messages_when_all_fields_are_filled', () => {
  //   const compiled = fixture.nativeElement;
  //   const form = compiled.querySelector('form');

  //   // Fill all fields
  //   component.newLaptop = {
  //     laptopId: 0, // or omit this line if laptopId is auto-generated
  //     brand: 'Test Brand',
  //     model: 'Test Model',
  //     description: 'Test Description',
  //     processor: 'Test Processor',
  //     storage: 'Test Storage',
  //     price: 1000
  //   };

  //   fixture.detectChanges();

  //   form.dispatchEvent(new Event('submit')); // Submit the form

  //   // Check if no error messages are rendered
  //   expect(compiled.querySelector('#brandError')).toBeNull();
  //   expect(compiled.querySelector('#modelError')).toBeNull();
  //   expect(compiled.querySelector('#descriptionError')).toBeNull();
  //   expect(compiled.querySelector('#processorError')).toBeNull();
  //   expect(compiled.querySelector('#storageError')).toBeNull();
  //   expect(compiled.querySelector('#priceError')).toBeNull();
  // });

  fit('LaptopFormComponent_should_call_add_laptop_method_while_adding_the_laptop', () => {
    // Create a mock Laptop object with all required properties
    const laptop: Laptop = {
      laptopId: 1,
      brand: 'Test Brand',
      model: 'Test Model',
      description: 'Test Description',
      processor: 'Test Processor',
      storage: 'Test Storage',
      price: 1500
    };
    const addLaptopSpy = spyOn(component, 'addLaptop').and.callThrough();
    component.addLaptop();
    expect(addLaptopSpy).toHaveBeenCalled();
  });
});
