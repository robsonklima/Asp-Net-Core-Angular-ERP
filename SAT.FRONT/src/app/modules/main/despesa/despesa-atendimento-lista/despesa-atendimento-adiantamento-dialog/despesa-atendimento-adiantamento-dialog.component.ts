import { Component, Inject, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaAdiantamentoService } from 'app/core/services/despesa-adiantamento.service';
import { DespesaAdiantamento, DespesaAdiantamentoData, DespesaAdiantamentoPeriodoData } from 'app/core/types/despesa-adiantamento.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-despesa-atendimento-adiantamento-dialog',
  templateUrl: './despesa-atendimento-adiantamento-dialog.component.html',
  styles: [`.list-grid-despesa-atendimento-adiantamento {
            grid-template-columns: 100px 100px 100px;
            @screen sm { grid-template-columns: 100px 100px 100px; }
            @screen md { grid-template-columns: 100px 100px 100px; }
            @screen lg { grid-template-columns: 100px 100px 100px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaAtendimentoAdiantamentoDialogComponent implements OnInit
{
  userSession: UserSession;
  codTecnico: number;
  codPeriodo: number;
  adiantamentos: DespesaAdiantamentoData;
  adiantamentosPeriodo: DespesaAdiantamentoPeriodoData;
  isLoading: boolean = false;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _userSvc: UserService,
    private _despesaAdiantamentoService: DespesaAdiantamentoService,
    private _despesaAdiantamentoPeriodoService: DespesaAdiantamentoPeriodoService,
    private dialogRef: MatDialogRef<DespesaAtendimentoAdiantamentoDialogComponent>)
  {
    if (data)
    {
      this.codTecnico = data.codTecnico;
      this.codPeriodo = data.codPeriodo;
    }

    this.userSession = JSON.parse(this._userSvc.userSession);
  };

  async ngOnInit()
  {
    this.isLoading = true;

    await this.obterAdiantamentos();

    this.isLoading = false;
  }

  async obterAdiantamentos()
  {
    this.adiantamentos = (await this._despesaAdiantamentoService.obterPorParametros(
      {
        codTecnicos: this.codTecnico.toString(),
        indAtivo: statusConst.ATIVO
      }).toPromise());

    this.adiantamentosPeriodo = (await this._despesaAdiantamentoPeriodoService.obterPorParametros(
      {
        codTecnico: this.codTecnico,
        indAtivoAdiantamento: statusConst.ATIVO
      }).toPromise());
  }

  obterValorUtilizado(da: DespesaAdiantamento)
  {
    var adiantamentoUtilizado = Enumerable.from(this.adiantamentosPeriodo.items)
      .where(i => i.codDespesaAdiantamento == da.codDespesaAdiantamento)
      .sum(i => i.valorAdiantamentoUtilizado);

    return adiantamentoUtilizado;
  }

  fechar()
  {
    this.dialogRef.close();
  }
}