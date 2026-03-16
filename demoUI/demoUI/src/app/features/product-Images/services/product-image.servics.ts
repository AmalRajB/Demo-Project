import { HttpClient, httpResource } from '@angular/common/http';
import { inject, Injectable, InputSignal } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { ProductFileinput, ProductFileoutput } from '../models/product-image.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductImageServics {

  private http = inject(HttpClient)
  private baseUrl = environment.apiBaseUrl


  productImageUpload(id:string, file:FormData) {
    return this.http.post(`${this.baseUrl}/api/ProductImage/by-product/${id}`, file);
  }

  getProductImageByid(id: InputSignal<string | undefined>) {
    return httpResource<ProductFileoutput>(() => `${this.baseUrl}/api/ProductImage/${id()}`)
  }

  

  DeleteProductImageById(id: string): Observable<ProductFileinput> {
    return this.http.delete<ProductFileinput>(`${this.baseUrl}/api/ProductImage/${id}`)
  }

  getProductImageByProductId(id: InputSignal<string | undefined>) {
    return httpResource<ProductFileoutput[]>(() => `${this.baseUrl}/api/ProductImage/by-product/${id()}`)
  }




}

