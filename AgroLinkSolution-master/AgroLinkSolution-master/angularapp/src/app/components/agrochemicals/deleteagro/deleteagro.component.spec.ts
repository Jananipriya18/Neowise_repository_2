import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteagroComponent } from './deleteagro.component';

describe('DeleteagroComponent', () => {
  let component: DeleteagroComponent;
  let fixture: ComponentFixture<DeleteagroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteagroComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteagroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
