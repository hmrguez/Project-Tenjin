import { Component } from '@angular/core';
import {JwtHelperService} from "@auth0/angular-jwt";
import {UserService} from "../../services/user.service";
import {AuthService} from "../../services/auth.service";
import {PostService} from "../../services/post.service";
import {Post} from "../../models/post";

@Component({
  selector: 'app-create-post-form',
  templateUrl: './create-post-form.component.html',
  styleUrls: ['./create-post-form.component.css']
})
export class CreatePostFormComponent {
  private jwtHelper: JwtHelperService = new JwtHelperService();
  post: Post = new Post();

  constructor(private postService: PostService, private authService: AuthService) { }

  onSubmit(){
    this.post.userAlias = this.getAliasFromToken();
    this.post.id = "11111111-1111-1111-1111-111111111111"
    this.postService.createPost(this.post).subscribe((x) => {
      console.log("Posted successfully");
    })
  }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }
}
