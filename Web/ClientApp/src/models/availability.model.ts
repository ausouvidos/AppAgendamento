import moment from 'moment';
import User from './user.model';

export default class Availability {
  public id: number;
  public user: User;
  public userId: string;
  public start: Date;
  public end: Date;
  public isFree: boolean;
  public isCompleted: boolean;
  public customerName: string;
  public customerEmail: string;
  public notes: string;
  public observations: string;

  constructor(obj: Availability) {
    this.id = obj.id;
    this.user = obj.user;
    this.userId = obj.userId;
    this.start = moment.utc(obj.start).toDate();
    this.end = moment.utc(obj.end).toDate();
    this.isFree = obj.isFree;
    this.isCompleted = obj.isCompleted;
    this.customerName = obj.customerName;
    this.customerEmail = obj.customerEmail;
    this.notes = obj.notes;
    this.observations = obj.observations;
  }
}
