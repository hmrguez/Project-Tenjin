import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-profile-card',
  templateUrl: './profile-card.component.html',
  styleUrls: ['./profile-card.component.css']
})
export class ProfileCardComponent implements OnInit{
  @Input() profilePictureUrl: string = "";
  @Input() username: string = "";

  profileRoute: string = ''
  constructor() { }

  ngOnInit(): void {
    this.profileRoute = '/profile/' + this.username;
  }
}
