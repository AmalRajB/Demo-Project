import { Component, inject, input, ViewChild, ElementRef } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ProductImageServics } from '../services/product-image.servics';

@Component({
  selector: 'app-add-product.image',
  imports: [ReactiveFormsModule],
  templateUrl: './add-product.image.html',
  styleUrl: './add-product.image.css',
})
export class AddProductImage {

  id = input<string>();

  private productFileAddService = inject(ProductImageServics);
  @ViewChild('fileInput') fileInput!: ElementRef;


  fileform = new FormGroup({

    file: new FormControl<File | null | undefined>(null,
      { nonNullable: true }
    )
  })

  file: File | null = null;


  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length == 0) {
      return;
    }
    const file = input.files[0];
    this.fileform.patchValue({
      file: file
    });
  }

  onSubmit() {
    const formdata = this.fileform.getRawValue();
    if (!formdata.file) {
      console.error("File or productId is not selected");
      return;
    }
    const formData = new FormData();
    formData.append('file', formdata.file);

    const id = this.id();

    if (id) {
      this.productFileAddService.productImageUpload(id, formData).subscribe({
        next: (response) => {
          alert("image uploaded successfully ...")
          this.fileform.reset();
          // clear selected file
          this.file = null;
          this.fileInput.nativeElement.value = '';
        },
        error: (error) => {
          console.error(error);
        }
      })
    }

  }

}
