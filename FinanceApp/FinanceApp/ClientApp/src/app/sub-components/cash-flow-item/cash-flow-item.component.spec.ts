import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashFlowItemComponent } from './cash-flow-item.component';

describe('CashFlowItemComponent', () => {
  let component: CashFlowItemComponent;
  let fixture: ComponentFixture<CashFlowItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CashFlowItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CashFlowItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
