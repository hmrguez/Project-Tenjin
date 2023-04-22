import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {RegisterUserDto} from "../models/dtos/registerUserDto";
import {LoginUserDto} from "../models/dtos/loginUserDto";
import {catchError, map, Observable, of} from "rxjs";
import {AuthService} from "./auth.service";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private registerUrl = 'https://localhost:7086/api/account/register';
  private loginUrl = 'https://localhost:7086/api/account/login';

  constructor(private http: HttpClient, private authService : AuthService) { }

  login(loginUserDto: LoginUserDto) : Observable<boolean>{
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<{token: string}>(this.loginUrl, loginUserDto, {headers: headers}).pipe(
      map(response => {
        const token = response.token;

        const currentDate = new Date().getTime();
        const futureDate = currentDate + 5 * 60 * 60 * 1000;
        this.authService.setToken(token, futureDate);

        return true;
      }),
      catchError(() => of(false))
    );
  }

  register(registerUserDto: RegisterUserDto) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.registerUrl, registerUserDto, {headers: headers});
  }
}
