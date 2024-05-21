import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellernavComponent } from './sellernav.component';

describe('SellernavComponent', () => {
  let component: SellernavComponent;
  let fixture: ComponentFixture<SellernavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellernavComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellernavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
