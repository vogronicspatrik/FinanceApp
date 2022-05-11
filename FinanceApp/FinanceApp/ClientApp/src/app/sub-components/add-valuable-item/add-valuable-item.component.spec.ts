import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddValuableItemComponent } from './add-valuable-item.component';

describe('AddValuableItemComponent', () => {
  let component: AddValuableItemComponent;
  let fixture: ComponentFixture<AddValuableItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddValuableItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddValuableItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
