import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Equipamento } from 'app/core/types/equipamento.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';
import { GrupoEquipamento } from 'app/core/types/grupo-equipamento.types';
import { TipoEquipamento } from 'app/core/types/tipo-equipamento.types';
import { GrupoEquipamentoService } from 'app/core/services/grupo-equipamento.service';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';

@Component({
  selector: 'app-equipamento-form',
  templateUrl: './equipamento-form.component.html'
})
export class EquipamentoFormComponent implements OnInit, OnDestroy {
  codEquip: number;
  equipamento: Equipamento;
  isAddMode: boolean;
  form: FormGroup;
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  public gruposEquip: GrupoEquipamento[] = [];
  public tiposEquip: TipoEquipamento[] = [];
  public loading: boolean = true;

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _equipamentoService: EquipamentoService,
    private _location: Location,
    private _grupoEquipamentoService: GrupoEquipamentoService,
    private _tipoEquipamentoService: TipoEquipamentoService,
    private _cdr: ChangeDetectorRef
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codEquip = +this._route.snapshot.paramMap.get('codEquip');
    this.isAddMode = !this.codEquip;
    this.inicializaForm();

    this.tiposEquip = (await this._tipoEquipamentoService.obterPorParametros({}).toPromise()).items;

    this.form.controls['codTipoEquip'].valueChanges.subscribe(async () => {
      this.gruposEquip = [];
      this.gruposEquip = (await this._grupoEquipamentoService.obterPorParametros({ codTipoEquip: this.form.controls['codTipoEquip'].value }).toPromise()).items;
      this._cdr.detectChanges();
    });

    if (!this.isAddMode) {
      this._equipamentoService.obterPorCodigo(this.codEquip)
        .pipe(first())
        .subscribe(data => {
          this.equipamento = data;
          this.form.patchValue(data);
        });
    }

    this.loading = false;
  }

  private inicializaForm() {
    this.form = this._formBuilder.group({
      codEquip: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nomeEquip: [undefined, [Validators.required, Validators.maxLength(50)]],
      codEEquip: [undefined, [Validators.required, Validators.maxLength(30)]],
      descEquip: [undefined, [Validators.required, Validators.maxLength(100)]],
      codGrupoEquip: [undefined, Validators.required],
      codTipoEquip: [undefined, Validators.required]
    });
  }

  salvar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.equipamento,
      ...form
    };

    if (this.isAddMode) {
      obj.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
      obj.codUsuarioCad = this.userSession.usuario.codUsuario;
      this._equipamentoService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Equipamento ${obj.nomeEquip} adicionado com sucesso!`, "success");
        this._location.back();
      });
    }
    else {
      obj.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
      obj.codUsuarioManut = this.userSession.usuario.codUsuario;
      this._equipamentoService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Equipamento ${obj.nomeEquip} atualizado com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
