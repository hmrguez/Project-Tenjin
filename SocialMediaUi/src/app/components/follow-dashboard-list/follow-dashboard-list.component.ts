import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {FollowService} from "../../services/follow.service";

@Component({
  selector: 'app-follow-dashboard-list',
  templateUrl: './follow-dashboard-list.component.html',
  styleUrls: ['./follow-dashboard-list.component.css']
})
export class FollowDashboardListComponent implements OnInit{

  private jwtHelper = new JwtHelperService();
  profiles: { profilePicture: string, username: string }[] = [
    { profilePicture: 'profile1.jpg', username: 'John Doe' },
    { profilePicture: 'profile2.jpg', username: 'Jane Smith' },
    { profilePicture: 'profile3.jpg', username: 'Bob Johnson' },
  ];

  constructor(private authService: AuthService, private followService: FollowService) {

  }



  private getAliasFromToken(): string {
    const token = this.authService.getToken() as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
  }

  ngOnInit(): void {
    const followerAlias = this.getAliasFromToken();
    this.followService.getFollows()
      .subscribe(follows => {
        this.profiles = follows.filter(follow => follow.followerAlias === followerAlias).map(x => ({
          profilePicture: 'some_path/' + x.followedAlias + '.jpg',
          username: x.followedAlias
        }));
      });
  }
}
