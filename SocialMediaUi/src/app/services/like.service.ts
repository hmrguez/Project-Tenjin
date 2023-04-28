import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Like} from "../models/like";
import {map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LikeService {

  private likeUrl = 'https://localhost:7086/api/like';

  constructor(private http: HttpClient) { }

  public getLikes(): Observable<Like[]> {
    return this.http.get<Like[]>(this.likeUrl);
  }

  public getLikeById(id: string): Observable<Like> {
    const url = `${this.likeUrl}/${id}`;
    return this.http.get<Like>(url);
  }

  public createLike(like: any): Observable<Like> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Like>(this.likeUrl, like, { headers });
  }

  public deleteLike(id: string): Observable<any> {
    const url = `${this.likeUrl}/${id}`;
    return this.http.delete<any>(url);
  }

  hasLikedByUser(userId: string, postId: string): Observable<boolean> {
    return this.getLikes().pipe(
      map((likes: Like[]) => {
        const foundLike = likes.find((like: Like) => like.userAlias === userId && like.postId === postId);
        return !!foundLike; // Return true if a matching like was found, false otherwise
      })
    );
  }
}
