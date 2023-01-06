import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { CausaImprodutividadeService } from 'app/core/services/causa-improdutividade.service';
import { CausaService } from 'app/core/services/causa.service';
import { ImprodutividadeService } from 'app/core/services/improdutividade.service';
import { OrdemServicoSTNOrigemService } from 'app/core/services/ordem-servico-stn-origem.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { ProtocoloChamadoSTNService } from 'app/core/services/protocolo-chamado-stn.service';
import { StatusServicoSTNService } from 'app/core/services/status-servico-stn.service';
import { TipoChamadoSTNService } from 'app/core/services/tipo-chamado-stn.service';
import { TipoServicoService } from 'app/core/services/tipo-servico.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { CausaImprodutividade } from 'app/core/types/causa-improdutividade.types';
import { Causa } from 'app/core/types/causa.types';
import { Improdutividade } from 'app/core/types/improdutividade.types';
import { OrdemServicoSTNOrigem } from 'app/core/types/ordem-servico-stn-origem.types';
import { OrdemServicoSTN } from 'app/core/types/ordem-servico-stn.types';
import { ProtocoloChamadoSTN } from 'app/core/types/protocolo-chamado-stn.types';
import { StatusServicoSTN } from 'app/core/types/status-servico-stn.types';
import { statusConst } from 'app/core/types/status-types';
import { TipoChamadoSTN } from 'app/core/types/tipo-chamado-stn.types';
import { TipoServico } from 'app/core/types/tipo-servico.types';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import { forkJoin, Subject } from 'rxjs';
import { debounceTime, filter, map, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-ordem-servico-stn-form-atendimento',
  templateUrl: './ordem-servico-stn-form-atendimento.component.html'
})
export class OrdemServicoStnFormAtendimentoComponent implements OnInit {
  @Input() codAtendimento: number;
  atendimento: OrdemServicoSTN;
  status: StatusServicoSTN[] = [];
  protocolo: ProtocoloChamadoSTN;
  tipoCausa: TipoServico[] = [];
  tipoChamadoSTN: TipoChamadoSTN[] = [];
  tipoServico: string;
  modulos: Causa[] = [];
  subModulos: Causa[] = [];
  componentes: Causa[] = [];
  usuario: Usuario;
  origens: OrdemServicoSTNOrigem[] = [];
  improdutividades: Improdutividade[] = [];
  causaImprodutividade: CausaImprodutividade[] = [];
  userSession: UsuarioSessao;
  form: FormGroup;
  searching: boolean;
  causasFiltro: FormControl = new FormControl();
  tipoCausaFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _statusSTNService: StatusServicoSTNService,
    private _protocoloChamadoSTNService: ProtocoloChamadoSTNService,
    private _tipoChamadoSTNService: TipoChamadoSTNService,
    private _ordemServicoSTNOrigemSrv: OrdemServicoSTNOrigemService,
    private _tipoServicoService: TipoServicoService,
    private _causaService: CausaService,
    private _usuarioService: UsuarioService,
    private _causaImprodutividadeService: CausaImprodutividadeService,
    private _improdutividadeService: ImprodutividadeService,
    private _formBuilder: FormBuilder,
    private _userService: UserService) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.atendimento = await this._ordemServicoSTNService.obterPorCodigo(this.codAtendimento).toPromise();
    this.protocolo = (await this._protocoloChamadoSTNService.obterPorParametros({ codAtendimento: this.codAtendimento }).toPromise()).items.shift();
    this.usuario = (await this._usuarioService.obterPorParametros({ codUsuario: this.atendimento.codUsuarioCad }).toPromise()).items.shift();
    this.improdutividades = (await this._improdutividadeService.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
    this.causaImprodutividade = (await this._causaImprodutividadeService.obterPorParametros({ codProtocolo: this.protocolo.codProtocoloChamadoSTN }).toPromise()).items;

    this.inicializarForm();
    this.obterOrigens();
    this.obterTipoCausa();
    this.obterTipoChamados();
    this.obterStatus();
    this.obterCausas();
    this.registrarEmitters();
  }

  inicializarForm() {
    this.form = this._formBuilder.group({
      codOrigemChamadoSTN: [undefined, Validators.required],
      codTipoCausa: [undefined, Validators.required],
      dataHoraAberturaSTN: [undefined],
      codAtendimento: [ this.atendimento.codAtendimento ],
      codStatusSTN: [undefined],
      codTipoChamadoSTN: [undefined],
      tecnicoCampo: [undefined],
      codGrupoCausa: [undefined],
      codCausa: [undefined],
      codDefeito: [undefined],
      acaoSTN: [undefined],
      tipoServico: [undefined],
      nomeTecnico: [undefined]
    });
  }

  registrarEmitters() {
    
    this.tipoCausaFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          const data = await this._tipoServicoService.obterPorParametros({
            sortActive: 'codETipoServico',
            sortDirection: 'asc',
            filter: query,
            pageSize: 100,
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.tipoCausa = await data;
      });

    this.causasFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          const data = await this._causaService.obterPorParametros({
            sortActive: 'codECausa',
            sortDirection: 'asc',
            indAtivo: statusConst.ATIVO,
            filter: query,
            pageSize: 100,
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.componentes = await data;
      });
  }

  async obterCausas() {
    this.componentes = (await this._causaService.obterPorParametros({ codECausa: this.atendimento.codDefeito }).toPromise()).items;
  }

  async obterOrigens() {
    this.origens = (await this._ordemServicoSTNOrigemSrv.obterPorParametros({
      sortActive: 'descOrigemChamadoSTN',
      sortDirection: 'asc',
    }).toPromise()).items;
  }

  async obterTipoCausa() {
    this.tipoCausa = (await this._tipoServicoService.obterPorParametros({ 
        sortActive: 'nomeServico',
        sortDirection: 'asc',
        pageSize: 100,
     }).toPromise()).items;
  }

  async obterStatus() {
    this.status = (await this._statusSTNService.obterPorParametros({
      sortDirection: 'asc',
      pageSize: 100,
    }).toPromise()).items;
  }

  async obterTipoChamados() {
    this.tipoChamadoSTN = (await this._tipoChamadoSTNService.obterPorParametros({
      indAtivo: statusConst.ATIVO
    }).toPromise()).items;
  }

  async onChange($event: MatSlideToggleChange, codigo) {
    if ($event.checked) {
      this._causaImprodutividadeService.criar({
        codImprodutividade: codigo,
        codProtocolo: this.protocolo.codProtocoloChamadoSTN,
        indAtivo: statusConst.ATIVO
      }).subscribe();
    } else {
      const causaImprodutividade = (await this._causaImprodutividadeService.obterPorParametros({
        codProtocolo: this.protocolo.codProtocoloChamadoSTN,
        codImprodutividade: codigo
      }).toPromise()).items.shift();

      this._causaImprodutividadeService.deletar(causaImprodutividade.codCausaImprodutividade).subscribe();
    }
  }

  async onChangePendencia($event: MatSlideToggleChange) {
    if ($event.checked) {
      this.atendimento.indEvitaPendencia = 1;
    }
    else {
      this.atendimento.indEvitaPendencia = 0;
    }
  }

  public verificarSelecionado(codImprodutividade: number): boolean {
    return _.find(this.causaImprodutividade, { codImprodutividade: codImprodutividade, codProtocolo: this.protocolo?.codProtocoloChamadoSTN }) != null;
  }

  salvar() {
    this.codAtendimento ? this.atualizar() : this.criar();
  }
  
  atualizar() {
    throw new Error('Method not implemented.');
  }

  criar() {
    forkJoin([
      this._ordemServicoSTNService.criar(this.form.getRawValue()),
      this._ordemServicoSTNService.criar(this.form.getRawValue()),
    ]).subscribe(([result1, result2]) => {
      if (result1 && result2) {
        //mensagem pro usuario dizendo que cadastrou os 2
      }      
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
