import { ActivatedRoute } from '@angular/router';
import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { forkJoin, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UserService } from 'app/core/user/user.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { AcaoComponente } from 'app/core/types/acao-componente.types';
import { CausaService } from 'app/core/services/causa.service';
import { Causa } from 'app/core/types/causa.types';
import { AcaoComponenteService } from 'app/core/services/acao-componente.service';
import Enumerable from 'linq';

@Component({
  selector: 'app-acao-causa-form',
  templateUrl: './acao-causa-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class AcaoCausaFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codAcaoComponente: number;
  form: FormGroup;
  acaoComponente: AcaoComponente;
  public isLoading: Boolean = true;
  public causas: Causa[] = [];
  public acoes: AcaoComponente[] = [];

  causasFiltro: FormControl = new FormControl();
  acoesFiltro: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _acaoComponenteService: AcaoComponenteService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
    private _causaService: CausaService,
    private _cdr: ChangeDetectorRef
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codAcaoComponente = +this._route.snapshot.paramMap.get('codAcaoComponente');

    this.obterCausas();

    this.form = this._formBuilder.group({
      codAcaoComponente: [
        {
          value: undefined,
          disabled: true
        },
      ],
      codECausa: [undefined],
      codEAcao: [undefined],
      selecionado: [undefined]
    });

    this.causasFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (filtro) => {
        this.obterCausas(filtro);
      });

    this.form.controls['codECausa'].valueChanges.subscribe(async () => {
      this.acoes = [];
      this.acoes = (await this._acaoComponenteService.obterPorParametros({ codECausa: this.form.controls['codECausa'].value }).toPromise()).items;
      this._cdr.detectChanges();
    });

    this.acaoComponente = await this._acaoComponenteService.obterPorCodigo(this.codAcaoComponente).toPromise();

    this._acaoComponenteService.obterPorParametros({ codECausa: this.acaoComponente.codECausa })
      .pipe(first())
      .subscribe(apiData => {
        this.form.controls["codECausa"].setValue(this.acaoComponente.codECausa);
        this.form.controls["codEAcao"].setValue(Enumerable.from(apiData.items).where(s => s.selecionado == 1).select(s => s.acao?.codEAcao).distinct().toArray());
      });

    this.isLoading = false;
  }

  private async obterCausas(filtro: string = '') {
    const data = await this._causaService.obterPorParametros({
      indAtivo: 1,
      sortActive: 'codECausa',
      sortDirection: 'asc',
      filter: filtro
    }).toPromise();
    this.causas = data.items;
  }

  salvar(): void {
    this.isLoading = true;
    const form: any = this.form.getRawValue();
    let eventos: any[] = [];

    for (let dataAcoes of this.acoes) {

      let parametros = {
        ...this.acaoComponente,
        ...form,
        codAcao: dataAcoes.acao?.codAcao,
        selecionado: Enumerable.from(form.codEAcao).firstOrDefault(f => f == dataAcoes.acao?.codEAcao) != null ? 1 : 0
      };

      eventos.push(this._acaoComponenteService.atualizar(parametros));
    }

    forkJoin({
      ...eventos
    }).subscribe(resultado => {
      this.isLoading = false;
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    });
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
