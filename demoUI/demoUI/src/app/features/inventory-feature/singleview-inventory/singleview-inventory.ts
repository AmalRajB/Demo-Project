import { Component, computed, inject, input } from '@angular/core';
import { InventoryService } from '../service/inventory-service';

@Component({
  selector: 'app-singleview-inventory',
  imports: [],
  templateUrl: './singleview-inventory.html',
  styleUrl: './singleview-inventory.css',
})
export class SingleviewInventory {

  id = input<string>();
  private inventoryservice = inject(InventoryService);

  inventoryRef = this.inventoryservice.getAllDetailsById(this.id);
  inventoryResponse = this.inventoryRef.value;


}
