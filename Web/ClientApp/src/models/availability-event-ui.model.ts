import Availability from './availability.model';

export default class AvailabilityEventUI extends Availability {
  public classNames: string[];

  constructor(obj: Availability) {
    super(obj);

    this.classNames = [];

    if (!obj.isFree) {
      this.classNames.push('reserved');
    }
    if (obj.isCompleted) {
      this.classNames.push('completed');
    }
  }
}
