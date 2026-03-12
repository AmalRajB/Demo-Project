import { TestBed } from '@angular/core/testing';

import { CountryServices } from './country.services';

describe('CountryServices', () => {
  let service: CountryServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CountryServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
