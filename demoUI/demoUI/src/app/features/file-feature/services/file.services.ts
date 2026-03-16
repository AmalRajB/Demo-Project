import { HttpClient, httpResource, HttpResourceRef } from '@angular/common/http';
import { inject, Injectable, Input, InputSignal, Signal } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { EditFile, Fileinput, Fileoutput } from '../models/file.model';
import { PagedResponse } from '../../state-feature/models/state.model';

@Injectable({
  providedIn: 'root',
})
export class FileServices {

  private http = inject(HttpClient)
  private baseUrl = environment.apiBaseUrl

  // FileUpload(file: File): Observable<Fileinput> {
  //   const formData = new FormData();
  //   formData.append('File', file);
  //   return this.http.post<Fileinput>(`${this.baseUrl}/api/File/upload`, formData)
  // }

  FileUpload(formData: FormData){
    return this.http.post(`${this.baseUrl}/api/File/upload`,formData);
  }

  getAllFiles(pageNumber:Signal<number>, pageSize:number): HttpResourceRef<PagedResponse<Fileoutput> | undefined> {
    return httpResource<PagedResponse<Fileoutput>>(() => `${this.baseUrl}/api/File?pageNumber=${pageNumber()}&pageSize=${pageSize}`);
  }
  getFileByid(id: InputSignal<string | undefined>) {
    return httpResource<Fileoutput>(() => `${this.baseUrl}/api/File/${id()}`)
  }
  editFile(id: string, file: FormData): Observable<Fileinput> {
    
    return this.http.put<Fileinput>(`${this.baseUrl}/api/File/${id}`, file)
  }
  deleteFile(id:string):Observable<Fileinput>{
    return this.http.delete<Fileinput>(`${this.baseUrl}/api/File/${id}`)
  }






}
  //  getAllState(pageNumber: Signal<number>, pageSize: number): HttpResourceRef<PagedResponse<stateDto> | undefined> {
  //   return httpResource<PagedResponse<stateDto>>(() => `${this.baseUrl}/api/State?pageNumber=${pageNumber()}&pageSize=${pageSize}`);
  // }
