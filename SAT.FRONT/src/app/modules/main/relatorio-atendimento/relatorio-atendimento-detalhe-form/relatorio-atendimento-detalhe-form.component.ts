import { Component, OnDestroy, OnInit } from '@angular/core';
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
import { MatDialogRef } from '@angular/material/dialog';
import moment from 'moment';

@Component({
  selector: 'app-relatorio-atendimento-detalhe-form',
  templateUrl: './relatorio-atendimento-detalhe-form.component.html'
})
export class RelatorioAtendimentoDetalheFormComponent implements OnInit, OnDestroy
{
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

  constructor (
    private _formBuilder: FormBuilder,
    private _tipoServicoService: TipoServicoService,
    private _causaService: CausaService,
    private _defeitoService: DefeitoService,
    private _acaoService: AcaoService,
    private _userService: UserService,
    public dialogRef: MatDialogRef<RelatorioAtendimentoFormComponent>
  )
  {
    this.usuario = JSON.parse(this._userService.userSession).usuario;
  }

  async ngOnInit()
  {
    this.inicializarForm();

    this.form.controls['maquina'].valueChanges.subscribe(async maquina =>
    {
      const data = await this._tipoServicoService.obterPorParametros({
        sortActive: 'nomeServico',
        sortDirection: 'asc',
        pageSize: 100,
      }).toPromise();

      this.tiposServico = data.items.filter(
        t => t.codETipoServico.substring(0, 1) === String(maquina)
      );
    });

    this.form.controls['codServico'].valueChanges.subscribe(async codServico =>
    {
      const data = await this._causaService.obterPorParametros({
        sortActive: 'codECausa',
        sortDirection: 'asc',
        pageSize: 100,
        indAtivo: 1
      }).toPromise();

      this.causas = data.items;
    });

    this.form.controls['codCausa'].valueChanges.subscribe(async codCausa =>
    {
      const data = await this._defeitoService.obterPorParametros({
        sortActive: 'codEDefeito',
        sortDirection: 'asc',
        pageSize: 100,
        indAtivo: 1
      }).toPromise();

      this.defeitos = data.items;
    });

    this.causasFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query =>
        {
          const data = await this._causaService.obterPorParametros({
            sortActive: 'codECausa',
            sortDirection: 'asc',
            indAtivo: 1,
            filter: query,
            pageSize: 100,
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data =>
      {
        this.causas = await data;
      });

    this.form.controls['codDefeito'].valueChanges.subscribe(async maquina =>
    {
      const data = await this._acaoService.obterPorParametros({
        sortActive: 'codEAcao',
        sortDirection: 'asc',
        pageSize: 100,
        indAtivo: 1
      }).toPromise();

      this.acoes = data.items;
    });

    this.defeitosFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query =>
        {
          const data = await this._defeitoService.obterPorParametros({
            sortActive: 'codEDefeito',
            sortDirection: 'asc',
            indAtivo: 1,
            filter: query,
            pageSize: 100
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data =>
      {
        this.defeitos = await data;
      });

    this.acoesFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query =>
        {
          const data = await this._acaoService.obterPorParametros({
            sortActive: 'codEAcao',
            sortDirection: 'asc',
            indAtivo: 1,
            filter: query,
            pageSize: 100,
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data =>
      {
        this.acoes = await data;
      });
  }

  inserir(): void
  {
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

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      maquina: [undefined, [Validators.required]],
      codServico: [undefined, [Validators.required]],
      codCausa: [undefined, [Validators.required]],
      codAcao: [undefined, [Validators.required]],
      codDefeito: [undefined, [Validators.required]],
      codTipoCausa: [undefined],
      codGrupoCausa: [undefined]
    });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
