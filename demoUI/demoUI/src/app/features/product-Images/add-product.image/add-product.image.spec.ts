import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProductImage } from './add-product.image';

describe('AddProductImage', () => {
  let component: AddProductImage;
  let fixture: ComponentFixture<AddProductImage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddProductImage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddProductImage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
