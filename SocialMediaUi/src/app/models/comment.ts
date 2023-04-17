import {Post} from "./post";

export class Comment {
  id: string = "";
  userAlias: string = "";
  postId: string = "";
  post?: Post;
  text: string = "";
}
