import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductcategoryServices } from '../services/productcategory.services';
import { AddCategoryDto } from '../models/productCategory.model';

@Component({
  selector: 'app-add-category',
  imports: [ReactiveFormsModule],
  templateUrl: './add-category.html',
  styleUrl: './add-category.css',
})
export class AddCategory {

  private productCategoryServices = inject(ProductcategoryServices)



  addCategoryForm = new FormGroup({
    categoryName: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
    })
  })

  get categoryNameControlForm() {
    return this.addCategoryForm.controls.categoryName;
  };


  onSubmit() {

    const formvalue = this.addCategoryForm.getRawValue();

    const addCategoryRequestDto: AddCategoryDto = {
      categoryName: formvalue.categoryName
    }

    this.productCategoryServices.addProductCategory(addCategoryRequestDto).subscribe({
      next: (response) => {
        console.log(response)
      },
      error: (error) => {
        console.error(error)
      }
    })
  }

}
