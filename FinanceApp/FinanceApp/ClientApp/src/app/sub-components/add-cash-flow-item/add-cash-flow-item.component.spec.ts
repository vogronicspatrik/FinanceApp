import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCashFlowItemComponent } from './add-cash-flow-item.component';

describe('AddCashFlowItemComponent', () => {
  let component: AddCashFlowItemComponent;
  let fixture: ComponentFixture<AddCashFlowItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCashFlowItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCashFlowItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
