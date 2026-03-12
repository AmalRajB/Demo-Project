import { HttpClient, httpResource, HttpResourceRef } from '@angular/common/http';
import { inject, Injectable, InputSignal, signal } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { AddcountryRequest, countryDto, Editcountryrequest } from '../models/addcountry.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CountryServices {

  private http = inject(HttpClient)
  private baseUrl = environment.apiBaseUrl


  addCountry(country: AddcountryRequest): Observable<countryDto> {
    return this.http.post<countryDto>(`${this.baseUrl}/api/Country`, country)

  }

  getcountrys(): HttpResourceRef<countryDto[] | undefined> {
    return httpResource<countryDto[]>(() => `${this.baseUrl}/api/Country`);
  }

  getcountryByid(id: InputSignal<string | undefined>) {
    
    return httpResource<countryDto>(() => `${this.baseUrl}/api/Country/${id()}`)
  }

  editcountry(id:string ,editcountryRequestDto : Editcountryrequest): Observable<countryDto[]>{
    return this.http.put<countryDto[]>(`${this.baseUrl}/api/Country/${id}`, editcountryRequestDto)

  }

  deletecountry(id:string):Observable<countryDto>{
    return this.http.delete<countryDto>(`${this.baseUrl}/api/Country/${id}`)
  }

}
