import User from './user.model';

export default class Availability {
  public id: number;
  public user: User;
  public userId: string;
  public start: Date;
  public end: Date;
  public isFree: boolean;
  public customerName: string;
  public customerEmail: string;

  constructor(obj: Availability) {
    this.id = obj.id;
    this.user = obj.user;
    this.userId = obj.userId;
    this.start = obj.start;
    this.end = obj.end;
    this.isFree = obj.isFree;
    this.customerName = obj.customerName;
    this.customerEmail = obj.customerEmail;
  }
}
