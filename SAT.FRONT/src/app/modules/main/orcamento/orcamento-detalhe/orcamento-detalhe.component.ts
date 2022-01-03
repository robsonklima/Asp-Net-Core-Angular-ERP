import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Orcamento, OrcamentoDadosLocal, OrcamentoDadosLocalEnum } from 'app/core/types/orcamento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-orcamento-detalhes',
  templateUrl: './orcamento-detalhe.component.html'
})
export class OrcamentoDetalheComponent implements OnInit
{

  codOrc: number;
  orcamento: Orcamento;
  os: OrdemServico;
  userSession: UsuarioSessao;
  isLoading: boolean = false;

  dadosLocalFaturamento: OrcamentoDadosLocal;
  dadosLocalEnvioNF: OrcamentoDadosLocal;
  dadosLocalAtendimento: OrcamentoDadosLocal;

  constructor (
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _osService: OrdemServicoService,
    private _orcamentoService: OrcamentoService) 
  {
    this.codOrc = +this._route.snapshot.paramMap.get('codOrc');
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void
  {
    this.obterDados();
  }

  private async obterDados()
  {
    this.isLoading = true;

    this.orcamento =
      (await this._orcamentoService.obterPorCodigo(this.codOrc).toPromise());

    this.os =
      (await this._osService.obterPorCodigo(this.orcamento.codigoOrdemServico).toPromise());

    this.obterEnderecos();

    this.isLoading = false;
  }

  private obterEnderecos()
  {
    this.dadosLocalFaturamento =
    {
      tipo: OrcamentoDadosLocalEnum.FATURAMENTO
    }

    this.dadosLocalEnvioNF =
    {
      tipo: OrcamentoDadosLocalEnum.NOTA_FISCAL
    }

    this.dadosLocalAtendimento =
    {
      tipo: OrcamentoDadosLocalEnum.ATENDIMENTO
    }

  }

  trocarTab(tab: any)
  {
    if (tab.index !== 5 || !this.orcamento)
    {
      return;
    }
  }
}