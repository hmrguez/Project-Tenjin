import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterFormComponent } from './components/register-form/register-form.component';
import { FormsModule } from "@angular/forms";
import { LoginFormComponent } from './components/login-form/login-form.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProfileComponent } from './components/profile/profile.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { EditProfileFormComponent } from './components/edit-profile-form/edit-profile-form.component';
import { CreatePostFormComponent } from './components/create-post-form/create-post-form.component';
import { PostCardComponent } from './components/post-card/post-card.component';
import { FeedComponent } from './components/feed/feed.component';
import { FollowBtnComponent } from './components/follow-btn/follow-btn.component';
import { ProfileCardComponent } from './components/profile-card/profile-card.component';
import { ProfileCarouselComponent } from './components/profile-carousel/profile-carousel.component';
import { FollowDashboardListComponent } from './components/follow-dashboard-list/follow-dashboard-list.component';
import { FollowDashboardElementComponent } from './components/follow-dashboard-element/follow-dashboard-element.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterFormComponent,
    LoginFormComponent,
    DashboardComponent,
    ProfileComponent,
    NavbarComponent,
    EditProfileFormComponent,
    CreatePostFormComponent,
    PostCardComponent,
    FeedComponent,
    FollowBtnComponent,
    ProfileCardComponent,
    ProfileCarouselComponent,
    FollowDashboardListComponent,
    FollowDashboardElementComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
