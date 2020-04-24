export default class ReserveSpotRequest {
  public name: string;
  public email: string;
  public mobile: string;
  public start?: Date;
  public end?: Date;
  public recaptchaResponse?: string;

  constructor() {
    this.name = sessionStorage.getItem('reservationName') || '';
    this.email = sessionStorage.getItem('reservationEmail') || '';
    this.mobile = sessionStorage.getItem('reservationMobile') || '';
    this.start = new Date();
    this.end = new Date();
  }

  public saveCache() {
    sessionStorage.setItem('reservationName', this.name);
    sessionStorage.setItem('reservationEmail', this.email);
    sessionStorage.setItem('reservationMobile', this.mobile);
  }
}
