import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FileServices } from '../services/file.services';
import { Router } from '@angular/router';
import { CountryServices } from '../../country-feature/services/country.services';
import { HomeServices } from '../../home-feature/services/home.services';
import { ChangeDetectorRef } from '@angular/core';


@Component({
  selector: 'app-add-file-component',
  imports: [ReactiveFormsModule],
  templateUrl: './add-file-component.html',
  styleUrl: './add-file-component.css',
})
export class AddFileComponent {

  private fileservices = inject(FileServices)
  private router = inject(Router)
  private countryservice = inject(CountryServices)
  private homeservice = inject(HomeServices)
  
  private cdr = inject(ChangeDetectorRef);

  countryref = this.countryservice.getcountrys();
  countryResponse = this.countryref.value;

  states: any[] = [];



  fileform = new FormGroup({
    countryId: new FormControl<string | null>(null, {
      nonNullable: true
    }),
    stateId: new FormControl<string | null>(null,
      { nonNullable: true }
    ),
    file: new FormControl<File | null | undefined>(null,
      { nonNullable: true }
    )
  })

  onCountryChange(event: Event) {
    const countryId = (event.target as HTMLSelectElement).value;

    this.homeservice.getstateBycountry(countryId).subscribe({
      next: (res) => {
        this.states = res;
        this.cdr.detectChanges();
      }
    });
  }


  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length == 0) {
      return;
    }
    const file = input.files[0];
    this.fileform.patchValue({
      file: file
    });
  }


  onSubmit() {
    const formdata = this.fileform.getRawValue();
    if (!formdata.file || !formdata.stateId) {
      console.error("File or state is not selected");
      return;
    }
    const formData = new FormData();
    formData.append('file', formdata.file);
    formData.append('stateId', formdata.stateId);

    this.fileservices.FileUpload(formData).subscribe({
      next: (response) => {
        this.router.navigate(['/admin/filelist']);
        console.log(response);
      },
      error: (error) => {
        console.error(error);
      }
    })
  }
}



//   this.fileservices.FileUpload(formData).subscribe({
//     next: (response) => {
//       console.log(response);
//       this.router.navigate(['/admin/filelist']);
//     },
//     error: (error) => {
//       console.error(error);
//     }
//   });

// }


