export default class Professional {
  public id: number;
  public nome?: string;
  public ultimoNome?: string;
  public funcao?: string;
  public email?: string;
  public linkedIn?: string;

  public get nomeCompleto() {
    return `${this.nome} ${this.ultimoNome}`;
  }

  public get photoUrl() {
    return `/api/Team/Members/${this.id}/photo`;
  }

  constructor(obj: Professional) {
    this.id = obj?.id;
    this.nome = obj?.nome;
    this.ultimoNome = obj?.ultimoNome;
    this.funcao = obj?.funcao;
    this.email = obj?.email;
    this.linkedIn = obj?.linkedIn;
  }
}
