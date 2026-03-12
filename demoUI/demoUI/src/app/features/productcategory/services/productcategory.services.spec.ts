import { TestBed } from '@angular/core/testing';

import { ProductcategoryServices } from './productcategory.services';

describe('ProductcategoryServices', () => {
  let service: ProductcategoryServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductcategoryServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
