import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { DespesaCartaoCombustivel, DespesaCartaoCombustivelTecnico } from 'app/core/types/despesa-cartao-combustivel.types';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { DespesaCartaoCombustivelTecnicoService } from 'app/core/services/despesa-cartao-combustivel-tecnico.service';
import { MatDialog } from '@angular/material/dialog';
import { DespesaCartaoCombustivelDialogComponent } from './despesa-cartao-combustivel-dialog/despesa-cartao-combustivel-dialog.component';
import Enumerable from 'linq';

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
  displayedColumns: string[] = ['tecnico', 'inicio de uso'];
  isHistoricoLoading: boolean;

  constructor (
    private _route: ActivatedRoute,
    private _cartaoCombustivelSvc: DespesaCartaoCombustivelService,
    private _cartaoCombustivelControleSvc: DespesaCartaoCombustivelTecnicoService,
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
    this.isHistoricoLoading = true;

    this.historico =
      Enumerable.from((await this._cartaoCombustivelControleSvc
        .obterPorParametros(
          {
            codDespesaCartaoCombustivel: this.cartao?.codDespesaCartaoCombustivel
          }
        )
        .toPromise()).items).orderBy(i => i.dataHoraInicio).toArray();

    this.isHistoricoLoading = false;
  }

  vincularNovoTecnico(): void
  {
    const dialogRef = this._dialog.open(DespesaCartaoCombustivelDialogComponent, {
      data:
      {
        codDespesaCartaoCombustivel: this.codDespesaCartaoCombustivel
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        this.obterHistorico();
    });
  }
}