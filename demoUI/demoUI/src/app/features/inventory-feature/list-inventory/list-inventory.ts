

// --- list-component/product-list.component.ts ---
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ProductDetails } from '../models/inventory.model';
import { ProductImageService } from '../service/inventory-service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule,RouterLink],
  selector: 'app-product-list',
  templateUrl: './list-inventory.html',
  styleUrl: './list-inventory.css',
})
export class ProductListComponent implements OnInit {

  products: ProductDetails[] = [];
  isLoading: boolean = true;
  errorMessage: string = '';

  constructor(
    private productImageService: ProductImageService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading = true;
    this.productImageService.getAllProductDetails().subscribe({
      next: (response) => {
        // console.log('API RESPONSE:', response);
        // console.log('SUCCESS:', response.success);
        // console.log('DATA:', response.data);
        // this.isLoading = false;
        if (response.success) {
          this.products = response.data;
        } else {
          this.errorMessage = response.message;
        }
        this.isLoading = false;
        this.cdr.detectChanges(); 
      },
      error: (err) => {
        this.errorMessage = 'Failed to load products';
        this.isLoading = false;
        console.error(err);
      }
    });
  }

  // Get the first image URL for the product card thumbnail
  getThumbnail(product: ProductDetails): string {
    if (product.productFiles && product.productFiles.length > 0) {
      return product.productFiles[0].fileUrl;
    }
    return 'assets/placeholder.png'; // fallback image
  }

  // Navigate to single view details
  viewDetails(productId: string): void {
    this.router.navigate(['/product-details', productId]);
  }
}