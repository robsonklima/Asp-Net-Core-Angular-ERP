import { AfterViewInit, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { DespesaCartaoCombustivel, DespesaCartaoCombustivelTecnico } from 'app/core/types/despesa-cartao-combustivel.types';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { DespesaCartaoCombustivelTecnicoService } from 'app/core/services/despesa-cartao-combustivel-tecnico.service';
import { MatDialog } from '@angular/material/dialog';
import Enumerable from 'linq';
import { TecnicoService } from 'app/core/services/tecnico.service';

@Component({
  selector: 'app-cartao-combustivel-detalhe',
  templateUrl: './cartao-combustivel-detalhe.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class CartaoCombustivelDetalheComponent implements AfterViewInit
{
  codDespesaCartaoCombustivel: number;
  cartao: DespesaCartaoCombustivel;
  historico: DespesaCartaoCombustivelTecnico[] = [];
  userSession: UsuarioSessao;
  displayedColumns: string[] = ['tecnico', 'inicio de uso'];
  isHistoricoLoading: boolean = true;

  constructor (
    private _route: ActivatedRoute,
    private _cartaoCombustivelSvc: DespesaCartaoCombustivelService,
    private _tecnicoSvc: TecnicoService,
    private _despesaCartaoCombustivelTecnicoSvc: DespesaCartaoCombustivelTecnicoService,
    private _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _dialog: MatDialog
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
      Enumerable.from((await this._despesaCartaoCombustivelTecnicoSvc
        .obterPorParametros({ codDespesaCartaoCombustivel: this.cartao?.codDespesaCartaoCombustivel })
        .toPromise()).items)
        .orderBy(i => i.dataHoraInicio)
        .toArray();

    for (var h of this.historico)
      h.tecnico = (await this._tecnicoSvc.obterPorCodigo(h.codTecnico).toPromise());

    this.isHistoricoLoading = false;
  }

  vincularNovoTecnico(): void
  {
    const dialogRef = this._dialog.open(CartaoCombustivelDetalheComponent, {
      data: { codDespesaCartaoCombustivel: this.codDespesaCartaoCombustivel }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        this.obterHistorico();
    });
  }
}