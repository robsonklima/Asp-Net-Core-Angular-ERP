import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaPeriodo, DespesaPeriodoTecnico, DespesaPeriodoTecnicoData } from 'app/core/types/despesa-periodo.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-despesa-atendimento-lista',
  templateUrl: './despesa-atendimento-lista.component.html',
  styles: [`
        .list-grid-despesa-atendimento {
            grid-template-columns: 130px 130px 130px 130px 130px 130px 130px 130px  auto;
            @screen sm { grid-template-columns: 130px 130px 130px 130px 130px 130px 130px 130px  auto; }
            @screen md { grid-template-columns: 130px 130px 130px 130px 130px 130px 130px 130px  auto; }
            @screen lg { grid-template-columns: 130px 130px 130px 130px 130px 130px 130px 130px  auto; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class DespesaAtendimentoListaComponent implements OnInit
{
  userSession: UserSession;
  isLoading: boolean = false;
  dataSourceData: DespesaPeriodoTecnicoData;
  despesasPeriodoTecnico: DespesaPeriodoTecnico[] = [];
  despesasPeriodo: DespesaPeriodo[] = [];

  constructor (
    private _userService: UserService,
    private _despesaPeriodoSvc: DespesaPeriodoService,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  { this.userSession = JSON.parse(this._userService.userSession); }

  ngOnInit(): void
  {
    this.obterDespesasPeriodo();
    this.obterDespesasPeriodoTecnico();
  }

  private async obterDespesasPeriodoTecnico()
  {
    if (!this.userSession.usuario.codTecnico) return;

    this.despesasPeriodoTecnico = (await this._despesaPeriodoTecnicoSvc.obterPorParametros({
      codTecnico: this.userSession.usuario.codTecnico,
      pageSize: 500,
    }).toPromise()).items;
  }

  private async obterDespesasPeriodo()
  {
    this.despesasPeriodo = (await this._despesaPeriodoSvc.obterPorParametros({
      indAtivo: 1,
      pageSize: 500,
    }).toPromise()).items;
  }


  paginar()
  {
  }
}
