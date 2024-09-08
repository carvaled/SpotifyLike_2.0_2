
import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { Usuario } from './model/usuario';
import { UsuarioService } from './services/usuario.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MatToolbarModule, MatIconModule, MatButtonModule, MatGridListModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  userName: string | null = null;


  constructor(private usuarioService: UsuarioService, private router: Router) {
    this.userName = this.getUserName();

  }

  getUserName(): string {
    const sessionItem = sessionStorage.getItem("user_session");

    if (!sessionItem) {
      return "";
    }

    let token: any;
    try {
      token = JSON.parse(sessionItem);
    } catch (e) {
      console.error("Erro ao analisar o token JSON", e);
      return "";
    }
    return token && token.name ? token.name : "";
  }

  ngOnInit(): void {
    if (sessionStorage.getItem("user_session")) {
      let token = JSON.parse(sessionStorage.getItem("user_session") as string);
      this.userName = token.name;
    }
  }

  logout(): void {
    sessionStorage.clear();
    this.router.navigate(["/login"]);
  }
}
