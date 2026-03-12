import { HttpClient, httpResource, HttpResourceRef } from '@angular/common/http';
import { inject, Injectable, InputSignal } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { AddCategoryDto, editCategoryDto, ProductCategoryDto } from '../models/productCategory.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductcategoryServices {

  private http = inject(HttpClient)
  private baseUrl = environment.apiBaseUrl;


  addProductCategory(data: AddCategoryDto): Observable<ProductCategoryDto> {
    return this.http.post<ProductCategoryDto>(`${this.baseUrl}/api/ProductCategory`, data)
  }

  getAllProductCategory(): HttpResourceRef<ProductCategoryDto[] | undefined> {
    return httpResource<ProductCategoryDto[]>(() => `${this.baseUrl}/api/ProductCategory`);
  }

  getProductCategoryById(id: InputSignal<string | undefined>) {
    return httpResource<ProductCategoryDto>(() => `${this.baseUrl}/api/ProductCategory/${id()}`)
  }

  editProductCategoryById(id: string, editcategoryRequestDto: editCategoryDto): Observable<ProductCategoryDto[]> {
    return this.http.put<ProductCategoryDto[]>(`${this.baseUrl}/api/ProductCategory/${id}`, editcategoryRequestDto)
  }

  deleteProductCategory(id: string): Observable<ProductCategoryDto> {
    return this.http.delete<ProductCategoryDto>(`${this.baseUrl}/api/ProductCategory/${id}`)
  }










}
