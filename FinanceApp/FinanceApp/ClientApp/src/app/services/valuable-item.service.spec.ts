import { TestBed } from '@angular/core/testing';

import { ValuableItemService } from './valuable-item.service';

describe('ValuableItemService', () => {
  let service: ValuableItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ValuableItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
