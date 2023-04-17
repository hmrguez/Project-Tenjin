import { Component } from '@angular/core';
import {PostService} from "../../services/post.service";
import {Post} from "../../models/post";
import {User} from "../../models/user";

@Component({
  selector: 'app-temp-button',
  templateUrl: './temp-button.component.html',
  styleUrls: ['./temp-button.component.css']
})
export class TempButtonComponent {
  constructor(private postService: PostService) { }
  onCreatePost() {

  }
}
