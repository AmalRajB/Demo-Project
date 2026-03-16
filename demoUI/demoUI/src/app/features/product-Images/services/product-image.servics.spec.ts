import { TestBed } from '@angular/core/testing';

import { ProductImageServics } from './product-image.servics';

describe('ProductImageServics', () => {
  let service: ProductImageServics;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductImageServics);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
