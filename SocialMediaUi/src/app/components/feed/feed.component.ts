import {Component, OnInit} from '@angular/core';
import {PostService} from "../../services/post.service";
import {PostResponse} from "../../models/dtos/postResponse";

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit{
  posts : PostResponse[] = []

  constructor(private postService : PostService) {
  }

  ngOnInit(): void {
    this.getPosts()
  }

  getPosts(): void{
    this.postService.getPosts()
      .subscribe(posts => {
        this.posts = posts
        console.log(this.posts.map(x=>x.guid))
      })
  }
}
