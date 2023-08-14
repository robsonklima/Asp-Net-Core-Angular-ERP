import { Component, Input, OnInit } from '@angular/core';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { RelatorioAtendimento, RelatorioAtendimentoData } from 'app/core/types/relatorio-atendimento.types';
import { SetorEnum } from 'app/core/types/setor.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import moment from 'moment';

@Component({
  selector: 'app-ordem-servico-rats',
  templateUrl: './ordem-servico-rats.component.html'
})
export class OrdemServicoRatsComponent implements OnInit {
  @Input() codOS: number;
  sessionData: UsuarioSessao;
  isLoading: boolean = true;
  rats: RelatorioAtendimento[] = [];
  perfilCliente: boolean;

  constructor(
    private _relatorioAtendimentoService: RelatorioAtendimentoService,
    private _exportacaoService: ExportacaoService,
    private _userService: UserService
  ) {
    this.sessionData = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.rats = (await this.obterRATS()).items;
    this.perfilCliente = this._userService.isCustomer;

    this.isLoading = false;
  }

  private async obterRATS(): Promise<RelatorioAtendimentoData> {
    return this._relatorioAtendimentoService
      .obterPorParametros({ codOS: this.codOS })
      .toPromise();
  }

  public verificarPermissaoEdicao(): boolean {
    if ((this.sessionData.usuario.codSetor == SetorEnum.HELPDESK_NOC
      || (this.sessionData.usuario.codSetor == SetorEnum.COORDENACAO_DE_CONTRATOS && this.sessionData.usuario.codPerfil == PerfilEnum.PV_COORDENADOR_DE_CONTRATO)
      || (this.sessionData.usuario.codSetor == SetorEnum.CENTRO_TECNICO_OPERACIONAL && this.sessionData.usuario.codPerfil == PerfilEnum.FILIAL_TECNICO_DE_CAMPO)
      || (this.sessionData.usuario.codSetor == SetorEnum.CENTRO_TECNICO_OPERACIONAL && this.sessionData.usuario.codPerfil == PerfilEnum.TECNICO_OPERACOES)
      || (this.sessionData.usuario.codSetor == SetorEnum.CENTRO_TECNICO_OPERACIONAL && this.sessionData.usuario.codPerfil == PerfilEnum.LIDER)
      || (this.sessionData.usuario.codSetor == SetorEnum.CENTRO_TECNICO_OPERACIONAL && this.sessionData.usuario.codPerfil == PerfilEnum.COORDENADOR)
      || (this.sessionData.usuario.codSetor == SetorEnum.CENTRO_TECNICO_OPERACIONAL && this.sessionData.usuario.codPerfil == PerfilEnum.SUPERVISOR)
      || (this.sessionData.usuario.codSetor == SetorEnum.OPERACAO_DE_CAMPO && this.sessionData.usuario.codPerfil == PerfilEnum.FILIAL_TECNICO_DE_CAMPO)
      || (this.sessionData.usuario.codSetor == SetorEnum.OPERACAO_DE_CAMPO && this.sessionData.usuario.codPerfil == PerfilEnum.TECNICO_OPERACOES)
      || (this.sessionData.usuario.codSetor == SetorEnum.OPERACAO_DE_CAMPO && this.sessionData.usuario.codPerfil == PerfilEnum.LIDER)
      || (this.sessionData.usuario.codSetor == SetorEnum.OPERACAO_DE_CAMPO && this.sessionData.usuario.codPerfil == PerfilEnum.COORDENADOR)
      || (this.sessionData.usuario.codSetor == SetorEnum.OPERACAO_DE_CAMPO && this.sessionData.usuario.codPerfil == PerfilEnum.SUPERVISOR)
      || (this.sessionData.usuario.codSetor == SetorEnum.SUPORTE_TECNICO && this.sessionData.usuario.codPerfil == PerfilEnum.FILIAL_SUPORTE_TÉCNICO_CAMPO)
      || this.sessionData.usuario.codPerfil == PerfilEnum.ADM_DO_SISTEMA))
      return true;

    else
      return false;
  }

  getTimeFromMins(mins) {
    var h = mins / 60 | 0, m = mins % 60 | 0;
    return moment.utc().hours(h).minutes(m).format("HH:mm");
  }

  public exportar() {
    let exportacaoParam: Exportacao = {
      formatoArquivo: ExportacaoFormatoEnum.PDF,
      tipoArquivo: ExportacaoTipoEnum.ORDEM_SERVICO,
      entityParameters: {
        codOS: this.codOS
      }
    }
    this._exportacaoService.exportar(FileMime.PDF, exportacaoParam);
  }
}
