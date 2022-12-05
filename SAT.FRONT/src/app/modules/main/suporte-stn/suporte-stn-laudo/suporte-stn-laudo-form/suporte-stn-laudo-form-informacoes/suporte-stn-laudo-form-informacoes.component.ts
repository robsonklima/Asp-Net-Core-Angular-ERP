import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { Laudo } from 'app/core/types/laudo.types';
import { LaudoService } from 'app/core/services/laudo.service';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';

@Component({
    selector: 'app-suporte-stn-laudo-form-informacoes',
    templateUrl: './suporte-stn-laudo-form-informacoes.component.html'
  })
  export class SuporteStnLaudoFormInformacoesComponent implements OnInit, OnDestroy {
	
	codLaudo: number;
	laudo: Laudo;
    relatorioAtendimento: RelatorioAtendimento;
	isLoading: boolean;
	userSession: UsuarioSessao;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _laudoService: LaudoService,
        private _relatorioAtendimentoService: RelatorioAtendimentoService,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
        this.codLaudo = +this._route.snapshot.paramMap.get('codLaudo');
        this.laudo = await this._laudoService.obterPorCodigo(this.codLaudo).toPromise();
        this.relatorioAtendimento = await this._relatorioAtendimentoService.obterPorCodigo(this.laudo.codRAT).toPromise();
	}

    ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}