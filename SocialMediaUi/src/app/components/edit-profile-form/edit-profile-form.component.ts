import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {AuthService} from "../../services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {User} from "../../models/user";

@Component({
  selector: 'app-edit-profile-form',
  templateUrl: './edit-profile-form.component.html',
  styleUrls: ['./edit-profile-form.component.css']
})
export class EditProfileFormComponent implements OnInit{
  private jwtHelper: JwtHelperService = new JwtHelperService();
  public user: User = new User();

  onSubmit() {
    this.userService.updateUser(this.user).subscribe(() => {
      console.log('User updated successfully');
    }, error => {
      console.error('Error updating user:', error);
    });
  }

  constructor(private userService: UserService, private authService: AuthService) { }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }

  ngOnInit(): void {
    const alias = this.getAliasFromToken();
    this.userService.getUserByAlias(alias).subscribe( (x) => this.user = x);
  }
}
