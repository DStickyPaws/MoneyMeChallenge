import { TestBed } from '@angular/core/testing';

import { BlacklistMobilenumberServiceService } from './blacklist-mobilenumber-service.service';

describe('BlacklistMobilenumberServiceService', () => {
  let service: BlacklistMobilenumberServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlacklistMobilenumberServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
