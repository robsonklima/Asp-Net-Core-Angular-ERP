import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORService } from 'app/core/services/or.service';
import { PecaService } from 'app/core/services/peca.service';
import { OR } from 'app/core/types/OR.types';
import { Peca, PecaData, PecaParameters } from 'app/core/types/peca.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, first, map, take, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-laboratorio-ordem-reparo-form',
  templateUrl: './laboratorio-ordem-reparo-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class LaboratorioOrdemReparoFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codOR: number;
  isAddMode: boolean;
  form: FormGroup;
  or: OR;
  pecas: Peca[] = [];
  pecaFilterCtrl: FormControl = new FormControl();
  isLoading: boolean;
  pageSize: number = 10;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _orService: ORService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _pecaService: PecaService,
    private _route: ActivatedRoute
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codOR = +this._route.snapshot.paramMap.get('codDefeito');
    this.isAddMode = !this.codOR;
    this.inicializarForm();
    this.registrarEmitters();

    if (!this.isAddMode) {
      this._orService.obterPorCodigo(this.codOR)
        .pipe(first())
        .subscribe(data => {
          this.or = data;
          this.form.patchValue(this.or);
        })
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codPeca: [null, [Validators.required]],
      quantidade: [null, [Validators.required]],
    });
  }

  async obterPecas(filtro): Promise<PecaData> {
    if (!filtro) return;

		let params: PecaParameters = {
			sortActive: 'nomePeca',
			sortDirection: 'asc',
      pageSize: this.pageSize,
      filter: filtro
		};

		return await this._pecaService
			.obterPorParametros(params)
			.toPromise();
	}

  private registrarEmitters() {
    this.pecaFilterCtrl.valueChanges
			.pipe(
				tap(() => this.isLoading = true),
				debounceTime(700),
        distinctUntilChanged(),
        delay(500),
				map(async query => {
          return (await this.obterPecas(query))?.items?.slice();
				}),
				takeUntil(this._onDestroy)
			)
			.subscribe(async data => {
        const registros = await data;
        if (registros) this.pecas = registros;
        this.isLoading = false;
			});
  }

  salvar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.or,
      ...form,
      ...{
        indAtivo: +form.indAtivo,
      }
    };

    if (this.isAddMode) {
      this._orService.criar(obj).subscribe(() => {
        this._snack.exibirToast("Registro criado com sucesso!", "success");
        this._location.back();
      })
    } else {
      this._orService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast("Registro atualizado com sucesso!", "success");
        this._location.back();
      })
    }
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

