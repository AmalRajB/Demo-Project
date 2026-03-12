import { HttpClient, httpResource, HttpResourceRef } from '@angular/common/http';
import { inject, Injectable, InputSignal, Signal } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { AddStateRequest, EditStateDto, PagedResponse, stateDto } from '../models/state.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StateServices {
  private http = inject(HttpClient)
  private baseUrl = environment.apiBaseUrl 

  addCountry(state: AddStateRequest): Observable<stateDto> {
    return this.http.post<stateDto>(`${this.baseUrl}/api/State`, state)

  }
  getstatebyId(id: InputSignal<string | undefined>) {
    return httpResource<stateDto>(() => `${this.baseUrl}/api/State/${id()}`)
  }

  getAllState(pageNumber: Signal<number>, pageSize: number): HttpResourceRef<PagedResponse<stateDto> | undefined> {
    return httpResource<PagedResponse<stateDto>>(() => `${this.baseUrl}/api/State?pageNumber=${pageNumber()}&pageSize=${pageSize}`);
  }
  editstate(id: string, body: EditStateDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/api/State/${id}`, body)
  }
  deletestate(id: string): Observable<stateDto> {
    return this.http.delete<stateDto>(`${this.baseUrl}/api/State/${id}`)
  }
  getstatebyStringId(id: string) {
    return this.http.get<stateDto>(`${this.baseUrl}/api/State/${id}`)
  }



  // getAllState(): HttpResourceRef<stateDto[] | undefined> {
  //     return httpResource<stateDto[]>(() => `${this.baseUrl}/api/State`);
  //   }


}
