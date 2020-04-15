import moment from 'moment';

export default class AvailabilityDate {
  public start: Date;
  public end: Date;

  constructor(obj: AvailabilityDate) {
    this.start = moment.utc(obj.start).toDate();
    this.end = moment.utc(obj.end).toDate();
  }
}
