import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Comment} from "../models/comment";

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private commentUrl = 'https://localhost:7086/api/comment';

  constructor(private http: HttpClient) { }

  public getComments(): Observable<Comment[]> {
    return this.http.get<Comment[]>(this.commentUrl);
  }

  public getCommentById(id: string): Observable<Comment> {
    const url = `${this.commentUrl}/${id}`;
    return this.http.get<Comment>(url);
  }

  public createComment(comment: any): Observable<Comment> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Comment>(this.commentUrl, comment, { headers });
  }

  public updateComment(comment: any): Observable<Comment> {
    return this.http.put<Comment>(this.commentUrl, comment);
  }

  public deleteComment(id: string): Observable<any> {
    const url = `${this.commentUrl}/${id}`;
    return this.http.delete<any>(url);
  }
}
