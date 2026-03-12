import { Component, effect, inject, input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CountryServices } from '../services/country.services';
import { Editcountryrequest } from '../models/addcountry.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-country',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-country.html',
  styleUrl: './edit-country.css',
})
export class EditCountry {
  id = input<string>();
  private countryservice = inject(CountryServices)
  private router = inject(Router)

  countryserviceref = this.countryservice.getcountryByid(this.id)
  countryResponse = this.countryserviceref.value;

  editCountryGroup = new FormGroup({
    countryName: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
    })
  })

  get countryNameFormControl() {
    return this.editCountryGroup.controls.countryName
  };

  effectref = effect(() => {
    this.editCountryGroup.controls.countryName.patchValue(this.countryResponse()?.countryName ?? '')
  })

  onSubmit() {
    const id = this.id();
    if (!this.editCountryGroup.valid || !id) {
      return;
    }

    const formvalues = this.editCountryGroup.getRawValue();

    const editcountryRequestDto: Editcountryrequest = {
      countryName: formvalues.countryName
    }
    this.countryservice.editcountry(id, editcountryRequestDto).subscribe({
      next:(response)=>{
        console.log(response)
        this.router.navigate(['/admin/listcountry'])

      },error:(error)=>{
        console.error(error)
      }
    })


  }

  onDelete() {
    const id = this.id();
    if(!id){
      return;
    }
    this.countryservice.deletecountry(id).subscribe({
      next:()=>{
        console.log("the country deleted ...")
        this.router.navigate(['/admin/listcountry'])
      },
      error:(error)=>{
        console.error(error)
      }
    })
  }

}
