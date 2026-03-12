import { Component, inject, signal } from '@angular/core';
import { ProductServices } from '../services/product.services';
import { RouterLink } from '@angular/router';
import { ProductcategoryServices } from '../../productcategory/services/productcategory.services';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-list-product',
  imports: [RouterLink, ReactiveFormsModule],
  templateUrl: './list-product.html',
  styleUrl: './list-product.css',
})
export class ListProduct {

  private productservice = inject(ProductServices);
  private categoryservice = inject(ProductcategoryServices)


  categoryref = this.categoryservice.getAllProductCategory();
  categoryResponse = this.categoryref.value;


  form = new FormGroup({
    category: new FormControl('')
  });

  categoryId = signal<string | null>(null);

  productref = this.productservice.getproductbyCategory(this.categoryId);

  productResponse = this.productref.value;

  constructor() {
    this.form.get('category')?.valueChanges.subscribe(id => {
      this.categoryId.set(id);
    });
  }
}


