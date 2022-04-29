import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ValuableItemsDashboardComponent } from './valuable-items-dashboard.component';

describe('ValuableItemsDashboardComponent', () => {
  let component: ValuableItemsDashboardComponent;
  let fixture: ComponentFixture<ValuableItemsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ValuableItemsDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ValuableItemsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
