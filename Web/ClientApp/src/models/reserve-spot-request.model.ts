export default class ReserveSpotRequest {
  public name: string;
  public email: string;
  public start: Date;
  public end: Date;

  constructor(obj: ReserveSpotRequest) {
    this.name = obj.name;
    this.email = obj.email;
    this.start = obj.start;
    this.end = obj.end;
  }
}
