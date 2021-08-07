import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TipoServicoService } from 'app/core/services/tipo-servico.service';
import { CausaService } from 'app/core/services/causa.service';
import { TipoServico, TipoServicoData } from 'app/core/types/tipo-servico.types';
import { Causa, CausaData } from 'app/core/types/causa.types';
import { DefeitoService } from 'app/core/services/defeito.service';
import { AcaoService } from 'app/core/services/acao.service';
import { Defeito, DefeitoData } from 'app/core/types/defeito.types';
import { Acao, AcaoData } from 'app/core/types/acao.types';
import { TipoCausaService } from 'app/core/services/tipo-causa.service';
import { GrupoCausaService } from 'app/core/services/grupo-causa.service';
import { TipoCausa, TipoCausaData } from 'app/core/types/tipo-causa.types';
import { GrupoCausa, GrupoCausaData } from 'app/core/types/grupo-causa.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { RelatorioAtendimentoFormComponent } from '../relatorio-atendimento-form/relatorio-atendimento-form.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
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
  moduloFilterCtrl: FormControl = new FormControl();
  subModuloFilterCtrl: FormControl = new FormControl();
  componenteFilterCtrl: FormControl = new FormControl();
  acaoFilterCtrl: FormControl = new FormControl();
  defeitoFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _tipoServicoService: TipoServicoService,
    private _causaService: CausaService,
    private _defeitoService: DefeitoService,
    private _acaoService: AcaoService,
    private _tipoCausaService: TipoCausaService,
    private _grupoCausaService: GrupoCausaService,
    private _userService: UserService,
    public dialogRef: MatDialogRef<RelatorioAtendimentoFormComponent>
  ) { 
    this.usuario = JSON.parse(this._userService.userSession).usuario;
  }

  ngOnInit(): void {
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
    });

    this.tipoServicoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterTiposServico(this.tipoServicoFilterCtrl.value);
      });

    this.moduloFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterModulos(this.moduloFilterCtrl.value);
      });

    this.subModuloFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterSubModulos(this.subModuloFilterCtrl.value);
      });

    this.componenteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterComponentes(this.componenteFilterCtrl.value);
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

    this.obterCausas();
  }

  inserirDetalhe(): void {
    
  }

  obterTiposServico(filter: string=''): void {
    this._tipoServicoService.obterPorParametros({
      sortActive: 'nomeServico',
      sortDirection: 'asc',
      pageSize: 500,
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

  obterModulos(filter: string=''): void {
    this.modulos = this.causas.filter(c => c.codECausa.substring(2, 5) === '000');
    
    if (filter) {
      let modulos = this.modulos
        .filter(c => c.codECausa.toLowerCase().includes(filter.toLowerCase()) || 
          c.nomeCausa.toLowerCase().includes(filter.toLowerCase()));

      if (modulos.length) {
        this.modulos = modulos;
      }
    }
  }

  obterSubModulos(filter: string=''): void {
    let codModulo = this.form.controls['codModulo'].value;
    let modulo = this.causas.filter(m => m.codCausa === codModulo).shift();

    this.subModulos = this.causas
      .filter(c => c.codECausa.substring(0, 2) === modulo.codECausa.substring(0, 2) && 
        c.codECausa.substring(3, 5) === '00');
    
    if (filter) {
      let subModulos = this.subModulos
        .filter(c => c.codECausa.toLowerCase().includes(filter.toLowerCase()) || 
          c.nomeCausa.toLowerCase().includes(filter.toLowerCase()));

      if (subModulos.length) {
        this.subModulos = subModulos;
      }
    }
  }

  obterComponentes(filter: string=''): void {
    let codSubModulo = this.form.controls['codSubModulo'].value;
    let subModulo = this.causas.filter(s => s.codCausa === codSubModulo).shift();

    this.componentes = this.causas
      .filter(c => c.codECausa.substring(0, 3) === subModulo.codECausa.substring(0, 3));
    
    if (filter) {
      let componentes = this.componentes
        .filter(c => c.codECausa.toLowerCase().includes(filter.toLowerCase()) || 
          c.nomeCausa.toLowerCase().includes(filter.toLowerCase()));

      if (componentes.length) this.componentes = componentes;
    }
  }

  obterCausas(filter: string=''): void {
    this._causaService.obterPorParametros({
      sortActive: 'codECausa',
      sortDirection: 'asc',
      pageSize: 1500,
      filter: filter,
      indAtivo: 1
    }).subscribe((data: CausaData) => {
      this.causas = data.causas;
    });
  }

  obterDefeitos(filter: string=''): void {
    this._defeitoService.obterPorParametros({
      sortActive: 'codEDefeito',
      sortDirection: 'asc',
      pageSize: 500,
      filter: filter,
      indAtivo: 1
    }).subscribe((data: DefeitoData) => {
      this.defeitos = data.defeitos;
    });
  }

  obterAcoes(filter: string=''): void {
    this._acaoService.obterPorParametros({
      sortActive: 'codEAcao',
      sortDirection: 'asc',
      pageSize: 500,
      filter: filter,
      indAtivo: 1
    }).subscribe((data: AcaoData) => {
      this.acoes = data.acoes;
    });
  }

  private obterTipoCausa(codETipoCausa: string): void {
    this._tipoCausaService.obterPorParametros({
      indAtivo: 1,
      codETipoCausa: codETipoCausa
    }).subscribe((data: TipoCausaData) => {
      this.tipoCausa = data.tiposCausa.shift();
    });
  }

  private obterGrupoCausa(codEGrupoCausa: string): void {
    this._grupoCausaService.obterPorParametros({
      indAtivo: 1,
      codEGrupoCausa: codEGrupoCausa
    }).subscribe((data: GrupoCausaData) => {
      this.grupoCausa = data.gruposCausa.shift();
    });
  }

  inserir(): void {
    let form = this.form.getRawValue();
    form.tipoServico = this.tiposServico.filter(ts => ts.codServico === form.codServico).shift();
    form.defeito = this.defeitos.filter(ts => ts.codDefeito === form.codDefeito).shift();
    form.acao = this.acoes.filter(ts => ts.codAcao === form.codAcao).shift();
    form.causa = this.causas.filter(ts => ts.codCausa === form.codCausa).shift();
    form.grupoCausa = this.grupoCausa;
    form.codGrupoCausa = this.grupoCausa?.codGrupoCausa;
    form.tipoCausa = this.tipoCausa;
    form.codTipoCausa = this.tipoCausa?.codTipoCausa;
    form.codUsuarioCad = this.usuario.codUsuario;
    form.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
    this.dialogRef.close(form);
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
