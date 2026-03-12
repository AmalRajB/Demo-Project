import { Component, effect, inject, input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FileServices } from '../services/file.services';
import { Router } from '@angular/router';
import { CountryServices } from '../../country-feature/services/country.services';
import { StateServices } from '../../state-feature/services/state.services';
import { HomeServices } from '../../home-feature/services/home.services';
import { ChangeDetectorRef } from '@angular/core';


@Component({
  selector: 'app-edit-file-component',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-file-component.html',
  styleUrl: './edit-file-component.css',
})
export class EditFileComponent {
  id = input<string>();

  private fileservices = inject(FileServices);
  private countryservice = inject(CountryServices)
  private router = inject(Router)
  private stateservice = inject(StateServices)
  private homeservice = inject(HomeServices)
  private cdr = inject(ChangeDetectorRef);

  states: any[] = [];

  countryref = this.countryservice.getcountrys();
  countryResponse = this.countryref.value;

  fileref = this.fileservices.getFileByid(this.id);
  fileResponse = this.fileref.value;


  editfileForm = new FormGroup({
    countryId: new FormControl<string | null>(null,
      {
        nonNullable: true
      }
    ),
    stateId: new FormControl<string | null>(null,
      {
        nonNullable: true
      }
    ),

    file: new FormControl<File | null | undefined>(null,
      { nonNullable: true }
    )


  })

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length == 0) {
      return;
    }
    const file = input.files[0];
    this.editfileForm.patchValue({
      file: file
    });

  }


  effectref = effect(() => {
    const filedata = this.fileResponse();
    if (!filedata) return;
    const stateId = filedata.stateId

    this.stateservice.getstatebyStringId(stateId).subscribe({
      next: (state) => {
        const countryId = state.country.id;

        this.editfileForm.patchValue({
          countryId: countryId,
          stateId: stateId

        });

        this.loadState(countryId);
      }
    });

  });

  loadState(countryId: string) {
    this.homeservice.getstateBycountry(countryId).subscribe({
      next: (response) => {

        this.states = response;
        this.cdr.detectChanges();

      }
    })
  }

  onCountryChange(event: Event) {

    const countryId = (event.target as HTMLSelectElement).value;

    this.editfileForm.patchValue({
      stateId: ''
    });

    this.loadState(countryId);
    this.cdr.detectChanges();

  }


  onSubmit() {

    const id = this.id();
    const formdata = this.editfileForm.getRawValue();

    if (!id) return;

    const formData = new FormData();

    formData.append('stateId', formdata.stateId!);
    if (formdata.file) {
      formData.append('file', formdata.file);
    }

    this.fileservices.editFile(id, formData).subscribe({
      next: () => {
        console.log("File updated successfully...");
        this.router.navigate(['/admin/filelist']);
      },
      error: (error) => {
        console.error(error);
      }
    });

  }

  onDelete() {
    const id = this.id();
    if (!id) {
      return
    }
    this.fileservices.deleteFile(id).subscribe({
      next: (response) => {
        this.router.navigate(['/admin/filelist']);
        console.log(response)
      },
      error: (error) => {
        console.error(error)
      }
    })
  }

}
