import { Component, effect, inject, input } from '@angular/core';
import { ProductcategoryServices } from '../services/productcategory.services';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { editCategoryDto } from '../models/productCategory.model';

@Component({
  selector: 'app-edit-category',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-category.html',
  styleUrl: './edit-category.css',
})
export class EditCategory {

  id = input<string>()

  private productCategoryServices = inject(ProductcategoryServices)

  productCategoryRef = this.productCategoryServices.getProductCategoryById(this.id);

  productcategoryResponse = this.productCategoryRef.value;


  editCategoryForm = new FormGroup({
    categoryName: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
    })
  })

  get categoryNameControlForm() {
    return this.editCategoryForm.controls.categoryName;
  };

  effectRef = effect(() => {
    this.editCategoryForm.controls.categoryName.patchValue(this.productcategoryResponse()?.categoryName ?? '')
  })


  onSubmit() {

    const id = this.id();

    if (!this.editCategoryForm.valid || !id) {
      return;
    }

    const editformDate = this.editCategoryForm.getRawValue();

    const editCategoryRequestDto: editCategoryDto = {

      categoryName: editformDate.categoryName

    }

    this.productCategoryServices.editProductCategoryById(id, editCategoryRequestDto).subscribe({
      next: (response) => {

        console.log(response)

      },
      error: (error) => {
        console.error(error)
      }
    })

  }

  onDelete() {
    const id = this.id()
    if (!id) {
      return;
    }
    this.productCategoryServices.deleteProductCategory(id).subscribe({
      next: (response) => {
        console.log(response)
      },
      error: (error) => {
        console.error(error)
      }
    })
  }

}
