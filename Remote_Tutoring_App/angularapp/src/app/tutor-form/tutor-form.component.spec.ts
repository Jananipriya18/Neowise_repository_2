import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { RouterTestingModule } from '@angular/router/testing';
import { TutorFormComponent } from './tutor-form.component';
import { TutorService } from '../services/tutor.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Tutor } from '../models/tutor.model';
import { fakeAsync, tick } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { TutorListComponent } from '../tutor-list/tutor-list.component';
 
describe('TutorFormComponent', () => {
  let component: TutorFormComponent;
  let fixture: ComponentFixture<TutorFormComponent>;
  let tutorService: TutorService;
  let router: Router;
  let tutorListComponent: TutorListComponent;
 
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TutorFormComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientTestingModule],
      providers: [
        TutorService,
      ]
    })
      .compileComponents();
  });
 
  beforeEach(() => {
    fixture = TestBed.createComponent(TutorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
 
    tutorService = TestBed.inject(TutorService);
    router = TestBed.inject(Router);
 
  });
 
  fit('should_create_TutorFormComponent', () => {
    expect(component).toBeTruthy();
});
 
 
  fit('TutorFormComponent_should_render_error_messages_when_required_fields_are_empty_on_submit', () => {
  // Set all fields to empty strings
  component.newTutor = {
    tutorId: null,
    name: '',
    email: '',
    subjectsOffered: '',
    contactNumber: '',
    availability: ''
  } as any;
 
  // Manually trigger form submission
  component.formSubmitted = true;
 
  fixture.detectChanges();
 
  // Find the form element
  const form = fixture.debugElement.query(By.css('form')).nativeElement;
 
  // Submit the form
  form.dispatchEvent(new Event('submit'));
 
  fixture.detectChanges();
 
  // Check if error messages are rendered for each field
  expect(fixture.debugElement.query(By.css('#name + .error-message'))).toBeTruthy();
  expect(fixture.debugElement.query(By.css('#email + .error-message'))).toBeTruthy();
  expect(fixture.debugElement.query(By.css('#subjectsOffered + .error-message'))).toBeTruthy();
  expect(fixture.debugElement.query(By.css('#contactNumber + .error-message'))).toBeTruthy();
  expect(fixture.debugElement.query(By.css('#availability + .error-message'))).toBeTruthy();
});
 
  fit('TutorFormComponent_should_call_add_tutor_method_while_adding_the_tutor', () => {
    // Create a mock Tutor object with all required properties
    const tutor: Tutor = { 
      tutorId: 1, 
      name: 'Test Tutor', 
      email: 'Test Tutor Email', 
      subjectsOffered: ' Test subjects Offered', 
      contactNumber: 'Test Contact Number', 
      availability: 'Test Availability'
    } as any;
    const addTutorSpy = spyOn(component, 'addTutor').and.callThrough();
    component.addTutor();
    expect(addTutorSpy).toHaveBeenCalled();
  });
});
 