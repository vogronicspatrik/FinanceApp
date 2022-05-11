import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegularCashFlowDashboardComponent } from './regular-cash-flow-dashboard.component';

describe('RegularCashFlowDashboardComponent', () => {
  let component: RegularCashFlowDashboardComponent;
  let fixture: ComponentFixture<RegularCashFlowDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegularCashFlowDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegularCashFlowDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
