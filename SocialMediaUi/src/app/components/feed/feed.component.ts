import {Component, OnInit} from '@angular/core';
import {PostService} from "../../services/post.service";
import {PostResponse} from "../../models/dtos/postResponse";
import {AuthService} from "../../services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {FollowService} from "../../services/follow.service";
import {Follow} from "../../models/follow";
import {Post} from "../../models/post";

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit{
  posts : [PostResponse, number][] = []
  private jwtHelper = new JwtHelperService();
  private followList: Follow[] = []

  constructor(private postService : PostService, private authService: AuthService, private followService: FollowService) {
  }

  ngOnInit(): void {
    this.getPosts()
  }

  getPosts(): void{
    this.postService.getPosts()
      .subscribe(posts => {
        this.followService.getFollows()
          .subscribe(follows => {
            this.followList = follows;
            this.posts = posts.map(x => [x, 0])
            this.orderPosts()
          })
      })
  }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }

  private follows(userA: string, userB: string): boolean{
    return this.followList.some(x => x.followerAlias === userA && x.followedAlias == userB);
  }

  orderPosts(): void{
    const currentUserAlias = this.getAliasFromToken();
    this.posts.forEach(postTuple => {
      const post = postTuple[0];
      let score = 0;
      if(this.follows(currentUserAlias, post.userAlias)) {
        score += 2;
      }
      score += post.likeCount * 0.05;
      score += 0.1;
      postTuple[1] = score;
    });
    // Sort the posts array based on the score in descending order
    this.posts.sort((a, b) => b[1] - a[1]);
  }
}
