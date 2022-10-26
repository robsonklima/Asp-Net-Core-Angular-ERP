import { Component, Input, OnInit } from '@angular/core';
import { CheckinCheckoutService } from 'app/core/services/checkin-checout.service';
import { OrdemServicoHistoricoService } from 'app/core/services/ordem-servico-historico.service';
import { OrdemServicoHistoricoData } from 'app/core/types/ordem-servico-historico.types';
import moment from 'moment';

@Component({
  selector: 'app-ordem-servico-logs',
  templateUrl: './ordem-servico-logs.component.html'
})
export class OrdemServicoLogsComponent implements OnInit {
  @Input() codOS: number;
  isLoading: boolean = true;
  historico: any[] = [];

  constructor(
    private _ordemServicoHistoricoService: OrdemServicoHistoricoService,
    private _checkinCheckoutService: CheckinCheckoutService
  ) { }

  async ngOnInit() {
    this.obterCheckinsECheckouts();
    this.historico = (await this.obterHistorico()).items;
    this.isLoading = false;
  }

  private async obterHistorico(): Promise<OrdemServicoHistoricoData> {
    return this._ordemServicoHistoricoService
      .obterPorParametros({ codOS: this.codOS })
      .toPromise();
  }

  private async obterCheckinsECheckouts() {
		const cks = await this._checkinCheckoutService.obterPorParametros({
			codOS: this.codOS,
			sortDirection: 'asc',
			sortActive: 'dataHoraCad'
		}).toPromise();

		this.historico.concat();

		const checkinCheckout = cks?.items.map((i) => {
			return {
				tipo: i.tipo,
				dataHoraCadSmartphone: i.tipo == 'CHECKOUT' ? moment(i.dataHoraCadSmartphone).add('minutes', 2) : i.dataHoraCadSmartphone,
				codUsuarioCad: i.codUsuarioTecnico
			}
		})

		this.historico.push.apply(this.historico, checkinCheckout);
		this.historico = this.historico
			.sort((a, b) => (moment(a.dataHoraCadSmartphone || a.dataHoraCad) > moment(b.dataHoraCadSmartphone || b.dataHoraCad)) ? 1 : -1);
	}
}
