import { Component } from '@angular/core';
import {RegisterUserDto} from "../../models/dtos/registerUserDto";
import {AccountService} from "../../services/account.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent {
  username: string = "";
  email: string = "";
  password: string = "";
  confirmPassword: string = "";
  fullName: string = "";

  constructor(private accountService: AccountService, private router: Router) {}
  onSubmit() {
    const registerUserDto = new RegisterUserDto(this.username, this.fullName, this.email, this.password);
    this.accountService.register(registerUserDto).subscribe(
      (response) => {
        console.log('User registered successfully:', response);
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('Error registering user:', error);
      }
    );
  }
}
