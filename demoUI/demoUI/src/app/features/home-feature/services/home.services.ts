import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class HomeServices {

  private http = inject(HttpClient);
  private baseUrl = environment.apiBaseUrl 

  getstateBycountry(countryId:string):Observable<any>{
    return this.http.get(`${this.baseUrl}/api/State/by-country/${countryId}`)
  }

  getFileBystate(stateId:string):Observable<any>{
    return this.http.get(`${this.baseUrl}/api/File/by-state/${stateId}`)
  }


}

