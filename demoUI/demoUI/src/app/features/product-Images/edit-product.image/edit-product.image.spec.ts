import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProductImage } from './edit-product.image';

describe('EditProductImage', () => {
  let component: EditProductImage;
  let fixture: ComponentFixture<EditProductImage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditProductImage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditProductImage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
