import { Component, inject } from '@angular/core';
import { InventoryService } from '../service/inventory-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-list-inventory',
  imports: [RouterLink],
  templateUrl: './list-inventory.html',
  styleUrl: './list-inventory.css',
})
export class ListInventory {

  private inventoryservice = inject(InventoryService);

  inventoryRef = this.inventoryservice.getAllProductData();
  isLoading = this.inventoryRef.isLoading;
  inventoryResponse = this.inventoryRef.value;


}
