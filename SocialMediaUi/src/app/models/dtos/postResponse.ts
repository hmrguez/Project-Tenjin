import {User} from "../user";

export class PostResponse {
  guid: string = "";
  likeCount: number = 0;
  dateCreated: Date = new Date();
  picture?: string;
  text?: string;
  userAlias: string = "";
}
