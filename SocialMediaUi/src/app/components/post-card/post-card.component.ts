import {Component, Input, OnInit} from '@angular/core';
import {CommentService} from "../../services/comment.service";
import {AuthService} from "../../services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {PostResponse} from "../../models/dtos/postResponse";
import {LikeService} from "../../services/like.service";
import {Like} from "../../models/like";
import {PostService} from "../../services/post.service";

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent implements OnInit {
  @Input() post: PostResponse = new PostResponse();
  liked: boolean = false;
  commentText: string = '';
  private jwtHelper = new JwtHelperService();

  constructor(private likeService: LikeService, private commentService: CommentService, private authService: AuthService, private postService: PostService) {
  }

  ngOnInit(): void {
    const user = this.getAliasFromToken();
    this.likeService.hasLikedByUser(user, this.post.guid).subscribe(
      x => this.liked = x
    );
  }

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
  onLike() {
    let like = new Like();
    like.userAlias = this.getAliasFromToken()
    like.postId = this.post.guid;
    this.post.likeCount++;

    this.likeService.createLike(like).subscribe(x => {
      console.log('Liked successfully')
    })
  }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }
}
