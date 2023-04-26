import {Component, OnInit} from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import {UserService} from "../../services/user.service";
import {User} from "../../models/user";
import {AuthService} from "../../services/auth.service";
import {ActivatedRoute} from "@angular/router";
import {Follow} from "../../models/follow";
import {FollowService} from "../../services/follow.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit{

  private jwtHelper: JwtHelperService = new JwtHelperService();
  public user: User = new User()
  public alias: string = ""
  public profileAlias: string = ""
  constructor(private route: ActivatedRoute, private userService: UserService, private authService: AuthService, private followService: FollowService) { }

  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.profileAlias = params['alias']
    })
    this.userService.getUserByAlias(this.alias).subscribe((x) => {
      this.user = x;
      console.log(`User: ${x}`)
    })
    this.alias = this.getAliasFromToken();
  }

  onFollow(): void{
    const newFollow = new Follow();
    newFollow.followedAlias = this.profileAlias;
    newFollow.followerAlias = this.alias;
    newFollow.id = "11111111-1111-1111-1111-111111111111";
    this.followService.createFollow(newFollow).subscribe(x => {
      console.log(x)
    })
  }
}
