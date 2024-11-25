import { ValueLabel } from "./value-label";

export class CompanySize
{
  static SMALL: ValueLabel = { value: '1', label: 'Pequena' };
  static MEDIUM: ValueLabel = { value: '2', label: 'Média' };
  static LARGE: ValueLabel = { value: '3', label: 'Grande' };
}

export interface ClienteModel
{
  id?: number;
  nome?: string,
  empresaPorteId?: number,
  empresaPorte?: string
}
