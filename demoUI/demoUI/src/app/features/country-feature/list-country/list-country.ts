import { Component, inject } from '@angular/core';
import { CountryServices } from '../services/country.services';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-list-country',
  imports: [RouterLink],
  templateUrl: './list-country.html',
  styleUrl: './list-country.css',
})
export class ListCountry {

  private countryservice = inject(CountryServices)

  getCountryref = this.countryservice.getcountrys();
  isLoading = this.getCountryref.isLoading;
  response = this.getCountryref.value;



}
