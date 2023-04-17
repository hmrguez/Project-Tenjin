import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Like} from "../models/like";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LikeService {

  private likeUrl = 'http://localhost:5007/api/like';

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
}
