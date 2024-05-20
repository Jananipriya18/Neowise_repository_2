import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatecropComponent } from './updatecrop.component';

describe('UpdatecropComponent', () => {
  let component: UpdatecropComponent;
  let fixture: ComponentFixture<UpdatecropComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdatecropComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdatecropComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
