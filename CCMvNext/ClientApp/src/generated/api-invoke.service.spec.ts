import { TestBed } from '@angular/core/testing';

import { ApiInvokeService } from './api-invoke.service';

describe('ApiInvokeServiceService', () => {
  let service: ApiInvokeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiInvokeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
