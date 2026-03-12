export interface Country {
    id: string;
    countryName: string;
}

export interface AddStateRequest {
    StateName: string,
    CountryId: string
}

export interface stateDto {
    id: string,
    stateName: string,
    CountryId: string,
    country: Country;
}
export interface EditStateDto {
    stateName: string,
    CountryId: string,

}
export interface PagedResponse<T>{
  totalRecord:number
  pageNumber:number
  pageSize:number
  data:T[]
}