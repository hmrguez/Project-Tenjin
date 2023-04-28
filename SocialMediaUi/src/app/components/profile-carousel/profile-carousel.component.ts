import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";

@Component({
  selector: 'app-profile-carousel',
  templateUrl: './profile-carousel.component.html',
  styleUrls: ['./profile-carousel.component.css']
})
export class ProfileCarouselComponent implements OnInit{
  profiles : {profilePictureUrl: string, username: string}[] = []

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((x) => {
      this.profiles = x.map(user => ({
        profilePictureUrl: user.profilePicture,
        username: user.alias
      }));
    })
  }
}
