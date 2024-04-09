import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerViewGiftsComponent } from './customer-view-gifts.component';

describe('CustomerViewGiftsComponent', () => {
  let component: CustomerViewGiftsComponent;
  let fixture: ComponentFixture<CustomerViewGiftsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerViewGiftsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerViewGiftsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
