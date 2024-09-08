import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../model/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private url = "http://localhost:5105/connect/token"

  constructor(private http: HttpClient) { }

  public autenticar(email: string, senha: string): Observable<any> {

    let body = new URLSearchParams();
    body.set("username", email);
    body.set("password", senha);
    body.set("client_id", "client-angular-spotify-like");
    body.set("client_secret", "spotify-like-secret");
    body.set("grant_type", "password");
    body.set("scope", "spotify-like-scope");

    let options = {
      headers: new HttpHeaders().set("Content-Type", "application/x-www-form-urlencoded")
    }

    return this.http.post(`${this.url}`, body.toString(), options);
  }

}
