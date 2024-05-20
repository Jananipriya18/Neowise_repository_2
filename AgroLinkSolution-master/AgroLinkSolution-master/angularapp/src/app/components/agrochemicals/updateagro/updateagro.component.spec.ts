import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateagroComponent } from './updateagro.component';

describe('UpdateagroComponent', () => {
  let component: UpdateagroComponent;
  let fixture: ComponentFixture<UpdateagroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateagroComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateagroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
