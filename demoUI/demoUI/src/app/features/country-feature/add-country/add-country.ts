import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddcountryRequest } from '../models/addcountry.model';
import { CountryServices } from '../services/country.services';
import { ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-country',
  imports: [ReactiveFormsModule],
  templateUrl: './add-country.html',
  styleUrl: './add-country.css',
})
export class AddCountry {

  private countryservice = inject(CountryServices)
  private cdr = inject(ChangeDetectorRef)
  private router = inject(Router)
  errorMessage?: string;
  addCountryGroup = new FormGroup({
    countryName: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
    })
  })

  get countryNameFormControl() {
    return this.addCountryGroup.controls.countryName
  };

  onSubmit() {
    this.errorMessage = undefined

    const addCountryData = this.addCountryGroup.getRawValue();

    const addcountryrequestDto: AddcountryRequest = {
      countryName: addCountryData.countryName
    }
    this.countryservice.addCountry(addcountryrequestDto).subscribe({
      next: (response) => {
        console.log(response)
        this.router.navigate(['/admin/listcountry'])
      },
      error: (error) => {
        if (error.status === 400) {
          this.errorMessage = error.error.message;
          this.cdr.detectChanges();

        }
      }
    })

  }

}
