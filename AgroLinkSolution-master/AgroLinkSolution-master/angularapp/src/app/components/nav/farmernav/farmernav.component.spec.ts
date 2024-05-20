import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FarmernavComponent } from './farmernav.component';

describe('FarmernavComponent', () => {
  let component: FarmernavComponent;
  let fixture: ComponentFixture<FarmernavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FarmernavComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FarmernavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
