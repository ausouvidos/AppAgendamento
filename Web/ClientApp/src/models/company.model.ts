export default class Company {
  public id: number;
  public name: string;
  public address: string;
  public district: string;
  public city: string;
  public state: string;
  public zipCode: string;
  public phone: string;
  public contactPerson: string;
  public contactPersonEmail: string;
  public recaptchaResponse: string;

  constructor(obj?: Company) {
    this.id = obj?.id || 0;
    this.name = obj?.name || '';
    this.address = obj?.address || '';
    this.district = obj?.district || '';
    this.city = obj?.city || '';
    this.state = obj?.state || '';
    this.zipCode = obj?.zipCode || '';
    this.phone = obj?.phone || '';
    this.contactPerson = obj?.contactPerson || '';
    this.contactPersonEmail = obj?.contactPersonEmail || '';
    this.recaptchaResponse = obj?.recaptchaResponse || '';
  }
}
