import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateagroComponent } from './createagro.component';

describe('CreateagroComponent', () => {
  let component: CreateagroComponent;
  let fixture: ComponentFixture<CreateagroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateagroComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateagroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
