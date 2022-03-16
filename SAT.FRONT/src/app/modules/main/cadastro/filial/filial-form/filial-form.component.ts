import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CidadeService } from 'app/core/services/cidade.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { PaisService } from 'app/core/services/pais.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Cidade } from 'app/core/types/cidade.types';
import { Geolocalizacao, GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';
import { Pais } from 'app/core/types/pais.types';
import { UnidadeFederativa } from 'app/core/types/unidade-federativa.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, first, map, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FilialService } from 'app/core/services/filial.service';
import { Filial } from 'app/core/types/filial.types';

@Component({
  selector: 'app-filial-form',
  templateUrl: './filial-form.component.html'
})
export class FilialFormComponent implements OnInit, OnDestroy {

  private userSession: UsuarioSessao;

  protected _onDestroy = new Subject<void>();
  public filial: Filial;
  public loading: boolean = true;
  public codFilial: number;
  public isAddMode: boolean;
  public form: FormGroup;

  public buscandoCEP: boolean = false;
  public paises: Pais[] = [];
  public unidadesFederativas: UnidadeFederativa[] = [];
  public cidades: Cidade[] = [];

  cidadeFiltro: FormControl = new FormControl();
  clienteFilterCtrl: FormControl = new FormControl();
  contratosFilterCtrl: FormControl = new FormControl();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _paisService: PaisService,
    private _cidadeService: CidadeService,
    private _filialService: FilialService,
    private _userService: UserService,
    private _unidadeFederativaService: UnidadeFederativaService,
    private _cdr: ChangeDetectorRef,
    private _googleGeolocationService: GeolocalizacaoService,
    private _location: Location,
  ) { this.userSession = JSON.parse(this._userService.userSession); }

  async ngOnInit() {

    this.codFilial = +this._route.snapshot.paramMap.get('codFilial');
    this.isAddMode = !this.codFilial;
    this.inicializarForm();

    this.paises = await this._paisService.obterPaises();
    this.form.controls['indAtivo'].setValue(true);

    if (!this.isAddMode) {
      this.carregarDadosCliente();
    }

    this.loading = false;
  }

  private carregarDadosCliente() {
    this._filialService.obterPorCodigo(this.codFilial)
      .pipe(first())
      .subscribe(data => {
        this.filial = data;
        this.form.patchValue(data);
        this.form.controls['codPais'].setValue(data.cidade?.unidadeFederativa?.codPais);
        this.form.controls['codUF'].setValue(data.cidade?.codUF);
        this.form.controls['codCidade'].setValue(data.cidade?.codCidade);
        this.form.controls['icms'].setValue(data.icms?.toFixed(2));
      });
  }

  private inicializarForm() {

    this.form = this._formBuilder.group({
      codFilial: [
        {
          value: undefined,
          disabled: true
        }
      ],
      razaoSocial: [undefined, [Validators.required, Validators.maxLength(50)]],
      endereco: [undefined, [Validators.required, Validators.maxLength(100)]],
      nomeFilial: [undefined, [Validators.required, Validators.maxLength(50)]],
      cnpj: [undefined, Validators.required],
      inscricaoEstadual: [undefined, Validators.required],
      bairro: [undefined, [Validators.required]],
      cep: [undefined, [Validators.required]],
      codUF: [undefined, [Validators.required]],
      codPais: [undefined, [Validators.required]],
      codCidade: [undefined, Validators.required],
      indAtivo: [undefined],
      fone: [undefined],
      email: [undefined],
      icms: [undefined]
    });

    this.form.controls['codPais'].valueChanges.subscribe(async () => {
      this.unidadesFederativas = [];
      this.unidadesFederativas = await this._unidadeFederativaService.obterUnidadesFederativas(this.form.controls['codPais'].value);
      this._cdr.detectChanges();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async () => {
      this.cidades = [];
      this.cidades = await this._cidadeService.obterCidades(this.form.controls['codUF'].value);
      this._cdr.detectChanges();
    });

    this.cidadeFiltro.valueChanges.pipe(
      filter(filtro => !!filtro),
      debounceTime(700),
      delay(500),
      takeUntil(this._onDestroy),
      map(async filtro => {
        this.cidades = await this._cidadeService.obterCidades(this.form.controls['codUF'].value, filtro);
        this._cdr.detectChanges();
      })
    ).toPromise();

  }

  async buscaCEP(cepCmp: any) {
    if (cepCmp.target.value) {
      this.form.disable();
      // Google
      // Tenta pelo cep (nem sempre os endereços são corretos)
      this._googleGeolocationService.obterPorParametros
        ({ enderecoCep: cepCmp.target.value.replace(/\D+/g, ''), geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE })
        .subscribe((mapService: Geolocalizacao) => {
          if (mapService) {
            this.form.controls['endereco'].setValue(mapService.endereco);
            this.form.controls['bairro'].setValue(mapService.bairro);

            this._cidadeService.obterCidades(null, mapService.cidade).then(c => {
              const data = c[0];
              if (data) {
                this.form.controls['codPais'].setValue(data.unidadeFederativa?.codPais);
                this.form.controls['codUF'].setValue(data.codUF);
                this.form.controls['codCidade'].setValue(data.codCidade);
              }
            });
          }
          this.form.enable();
          this._cdr.detectChanges();
        });
    }
  }

  public salvar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.filial,
      ...form,
      ...{
        indAtivo: +form.indAtivo,
      }
    };

    if (this.isAddMode) {
      obj.codUsuarioCad = this.userSession.usuario.codUsuario;
      obj.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');

      this._filialService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Filial adicionada com sucesso!`, "success");
        this._location.back();
      });
    } else {
      obj.codUsuarioManut = this.userSession.usuario.codUsuario;
      obj.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

      this._filialService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Filial atualizada com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
