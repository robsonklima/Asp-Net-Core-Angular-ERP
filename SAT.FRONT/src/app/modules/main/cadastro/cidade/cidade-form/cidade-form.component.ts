import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import moment from 'moment';
import { Subject } from 'rxjs';
import { delay, first, map, takeUntil } from 'rxjs/operators';
import { CidadeService } from 'app/core/services/cidade.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FilialService } from 'app/core/services/filial.service';
import { GoogleGeolocationService } from 'app/core/services/google-geolocation.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Cidade } from 'app/core/types/cidade.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { GoogleGeolocation } from 'app/core/types/google-geolocation.types';
import { UnidadeFederativa, UnidadeFederativaData } from 'app/core/types/unidade-federativa.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { statusConst } from 'app/core/types/status-types';

@Component({
  selector: 'app-cidade-form',
  templateUrl: './cidade-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class CidadeFormComponent implements OnInit {

  codCidade: number;
  isAddMode: boolean;
  form: FormGroup;
  cidade: Cidade;
  userSession: UsuarioSessao;
  filiais: Filial[] = [];
  unidadesFederativas: UnidadeFederativa[] = [];

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _cidadeService: CidadeService,
    private _filialService: FilialService,
    private _unidadeFederativaService: UnidadeFederativaService,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _googleGeolocationService: GoogleGeolocationService,
    private _userService: UserService,
    private _location: Location,
    private _dialog: MatDialog,
    private _router: Router
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit() {

    this.codCidade = +this._route.snapshot.paramMap.get('codCidade');
    this.isAddMode = !this.codCidade;
    this.inicializarForm();
    this.obterFiliais();
    this.obterUnidadesFederativas();

    this.form.controls['nomeCidade'].valueChanges.pipe(
      map(async text => {
        this.obterLatLngPorEndereco();
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['codUF'].valueChanges.pipe(
      map(async text => {
        this.obterLatLngPorEndereco();
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    if (!this.isAddMode) {
      this._cidadeService.obterPorCodigo(this.codCidade)
        .pipe(first())
        .subscribe(autorizada => {
          this.cidade = autorizada;
          this.form.patchValue(this.cidade);
          this.form.controls['codUF'].setValue(this.cidade.codUF);
        });
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codCidade: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nomeCidade: [undefined, Validators.required],
      codFilial: [undefined, Validators.required],
      codUF: [undefined, Validators.required],
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
      indAtivo: [true]
    });
  }

  private async obterLatLngPorEndereco() {

    const uf = this.form.controls['codUF']?.value;
    const nomeCidade = this.form.controls['nomeCidade']?.value;

    if (nomeCidade !== null && uf !== null) {
      const query = `${nomeCidade}, ${uf}`;
      this._googleGeolocationService.obterPorParametros({ enderecoCep: query.trim()}).subscribe((data: GoogleGeolocation) => {
        if (data && data.results.length > 0) {
          const res = data.results.shift();
          this.form.controls['latitude'].setValue(res.geometry.location.lat);
          this.form.controls['longitude'].setValue(res.geometry.location.lng);
        }
      });
    }
  }

  private async obterFiliais() {
    const params: FilialParameters = {
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      indAtivo: statusConst.ATIVO,
      pageSize: 100
    }
    const data = await this._filialService.obterPorParametros(params).toPromise();
    this.filiais = data.items;
  }

  obterUnidadesFederativas(): void {
    const codPais = 1; // Brasil

    this._unidadeFederativaService.obterPorParametros({
      pageSize: 50,
      codPais: codPais
    }).subscribe((data: UnidadeFederativaData) => {
      this.unidadesFederativas = data.items;
    })
  }

  salvar(): void {
    this.form.disable();
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.cidade,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo
      }
    };

    this._cidadeService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Cidade atualizada com sucesso!", "success");
      this._location.back();
    }, e => {
      this.form.enable();
    });
  }

  private criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.cidade,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo
      }
    };

    this._cidadeService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Cidade inserida com sucesso!", "success");
      this._location.back();
    }, e => {
      this.form.enable();
    });
  }

  async deletar() {

    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja excluir esta cidade?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        this.form.disable();
        this._cidadeService.deletar(this.codCidade).subscribe(() => {
          this._snack.exibirToast('Cidade removida com sucesso!', 'success');
          this._router.navigate(['/cidade']);
          this.form.enable();
        }, e => {
          this._snack.exibirToast('Erro ao remover cidade', 'error');
          this.form.enable();
        })
      }
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

}
