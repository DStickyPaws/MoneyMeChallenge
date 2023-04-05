import { TestBed } from '@angular/core/testing';

import { BlacklistdomainService } from './blacklistdomain.service';

describe('BlacklistdomainService', () => {
  let service: BlacklistdomainService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlacklistdomainService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
