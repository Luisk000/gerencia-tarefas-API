import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OAuthService {
  url = 'https://localhost:44353/api/OAuth';
  
  constructor(private http: HttpClient) { }

  getAcessToken(){
    return this.http.get<string>(this.url);
  }
}