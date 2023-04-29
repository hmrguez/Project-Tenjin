import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  private jwtHelper = new JwtHelperService();
  profile: string = '';
  isLoggedIn: boolean = false;

  constructor(private authService: AuthService) {

  }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn()
    const alias = this.getAliasFromToken();
    this.profile = `/profile/${alias}`;
    console.log(this.profile)
  }
}
