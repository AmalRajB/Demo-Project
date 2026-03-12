import { Component, inject } from '@angular/core';
import { ProductcategoryServices } from '../../productcategory/services/productcategory.services';
import { ProductServices } from '../services/product.services';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddProductDto } from '../models/product.model';

@Component({
  selector: 'app-add-product',
  imports: [ReactiveFormsModule],
  templateUrl: './add-product.html',
  styleUrl: './add-product.css',
})
export class AddProduct {

  private categoryservice = inject(ProductcategoryServices);
  private productservice = inject(ProductServices);

  categoryref = this.categoryservice.getAllProductCategory();
  isLoading = this.categoryref.isLoading;
  categoryRseponse = this.categoryref.value;

  productform = new FormGroup({
    productName: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
    }),
    productPrice: new FormControl<number>(0, {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(20)]
    }),
    categoryId: new FormControl<string>('', {
      nonNullable: true
    })
  })

  onSubmit() {
    const formdata = this.productform.getRawValue();

    const AddProductRequestDto: AddProductDto = {
      productName: formdata.productName,
      productPrice: formdata.productPrice,
      categoryId: formdata.categoryId
    };

    this.productservice.addproduct(AddProductRequestDto).subscribe({
      next: (response) => {
        console.log(response)
      },
      error: (error) => {
        console.error(error)
      }
    })


  }


}
