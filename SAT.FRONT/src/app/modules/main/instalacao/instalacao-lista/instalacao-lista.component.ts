import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { InstalacaoLoteService } from 'app/core/services/instalacao-lote.service';
import { InstalacaoService } from 'app/core/services/instalacao.service';
import { TransportadoraService } from 'app/core/services/transportadora.service';
import { Contrato } from 'app/core/types/contrato.types';
import { InstalacaoLote } from 'app/core/types/instalacao-lote.types';
import { Instalacao, InstalacaoParameters, InstalacaoData } from 'app/core/types/instalacao.types';
import { Transportadora } from 'app/core/types/transportadora.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-lista',
  templateUrl: './instalacao-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-instalacao {
          grid-template-columns: 72px auto 64px 240px 72px 72px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoListaComponent implements AfterViewInit {
  codContrato: number;
  contrato: Contrato;
  codInstalLote: number;
  instalacaoLote: InstalacaoLote;
  instalacaoSelecionada: Instalacao;
  transportadoras: Transportadora[] = [];
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: InstalacaoData;
  isLoading: boolean = false;
  userSession: UserSession;
  form: FormGroup;
  transportadorasFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _route: ActivatedRoute,
    private _cdr: ChangeDetectorRef,
    private _formBuilder: FormBuilder,
    private _instalacaoSvc: InstalacaoService,
    private _transportadoraSvc: TransportadoraService,
    private _contratoSvc: ContratoService,
    private _instalacaoLoteSvc: InstalacaoLoteService,
    private _snack: CustomSnackbarService,
    private _userSvc: UserService
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
    this.codInstalLote = +this._route.snapshot.paramMap.get('codInstalLote');

    this.obterInstalacoes();
    this.obterTransportadoras();
    this.obterContrato();
    this.obterLote();

    if (this.sort && this.paginator) {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterInstalacoes();
      });
    }

    this.form = this._formBuilder.group({
      codInstalacao: [''],
      codInstalLote: [''],
      a: [''],
      b: [''],
      sku: [''],
      codTransportadora: ['']
    })

    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(700)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      this.paginator.pageIndex = 0;
      this.searchInputControl.nativeElement.val = text;
      this.obterInstalacoes();
    });

    this.transportadorasFiltro.valueChanges
      .pipe(
        tap(() => {}),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          this.obterTransportadoras(query);
        }),
        delay(100),
        takeUntil(this._onDestroy)
      )
      .subscribe();

    this._cdr.detectChanges();
  }

  private async obterInstalacoes() {
    this.isLoading = true;

    const params: InstalacaoParameters = {
      codContrato: this.codContrato || undefined,
      codInstalLote: this.codInstalLote || undefined,
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val,
      pageNumber: this.paginator.pageIndex + 1,
      sortActive: this.sort.active || 'CodInstalacao',
      sortDirection: this.sort.direction || 'desc',
    };

    const data: InstalacaoData = await this._instalacaoSvc
      .obterPorParametros(params)
      .toPromise();

    this.isLoading = false;
    this.dataSourceData = data;
  }

  private async obterTransportadoras(filter: string='') {
    const data = await this._transportadoraSvc.obterPorParametros({
      indAtivo: 1,
      sortActive: 'NomeTransportadora',
      sortDirection: 'asc',
      filter: filter
    }).toPromise();

    this.transportadoras = data.items;
  }

  private async obterContrato() {
    this.contrato = await this._contratoSvc.obterPorCodigo(this.codContrato).toPromise();
  }

  private async obterLote() {
    this.instalacaoLote = await this._instalacaoLoteSvc.obterPorCodigo(this.codInstalLote).toPromise();
  }

  paginar() {
    this.obterInstalacoes();
  }

  alternarDetalhe(codInstalacao: number): void {
    if (this.instalacaoSelecionada && this.instalacaoSelecionada.codInstalacao === codInstalacao) {
      this.fecharDetalhe();
      return;
    }

    this.isLoading = true;

    this._instalacaoSvc.obterPorCodigo(codInstalacao)
      .subscribe((instalacao) => {
          this.instalacaoSelecionada = instalacao;
          this.form.patchValue(instalacao);
          this.isLoading = false;
          this._cdr.markForCheck();
      }, () => {
        this.isLoading = false;
      });
  }

  fecharDetalhe(): void {
    this.instalacaoSelecionada = null;
  }

  atualizar() {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.instalacaoSelecionada,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario?.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._instalacaoSvc.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Instalação atualizada com sucesso!", "success");
    });

    this.obterInstalacoes();
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
