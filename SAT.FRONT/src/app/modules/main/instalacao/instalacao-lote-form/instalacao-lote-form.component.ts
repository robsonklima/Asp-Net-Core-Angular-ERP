import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { InstalacaoLoteService } from 'app/core/services/instalacao-lote.service';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { InstalacaoLote } from 'app/core/types/instalacao-lote.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-lote-form',
  templateUrl: './instalacao-lote-form.component.html'
})
export class InstalacaoLoteFormComponent implements OnInit, OnDestroy {
  codContrato: number;
  codInstalLote: number;
  instalacaoLote: InstalacaoLote;
  contratos: Contrato[] = [];
  isAddMode: boolean;
  form: FormGroup;
  userSession: UsuarioSessao;
  contratoFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _instalacaoLoteSvc: InstalacaoLoteService,
    private _contratoSvc: ContratoService,
    private _location: Location,
    private _userService: UserService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {    
    this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
    this.isAddMode = true;
    this.obterContratos();
    this.inicializarForm();

    if (!this.isAddMode) {
      this._instalacaoLoteSvc.obterPorCodigo(this.codInstalLote)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.instalacaoLote = data;
        });
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codInstalLote: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nomeLote: [undefined, Validators.required],
      descLote: [undefined, Validators.required],
      codContrato: this.codContrato,
      dataRecLote: [undefined, Validators.required],
      qtdEquipLote: [undefined, Validators.required],
    });
  }

	private async obterContratos(filter: string = '') {
		var idContrato = this.codContrato ?? null;

		this.contratos = (await this._contratoSvc.obterPorParametros({
			filter: filter,
			indAtivo: statusConst.ATIVO,
      codContrato: idContrato,
			pageSize: 500,
			sortActive: 'nomeContrato',
			sortDirection: 'asc'
		}).toPromise()).items;

		this.contratoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe(() =>
      {
        this.obterContratos(this.contratoFilterCtrl.value);
      });
	}  

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.instalacaoLote,
      ...form,
      ...{  
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indAtivo: statusConst.ATIVO
      }
    };

    this._instalacaoLoteSvc.atualizar(obj).subscribe(() => {
      this._snack.exibirToast(`Lote ${obj.nome} atualizado com sucesso!`, "success");
      this._location.back();
    });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.instalacaoLote,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: statusConst.ATIVO
      }
    };

    this._instalacaoLoteSvc.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Lote ${obj.nomeLote} adicionado com sucesso!`, "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
