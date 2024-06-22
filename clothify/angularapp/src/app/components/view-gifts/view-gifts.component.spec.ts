import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewGiftsComponent } from './view-gifts.component';

describe('ViewGiftsComponent', () => {
  let component: ViewGiftsComponent;
  let fixture: ComponentFixture<ViewGiftsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewGiftsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewGiftsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
