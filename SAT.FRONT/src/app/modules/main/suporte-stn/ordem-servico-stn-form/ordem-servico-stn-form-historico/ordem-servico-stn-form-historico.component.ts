import { Component, Input, OnInit } from '@angular/core';
import { CausaImprodutividadeService } from 'app/core/services/causa-improdutividade.service';
import { ImprodutividadeService } from 'app/core/services/improdutividade.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { CausaImprodutividade } from 'app/core/types/causa-improdutividade.types';
import { Improdutividade } from 'app/core/types/improdutividade.types';
import { OrdemServicoSTNData } from 'app/core/types/ordem-servico-stn.types';
import { ProtocoloChamadoSTN } from 'app/core/types/protocolo-chamado-stn.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';

@Component({
  selector: 'app-ordem-servico-stn-form-historico',
  templateUrl: './ordem-servico-stn-form-historico.component.html'
})
export class OrdemServicoStnFormHistoricoComponent implements OnInit {
  @Input() codAtendimento: number;
  codOS: number;
  atendimentos: OrdemServicoSTNData;
  improdutividades: Improdutividade[] = [];
  causasImprodutividades: CausaImprodutividade[] = [];
  userSession: UsuarioSessao;

  constructor(
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _improdutividadeService: ImprodutividadeService,
    private _causaImprodSvc: CausaImprodutividadeService,
    private _userService: UserService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codOS = await (await this._ordemServicoSTNService.obterPorCodigo(this.codAtendimento).toPromise()).codOS;
    this.atendimentos = (await this._ordemServicoSTNService
      .obterPorParametros
      ({
        codOS: this.codOS,
        indAtivoCausa: statusConst.ATIVO, 
        sortDirection: 'desc', 
        sortActive:'codAtendimento'
      }).toPromise());

    this.improdutividades = (await this._improdutividadeService.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
    this.causasImprodutividades = (await this._causaImprodSvc.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
    
  }

  public verificarSolucaoSelecionado(codImprodutividade: number, protocolo: ProtocoloChamadoSTN): boolean {
    return _.find(this.causasImprodutividades, { codImprodutividade: codImprodutividade, codProtocolo: protocolo.codProtocoloChamadoSTN }) != null;
  }

}
