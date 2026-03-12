import { Component, inject } from '@angular/core';
import { ProductcategoryServices } from '../services/productcategory.services';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-list-category',
  imports: [RouterLink],
  templateUrl: './list-category.html',
  styleUrl: './list-category.css',
})
export class ListCategory {
  
  private productCategoryServices = inject(ProductcategoryServices)


  CategoryServiceRef = this.productCategoryServices.getAllProductCategory();
  isLoading = this.CategoryServiceRef.isLoading;
  CategoryServiceResponse = this.CategoryServiceRef.value;

}
