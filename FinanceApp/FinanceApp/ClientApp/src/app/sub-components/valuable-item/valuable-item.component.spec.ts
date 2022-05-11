import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ValuableItemComponent } from './valuable-item.component';

describe('ValuableItemComponent', () => {
  let component: ValuableItemComponent;
  let fixture: ComponentFixture<ValuableItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ValuableItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ValuableItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
