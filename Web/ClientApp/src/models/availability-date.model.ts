export default class AvailabilityDate {
  public start: Date;
  public end: Date;

  constructor(obj: AvailabilityDate) {
    this.start = obj.start;
    this.end = obj.end;
  }
}
