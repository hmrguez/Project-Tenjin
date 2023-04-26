import {Component, OnInit} from '@angular/core';
import {PostService} from "../../services/post.service";
import {Post} from "../../models/post";

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit{
  posts : Post[] = []

  constructor(private postService : PostService) {
  }

  ngOnInit(): void {
    this.getPosts()
  }

  getPosts(): void{
    this.postService.getPosts()
      .subscribe(posts => this.posts = posts)
  }
}
