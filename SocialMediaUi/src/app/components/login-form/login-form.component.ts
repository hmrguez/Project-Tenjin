import { Component } from '@angular/core';
import {AccountService} from "../../services/account.service";
import {LoginUserDto} from "../../models/dtos/loginUserDto";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {

  userName : string = "";
  password: string = "";

  constructor(private accountService: AccountService, private authService: AuthService, private router: Router) { }

  onSubmit() {
    const userLoginDto = new LoginUserDto(this.userName, this.password);
    this.accountService.login(userLoginDto).subscribe( (response) => {
          if(response){
            this.router.navigate(['']);
          }
          else{
            this.password = '';
          }
      },
      (error) => {
        console.error('Error:', error);
      });
  }
}
