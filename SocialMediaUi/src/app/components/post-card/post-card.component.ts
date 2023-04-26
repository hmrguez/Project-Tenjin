import {Component, Input} from '@angular/core';
import {Post} from "../../models/post";
import {User} from "../../models/user";

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent {
  @Input() post: Post = new Post();
  constructor() { }
}
