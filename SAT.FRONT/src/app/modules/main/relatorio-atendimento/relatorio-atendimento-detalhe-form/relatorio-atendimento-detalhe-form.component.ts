import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TipoServicoService } from 'app/core/services/tipo-servico.service';
import { CausaService } from 'app/core/services/causa.service';
import { TipoServico } from 'app/core/types/tipo-servico.types';
import { RelatorioAtendimentoFormComponent } from '../relatorio-atendimento-form/relatorio-atendimento-form.component';
import { Causa } from 'app/core/types/causa.types';
import { DefeitoService } from 'app/core/services/defeito.service';
import { AcaoService } from 'app/core/services/acao.service';
import { Defeito } from 'app/core/types/defeito.types';
import { Acao } from 'app/core/types/acao.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { debounceTime, filter, map, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { statusConst } from 'app/core/types/status-types';
import { RelatorioAtendimentoDetalhe } from 'app/core/types/relatorio-atendimento-detalhe.type';
import moment from 'moment';

@Component({
  selector: 'app-relatorio-atendimento-detalhe-form',
  templateUrl: './relatorio-atendimento-detalhe-form.component.html'
})
export class RelatorioAtendimentoDetalheFormComponent implements OnInit, OnDestroy {
  detalhe: RelatorioAtendimentoDetalhe;
  form: FormGroup;
  modulos: Causa[] = [];
  causas: Causa[] = [];
  tiposServico: TipoServico[] = [];
  defeitos: Defeito[] = [];
  acoes: Acao[] = [];
  usuario: Usuario;
  causasFiltro: FormControl = new FormControl();
  acoesFiltro: FormControl = new FormControl();
  defeitosFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _tipoServicoService: TipoServicoService,
    private _causaService: CausaService,
    private _defeitoService: DefeitoService,
    private _acaoService: AcaoService,
    private _userService: UserService,
    public dialogRef: MatDialogRef<RelatorioAtendimentoFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.usuario = JSON.parse(this._userService.userSession).usuario;
    this.detalhe = data?.detalhe;

    console.log(this.detalhe);
    
  }

  async ngOnInit() {
    this.inicializarForm();
    this.registrarEmitters();

    if (this.detalhe) {
      this.tiposServico = [(await this.obterTiposServico(this.detalhe.tipoServico.nomeServico)).shift()];
      this.causas = [(await this.obterCausas(this.detalhe.causa.codECausa)).shift()];
      this.defeitos = [(await this.obterDefeitos(this.detalhe.defeito.nomeDefeito)).shift()];
      this.acoes = [(await this.obterAcoes(this.detalhe.acao.nomeAcao)).shift()];
    }
  }

  salvar(): void {
    let form = this.form.getRawValue();
    form.tipoServico = this.tiposServico.filter(ts => ts.codServico === form.codServico).shift();
    form.defeito = this.defeitos.filter(ts => ts.codDefeito === form.codDefeito).shift();
    form.acao = this.acoes.filter(ts => ts.codAcao === form.codAcao).shift();
    form.causa = this.causas.filter(ts => ts.codCausa === form.codCausa).shift();
    form.codUsuarioCad = this.usuario.codUsuario;
    form.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
    form.relatorioAtendimentoDetalhePecas = [];

    const codCausa = this.form.controls['codCausa'].value;
    const causa = this.causas.filter(c => c.codCausa === codCausa).shift();
    form.codTipoCausa = causa.codTipoCausa;
    form.codGrupoCausa = causa.codGrupoCausa;

    this.dialogRef.close(form);
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      maquina: [this.detalhe?.codOrigemCausa?.toString(), [Validators.required]],
      codServico: [this.detalhe?.codServico, [Validators.required]],
      codCausa: [this.detalhe?.codCausa, [Validators.required]],
      codAcao: [this.detalhe?.codAcao, [Validators.required]],
      codDefeito: [this.detalhe?.codDefeito, [Validators.required]],
      codTipoCausa: [this.detalhe?.codTipoCausa],
      codGrupoCausa: [this.detalhe?.codGrupoCausa]
    });
  }

  registrarEmitters() {
    this.form.controls['maquina'].valueChanges.subscribe(async () => {
      this.tiposServico = await this.obterTiposServico();
    });

    this.form.controls['codServico'].valueChanges.subscribe(async () => {
      this.causas = await this.obterCausas();
    });

    this.form.controls['codCausa'].valueChanges.subscribe(async () => {
      this.defeitos = await this.obterDefeitos();
    });

    this.causasFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          return await this.obterCausas(query);
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.causas = await data;
      });

    this.form.controls['codDefeito'].valueChanges.subscribe(async maquina => {
      this.acoes = await this.obterAcoes();
    });

    this.defeitosFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          return await this.obterDefeitos(query);
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.defeitos = await data;
      });

    this.acoesFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          return await this.obterAcoes(query);
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.acoes = await data;
      });
  }

  private async obterTiposServico(filter: string=''): Promise<TipoServico[]> {
    return new Promise(async (resolve, reject) => {
      const data = await this._tipoServicoService.obterPorParametros({
        sortActive: 'nomeServico',
        sortDirection: 'asc',
        pageSize: 100,
        filter: filter
      }).toPromise();
  
      resolve(data.items);
    });
  }

  private async obterCausas(filter: string=''): Promise<Causa[]> {
    return new Promise(async (resolve, reject) => {
      const data = await this._causaService.obterPorParametros({
          sortActive: 'codECausa',
          sortDirection: 'asc',
          pageSize: 100,
          indAtivo: statusConst.ATIVO,
          filter: filter
        }).toPromise();
  
        resolve(data.items);
    });
  }

  private async obterDefeitos(filter: string=''): Promise<Defeito[]> {
    return new Promise(async (resolve, reject) => {
      const data = await this._defeitoService.obterPorParametros({
        sortActive: 'codEDefeito',
        sortDirection: 'asc',
        pageSize: 100,
        filter: filter,
        indAtivo: statusConst.ATIVO
      }).toPromise();
  
      resolve(data.items);
    });

  }

  private async obterAcoes(filter: string=''): Promise<Acao[]> {
    return new Promise(async (resolve, reject) => {
      const data = await this._acaoService.obterPorParametros({
        sortActive: 'codEAcao',
        sortDirection: 'asc',
        pageSize: 100,
        indAtivo: statusConst.ATIVO,
        filter: filter
      }).toPromise();
      
      resolve(data.items);
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
