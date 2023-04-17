import {User} from "./user";

export class Post {
  id: string = "";
  likeCount: number = 0;
  dateCreated: Date = Date.prototype;
  picture?: string;
  text?: string;
  userAlias: string = "";
  user?: User;
}
