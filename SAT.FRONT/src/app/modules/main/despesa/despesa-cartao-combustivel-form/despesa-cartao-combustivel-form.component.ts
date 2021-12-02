import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatCheckbox } from '@angular/material/checkbox';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { DespesaCartaoCombustivel } from 'app/core/types/despesa-cartao-combustivel.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-despesa-cartao-combustivel-form',
  templateUrl: './despesa-cartao-combustivel-form.component.html'
})

export class DespesaCartaoCombustivelFormComponent implements OnInit, OnDestroy
{
  codDespesaCartaoCombustivel: number;
  despesaCartaoCombustivel: DespesaCartaoCombustivel;
  form: FormGroup;
  isAddMode: boolean;
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();
  @ViewChild('indAtivo') private _formIndAtivo: MatCheckbox;

  constructor (
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _userService: UserService,
    private _despesaCartaoCombustivelSvc: DespesaCartaoCombustivelService,
    private _snack: CustomSnackbarService,
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit()
  {
    this.codDespesaCartaoCombustivel = +this._route.snapshot.paramMap.get('codDespesaCartaoCombustivel');
    this.isAddMode = !this.codDespesaCartaoCombustivel;

    this.inicializarForm();
    this.loadData();
  }

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      codDespesaCartaoCombustivel: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      numero: [undefined, Validators.required],
      carro: [undefined, Validators.required],
      placa: [undefined, Validators.required],
      ano: [undefined, Validators.required],
      cor: [undefined, Validators.required],
      combustivel: [undefined, Validators.required],
      indAtivo: [true, Validators.required],
    });
  }

  async loadData()
  {
    await this.obterCartaoCombustivel();
  }

  async obterCartaoCombustivel()
  {
    if (this.isAddMode) return;

    this.despesaCartaoCombustivel =
      (await this._despesaCartaoCombustivelSvc.obterPorCodigo(this.codDespesaCartaoCombustivel).toPromise());

    this.form.patchValue(this.despesaCartaoCombustivel);
  }

  salvar(): void
  {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar()
  {
    let cartao: DespesaCartaoCombustivel = {
      ...this.despesaCartaoCombustivel,
      ...this.form.getRawValue(),
      ...{
        indAtivo: this._formIndAtivo.checked ? 1 : 0,
        codUsuarioManut: this.userSession.usuario?.codUsuario
      }
    };

    this._despesaCartaoCombustivelSvc.atualizar(cartao).toPromise()
      .then(() =>
      {
        this._snack.exibirToast("Cartão atualizado com sucesso!", "success");
        this._router.navigate(['/despesa/cartoes-combustivel/detalhe/' + this.codDespesaCartaoCombustivel]);
      }).catch(() =>
      {
        this._snack.exibirToast("Erro ao atualizar cartão.", "error");
        this._router.navigate(['/despesa/cartoes-combustivel/detalhe/' + this.codDespesaCartaoCombustivel]);
      });
  }

  private async criar()
  {
    let cartao: DespesaCartaoCombustivel = {
      ...this.form.getRawValue(),
      ...{
        indAtivo: this._formIndAtivo.checked ? 1 : 0,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario
      }
    };

    this._despesaCartaoCombustivelSvc.criar(cartao).toPromise()
      .then(() =>
      {
        this._snack.exibirToast("Cartão criado com sucesso!", "success");
        this._router.navigate(['/despesa/cartoes-combustivel']);
      }).catch(() =>
      {
        this._snack.exibirToast("Erro ao criar cartão.", "error");
        this._router.navigate(['/despesa/cartoes-combustivel']);
      });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}