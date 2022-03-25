import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Cidade } from 'app/core/types/cidade.types';
import { Pais } from 'app/core/types/pais.types';
import { UnidadeFederativa } from 'app/core/types/unidade-federativa.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first, } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { AcordoNivelServico } from 'app/core/types/acordo-nivel-servico.types';
import { AcordoNivelServicoService } from 'app/core/services/acordo-nivel-servico.service';

@Component({
  selector: 'app-sla-form',
  templateUrl: './sla-form.component.html'
})
export class SLAFormComponent implements OnInit, OnDestroy {

  private userSession: UsuarioSessao;

  protected _onDestroy = new Subject<void>();
  public sla: AcordoNivelServico;
  public loading: boolean = true;
  public codSLA: number;
  public isAddMode: boolean;
  public form: FormGroup;

  public buscandoCEP: boolean = false;
  public paises: Pais[] = [];
  public unidadesFederativas: UnidadeFederativa[] = [];
  public cidades: Cidade[] = [];

  cidadeFiltro: FormControl = new FormControl();
  clienteFilterCtrl: FormControl = new FormControl();
  contratosFilterCtrl: FormControl = new FormControl();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _location: Location,
    private _slaService: AcordoNivelServicoService
  ) { this.userSession = JSON.parse(this._userService.userSession); }

  async ngOnInit() {
    this.codSLA = +this._route.snapshot.paramMap.get('codSLA');
    this.isAddMode = !this.codSLA;
    this.inicializarForm();

    if (!this.isAddMode) {
      this.carregarDadosSLA();
    }

    this.loading = false;
  }

  private carregarDadosSLA() {
    this._slaService.obterPorCodigo(this.codSLA)
      .pipe(first())
      .subscribe(data => {
        this.sla = data;
        this.form.patchValue(data);
      });
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codSLA: [
        {
          value: undefined,
          disabled: true
        }
      ],
      nomeSLA: [undefined, Validators.required],
      descSLA: [undefined, Validators.required],
      tempoInicio: [undefined],
      tempoReparo: [undefined],
      tempoSolucao: [undefined],
      indAgendamento: [undefined],
      indHorasUteis: [undefined],
      indFeriado: [undefined],
      indSegunda: [undefined],
      indTerca: [undefined],
      indQuarta: [undefined],
      indQuinta: [undefined],
      indSexta: [undefined],
      indSabado: [undefined],
      indDomingo: [undefined]
    });
  }

  public salvar(): void {

    const form = this.form.getRawValue();
    let obj = {
      ...this.sla,
      ...form
    };

    if (this.isAddMode) {
      obj.codUsuarioCad = this.userSession.usuario.codUsuario;
      obj.dataCadastro = moment().format('YYYY-MM-DD HH:mm:ss');
      obj.horarioInicio = moment().format('YYYY-MM-DD HH:mm:ss');
      obj.horarioFim = moment().format('YYYY-MM-DD HH:mm:ss');

      this._slaService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`SLA adicionada com sucesso!`, "success");
        this._location.back();
      });
    } else {
      obj.codUsuarioManutencao = this.userSession.usuario.codUsuario;
      obj.dataManutencao = moment().format('YYYY-MM-DD HH:mm:ss');

      this._slaService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`SLA atualizada com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
