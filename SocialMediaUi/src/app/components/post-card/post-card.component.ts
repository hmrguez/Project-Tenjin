import {Component, Input} from '@angular/core';
import {Post} from "../../models/post";
import {User} from "../../models/user";
import {Comment} from "../../models/comment";
import {CommentService} from "../../services/comment.service";
import {AuthService} from "../../services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {PostResponse} from "../../models/dtos/postResponse";

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent {
  @Input() post: PostResponse = new PostResponse();
  private jwtHelper = new JwtHelperService();
  commentText: string = '';
  constructor(private commentService: CommentService, private authService: AuthService) { }

  onSubmit() {
    const newComment = {
      postId: this.post.guid,
      commentText: this.commentText,
      userAlias: this.getAliasFromToken()
    };

    console.log("post: " + newComment.postId)
    console.log("comment: " + newComment.commentText)
    console.log("user: " + newComment.userAlias)


    this.commentService.createComment(newComment).subscribe((result) => {
      console.log('Comment posted successfully: ', result);
    });
  }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }
}
