import AvailabilityDate from './availability-date.model';

export default class AddAvailabilitiesRequest {
  public userIdentityId: string;
  public dates: AvailabilityDate[];

  constructor(obj: AddAvailabilitiesRequest) {
    this.userIdentityId = obj.userIdentityId;
    this.dates = obj.dates;
  }
}
