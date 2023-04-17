import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Follow} from "../models/follow";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class FollowService {

  private followUrl = 'http://localhost:5007/api/follow';

  constructor(private http: HttpClient) { }

  public getFollows(): Observable<Follow[]> {
    return this.http.get<Follow[]>(this.followUrl);
  }

  public getFollowById(id: string): Observable<Follow> {
    const url = `${this.followUrl}/${id}`;
    return this.http.get<Follow>(url);
  }

  public createFollow(follow: any): Observable<Follow> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Follow>(this.followUrl, follow, { headers });
  }

  public deleteFollow(id: string): Observable<any> {
    const url = `${this.followUrl}/${id}`;
    return this.http.delete<any>(url);
  }
}
