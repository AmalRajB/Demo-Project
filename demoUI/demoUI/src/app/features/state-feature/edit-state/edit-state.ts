import { Component, effect, inject, input } from '@angular/core';
import { StateServices } from '../services/state.services';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CountryServices } from '../../country-feature/services/country.services';
import { Country, EditStateDto } from '../models/state.model';

@Component({
  selector: 'app-edit-state',
  imports: [ReactiveFormsModule], 
  templateUrl: './edit-state.html',
  styleUrl: './edit-state.css',
})
export class EditState {
  id = input<string>();
  private stateservice = inject(StateServices)
  private router = inject(Router)
  private countryserveice = inject(CountryServices)

  countryref = this.countryserveice.getcountrys();
  countryResponse = this.countryref.value;

  stateref = this.stateservice.getstatebyId(this.id)
  stateResponse = this.stateref.value;

  editStateForm = new FormGroup({
    StateName: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
    }),
    country: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required]
    })
  })
  
  effectref = effect(() => {
    const state = this.stateResponse(); 
    if (state) {
      this.editStateForm.patchValue({
        StateName: state.stateName,
        country: state.country.id
      });
    }

  })


  onSubmit() {
    const id = this.id();
    if (id && this.editStateForm.valid) {
      const formdata = this.editStateForm.getRawValue();
      const UpdatedstateDto: EditStateDto = {
        stateName: formdata.StateName,
        CountryId: formdata.country
      };
      this.stateservice.editstate(id, UpdatedstateDto).subscribe({
        next: () => {
          console.log("state details updated successfully...")
          this.router.navigate(['/admin/statelist'])

        },
        error: (error) => {
          console.error(error)

        }
      })

    }

  }

  onDeleteState() {
    const id = this.id();
    if (!id) {
      return
    }
    this.stateservice.deletestate(id).subscribe({
      next: (response) => {
        console.log(response)
        this.router.navigate(['/admin/statelist'])

      },
      error: (error) => {
        console.error(error)

      }})
  }
}
