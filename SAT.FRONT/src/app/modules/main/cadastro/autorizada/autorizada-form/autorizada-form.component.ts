import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Autorizada } from 'app/core/types/autorizada.types';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
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
  public filialFiltro: FormControl = new FormControl();
  public paisFiltro: FormControl = new FormControl();
  public unidadeFederativaFiltro: FormControl = new FormControl();
  public cidadeFiltro: FormControl = new FormControl();
  public tipoRotaFiltro: FormControl = new FormControl();
  public filiais: Filial[];
  public paises: Pais[];
  public unidadesFederativas: UnidadeFederativa[];
  public cidades: Cidade[];
  public tiposRota: TipoRota[];

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
  ) { }

  ngOnInit() {
    this.codAutorizada = +this._route.snapshot.paramMap.get('codAutorizada');
    this.isAddMode = !this.codAutorizada;
    this.obterFilial();
    this.obterPaises();
    this.obterUnidadesFederativas();
    this.obterCidades();
    this.obterTiposRota();

    this.form = this._formBuilder.group({
      codAutorizada: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nomeAutorizada: ['', Validators.required],
      codFilial: ['', Validators.required],      
      razaoSocial: ['', Validators.required],
      nomeFantasia: ['', Validators.required],
      cnpj: ['', Validators.required],
      inscricaoEstadual: ['', Validators.required],
      cep: ['', Validators.required],
      endereco: ['', Validators.required],
      bairro: ['', Validators.required],
      codPais: ['', Validators.required],
      nomePais: [''],
      codUF: ['', Validators.required],
      siglaUF: [''],
      codCidade: ['', Validators.required],
      nomeCidade: [''],
      codTipoRota: ['', Validators.required],
      nomeTipoRota: [''],
      latitude: [''],
      longitude: [''],
      fone: ['', Validators.required],
      fax: ['', Validators.required],
      indAtivo: [''],
      indFilialPerto: [''],      
    });

    this.filialFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterFilial(this.filialFiltro.value);
      });

    this.paisFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterPaises(this.paisFiltro.value);
      });

    this.unidadeFederativaFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() =>{
        this.obterUnidadesFederativas(this.unidadeFederativaFiltro.value);
      });

    this.cidadeFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()        
      )
      .subscribe(() => {
        this.obterCidades(this.cidadeFiltro.value);
      })

    this.tipoRotaFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()            
      )
      .subscribe(()=>{
        this.obterTiposRota(this.tipoRotaFiltro.value)
      })

    if (!this.isAddMode) {
        this._autorizadaService.obterPorCodigo(this.codAutorizada)
        .pipe(first())
        .subscribe(autorizada => {
          this.autorizada = autorizada;
          this.form.patchValue(this.autorizada);          
        });
      } 
  }

  obterFilial(filter: string = ''): void {
    this._filialService.obterPorParametros({
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      filter: filter,
      pageSize: 50
    }).subscribe((data: FilialData) => {
      if (data.filiais.length) this.filiais = data.filiais;
    });
  }

  obterPaises(filter: string = ''): void {
    this._paisService.obterPorParametros({
      filter: filter,
      pageSize: 50
    }).subscribe((paisData: PaisData) => {
      if (paisData.paises.length) this.paises = paisData.paises;
    });
  }

  obterUnidadesFederativas(filter: string = ''): void{
    this._unidadeFederativaService.obterPorParametros({
      filter: filter,
      pageSize: 50
    }).subscribe((unidadeFederativaData: UnidadeFederativaData) => {
      if (unidadeFederativaData.unidadesFederativas.length) this.unidadesFederativas = unidadeFederativaData.unidadesFederativas;
    })
  }

  obterCidades(filter: string = ''): void {
    this._cidadeService.obterPorParametros({
      filter: filter,
      pageSize: 50
    }).subscribe((cidadeData: CidadeData) => {
      if (cidadeData.cidades.length) this.cidades = cidadeData.cidades;
    });
  }

  obterTiposRota(filter: string = ''): void{
    this._tipoRotaService.obterPorParametros({
      filter: filter,
      pageSize: 50
    }).subscribe((tipoRotaData: TipoRotaData) =>{
      if(tipoRotaData.tiposRota.length) this.tiposRota = tipoRotaData.tiposRota;
    });
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
