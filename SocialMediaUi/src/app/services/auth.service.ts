import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private tokenKey = 'auth_token';
  private expirationKey = 'auth_token_expiration';

  constructor() {}

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (token) {
      if (this.isTokenExpired()) {
        this.logout();
        return false;
      } else {
        return true;
      }
    } else {
      return false;
    }
  }

  setToken(token: string, expirationDate: number) {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.expirationKey, expirationDate.toString());
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isTokenExpired(): boolean {
    const expirationDate = localStorage.getItem(this.expirationKey);
    if (!expirationDate) {
      return true;
    }
    const expirationTime = Number(expirationDate);
    return new Date().getTime() > expirationTime;
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.expirationKey);
  }
}
