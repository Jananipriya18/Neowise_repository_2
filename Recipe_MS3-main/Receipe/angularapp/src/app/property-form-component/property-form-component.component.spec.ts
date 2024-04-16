import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyFormComponentComponent } from './property-form-component.component';

describe('PropertyFormComponentComponent', () => {
  let component: PropertyFormComponentComponent;
  let fixture: ComponentFixture<PropertyFormComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PropertyFormComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyFormComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
