import { Component, computed, inject, signal, effect } from '@angular/core';
import { FileServices } from '../services/file.services';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-list-file-component',
  imports: [CommonModule, RouterLink],
  templateUrl: './list-file-component.html',
  styleUrl: './list-file-component.css',
})
export class ListFileComponent {
  pageNumber = signal(1);
  pageSize = 4;

  constructor() {
    effect(() => {
      console.log("API Response:", this.fileref.value());
    });
  }

  private fileservices = inject(FileServices)

  fileref = this.fileservices.getAllFiles(this.pageNumber, this.pageSize);

  files = computed(() => this.fileref.value()?.data ?? []);

 totalRecords = computed(() => this.fileref.value()?.totalRecord ?? 0);
  totalPages = computed(() =>
    Math.ceil(this.totalRecords() / this.pageSize)
  );


  isLoading = this.fileref.isLoading;

  nextPage() {
    if (this.pageNumber() < this.totalPages()) {
      this.pageNumber.update(p => p + 1)
    }
  }

  previousPage() {
    if (this.pageNumber() > 1) {
      this.pageNumber.update(p => p - 1)
    }
  }

}
