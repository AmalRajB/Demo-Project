import { Component, effect, inject, signal } from '@angular/core';
import { EditProductImageServices } from './service/edit-product-image-services';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-product.image',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-product.image.html',
  styleUrl: './edit-product.image.css',
})
export class EditProductImage {

  private editProductImageService = inject(EditProductImageServices);
  private route = inject(ActivatedRoute);
  private router = inject(Router)

  showimagemodal = this.editProductImageService.showImagemodal.asReadonly();

  closeimagemodal() {
    const productId = this.editImageResponse()?.productId;
    this.editProductImageService.closeImagemodal();
    this.router.navigate([`admin/product-image/list/${productId}`]);
  }

  id = signal<string | undefined>(undefined);

  ngOnInit() {
    const routeId = this.route.snapshot.paramMap.get('id');
    this.id.set(routeId ?? undefined);
    this.editProductImageService.displayImagemodal();
  }
  editImageRef = this.editProductImageService.getProductImageById(this.id);

  isLoading = this.editImageRef.isLoading;
  editImageResponse = this.editImageRef.value;


  editImageform = new FormGroup({
    file: new FormControl<File | null | undefined>(null,
      { nonNullable: true }
    ),
    oldUrl: new FormControl<string>("", {
      nonNullable: true, validators: [Validators.required]
    }),

  })

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length == 0) {
      return;
    }
    const file = input.files[0];
    this.editImageform.patchValue({
      file: file
    });

  }



  onSubmit() {
    const id = this.id();

    if (!id) {
      console.error('Image ID is missing');
      return;
    }

    const file = this.editImageform.value.file;

    if (!file) {
      alert('Please select a file');
      return;
    }

    const formData = new FormData();
    formData.append('file', file);
    formData.append('productId', this.editImageResponse()?.productId!);

    this.editProductImageService
      .EditProductImageById(id, formData)
      .subscribe({
        next: (response) => {
          const productId = this.editImageResponse()?.productId;
          this.router.navigate([`admin/product-image/list/${productId}`]);
          console.log('Image updated successfully', response);

          // close modal
          this.closeimagemodal();

          // OPTIONAL: reset form
          this.editImageform.reset();
        },
        error: (err) => {
          console.error('Update failed', err);
        }
      });

  }

  constructor() {
    effect(() => {
      const data = this.editImageResponse();

      if (data) {
        this.editImageform.patchValue({
          oldUrl: data.fileUrl
        });
      }
    });
  }



}













