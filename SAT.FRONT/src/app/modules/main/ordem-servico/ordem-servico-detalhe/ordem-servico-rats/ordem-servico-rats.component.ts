import { Component, Input, OnInit } from '@angular/core';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { RelatorioAtendimento, RelatorioAtendimentoData } from 'app/core/types/relatorio-atendimento.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import moment from 'moment';

@Component({
  selector: 'app-ordem-servico-rats',
  templateUrl: './ordem-servico-rats.component.html'
})
export class OrdemServicoRatsComponent implements OnInit {
  @Input() codOS:number;
  sessionData: UsuarioSessao;
  isLoading: boolean = true;
  rats: RelatorioAtendimento[] = [];

  constructor(
    private _relatorioAtendimentoService: RelatorioAtendimentoService,
    private _userService: UserService
  ) { 
      this.sessionData = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.rats = (await this.obterRATS()).items;
    console.log(this.rats);
    
    this.isLoading = false;
  }

  private async obterRATS(): Promise<RelatorioAtendimentoData> {
    return this._relatorioAtendimentoService
      .obterPorParametros({ codOS: this.codOS })
      .toPromise();
  }

  getTimeFromMins(mins) {
		var h = mins / 60 | 0,
			m = mins % 60 | 0;
		return moment.utc().hours(h).minutes(m).format("HH:mm");
	}
}
