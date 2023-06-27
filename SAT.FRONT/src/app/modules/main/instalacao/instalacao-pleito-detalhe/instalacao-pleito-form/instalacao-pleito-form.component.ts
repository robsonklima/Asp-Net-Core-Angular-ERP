import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { InstalacaoPleitoService } from 'app/core/services/instalacao-pleito.service';
import { InstalacaoTipoPleitoService } from 'app/core/services/instalacao-tipo-pleito-service';
import { Contrato } from 'app/core/types/contrato.types';
import { InstalacaoPleito } from 'app/core/types/instalacao-pleito.types';
import { InstalacaoTipoPleito } from 'app/core/types/instalacao-tipo-pleito.types';
import { statusConst } from 'app/core/types/status-types';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import moment from 'moment';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-instalacao-pleito-form',
  templateUrl: './instalacao-pleito-form.component.html'
})
export class InstalacaoPleitoFormComponent implements OnInit {
  @Input() instalPleito: InstalacaoPleito;
  form: FormGroup;
  isAddMode: boolean;
  contratos: Contrato[] = [];
  contrato: Contrato;
  tiposPleito: InstalacaoTipoPleito[] = [];
  contratoFilterCtrl: FormControl = new FormControl();
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formbuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _instalPleitoService: InstalacaoPleitoService,
    private _instalacaoTipoPleitoService: InstalacaoTipoPleitoService,
    private _contratoService: ContratoService,
    private _userService: UserService
    ) {
      this.userSession = JSON.parse(this._userService.userSession);
    }  

  ngOnInit(): void {
    this.isAddMode = !this.instalPleito;
    this.obterTiposPleito();
    this.inicializarForm();
    this.registrarEmitters();
  }
  
  registrarEmitters() {
    this.contratoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(400),
				distinctUntilChanged()
			)
			.subscribe((query) =>
			{
				this.obterContratos(query);
			});
  }

  async obterContratos(query: string="") {
    const data = await this._contratoService
      .obterPorParametros({ 
        sortActive: "NomeContrato", 
        sortDirection: "asc", 
        indAtivo: statusConst.ATIVO, 
        filter: query})
      .toPromise();

    this.contratos = data?.items;
  }

  async obterTiposPleito() {
    const data = await this._instalacaoTipoPleitoService
      .obterPorParametros({ 
        sortActive: "NomeTipoPleito", 
        sortDirection: "asc" })
      .toPromise();

    this.tiposPleito = data?.items;
  }

  inicializarForm() {
    this.form = this._formbuilder.group({
      nomePleito: [undefined, Validators.required],
      descPleito: [undefined, Validators.required],
      codInstalTipoPleito: [undefined, Validators.required],
      codContrato: [undefined, Validators.required],
      dataEnvio: [undefined, Validators.required],
    });

    if (!this.isAddMode) {
      this.form.patchValue(this.instalPleito);
      this.contratos.push(this.instalPleito.contrato);

    } else {
      this.obterContratos();
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.instalPleito,
      ...form,
      ...{  
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indAtivo: statusConst.ATIVO
      }
    };

    this._instalPleitoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast(`Pleito ${obj.nomePleito} atualizado com sucesso!`, "success");
      this._location.back();
    });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.instalPleito,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: statusConst.ATIVO
      }
    };

    this._instalPleitoService.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Pleito ${obj.nomePleito} adicionado com sucesso!`, "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
