import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginFormComponent} from "./components/login-form/login-form.component";
import {RegisterFormComponent} from "./components/register-form/register-form.component";
import {AuthGuardService} from "./services/auth-guard.service";
import {DashboardComponent} from "./components/dashboard/dashboard.component";

const routes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [AuthGuardService] },
  { path: 'login', component: LoginFormComponent },
  { path: 'register', component: RegisterFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
