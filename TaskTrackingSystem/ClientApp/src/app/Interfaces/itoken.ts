export interface IToken {
  email: string;
  role: string;
  unique_name: string;
  nbf: number;
  exp: number;
  iat: number;
}
