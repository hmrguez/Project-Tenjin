import {Post} from "./post";

export class Like {
  id: string = "";
  postId: string = "";
  post?: Post;
  userAlias: string = "";
  dateLiked: Date = new Date();
}
