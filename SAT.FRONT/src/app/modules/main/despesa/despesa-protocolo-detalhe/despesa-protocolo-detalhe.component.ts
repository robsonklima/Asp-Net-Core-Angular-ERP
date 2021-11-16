import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { DespesaProtocolo, DespesaProtocoloPeriodoListView } from 'app/core/types/despesa-protocolo.types';
import { DespesaProtocoloService } from 'app/core/services/despesa-protocolo.service';
import Enumerable from 'linq';
import { DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { DespesaTipoEnum } from 'app/core/types/despesa.types';
import { MatDialog } from '@angular/material/dialog';
import { DespesaProtocoloDetalhePeriodosDialogComponent } from './despesa-protocolo-detalhe-periodos-dialog/despesa-protocolo-detalhe-periodos-dialog.component';

@Component({
  selector: 'app-despesa-protocolo-detalhe',
  templateUrl: './despesa-protocolo-detalhe.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class DespesaProtocoloDetalheComponent implements AfterViewInit
{
  codDespesaProtocolo: number;
  displayedColumns: string[] = ['data inicial', 'data final', 'tecnico', 'valor'];
  protocolo: DespesaProtocolo;
  listView: DespesaProtocoloPeriodoListView[] = [];
  userSession: UsuarioSessao;
  isLoading: boolean;

  constructor (
    private _route: ActivatedRoute,
    private _despesaProtocoloSvc: DespesaProtocoloService,
    private _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _dialog: MatDialog
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
    this.codDespesaProtocolo = +this._route.snapshot.paramMap.get('codDespesaProtocolo');
  }

  ngAfterViewInit(): void
  {
    this.obterDados();
    this._cdr.detectChanges();
  }

  private async obterDados()
  {
    this.isLoading = true;

    await this.obterProtocolo();
    await this.criaListaPeriodos();

    this.isLoading = false;

  }

  private criaListaPeriodos()
  {
    this.protocolo.despesaProtocoloPeriodoTecnico.forEach(dppt =>
    {
      dppt.despesaPeriodoTecnico.forEach(dpt =>
      {
        this.listView.push(
          {
            dataInicial: dpt.despesaPeriodo.dataInicio,
            dataFinal: dpt.despesaPeriodo.dataFim,
            tecnico: dpt.tecnico.nome,
            valor: this.calculaDespesa(dpt)
          });
      });
    })

    this.listView = Enumerable.from(this.listView).orderByDescending(i => i.dataInicial).toArray();
  }

  private async obterProtocolo()
  {
    this.protocolo =
      (await this._despesaProtocoloSvc.obterPorCodigo(this.codDespesaProtocolo).toPromise());
  }

  calculaDespesa(dpt: DespesaPeriodoTecnico)
  {
    return Enumerable.from(dpt.despesas)
      .sum(d => Enumerable.from(d.despesaItens).
        where(i => i.indAtivo == 1 &&
          i.codDespesaTipo != DespesaTipoEnum.KM && i.codDespesaTipo != DespesaTipoEnum.COMBUSTIVEL)
        .sum(i => i.despesaValor));
  }

  fecharProtocolo(): void
  {

  }

  imprimirProtocolo(): void
  {

  }

  adicionarPeriodo(): void
  {

    const dialogRef = this._dialog.open(DespesaProtocoloDetalhePeriodosDialogComponent, {
      data:
      {
        codDespesaProtocolo: this.codDespesaProtocolo
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        console.log("e nois");
    });
  }
}