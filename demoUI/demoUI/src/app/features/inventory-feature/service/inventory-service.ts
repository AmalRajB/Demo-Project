import { HttpClient, httpResource, HttpResourceRef } from '@angular/common/http';
import { inject, Injectable, InputSignal } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { inventoryListModel } from '../models/inventory.model';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {

  private http = inject(HttpClient)
  private baseUrl = environment.apiBaseUrl 

  getAllProductData():HttpResourceRef<inventoryListModel[] | undefined>{
    return httpResource<inventoryListModel[]>(()=>`${this.baseUrl}/api/ProductImage/all-details`);
  }

  getAllDetailsById(id: InputSignal<string | undefined>){
    return httpResource<inventoryListModel>(()=>`${this.baseUrl}/api/ProductImage/by-id/${id}`);
  }

}
