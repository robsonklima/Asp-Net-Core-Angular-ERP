import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { MatSidenav } from '@angular/material/sidenav';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { DespesaCartaoCombustivel, DespesaCartaoCombustivelTecnico } from 'app/core/types/despesa-cartao-combustivel.types';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { DespesaCartaoCombustivelTecnicoService } from 'app/core/services/despesa-cartao-combustivel-tecnico.service';

@Component({
  selector: 'app-despesa-cartao-combustivel-detalhe',
  templateUrl: './despesa-cartao-combustivel-detalhe.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class DespesaCartaoCombustivelDetalheComponent implements AfterViewInit
{
  codDespesaCartaoCombustivel: number;
  cartao: DespesaCartaoCombustivel;
  historico: DespesaCartaoCombustivelTecnico[] = [];
  userSession: UsuarioSessao;

  constructor (
    private _route: ActivatedRoute,
    private _cartaoCombustivelSvc: DespesaCartaoCombustivelService,
    private _cartaoCombustivelControleSvc: DespesaCartaoCombustivelTecnicoService,
    private _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _snack: CustomSnackbarService,
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
    this.codDespesaCartaoCombustivel = +this._route.snapshot.paramMap.get('codDespesaCartaoCombustivel');
  }

  ngAfterViewInit(): void
  {
    this.obterDados();
    this._cdr.detectChanges();
  }


  private async obterDados()
  {
    await this.obterCartao();
    await this.obterHistorico();
  }

  private async obterCartao()
  {
    this.cartao =
      await this._cartaoCombustivelSvc
        .obterPorCodigo(this.codDespesaCartaoCombustivel)
        .toPromise();
  }

  private async obterHistorico()
  {
    this.historico =
      (await this._cartaoCombustivelControleSvc
        .obterPorParametros(
          {
            codDespesaCartaoCombustivel: this.cartao?.codDespesaCartaoCombustivel
          }
        )
        .toPromise()).items;

    console.log(this.historico);
  }
}