import { Component, inject } from '@angular/core';
import { CountryServices } from '../../country-feature/services/country.services';
import { HomeServices } from '../services/home.services';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-home-component',
  imports: [ReactiveFormsModule],
  templateUrl: './home-component.html',
  styleUrl: './home-component.css',
})
export class HomeComponent {

  private countryservice = inject(CountryServices);
  private homeservice = inject(HomeServices);
  private cdr = inject(ChangeDetectorRef);

  countryref = this.countryservice.getcountrys();
  countryResponse = this.countryref.value;


  states: any[] = [];
  files: any[] = [];

  filterform = new FormGroup({
    countryId: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required]
    }),
    stateId: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required]
    })
  });


  onCountryChange() {
    const countryId = this.filterform.value.countryId;

    this.states = [];
    this.files = [];

    this.homeservice.getstateBycountry(countryId!).subscribe(res => {
      this.states = res;
      this.cdr.detectChanges();
    });

  }

  onStateChange(event: Event) {

    const stateId = (event.target as HTMLSelectElement).value;

    this.homeservice.getFileBystate(stateId!).subscribe(res => {

      this.files = res;
      this.cdr.detectChanges();
    })

  }

}


