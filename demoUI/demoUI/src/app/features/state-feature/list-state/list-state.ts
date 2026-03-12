
import { Component, computed, effect, inject, signal } from '@angular/core';
import { StateServices } from '../services/state.services';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-list-state',
  imports: [CommonModule,RouterLink],
  templateUrl: './list-state.html',
  styleUrl: './list-state.css',
})
export class ListState {

  private stateservices = inject(StateServices);

  pageNumber = signal(1);
  pageSize = 4;


  stateref = this.stateservices.getAllState(this.pageNumber, this.pageSize);

  states = computed(() => this.stateref.value()?.data ?? []);

  totalRecords = computed(() => this.stateref.value()?.totalRecord ?? 0);

  totalPages = computed(() =>
    Math.ceil(this.totalRecords() / this.pageSize)
  );

  isLoading = this.stateref.isLoading;

  nextPage() {
    if (this.pageNumber() < this.totalPages()) {
      this.pageNumber.update(p => p + 1);
    }
  }

  previousPage() {
    if (this.pageNumber() > 1) {
      this.pageNumber.update(p => p - 1);
    }
  }

  constructor() {
    effect(() => {
      const response = this.stateref.value();
      console.log("API Response:", response);
    });
  }
}