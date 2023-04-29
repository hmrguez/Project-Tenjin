import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-follow-dashboard-element',
  templateUrl: './follow-dashboard-element.component.html',
  styleUrls: ['./follow-dashboard-element.component.css']
})
export class FollowDashboardElementComponent {
  @Input() profile: {profilePicture: string, username: string} = {profilePicture: "", username: ""}


}
