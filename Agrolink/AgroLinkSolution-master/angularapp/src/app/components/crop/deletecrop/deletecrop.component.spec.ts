import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletecropComponent } from './deletecrop.component';

describe('DeletecropComponent', () => {
  let component: DeletecropComponent;
  let fixture: ComponentFixture<DeletecropComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeletecropComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeletecropComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
