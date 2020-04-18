import Availability from './availability.model';

export default class AvailabilityEventUI {
  public start: Date;
  public end: Date;
  public classNames: string;

  constructor(obj: Availability) {
    this.start = obj.start;
    this.end = obj.end;
    this.classNames = obj.isFree ? '' : 'reserved';
  }
}
