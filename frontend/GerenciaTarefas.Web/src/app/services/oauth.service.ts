import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OAuthService {
  url = 'https://localhost:44353/api/OAuth';
  
  constructor(private http: HttpClient) { }

  getAcessToken(){
    return this.http.get<string>(this.url).pipe(
      map(token => {
        if (!token)
          throw new Error("Token está vazio")

        return token;
      })
    );
  }
}