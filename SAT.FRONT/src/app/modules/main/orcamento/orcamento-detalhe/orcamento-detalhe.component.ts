import { Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FilialService } from 'app/core/services/filial.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Filial } from 'app/core/types/filial.types';
import { Orcamento, OrcamentoDadosLocal, OrcamentoDadosLocalEnum } from 'app/core/types/orcamento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { EmailDialogComponent } from 'app/shared/email-dialog/email-dialog.component';

@Component({
  selector: 'app-orcamento-detalhes',
  templateUrl: './orcamento-detalhe.component.html',
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoDetalheComponent implements OnInit
{
  codOrc: number;
  orcamento: Orcamento;
  os: OrdemServico;
  filial: Filial;
  userSession: UsuarioSessao;
  isLoading: boolean = false;

  dadosLocalFaturamento: OrcamentoDadosLocal;
  dadosLocalEnvioNF: OrcamentoDadosLocal;
  dadosLocalAtendimento: OrcamentoDadosLocal;

  constructor (
    private _dialog: MatDialog,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _osService: OrdemServicoService,
    private _orcamentoService: OrcamentoService,
    private _filialService: FilialService) 
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
    this.orcamento = await this._orcamentoService.obterPorCodigo(this.codOrc).toPromise();
    this.os = await this._osService.obterPorCodigo(this.orcamento.codigoOrdemServico).toPromise();
    this.filial = await this._filialService.obterPorCodigo(this.orcamento.codigoFilial).toPromise();

    this.obterEnderecos();
    this.isLoading = false;
  }

  private obterEnderecos()
  {
    this.dadosLocalFaturamento = {
      tipo: OrcamentoDadosLocalEnum.FATURAMENTO,
      razaoSocial: this.os?.cliente?.razaoSocial,
      cnpj: this.os?.cliente?.cnpj,
      inscricaoEstadual: this.os.cliente?.inscricaoEstadual,
      responsavel: this.orcamento?.enderecoFaturamentoNF?.responsavelFaturamento,
      email: this.orcamento?.enderecoFaturamentoNF?.emailFaturamento,
      fone: this.orcamento?.enderecoFaturamentoNF?.foneFaturamento,
      endereco: this.orcamento?.enderecoFaturamentoNF?.enderecoFaturamento,
      numero: this.orcamento?.enderecoFaturamentoNF?.numeroFaturamento,
      bairro: this.orcamento?.enderecoFaturamentoNF?.bairroFaturamento,
      cep: this.orcamento?.enderecoFaturamentoNF?.cepFaturamento,
      complemento: this.orcamento?.enderecoFaturamentoNF?.complementoFaturamento,
      cidade: this.orcamento?.enderecoFaturamentoNF?.cidadeFaturamento?.nomeCidade,
      uf: this.orcamento?.enderecoFaturamentoNF?.cidadeFaturamento?.unidadeFederativa?.siglaUF
    }

    this.dadosLocalEnvioNF = {
      tipo: OrcamentoDadosLocalEnum.NOTA_FISCAL,
      razaoSocial: this.os?.cliente?.razaoSocial,
      cnpj: this.os?.cliente?.cnpj,
      inscricaoEstadual: this.os.cliente?.inscricaoEstadual,
      responsavel: this.orcamento?.enderecoFaturamentoNF?.responsavelEnvioNF,
      email: this.orcamento?.enderecoFaturamentoNF?.emailEnvioNF,
      fone: this.orcamento?.enderecoFaturamentoNF?.foneEnvioNF,
      endereco: this.orcamento?.enderecoFaturamentoNF?.enderecoEnvioNF,
      numero: this.orcamento?.enderecoFaturamentoNF?.numeroEnvioNF,
      bairro: this.orcamento?.enderecoFaturamentoNF?.bairroEnvioNF,
      cep: this.orcamento?.enderecoFaturamentoNF?.cepEnvioNF,
      complemento: this.orcamento?.enderecoFaturamentoNF?.complementoEnvioNF,
      cidade: this.orcamento?.enderecoFaturamentoNF?.cidadeEnvioNF?.nomeCidade,
      uf: this.orcamento?.enderecoFaturamentoNF?.cidadeEnvioNF?.unidadeFederativa?.siglaUF
    }

    this.dadosLocalAtendimento = {
      tipo: OrcamentoDadosLocalEnum.ATENDIMENTO,
      nroContrato: this.os?.equipamentoContrato?.contrato?.nroContrato,
      nomeLocal: this.os?.localAtendimento?.nomeLocal,
      oscliente: this.os?.numOSCliente,
      osPerto: this.os?.codOS.toString(),
      agencia: this.os?.localAtendimento?.numAgencia + '/' + this.os?.localAtendimento?.dcPosto,
      endereco: this.os?.localAtendimento?.endereco,
      bairro: this.os?.localAtendimento?.bairro,
      cep: this.os?.localAtendimento?.cep,
      complemento: this.os?.localAtendimento?.enderecoComplemento,
      cidade: this.os?.localAtendimento?.cidade?.nomeCidade,
      uf: this.os?.localAtendimento?.cidade?.unidadeFederativa?.siglaUF,
      modelo: this.os?.equipamento?.nomeEquip,
      nroSerie: this.os?.equipamentoContrato?.numSerie,
      motivoOrcamento: this.orcamento?.orcamentoMotivo?.descricao
    }
  }

  enviarEmail()
  {
    this._dialog.open(EmailDialogComponent, {
      width: '600px',
    });
  }

  trocarTab(tab: any)
  {
    if (tab.index == 0)
      this.ngOnInit();

    if (tab.index !== 5 || !this.orcamento)
    {
      return;
    }
  }
}