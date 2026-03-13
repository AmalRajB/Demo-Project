import { Component, computed, inject, signal } from '@angular/core';
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

  pageNumber = signal(1);
  pageSize = 4;

  selectedCategory = signal<string | null>(null);
  searchTerm = signal('');

  private productservice = inject(ProductServices);
  private categoryservice = inject(ProductcategoryServices)


  categoryref = this.categoryservice.getAllProductCategory();
  categoryResponse = this.categoryref.value;


  form = new FormGroup({
    category: new FormControl('')
  });

  productref = this.productservice.getProducts(this.pageNumber, this.pageSize, this.selectedCategory, this.searchTerm);

  productResponse = this.productref.value;

  products = computed(() => this.productref.value()?.data ?? []);

  totalRecords = computed(() => this.productref.value()?.totalRecord ?? 0);

  totalPages = computed(() =>
    Math.ceil(this.totalRecords() / this.pageSize)
  );

  isLoading = this.productref.isLoading;

  // pagination function

  nextPage() {
    if (this.pageNumber() < this.totalPages()) {
      this.pageNumber.update(p => p + 1);
    }
  }

  previousPage() {
    if (this.pageNumber() > 1) {
      this.pageNumber.update(p => p - 1);
    }
  }

  constructor() {
    this.form.get('category')?.valueChanges.subscribe(id => {
      this.pageNumber.set(1);   // reset pagination
      this.selectedCategory.set(id);  // update category signal
    });
  }

  onSearch(event: Event) {

    const value = (event.target as HTMLInputElement).value;

    this.pageNumber.set(1);
    this.searchTerm.set(value);

  }
}


