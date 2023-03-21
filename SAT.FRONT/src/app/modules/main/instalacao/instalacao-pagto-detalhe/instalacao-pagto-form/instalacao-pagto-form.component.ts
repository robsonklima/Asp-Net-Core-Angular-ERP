import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Contrato } from 'app/core/types/contrato.types';
import { InstalacaoPagto } from 'app/core/types/instalacao-Pagto.types';
import { statusConst } from 'app/core/types/status-types';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import moment from 'moment';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { InstalacaoTipoParcela } from 'app/core/types/instalacao-tipo-parcela.types';
import { InstalacaoTipoParcelaService } from 'app/core/services/instalacao-tipo-parcela.service';
import { InstalacaoPagtoService } from 'app/core/services/instalacao-pagto-service';

@Component({
  selector: 'app-instalacao-pagto-form',
  templateUrl: './instalacao-pagto-form.component.html'
})
export class InstalacaoPagtoFormComponent implements OnInit {
  @Input() instalPagto: InstalacaoPagto;
  form: FormGroup;
  isAddMode: boolean;
  contratos: Contrato[] = [];
  tiposParcela: InstalacaoTipoParcela[] = [];
  contratoFilterCtrl: FormControl = new FormControl();
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formbuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _instalPagtoService: InstalacaoPagtoService,
    private _instalacaoTipoParcelaService: InstalacaoTipoParcelaService,
    private _contratoService: ContratoService,
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.isAddMode = !this.instalPagto;
    this.obterTiposParcela();
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
      .subscribe((query) => {
        this.obterContratos(query);
      });
  }

  async obterContratos(query: string = "") {
    const data = await this._contratoService
      .obterPorParametros({
        sortActive: "NomeContrato",
        sortDirection: "asc",
        indAtivo: statusConst.ATIVO,
        filter: query
      })
      .toPromise();

    this.contratos = data?.items;
  }

  async obterTiposParcela() {
    const data = await this._instalacaoTipoParcelaService
      .obterPorParametros({
        sortActive: "NomeTipoParcela",
        sortDirection: "asc"
      })
      .toPromise();

    this.tiposParcela = data?.items;
  }

  inicializarForm() {
    this.form = this._formbuilder.group({
      codContrato: [undefined, Validators.required],
      dataPagto: [undefined, Validators.required],
      vlrPagto: [undefined, Validators.required]
    });

    if (!this.isAddMode) {
      this.form.patchValue(this.instalPagto);
      this.contratos.push(this.instalPagto.contrato);
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
      ...this.instalPagto,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indAtivo: statusConst.ATIVO
      }
    };

    this._instalPagtoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast(`Pagto ${obj.nomePagto} atualizado com sucesso!`, "success");
      this._location.back();
    });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.instalPagto,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario
      }
    };

    this._instalPagtoService.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Pagto ${obj.nomePagto} adicionado com sucesso!`, "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
