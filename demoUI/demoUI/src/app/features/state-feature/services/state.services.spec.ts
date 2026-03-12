import { TestBed } from '@angular/core/testing';

import { StateServices } from './state.services';

describe('StateServices', () => {
  let service: StateServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StateServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
