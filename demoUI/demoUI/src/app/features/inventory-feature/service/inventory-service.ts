
// --- services/product-image.service.ts ---
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { ApiResponse, ProductDetails } from '../models/inventory.model';

@Injectable({
  providedIn: 'root'
})
export class ProductImageService {

  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  // GET ALL: api/ProductImage/all-details
  getAllProductDetails(): Observable<ApiResponse<ProductDetails[]>> {
    return this.http.get<ApiResponse<ProductDetails[]>>(
      `${this.baseUrl}/api/ProductImage/all-details`
    );
  }

  // GET BY ID: api/ProductImage/all-details/{productId}
  getProductDetailsById(productId: string): Observable<ApiResponse<ProductDetails>> {
    return this.http.get<ApiResponse<ProductDetails>>(
      `${this.baseUrl}/api/ProductImage/all-details/${productId}`
    );
  }
}