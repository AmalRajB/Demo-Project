import { HttpClient, httpResource, HttpResourceRef } from '@angular/common/http';
import { inject, Injectable, InputSignal, Signal } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { AddProductDto, EditProductDto, ProductDto } from '../models/product.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductServices {

  private http = inject(HttpClient)
  private baseUrl = environment.apiBaseUrl

  addproduct(product: AddProductDto): Observable<ProductDto> {
    return this.http.post<ProductDto>(`${this.baseUrl}/api/Product`, product)
  }

  getallproduct(): HttpResourceRef<ProductDto[] | undefined> {
    return httpResource<ProductDto[]>(() => `${this.baseUrl}/api/Product`);
  }

  getproductbyid(id: InputSignal<string | undefined>) {
    return httpResource<ProductDto>(() =>
      `${this.baseUrl}/api/Product/${id()}`
    );
  }

  editproduct(id: string, body: EditProductDto): Observable<ProductDto> {
    return this.http.put<ProductDto>(`${this.baseUrl}/api/Product/${id}`, body)
  }

  deleteproduct(id: string): Observable<ProductDto> {
    return this.http.delete<ProductDto>(`${this.baseUrl}/api/Product/${id}`)
  }

  getproductbyCategory(categoryId: Signal<string | null>) {
    return httpResource<ProductDto[]>(() => {

      if (!categoryId()) {
        return `${this.baseUrl}/api/Product`;
      }
      return `${this.baseUrl}/api/Product/category/${categoryId()}`;
    });
  }



}

