import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Autorizada } from 'app/core/types/autorizada.types';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, filter, first, map, takeUntil, tap } from 'rxjs/operators';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { Filial, FilialData } from 'app/core/types/filial.types';
import { FilialService } from 'app/core/services/filial.service';
import { Pais, PaisData } from 'app/core/types/pais.types';
import { PaisService } from 'app/core/services/pais.service';
import { UnidadeFederativa, UnidadeFederativaData } from 'app/core/types/unidade-federativa.types';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Cidade, CidadeData } from 'app/core/types/cidade.types';
import { CidadeService } from 'app/core/services/cidade.service';
import { TipoRota, TipoRotaData } from 'app/core/types/tipo-rota.types';
import { TipoRotaService } from 'app/core/services/tipo-rota.services';
import { GoogleGeolocationService } from 'app/core/services/google-geolocation.service';
import moment from 'moment';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-autorizada-form',
  templateUrl: './autorizada-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class AutorizadaFormComponent implements OnInit {
  codAutorizada: number;
  isAddMode: boolean;
  form: FormGroup;
  autorizada: Autorizada;
  userSession: UsuarioSessionData;
  filialFiltro: FormControl = new FormControl();
  cidadeFiltro: FormControl = new FormControl();
  tipoRotaFiltro: FormControl = new FormControl();
  filiais: Filial[] = [];
  paises: Pais[] = [];
  unidadesFederativas: UnidadeFederativa[] = [];
  cidades: Cidade[] = [];

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _autorizadaService: AutorizadaService,
    private _filialService: FilialService,
    private _paisService: PaisService,
    private _unidadeFederativaService: UnidadeFederativaService,
    private _cidadeService: CidadeService,
    private _tipoRotaService: TipoRotaService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
    private _googleGeolocationService: GoogleGeolocationService,
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit() {
    this.codAutorizada = +this._route.snapshot.paramMap.get('codAutorizada');
    this.isAddMode = !this.codAutorizada;
    this.inicializarForm();
    this.obterFilial();
    this.obterPaises();

    this.form.controls['codPais'].valueChanges.subscribe(async () => {
      this.obterUnidadesFederativas();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async () => {
      this.obterCidades();
    });

    this.form.controls['cep'].valueChanges.pipe(
      filter(cep => !!cep),
      debounceTime(700),
      delay(500),
      takeUntil(this._onDestroy),
      map(async cep => { 
        if (cep.length === 9) {
          this.obterLatLngPorCep(cep);
        }
      })
    ).subscribe(() => { });

    this.cidadeFiltro.valueChanges.pipe(
      filter(filtro => !!filtro),
      debounceTime(700),
      delay(500),
      takeUntil(this._onDestroy),
      map(async filtro => { this.obterCidades(filtro) })
    ).subscribe(() => { });

    if (!this.isAddMode) {
      this._autorizadaService.obterPorCodigo(this.codAutorizada)
        .pipe(first())
        .subscribe(autorizada => {
          this.autorizada = autorizada;
          this.form.patchValue(this.autorizada);          
          this.form.controls['codPais'].setValue(this.autorizada.cidade?.unidadeFederativa?.codPais);
          this.form.controls['codUF'].setValue(this.autorizada.cidade?.codUF);
        });
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codAutorizada: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nomeFantasia: [undefined, Validators.required],
      codFilial: [undefined, Validators.required],
      razaoSocial: [undefined, Validators.required],
      cnpj: [undefined, Validators.required],
      inscricaoEstadual: [undefined],
      cep: [undefined, Validators.required],
      endereco: [undefined, Validators.required],
      bairro: [undefined, Validators.required],
      codPais: [undefined, Validators.required],
      codUF: [undefined, Validators.required],
      codCidade: [undefined, Validators.required],
      latitude: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      longitude: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      fone: [undefined, Validators.required],
      indAtivo: [undefined],
      indFilialPerto: [undefined],
    });
  }

  obterFilial(): void {
    this._filialService.obterPorParametros({
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      pageSize: 50
    }).subscribe((data: FilialData) => {
      this.filiais = data.items;
    });
  }

  obterPaises(): void {
    this._paisService.obterPorParametros({
      pageSize: 150
    }).subscribe((paisData: PaisData) => {
      this.paises = paisData.items;
    });
  }

  obterUnidadesFederativas(): void {
    const codPais = this.form.controls['codPais'].value;

    this._unidadeFederativaService.obterPorParametros({
      pageSize: 50,
      codPais: codPais
    }).subscribe((data: UnidadeFederativaData) => {
      this.unidadesFederativas = data.items;
    })
  }

  obterCidades(filter: string = ''): void {
    const codUF = this.form.controls['codUF'].value;

    this._cidadeService.obterPorParametros({
      sortDirection: 'asc',
      sortActive: 'nomeCidade',
      filter: filter,
      pageSize: 1000,
      indAtivo: 1,
      codUF: codUF
    }).subscribe((data: CidadeData) => {
      this.cidades = data.cidades;
    });
  }

  private async obterLatLngPorCep(cep: string='') {
    const data = await this._googleGeolocationService.buscarPorEnderecoOuCEP(cep).toPromise();
    
    if (data.results.length) {
      const resultado = data.results.shift();

      this.form.controls['endereco'].setValue(resultado.formatted_address);
      this.form.controls['latitude'].setValue(resultado.geometry.location.lat);
      this.form.controls['longitude'].setValue(resultado.geometry.location.lng);

      const localidades: any = resultado.address_components.filter(ac => ac.types.includes('sublocality'));
      this.form.controls['bairro'].setValue(localidades.shift()?.long_name);
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.autorizada,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo,
        indFilialPerto: +form.indFilialPerto
      }
    };

    this._autorizadaService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Técnico atualizado com sucesso!", "success");
      this._location.back();
    });
  }

  private criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.autorizada,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo,
        indFilialPerto: +form.indFilialPerto
      }
    };

    this._autorizadaService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Técnico inserido com sucesso!", "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
