import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBondComponent } from './add-bond.component';

describe('AddBondComponent', () => {
  let component: AddBondComponent;
  let fixture: ComponentFixture<AddBondComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddBondComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddBondComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
