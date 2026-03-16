import { Component, inject, input } from '@angular/core';
import { ProductImageServics } from '../services/product-image.servics';
import { RouterLink } from "@angular/router";
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-list-product.image',
  imports: [RouterLink],
  templateUrl: './list-product.image.html',
  styleUrl: './list-product.image.css',
})
export class ListProductImage {

  id = input<string>();

  private productImageService = inject(ProductImageServics)
  private cdr = inject(ChangeDetectorRef);

  productImageRef = this.productImageService.getProductImageByProductId(this.id);

  isLoading = this.productImageRef.isLoading;

  productImageResponse = this.productImageRef.value;

  onDelete(id: string) {

    const image_id = id;

    this.productImageService.DeleteProductImageById(image_id).subscribe({
      next: (response) => {
        alert("are you sure you want to delete this image ? ")

        // update the image list after deleting an image 
        const images = this.productImageResponse();

        if (!images) return;

        const updated = images.filter(x => x.id !== id);

        this.productImageRef.value.set(updated);

      },
      error: (error) => {
        console.error(error)
      }
    })


  }

}
