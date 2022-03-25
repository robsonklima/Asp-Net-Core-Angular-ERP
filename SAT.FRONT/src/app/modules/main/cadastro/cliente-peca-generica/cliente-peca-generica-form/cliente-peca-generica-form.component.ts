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
import { PecaService } from 'app/core/services/peca.service';
import { Peca } from 'app/core/types/peca.types';
import { Contrato } from 'app/core/types/contrato.types';
import { ClientePecaGenerica } from 'app/core/types/cliente-peca-generica.types';
import { ClientePecaGenericaService } from 'app/core/services/cliente-peca-generica.service';

@Component({
  selector: 'app-cliente-peca-generica-form',
  templateUrl: './cliente-peca-generica-form.component.html'
})
export class ClientePecaGenericaFormComponent implements OnInit {

  public codClientePecaGenerica: number;
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
  private clientePecaGenerica: ClientePecaGenerica;

  constructor(
    private _formBuilder: FormBuilder,
    private _clientePecaService: ClientePecaGenericaService,
    private _pecaService: PecaService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _location: Location,
    private _snack: CustomSnackbarService,
    private _cdr: ChangeDetectorRef) { this.userSession = JSON.parse(this._userService.userSession) }

  async ngOnInit() {
    this.codClientePecaGenerica = +this._route.snapshot.paramMap.get('codClientePecaGenerica');
    this.isAddMode = !this.codClientePecaGenerica;

    this.inicializarForm();
    await this.obterPecas();

    if (this.isAddMode) return;

    const data = await this._clientePecaService
      .obterPorCodigo(this.codClientePecaGenerica)
      .toPromise();
    this.form.patchValue(data);
    await this.obterPecas(data.peca?.codMagnus);
    this.clientePecaGenerica = data;
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codClientePeca: [
        {
          value: 0,
          disabled: true
        }
      ],
      codPeca: [undefined, Validators.required],
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
  }

  public salvar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.clientePecaGenerica,
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