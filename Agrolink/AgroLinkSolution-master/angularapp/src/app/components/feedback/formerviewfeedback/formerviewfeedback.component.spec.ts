import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormerviewfeedbackComponent } from './formerviewfeedback.component';

describe('FormerviewfeedbackComponent', () => {
  let component: FormerviewfeedbackComponent;
  let fixture: ComponentFixture<FormerviewfeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormerviewfeedbackComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormerviewfeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
