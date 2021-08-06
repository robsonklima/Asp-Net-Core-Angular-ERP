import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { ReplaySubject, Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { StatusServico, StatusServicoData } from 'app/core/types/status-servico.types';
import { Tecnico, TecnicoData } from 'app/core/types/tecnico.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
  selector: 'app-relatorio-atendimento-form',
  templateUrl: './relatorio-atendimento-form.component.html'
})
export class RelatorioAtendimentoFormComponent implements OnInit, OnDestroy {
  usuario: Usuario;
  codOS: number;
  codRAT: number;
  relatorioAtendimento: RelatorioAtendimento;
  form: FormGroup;
  stepperForm: FormGroup;
  isAddMode: boolean;
  public tecnicoFilterCtrl: FormControl = new FormControl();
  public tecnicos: ReplaySubject<Tecnico[]> = new ReplaySubject<Tecnico[]>(1);
  public statusServicoFilterCtrl: FormControl = new FormControl();
  public statusServicos: ReplaySubject<StatusServico[]> = new ReplaySubject<StatusServico[]>(1);

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _snack: CustomSnackbarService,
    private _relatorioAtendimentoService: RelatorioAtendimentoService,
    private _userService: UserService,
    private _statusServicoService: StatusServicoService,
    private _tecnicoService: TecnicoService,
  ) {
    this.usuario = JSON.parse(this._userService.userSession).usuario;
  }

  ngOnInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
    this.isAddMode = !this.codRAT;

    this.obterStatusServicos();
    this.obterTecnicos();

    this.tecnicoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged(),
      ).subscribe((query) => {
        if (query) {
          this.obterTecnicos();
        }
      });

    this.stepperForm = this._formBuilder.group({
      step1: this._formBuilder.group({
        codRAT: [
          {
            value: undefined,
            disabled: true,
          }, [Validators.required]
        ],
        numRAT: [undefined, [Validators.required]],
        codTecnico: [undefined, [Validators.required]],
        codStatusServico: [undefined, [Validators.required]],
        nomeAcompanhante: [undefined],
        data: [
          {
            value: moment(),
            disabled: false,
          }, [Validators.required]
        ],
        horaInicio: [moment().format('HH:mm'), [Validators.required]],
        horaFim: [undefined, [Validators.required]],
        obsRAT: [undefined],
      }),
      step2: this._formBuilder.group({
        //teste: [''],
      })
    });

    if (this.isAddMode) {
      this.relatorioAtendimento = new RelatorioAtendimento();
      this.relatorioAtendimento.codOS = this.codOS;
      this.relatorioAtendimento.relatorioAtendimentoDetalhes = [];
    } else {
      this._relatorioAtendimentoService.obterPorCodigo(this.codRAT)
        .pipe(first())
        .subscribe(res => {
          this.relatorioAtendimento = res;
          this.stepperForm.get('step1').get('horaInicio').setValue(moment(this.relatorioAtendimento.dataHoraInicio).format('HH:mm'));
          this.stepperForm.get('step1').get('horaFim').setValue(moment(this.relatorioAtendimento.dataHoraSolucao).format('HH:mm'));
          this.stepperForm.get('step1').patchValue(this.relatorioAtendimento);
        });
    }
  }

  obterTecnicos(): void {
    this._tecnicoService.obterPorParametros({
      indAtivo: 1,
      codPerfil: 35,
      filter: this.tecnicoFilterCtrl.value,
      pageSize: 500,
      sortActive: 'nome',
      sortDirection: 'asc'
    }).subscribe((data: TecnicoData) => {
      if (data.tecnicos.length) this.tecnicos.next(data.tecnicos.slice());
    })
  }

  obterStatusServicos(): void {
    this._statusServicoService.obterPorParametros({
      indAtivo: 1,
      filter: this.statusServicoFilterCtrl.value,
      sortActive: 'nomeStatusServico',
      sortDirection: 'asc',
      pageSize: 50,
    }).subscribe((data: StatusServicoData) => {
      if (data.statusServico.length) this.statusServicos
        .next(data.statusServico.filter(s => s.codStatusServico !== 2 && s.codStatusServico !== 1).slice());
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form = this.form.getRawValue();

    form.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
    form.codUsuarioManut = this.usuario.codUsuario;

    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean"
        ? this.relatorioAtendimento[key] = +form[key]
        : this.relatorioAtendimento[key] = form[key];
    });

    this._relatorioAtendimentoService.atualizar(this.relatorioAtendimento).subscribe(() => {
      this._snack.exibirToast('Chamado atualizado com sucesso!', 'success');
      this._router.navigate([`/ordem-servico/detalhe/${this.codOS}`]);
    });
  }

  criar(): void {
    const form = this.form.getRawValue();

    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean"
        ? this.relatorioAtendimento[key] = +form[key]
        : this.relatorioAtendimento[key] = form[key];
    });

    this.relatorioAtendimento.codOS = this.codOS;
    this.relatorioAtendimento.dataHoraInicio = moment(`${form.data.format('YYYY-MM-DD')} ${form.horaInicio}`).format('YYYY-MM-DD HH:mm:ss');
    this.relatorioAtendimento.dataHoraInicioValida = this.relatorioAtendimento.dataHoraInicio;
    this.relatorioAtendimento.dataHoraSolucao = moment(`${form.data.format('YYYY-MM-DD')} ${form.horaFim}`).format('YYYY-MM-DD HH:mm:ss');
    this.relatorioAtendimento.dataHoraSolucaoValida = this.relatorioAtendimento.dataHoraSolucao;
    this.relatorioAtendimento.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
    this.relatorioAtendimento.codUsuarioCad = this.usuario.codUsuario;
    this.relatorioAtendimento.codUsuarioCadastro = this.usuario.codUsuario;

    this._relatorioAtendimentoService.criar(this.relatorioAtendimento).subscribe(() => {
      this._snack.exibirToast('Chamado adicionado com sucesso!', 'success');
      this._router.navigate([`/ordem-servico/detalhe/${this.codOS}`]);
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
