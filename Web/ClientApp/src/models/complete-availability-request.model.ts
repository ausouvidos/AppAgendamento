export default class CompleteAvailabilityRequest {
  public id: number;
  public observacoes: string;
  public notas: string;

  constructor(obj: CompleteAvailabilityRequest) {
    this.id = obj.id;
    this.observacoes = obj.observacoes;
    this.notas = obj.notas;
  }
}
