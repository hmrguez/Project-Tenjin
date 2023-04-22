import {Component, OnInit} from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import {UserService} from "../../services/user.service";
import {User} from "../../models/user";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit{

  private jwtHelper: JwtHelperService = new JwtHelperService();
  public user: User = new User()

  constructor(private userService: UserService, private authService: AuthService) { }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }

  ngOnInit(): void {
    const alias = this.getAliasFromToken()
    this.userService.getUserByAlias(alias).subscribe((x) => {
      console.log("User: " + x)
      this.user = x;
    })
  }
}
