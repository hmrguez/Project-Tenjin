import {Component, OnInit} from '@angular/core';
import {Post} from "./models/post";
import {PostService} from "./services/post.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'SocialMediaUi';
  posts: Post[] = [];

  constructor(private postService: PostService) { }

  ngOnInit(): void {
    this.postService.getPosts().subscribe((x) => {
      this.posts = x
      // console.log(this.posts);
    });
  }
}
