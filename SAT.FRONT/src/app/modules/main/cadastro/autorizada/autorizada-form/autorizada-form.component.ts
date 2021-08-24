import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Autorizada } from 'app/core/types/autorizada.types';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { Filial } from 'app/core/types/filial.types';
import { FilialService } from 'app/core/services/filial.service';
import { Pais } from 'app/core/types/pais.types';
import { PaisService } from 'app/core/services/pais.service';
import { UnidadeFederativa } from 'app/core/types/unidade-federativa.types';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Cidade, CidadeData, CidadeParameters } from 'app/core/types/cidade.types';
import { CidadeService } from 'app/core/services/cidade.service';
import { Subject } from 'rxjs';
import { UserService } from 'app/core/user/user.service';
import { fuseAnimations } from '@fuse/animations';
import { ScrollDispatcher } from '@angular/cdk/scrolling';
//import { GoogleGeolocationService } from 'app/core/services/google-geolocation.service';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-autorizada-form',
  templateUrl: './autorizada-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class AutorizadaFormComponent implements OnInit {
  codAutorizada: number;
  autorizada: Autorizada;
  form: FormGroup;
  isAddMode: boolean;
  usuario: any;
  cidadeFilterCtrl: FormControl = new FormControl();
  filiais: Filial[] = [];
  paises: Pais[] = [];
  unidadesFederativas: UnidadeFederativa[] = [];
  loading: any = {
    status: false,
    ref: ''
  };

  public unidadesFederativaFiltro: FormControl = new FormControl();
  public ufs: UnidadeFederativa[] = [];
  public cidadeFiltro: FormControl = new FormControl();
  public cidades: Cidade[] = [];

  public searching = false;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _location: Location,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _autorizadaService: AutorizadaService,
    private _filialService: FilialService,
    private _paisService: PaisService,
    private _unidadeFederativaService: UnidadeFederativaService,
    private _cidadeService: CidadeService,
    private _snack: CustomSnackbarService,
    //private _googleGeolocationService: GoogleGeolocationService,
    readonly sd: ScrollDispatcher    
  ) {
    this.usuario = JSON.parse(this._userService.userSession).usuario;
  }

  async ngOnInit() {
    this.codAutorizada = +this._route.snapshot.paramMap.get('codAutorizada');
    this.isAddMode = !this.codAutorizada;
    this.inicializarForm();    

    this.filiais = (await this._filialService
      .obterPorParametros({
        indAtivo: 1,
        pageSize: 500,
        sortActive: 'nomeFilial',
        sortDirection: 'asc'
      }).toPromise()).filiais;

    this.paises = (await this._paisService
      .obterPorParametros({
        pageSize: 100,
        sortActive: 'nomePais',
        sortDirection: 'asc'
      }).toPromise()).paises;

    this.form.controls['codPais'].valueChanges.subscribe(async codPais => {
      const data = await this._unidadeFederativaService.obterPorParametros({
        sortActive: 'nomeUF',
        sortDirection: 'asc',
        codPais: codPais,
        pageSize: 1000
      }).toPromise();

      this.ufs = data.unidadesFederativas.slice();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async codUF => {
      const data = await this._cidadeService.obterPorParametros({
        sortActive: 'nomeCidade',
        sortDirection: 'asc',
        indAtivo: 1,
        codUF: codUF,
        pageSize: 1000
      }).toPromise();

      //console.log(codUF);
      //console.log(data);
      this.cidades = data.cidades.slice();
    });    

    this.form.controls['cep'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text => { 
        if (text.length === 9) {
          const cep = this.form.controls['cep']?.value || '';
          
          //this.obterLatLngPorEndereco(cep);
        }
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['numero'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text => { 
        const endereco = this.form.controls['endereco']?.value || '';
        const numero = this.form.controls['numero']?.value || '';
        const codCidade = this.form.controls['codCidade'].value;
        const cidade = (await this._cidadeService.obterPorCodigo(codCidade).toPromise());
        const query = `${endereco}, ${numero}, ${cidade.nomeCidade}`;
        //this.obterLatLngPorEndereco(query);
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    if (!this.isAddMode) {
      this.autorizada = await this._autorizadaService.obterPorCodigo(this.codAutorizada).toPromise();
      console.log(this.autorizada);
      this.form.patchValue(this.autorizada);
      this.form.controls['codPais'].setValue(this.autorizada?.cidade?.unidadeFederativa?.codPais);
      this.form.controls['codUF'].setValue(this.autorizada?.cidade?.codUF);
      this.form.controls['codCidade'].setValue(this.autorizada?.codCidade);
      //console.log(this.autorizada?.codCidade); 
    }

    this.unidadesFederativas = (await this._unidadeFederativaService
      .obterPorParametros({
        pageSize: 500,
        sortActive: 'nomeUF',
        sortDirection: 'asc',
        codPais: 1
      }).toPromise()).unidadesFederativas;

    this.cidades = (await this._cidadeService
      .obterPorParametros({
        indAtivo: 1,
        pageSize: 500,
        sortActive: 'nomeCidade',
        sortDirection: 'asc'
      }).toPromise()).cidades;
  }

  inicializarForm(): void {
    this.form = this._formBuilder.group({
      codAutorizada: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nomeAutorizada: [undefined, Validators.required],
      codFilial: [undefined, Validators.required],
      razaoSocial: [undefined, Validators.required],
      nomeFantasia: [undefined, Validators.required],
      cnpj: [undefined, Validators.required],
      inscricaoEstadual: [undefined, Validators.required],
      cep: [undefined, Validators.required],
      endereco: [undefined, Validators.required],
      bairro: [undefined, Validators.required],
      codPais: [undefined, Validators.required],
      nomePais: [undefined],
      codUF: [undefined, Validators.required],
      siglaUF: [undefined],
      codCidade: [undefined, Validators.required],
      nomeCidade: [undefined],
      latitude: [undefined],
      longitude: [undefined],
      fone: [undefined, Validators.required],
      fax: [undefined, Validators.required],
      indAtivo: [undefined],
      indFilialPerto: [undefined],
    });
  }

  private async obterCidades(filtro: string = '') {
    const codUF = this.form.controls['codUF'].value;

    const params: CidadeParameters = {
      sortActive: 'nomeCidade',
      sortDirection: 'asc',
      indAtivo: 1,
      codUF: codUF,
      pageSize: 1000,
      filter: filtro
    }

    const data = await this._cidadeService.obterPorParametros(params).toPromise();
    this.cidades = data.cidades;
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean"
        ? this.autorizada[key] = +form[key]
        : this.autorizada[key] = form[key];
    });

    this._autorizadaService.atualizar(this.autorizada).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    });
  }

  criar(): void {
    const form: any = this.form.getRawValue();

    Object.keys(form).forEach((key, i) => {
      typeof form[key] == "boolean" ? form[key] = +form[key] : form[key] = form[key];
    });

    this._autorizadaService.criar(form).subscribe(() => {
      this._snack.exibirToast("Registro inserido com sucesso!", "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next(); 
    this._onDestroy.complete();
  }
}