import { Component, inject } from '@angular/core';
import { StateServices } from '../services/state.services';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CountryServices } from '../../country-feature/services/country.services';
import { AddStateRequest } from '../models/state.model';

@Component({
  selector: 'app-add-state',
  imports: [ReactiveFormsModule],
  templateUrl: './add-state.html',
  styleUrl: './add-state.css',
})
export class AddState {
  private stateservices = inject(StateServices)
  private countryservices = inject(CountryServices)
  countryref = this.countryservices.getcountrys()
  countryResponse = this.countryref.value;

  addStateForm = new FormGroup({
    StateName: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
    }),
    country: new FormControl<string>('', {
      nonNullable: true
    })

  })


  onSubmit() {

    const formdata = this.addStateForm.getRawValue();
    const addstateRequestDto: AddStateRequest = {
      StateName: formdata.StateName,
      CountryId: formdata.country
    }
    this.stateservices.addCountry(addstateRequestDto).subscribe({
      next: (response) => {
        console.log(response)
      },
      error: (error) => {
        console.error(error)
      }
    })
  }

}
