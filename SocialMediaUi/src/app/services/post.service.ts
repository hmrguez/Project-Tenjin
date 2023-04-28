import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { Observable } from "rxjs";
import { Post } from "../models/post";
import {PostResponse} from "../models/dtos/postResponse";

@Injectable({
  providedIn: 'root'
})

export class PostService {
  private postUrl = "https://localhost:7086/api/post"
  constructor(private httpClient : HttpClient) { }

  public getPosts() : Observable<PostResponse[]>{
    return this.httpClient.get<PostResponse[]>(this.postUrl);
  }

  public getSinglePost(id: string) : Observable<Post>{
    return this.httpClient.get<Post>(`${this.postUrl}/${id}`);
  }

  public createPost(post: any): Observable<Post[]> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.post<Post[]>(this.postUrl, post, {headers: headers});
  }

  public updatePost(post: any): Observable<Post[]> {
    return this.httpClient.put<Post[]>(this.postUrl, post);
  }

  public deletePost(id: string): Observable<Post[]> {
    return this.httpClient.delete<Post[]>(`${this.postUrl}/${id}`);
  }
}
