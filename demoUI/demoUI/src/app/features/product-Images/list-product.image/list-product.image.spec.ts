import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListProductImage } from './list-product.image';

describe('ListProductImage', () => {
  let component: ListProductImage;
  let fixture: ComponentFixture<ListProductImage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListProductImage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListProductImage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
