import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { emitters } from '../emitters/emitters';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  message = 'Você esta logado';


  authenticated = false

  constructor(
    private http:HttpClient,
    private router:Router
  ) { }

  ngOnInit(): void {
    this.http.get("http://localhost:5000/user", {withCredentials:true}).subscribe(
      (res:any) => {
        this.message = `Bem vindo ${res.name}!`;
        emitters.authEmitter.emit(true)
      },
      err => {
        this.message = `Você precisa estar logado pra acessar aqui...`
        emitters.authEmitter.emit(false)
      }
    )

    emitters.authEmitter.subscribe(
      (auth:boolean) => {
        this.authenticated = auth;
      }
    )
  }
  logout():void {
    this.http.post('http://localhost:5000/logout', {}, {withCredentials:true})
    .subscribe(
      () => {
        this.authenticated = false
        this.router.navigate(['/login'])
      }
    )
  }

    

}
