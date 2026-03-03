import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MetadataService {
  url = 'https://localhost:44353/api/metadata';
  
  constructor(private http: HttpClient) { }

  getPrioridades(){
    return this.http.get<string[]>(this.url + "/prioridades");
  }
}
