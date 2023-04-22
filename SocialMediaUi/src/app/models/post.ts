import {User} from "./user";

export class Post {
  id: string = "";
  likeCount: number = 0;
  dateCreated: Date = new Date();
  picture?: string;
  text?: string;
  userAlias: string = "";
  user?: User
}
