import { Component, effect, inject, input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductServices } from '../services/product.services';
import { ProductcategoryServices } from '../../productcategory/services/productcategory.services';
import { EditProductDto } from '../models/product.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-product',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-product.html',
  styleUrl: './edit-product.css',
})
export class EditProduct {

  id = input<string>();


  private productservice = inject(ProductServices);
  private categoryservice = inject(ProductcategoryServices);
  private router = inject(Router)

  categoryref = this.categoryservice.getAllProductCategory();
  categoryResponse = this.categoryref.value;

  productref = this.productservice.getproductbyid(this.id);
  productresponse = this.productref.value;


  editform = new FormGroup({

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

  effectref = effect(() => {

    const product = this.productresponse();

    if (product) {
      this.editform.patchValue({
        productName: product.productName,
        productPrice: product.productPrice,
        categoryId: product.category.id
      })
    }

  })


  onSubmit() {

    const id = this.id()

    if (id && this.editform.valid) {
      const formvalue = this.editform.getRawValue();

      const EditproductRequestDto: EditProductDto = {

        productName: formvalue.productName,
        productPrice: formvalue.productPrice,
        categoryId: formvalue.categoryId

      }

      this.productservice.editproduct(id, EditproductRequestDto).subscribe({
        next: (response) => {
          console.log(response)
        },
        error: (error) => {
          console.error(error)
        }
      }
      )
    }
  }

  onDelete() {
    const id = this.id();

    if (!id) {
      return;
    }

    this.productservice.deleteproduct(id).subscribe({
      next: (response) => {
        this.router.navigate(['/admin/product/list']);
        console.log(response)
      },
      error: (error) => {
        console.error(error)
      }
    })
  }
}

