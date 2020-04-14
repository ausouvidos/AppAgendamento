export default class User {
  public id: string;
  public name: string;
  public email: string;

  constructor(obj: User) {
    this.id = obj.id;
    this.name = obj.name;
    this.email = obj.email;
  }
}
