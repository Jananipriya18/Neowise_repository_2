import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewagroComponent } from './viewagro.component';

describe('ViewagroComponent', () => {
  let component: ViewagroComponent;
  let fixture: ComponentFixture<ViewagroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewagroComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewagroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
