import { Component, inject, signal } from '@angular/core';
import { EditProductImageServices } from './service/edit-product-image-services';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-product.image',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-product.image.html',
  styleUrl: './edit-product.image.css',
})
export class EditProductImage {

  private editProductImageService = inject(EditProductImageServices);

  id = signal<string | undefined>(undefined);

  editImageRef = this.editProductImageService.getProductImageById(this.id());

  isLoading = this.editImageRef.isLoading;

  editImageResponse = this.editImageRef.value;


  

  







}
