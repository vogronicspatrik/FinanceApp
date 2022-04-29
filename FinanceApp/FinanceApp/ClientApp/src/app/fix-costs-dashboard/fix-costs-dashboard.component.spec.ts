import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FixCostsDashboardComponent } from './fix-costs-dashboard.component';

describe('FixCostsDashboardComponent', () => {
  let component: FixCostsDashboardComponent;
  let fixture: ComponentFixture<FixCostsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FixCostsDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FixCostsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
