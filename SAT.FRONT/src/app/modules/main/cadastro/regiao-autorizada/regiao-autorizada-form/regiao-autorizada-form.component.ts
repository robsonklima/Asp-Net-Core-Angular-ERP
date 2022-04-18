import { ActivatedRoute } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { RegiaoAutorizada } from 'app/core/types/regiao-autorizada.types';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { RegiaoService } from 'app/core/services/regiao.service';
import { CidadeService } from 'app/core/services/cidade.service';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { Regiao, RegiaoParameters } from 'app/core/types/regiao.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { FilialService } from 'app/core/services/filial.service';

@Component({
  selector: 'app-autorizada-regiao-form',
  templateUrl: './regiao-autorizada-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class RegiaoAutorizadaFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  regiaoAutorizada: RegiaoAutorizada;
  codRegiao: number;
  codAutorizada: number;
  codFilial: number;
  regioes: Regiao[] = [];
  autorizadas: Autorizada[] = [];
  cidades: Cidade[] = [];
  filiais: Filial[] = [];
  isAddMode: boolean;
  form: FormGroup;
  regioesFilterCtrl: FormControl = new FormControl();
  autorizadasFilterCtrl: FormControl = new FormControl();
  filiaisFilterCtrl: FormControl = new FormControl();
  cidadesFilterCtrl: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _regiaoService: RegiaoService,
    private _cidadeService: CidadeService,
    private _filialService: FilialService,
    private _autorizadaService: AutorizadaService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codRegiao = +this._route.snapshot.paramMap.get('codRegiao');
    this.codAutorizada = +this._route.snapshot.paramMap.get('codAutorizada');
    this.codFilial = +this._route.snapshot.paramMap.get('codFilial');
    this.isAddMode = !this.codRegiao;

    this.registrarEmitters();
    

    this.form = this._formBuilder.group({
      codRegiao: ['', Validators.required],
      codAutorizada: ['', Validators.required],
      codFilial: ['', Validators.required],
      codCidade: ['', Validators.required],
      pa: ['', Validators.required],
      indAtivo: [''],
    });

    if (!this.isAddMode) {
      this._regiaoAutorizadaService.obterPorCodigo(this.codRegiao, this.codAutorizada, this.codFilial)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.obterRegioes(data.regiao?.nomeRegiao);
          this.obterAutorizadas(data.autorizada?.nomeFantasia);
          this.obterCidades(data.cidade?.nomeCidade);
          this.obterFiliais(data.filial?.nomeFilial);        
          this.regiaoAutorizada = data;
        })
    } else {
      this.obterRegioes();
      this.obterAutorizadas();
      this.obterCidades();
      this.obterFiliais();
    }
  }

  private registrarEmitters() {
    this.regioesFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe((value) => {
        this.obterRegioes(value);
      });

    this.autorizadasFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe((value) => {
        this.obterAutorizadas(value);
      });

    this.filiaisFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe((value) => {
        this.obterFiliais(value);
      });

    this.cidadesFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe((value) => {
        this.obterCidades(value);
      });
  }

  private async obterRegioes(nome: string='') {
    const params: RegiaoParameters = {
      indAtivo: 1,
      sortActive: 'nomeRegiao',
      sortDirection: 'ASC',
      filter: nome,
      pageSize: 50
    };

    const data = await this._regiaoService
      .obterPorParametros(params)
      .toPromise();

    this.regioes = data.items;
  }

  private async obterAutorizadas(nome: string='') {
    const params: AutorizadaParameters = { 
      indAtivo: 1,
      sortActive: 'nomeFantasia',
      sortDirection: 'ASC',
      filter: nome,
      pageSize: 50
    };

    const data = await this._autorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.autorizadas = data.items;
  }

  private async obterCidades(nome: string='') {
    const params: CidadeParameters = { 
      indAtivo: 1, 
      sortActive: 'nomeCidade', 
      sortDirection: 'ASC', 
      pageSize: 50,
      filter: nome
    };

    const data = await this._cidadeService
      .obterPorParametros(params)
      .toPromise();

    this.cidades = data.items;
  }

  private async obterFiliais(nome: string='') {
    const params: FilialParameters = { 
      indAtivo: 1,
      sortActive: 'nomeFilial',
      sortDirection: 'ASC',
      filter: nome,
      pageSize: 50
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  criar(): void {
    const form: any = this.form.getRawValue();

    let obj =
    {
      ... this.regiaoAutorizada,
      ...form,
      indAtivo: 1
    }

    this._regiaoAutorizadaService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Registro criado com sucesso!", "success");
      this._location.back();
    })

  }
  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}