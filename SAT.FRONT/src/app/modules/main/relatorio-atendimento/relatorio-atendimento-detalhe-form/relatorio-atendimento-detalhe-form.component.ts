import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TipoServicoService } from 'app/core/services/tipo-servico.service';
import { CausaService } from 'app/core/services/causa.service';
import { TipoServico, TipoServicoData } from 'app/core/types/tipo-servico.types';
import { RelatorioAtendimentoFormComponent } from '../relatorio-atendimento-form/relatorio-atendimento-form.component';
import { Causa, CausaData } from 'app/core/types/causa.types';
import { DefeitoService } from 'app/core/services/defeito.service';
import { AcaoService } from 'app/core/services/acao.service';
import { Defeito, DefeitoData } from 'app/core/types/defeito.types';
import { Acao, AcaoData } from 'app/core/types/acao.types';
import { TipoCausa } from 'app/core/types/tipo-causa.types';
import { GrupoCausa } from 'app/core/types/grupo-causa.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { MatDialogRef } from '@angular/material/dialog';
import moment from 'moment';

@Component({
  selector: 'app-relatorio-atendimento-detalhe-form',
  templateUrl: './relatorio-atendimento-detalhe-form.component.html'
})
export class RelatorioAtendimentoDetalheFormComponent implements OnInit, OnDestroy {
  form: FormGroup;
  modulos: Causa[] = [];
  subModulos: Causa[] = [];
  componentes: Causa[] = [];
  causas: Causa[] = [];
  tiposServico: TipoServico[] = [];
  defeitos: Defeito[] = [];
  acoes: Acao[] = [];
  grupoCausa: GrupoCausa;
  tipoCausa: TipoCausa;
  usuario: Usuario;
  tipoServicoFilterCtrl: FormControl = new FormControl();
  causaFilterCtrl: FormControl = new FormControl();
  acaoFilterCtrl: FormControl = new FormControl();
  defeitoFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _tipoServicoService: TipoServicoService,
    private _causaService: CausaService,
    private _defeitoService: DefeitoService,
    private _acaoService: AcaoService,
    private _userService: UserService,
    public dialogRef: MatDialogRef<RelatorioAtendimentoFormComponent>
  ) {
    this.usuario = JSON.parse(this._userService.userSession).usuario;
  }

  ngOnInit(): void {
    this.inicializarForm();
    this.registrarEmitters();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      maquina: [
        {
          value: 0,
        }, [Validators.required]
      ],
      codServico: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      codModulo: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      codSubModulo: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      codCausa: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      codAcao: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      codDefeito: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      codTipoCausa: [
        {
          value: undefined
        }
      ],
      codGrupoCausa: [
        {
          value: undefined
        }
      ],
    });
  }

  private registrarEmitters(): void {
    this.tipoServicoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterTiposServico(this.tipoServicoFilterCtrl.value);
      });

    this.defeitoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterDefeitos(this.defeitoFilterCtrl.value);
      });

    this.acaoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterAcoes(this.acaoFilterCtrl.value);
      });

    this.causaFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterCausas(this.causaFilterCtrl.value);
      });
  }

  obterTiposServico(filter: string = ''): void {
    this._tipoServicoService.obterPorParametros({
      sortActive: 'nomeServico',
      sortDirection: 'asc',
      pageSize: 50,
      filter: filter
    }).subscribe((data: TipoServicoData) => {
      this.form.controls['codServico'].enable();

      let tiposServico = data.tiposServico.filter(
        t => t.codETipoServico.substring(0, 1) === String(this.form.controls['maquina'].value)
      );

      if (tiposServico.length) {
        this.tiposServico = tiposServico;
      }
    });
  }

  obterCausas(filter: string = ''): void {
    this._causaService.obterPorParametros({
      sortActive: 'codECausa',
      sortDirection: 'asc',
      pageSize: 50,
      filter: filter,
      indAtivo: 1
    }).subscribe((data: CausaData) => {
      this.causas = data.causas;
    });
  }

  obterDefeitos(filter: string = ''): void {
    this._defeitoService.obterPorParametros({
      sortActive: 'codEDefeito',
      sortDirection: 'asc',
      pageSize: 50,
      filter: filter,
      indAtivo: 1
    }).subscribe((data: DefeitoData) => {
      this.defeitos = data.defeitos;
    });
  }

  obterAcoes(filter: string = ''): void {
    this._acaoService.obterPorParametros({
      sortActive: 'codEAcao',
      sortDirection: 'asc',
      pageSize: 50,
      filter: filter,
      indAtivo: 1
    }).subscribe((data: AcaoData) => {
      this.acoes = data.acoes;
    });
  }

  inserir(): void {
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

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
