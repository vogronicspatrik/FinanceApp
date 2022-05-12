import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ValueWithCurrencyComponent } from './value-with-currency.component';

describe('ValueWithCurrencyComponent', () => {
  let component: ValueWithCurrencyComponent;
  let fixture: ComponentFixture<ValueWithCurrencyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ValueWithCurrencyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ValueWithCurrencyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
