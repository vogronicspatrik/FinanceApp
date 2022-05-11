import { TestBed } from '@angular/core/testing';

import { CashFlowItemService } from './cash-flow-item.service';

describe('CashFlowItemService', () => {
  let service: CashFlowItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CashFlowItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
