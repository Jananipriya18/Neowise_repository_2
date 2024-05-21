import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewcropComponent } from './viewcrop.component';

describe('ViewcropComponent', () => {
  let component: ViewcropComponent;
  let fixture: ComponentFixture<ViewcropComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewcropComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewcropComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
