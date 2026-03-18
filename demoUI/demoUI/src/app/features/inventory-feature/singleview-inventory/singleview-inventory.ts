
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDetails, ProductImage } from '../models/inventory.model';
import { ProductImageService } from '../service/inventory-service';
@Component({
  selector: 'app-single-view',
  templateUrl: './singleview-inventory.html',
  styleUrls: ['./singleview-inventory.css']
})
export class SingleviewInventory implements OnInit {

  product: ProductDetails | null = null;
  selectedImage: ProductImage | null = null;
  isLoading: boolean = true;
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private productImageService: ProductImageService,
     private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    if (productId) {
      this.loadProductDetails(productId);
    }
  }

  loadProductDetails(productId: string): void {
    this.isLoading = true;
    this.productImageService.getProductDetailsById(productId).subscribe({
      next: (response) => {
        if (response.success) {
          this.product = response.data;
          if (this.product.productFiles && this.product.productFiles.length > 0) {
            this.selectedImage = this.product.productFiles[0];
          }
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
        this.cdr.detectChanges(); 
      },
      error: (err) => {
        this.errorMessage = 'Failed to load product details';
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  selectImage(image: ProductImage): void {
    this.selectedImage = image;
  }
}