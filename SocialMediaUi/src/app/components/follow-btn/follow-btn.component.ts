import {Component, Input, OnInit} from '@angular/core';
import {User} from "../../models/user";
import {FollowService} from "../../services/follow.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {AuthService} from "../../services/auth.service";
import {Follow} from "../../models/follow";

@Component({
  selector: 'app-follow-btn',
  templateUrl: './follow-btn.component.html',
  styleUrls: ['./follow-btn.component.css']
})
export class FollowBtnComponent implements OnInit{
  @Input() profileAlias: string = ""
  private jwtHelper: JwtHelperService = new JwtHelperService();
  public alias: string = ""
  following: boolean = false;

  constructor(private followService: FollowService, private authService: AuthService) {
  }
  ngOnInit(): void {
    this.alias = this.getAliasFromToken();
    this.checkFollowing()
  }

  onFollow(): void{
    const newFollow = new Follow();
    newFollow.followedAlias = this.profileAlias;
    newFollow.followerAlias = this.alias;
    newFollow.id = "11111111-1111-1111-1111-111111111111";
    this.followService.createFollow(newFollow).subscribe(x => {
      this.following = true;
    })
  }

  checkFollowing(): void {
    this.followService.getFollows()
      .subscribe(follows => {
        this.following = follows.some(follow =>
          follow.followerAlias === this.alias && follow.followedAlias === this.profileAlias
        );
      });
  }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }
}
