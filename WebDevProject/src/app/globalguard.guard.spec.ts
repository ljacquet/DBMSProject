import { TestBed } from '@angular/core/testing';

import { GlobalguardGuard } from './globalguard.guard';

describe('GlobalguardGuard', () => {
  let guard: GlobalguardGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(GlobalguardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
