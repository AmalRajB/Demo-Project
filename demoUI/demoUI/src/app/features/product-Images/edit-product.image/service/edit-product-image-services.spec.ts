import { TestBed } from '@angular/core/testing';

import { EditProductImageServices } from './edit-product-image-services';

describe('EditProductImageServices', () => {
  let service: EditProductImageServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditProductImageServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
