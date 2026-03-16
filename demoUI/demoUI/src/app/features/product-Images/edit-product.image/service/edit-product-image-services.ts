import { HttpClient, httpResource } from '@angular/common/http';
import { inject, Injectable, InputSignal, signal } from '@angular/core';
import { environment } from '../../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { ProductFileinput, ProductFileoutput } from '../../models/product-image.model';

@Injectable({
  providedIn: 'root',
})
export class EditProductImageServices {
  private http = inject(HttpClient)
  private baseUrl = environment.apiBaseUrl
  showImagemodal = signal<boolean>(false);


  displayImagemodal() {
    this.showImagemodal.set(true)
  }

  closeImagemodal() {
    this.showImagemodal.set(false)
  }

  EditProductImageById(id: string, file: FormData): Observable<ProductFileinput> {
    return this.http.put<ProductFileinput>(`${this.baseUrl}/api/ProductImage/${id}`, file)
  }


  getProductImageById(id: string | undefined) {

    return httpResource<ProductFileoutput>(() => {

      if (!id) return undefined;

      return `${this.baseUrl}/api/ProductImage/${id}`;

    });

  }
}
