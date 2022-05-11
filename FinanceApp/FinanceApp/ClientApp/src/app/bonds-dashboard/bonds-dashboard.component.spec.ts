import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BondsDashboardComponent } from './bonds-dashboard.component';

describe('BondsDashboardComponent', () => {
  let component: BondsDashboardComponent;
  let fixture: ComponentFixture<BondsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BondsDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BondsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
