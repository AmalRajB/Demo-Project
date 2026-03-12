import { TestBed } from '@angular/core/testing';

import { FileServices } from './file.services';

describe('FileServices', () => {
  let service: FileServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FileServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
