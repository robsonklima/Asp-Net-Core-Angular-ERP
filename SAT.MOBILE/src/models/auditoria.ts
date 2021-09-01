import { Condutor } from "./condutor";
import { Veiculo } from "./veiculo";
import { AuditoriaStatus } from "./auditoria-status";
import { Usuario } from "./usuario";

export class Auditoria {
  usuario: Usuario;
  condutor: Condutor;
  auditoriaVeiculo: Veiculo;
  auditoriaStatus: AuditoriaStatus;
  assinaturaTecnico: string;
}