import { ActivatedRoute } from '@angular/router';
import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { forkJoin, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UserService } from 'app/core/user/user.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DefeitoComponente } from 'app/core/types/defeito-componente.types';
import { CausaService } from 'app/core/services/causa.service';
import { Causa } from 'app/core/types/causa.types';
import { DefeitoComponenteService } from 'app/core/services/defeito-componente.service';
import Enumerable from 'linq';

@Component({
  selector: 'app-defeito-causa-form',
  templateUrl: './defeito-causa-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class DefeitoCausaFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codDefeitoComponente: number;
  form: FormGroup;
  defeitoComponente: DefeitoComponente;
  public isLoading: Boolean = true;
  public causas: Causa[] = [];
  public acoes: DefeitoComponente[] = [];

  causasFiltro: FormControl = new FormControl();
  acoesFiltro: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _defeitoComponenteService: DefeitoComponenteService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
    private _causaService: CausaService,
    private _cdr: ChangeDetectorRef
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codDefeitoComponente = +this._route.snapshot.paramMap.get('codDefeitoComponente');

    this.obterCausas();

    this.form = this._formBuilder.group({
      codDefeitoComponente: [
        {
          value: undefined,
          disabled: true
        },
      ],
      codECausa: [undefined],
      codEDefeito: [undefined],
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
      this.acoes = (await this._defeitoComponenteService.obterPorParametros({ codECausa: this.form.controls['codECausa'].value }).toPromise()).items;
      this._cdr.detectChanges();
    });

    this.defeitoComponente = await this._defeitoComponenteService.obterPorCodigo(this.codDefeitoComponente).toPromise();

    this._defeitoComponenteService.obterPorParametros({ codECausa: this.defeitoComponente.codECausa })
      .pipe(first())
      .subscribe(apiData => {
        this.form.controls["codECausa"].setValue(this.defeitoComponente.codECausa);
        this.form.controls["codEDefeito"].setValue(Enumerable.from(apiData.items).where(s => s.selecionado == 1).select(s => s.defeito?.codEDefeito).distinct().toArray());
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
        ...this.defeitoComponente,
        ...form,
        codDefeito: dataAcoes.defeito?.codDefeito,
        selecionado: Enumerable.from(form.codEDefeito).firstOrDefault(f => f == dataAcoes.defeito?.codEDefeito) != null ? 1 : 0
      };

      eventos.push(this._defeitoComponenteService.atualizar(parametros));
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
