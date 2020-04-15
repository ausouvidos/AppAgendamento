import AvailabilityDate from './availability-date.model';

export default class AddAvailabilitiesRequest {
  public dates: AvailabilityDate[];

  constructor(obj?: AddAvailabilitiesRequest) {
    this.dates = obj ? obj.dates : [];
  }
}
