import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Location } from '@angular/common';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, map, takeUntil } from 'rxjs/operators';
import { Cliente } from 'app/core/types/cliente.types';
import { ClienteService } from 'app/core/services/cliente.service';
import { ClientePeca } from 'app/core/types/cliente-peca.types';
import { ClientePecaService } from 'app/core/services/cliente-peca.service';
import { PecaService } from 'app/core/services/peca.service';
import { Peca } from 'app/core/types/peca.types';
import { Contrato } from 'app/core/types/contrato.types';
import { ContratoService } from 'app/core/services/contrato.service';

@Component({
  selector: 'app-cliente-peca-form',
  templateUrl: './cliente-peca-form.component.html'
})
export class ClientePecaFormComponent implements OnInit {

  public codClientePeca: number;
  public isAddMode: boolean;
  public pecaStatus: any[] = [];
  public pecas: Peca[] = [];
  public clientes: Cliente[] = [];
  public contratos: Contrato[] = [];
  public pecasFiltro: FormControl = new FormControl();
  public clientesFiltro: FormControl = new FormControl();
  public contratosFiltro: FormControl = new FormControl();
  public _onDestroy = new Subject<void>();
  public form: FormGroup;
  private userSession: UsuarioSessao;
  private clientePeca: ClientePeca;

  constructor(
    private _formBuilder: FormBuilder,
    private _clientePecaService: ClientePecaService,
    private _pecaService: PecaService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _clienteService: ClienteService,
    private _contratoService: ContratoService,
    private _location: Location,
    private _snack: CustomSnackbarService,
    private _cdr: ChangeDetectorRef) { this.userSession = JSON.parse(this._userService.userSession) }

  async ngOnInit() {
    this.codClientePeca = +this._route.snapshot.paramMap.get('codClientePeca');
    this.isAddMode = !this.codClientePeca;

    this.inicializarForm();
    await this.obterPecas();
    await this.obterClientes();
    await this.obterContratos();

    this.pecasFiltro
      .valueChanges
      .pipe(filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => (await this._pecaService.obterPorParametros({
          sortActive: 'codMagnus',
          sortDirection: 'asc',
          filter: query,
          pageSize: 100,
        }).toPromise()).items.slice()),
        delay(500),
        takeUntil(this._onDestroy))
      .subscribe(async data =>
        this.pecas = await data);

    if (this.isAddMode) return;

    const data = await this._clientePecaService
      .obterPorCodigo(this.codClientePeca)
      .toPromise();
    this.form.patchValue(data);
    this.obterPecas(data.peca?.codMagnus);
    this.clientePeca = data;
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codClientePeca: [
        {
          value: undefined,
          disabled: true
        }
      ],
      codPeca: [undefined, Validators.required],
      codCliente: [undefined, Validators.required],
      codContrato: [undefined, Validators.required],
      valorUnitario: [undefined],
      valorIPI: [undefined],
      vlrSubstituicaoNovo: [undefined],
      vlrBaseTroca: [undefined]
    });
  }

  private async obterPecas(codMagnus: string = '') {
    this.pecas = (await this._pecaService.obterPorParametros({
      sortActive: 'codMagnus',
      sortDirection: 'asc',
      pageSize: 100,
      codMagnus: codMagnus
    }).toPromise()).items;
  }

  private async obterClientes() {
    this.clientes = (await this._clienteService.obterPorParametros({
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      //pageSize: 100,
    }).toPromise()).items;

    this.clientesFiltro
      .valueChanges
      .pipe(filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => (await this._clienteService.obterPorParametros({
          sortActive: 'nomeFantasia',
          sortDirection: 'asc',
          filter: query,
          //pageSize: 100
        }).toPromise()).items.slice()),
        delay(500),
        takeUntil(this._onDestroy))
      .subscribe(async data =>
        this.clientes = await data);


    this.form.controls['codCliente'].valueChanges.subscribe(async () => {
      this.contratos = [];
      this.contratos = (await this._contratoService.obterPorParametros({ codCliente: this.form.controls['codCliente'].value }).toPromise()).items;
      this._cdr.detectChanges();
    });
  }

  private async obterContratos() {
    this.contratosFiltro
      .valueChanges
      .pipe(filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => (await this._contratoService.obterPorParametros({
          sortActive: 'nomeContrato',
          sortDirection: 'asc',
          filter: query,
          //  pageSize: 100
        }).toPromise()).items.slice()),
        delay(500),
        takeUntil(this._onDestroy))
      .subscribe(async data =>
        this.contratos = await data);
  }

  public salvar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.clientePeca,
      ...form,
      ...{
        valorUnitario: +form.valorUnitario,
        valorIPI: +form.valorIPI,
        vlrSubstituicaoNovo: +form.vlrSubstituicaoNovo,
        vlrBaseTroca: +form.vlrBaseTroca
      }
    };

    if (this.isAddMode) {
      obj.codUsuarioCad = this.userSession.usuario.codUsuario;
      obj.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');

      this._clientePecaService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Cliente adicionado com sucesso!`, "success");
        this._location.back();
      });
    } else {
      obj.codUsuarioManut = this.userSession.usuario.codUsuario;
      obj.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

      this._clientePecaService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Cliente atualizado com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  public ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}