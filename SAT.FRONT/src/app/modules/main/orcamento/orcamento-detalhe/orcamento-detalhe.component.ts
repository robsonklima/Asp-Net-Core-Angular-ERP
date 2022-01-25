import { ChangeDetectorRef, Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { Orcamento, OrcamentoDadosLocal, OrcamentoDadosLocalEnum, OrcamentoDeslocamento, OrcamentoMotivo, OrcamentoStatus } from 'app/core/types/orcamento.types';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FilialService } from 'app/core/services/filial.service';
import { OrcamentoMotivoService } from 'app/core/services/orcamento-motivo.service';
import { OrcamentoStatusService } from 'app/core/services/orcamento-status.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Filial } from 'app/core/types/filial.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { EmailDialogComponent } from 'app/shared/email-dialog/email-dialog.component';
import { Subject } from 'rxjs';
import _ from 'lodash';

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
  status: OrcamentoStatus[] = [];
  motivos: OrcamentoMotivo[] = [];
  oldItem: any;
  isEditing: boolean;

  dadosLocalFaturamento: OrcamentoDadosLocal;
  dadosLocalEnvioNF: OrcamentoDadosLocal;
  dadosLocalAtendimento: OrcamentoDadosLocal;

  public orcamentoDeslocamentoChanged: Subject<OrcamentoDeslocamento[]> = new Subject<OrcamentoDeslocamento[]>();

  constructor (
    private _dialog: MatDialog,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _osService: OrdemServicoService,
    private _orcamentoService: OrcamentoService,
    private _filialService: FilialService,
    private _orcService: OrcamentoService,
    private _orcMotivoService: OrcamentoMotivoService,
    private _orcStatusService: OrcamentoStatusService,
    private _cdRef: ChangeDetectorRef
  ) 
  {
    this.codOrc = +this._route.snapshot.paramMap.get('codOrc');
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void
  {
    this.obterDados();
    this.obterStatus();
    this.obterMotivos();
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
      this.obterDados();

    if (tab.index !== 5 || !this.orcamento)
      return;
  }

  editar(): void
  {
    this.isEditing = true;
    this.oldItem = Object.assign({}, this.orcamento);
  }

  async salvar(): Promise<void>
  {
    this.orcamento =
      (await this._orcService.atualizar(this.orcamento).toPromise());

    this.oldItem = Object.assign({}, this.orcamento);

    this.isEditing = false;
    this.isLoading = true;
    this.isLoading = false;
  }

  cancelar(): void
  {
    this.isEditing = false;
    this.orcamento = Object.assign({}, this.oldItem);
    this._cdRef.detectChanges();
  }

  isEqual(): boolean
  {
    return _.isEqual(this.orcamento, this.oldItem);
  }

  isInvalid(): boolean
  {
    return false;
  }

  changeMotivo(value)
  {
    var parsedValue = parseInt(value);
    this.orcamento.codigoMotivo = parsedValue;
    this.orcamento.orcamentoMotivo = this.motivos.find(i => i.codOrcMotivo == parsedValue);
  }

  changeStatus(value)
  {
    var parsedValue = parseInt(value);
    this.orcamento.codigoStatus = parsedValue;
    this.orcamento.orcamentoStatus = this.status.find(i => i.codOrcStatus == parsedValue);
  }

  private async obterStatus()
  {
    this.status = (await this._orcStatusService.obterPorParametros({}).toPromise()).items;
  }

  private async obterMotivos()
  {
    this.motivos = (await this._orcMotivoService.obterPorParametros({}).toPromise()).items;
  }
}