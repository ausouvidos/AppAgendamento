export default class ApiResponse {
  public succeeded: boolean;
  public message?: string;

  constructor(obj: ApiResponse) {
    this.succeeded = obj?.succeeded;
    this.message = obj?.message;
  }
}
