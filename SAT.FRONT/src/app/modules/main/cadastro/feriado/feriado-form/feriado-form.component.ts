import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CidadeService } from 'app/core/services/cidade.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PaisService } from 'app/core/services/pais.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Cidade } from 'app/core/types/cidade.types';
import { Pais } from 'app/core/types/pais.types';
import { Transportadora } from 'app/core/types/transportadora.types';
import { UnidadeFederativa } from 'app/core/types/unidade-federativa.types';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, first, map, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { FormaPagamento } from 'app/core/types/forma-pagamento.types';
import { TipoFrete } from 'app/core/types/tipo-frete.types';
import { BancadaLista } from 'app/core/types/bancada-lista.types';
import { FeriadoService } from 'app/core/services/feriado.service';
import { Feriado } from 'app/core/types/feriado.types';

@Component({
  selector: 'app-feriado-form',
  templateUrl: './feriado-form.component.html'
})
export class FeriadoFormComponent implements OnInit, OnDestroy {

  protected _onDestroy = new Subject<void>();
  public feriado: Feriado;
  public loading: boolean = true;
  public codFeriado: number;
  public isAddMode: boolean;
  public form: FormGroup;

  public buscandoCEP: boolean = false;
  public paises: Pais[] = [];
  public unidadesFederativas: UnidadeFederativa[] = [];
  public cidades: Cidade[] = [];
  public formasPagamento: FormaPagamento[] = [];
  public bancadasLista: BancadaLista[] = [];
  public tiposFrete: TipoFrete[] = [];
  public transportadoras: Transportadora[] = [];

  cidadeFiltro: FormControl = new FormControl();
  clienteFilterCtrl: FormControl = new FormControl();
  contratosFilterCtrl: FormControl = new FormControl();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _paisService: PaisService,
    private _cidadeService: CidadeService,
    private _feriadoService: FeriadoService,
    private _unidadeFederativaService: UnidadeFederativaService,
    private _cdr: ChangeDetectorRef,
    private _location: Location,
  ) { }

  async ngOnInit() {

    this.codFeriado = +this._route.snapshot.paramMap.get('codFeriado');
    this.isAddMode = !this.codFeriado;
    this.inicializarForm();

    this.paises = await this._paisService.obterPaises();

    if (!this.isAddMode) {
      this._feriadoService.obterPorCodigo(this.codFeriado)
        .pipe(first())
        .subscribe(data => {
          this.feriado = data;
          this.form.patchValue(data);
        });
    }

    this.loading = false;
  }

  private inicializarForm() {

    this.form = this._formBuilder.group({
      codFeriado: [
        {
          value: undefined,
          disabled: true
        }
      ],
      nomeFeriado: [undefined, Validators.required],
      data: [undefined, Validators.required],
      qtdeDias: [undefined],
      codUF: [undefined, [Validators.required]],
      codPais: [undefined, [Validators.required]],
      codCidade: [undefined, Validators.required],
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
  public salvar(): void {

    const form = this.form.getRawValue();

    let obj = {
      ...this.feriado,
      ...form
    };

    if (this.isAddMode) {
      this._feriadoService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Feriado adicionado com sucesso!`, "success");
        this._location.back();
      });
    } else {
      this._feriadoService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Feriado atualizado com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
