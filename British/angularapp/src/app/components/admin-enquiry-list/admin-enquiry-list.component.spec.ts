import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminEnquiryListComponent } from './admin-enquiry-list.component';

describe('AdminEnquiryListComponent', () => {
  let component: AdminEnquiryListComponent;
  let fixture: ComponentFixture<AdminEnquiryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminEnquiryListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminEnquiryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
