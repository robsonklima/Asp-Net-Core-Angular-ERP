import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { Utils } from 'app/core/utils/utils';

@Component({
  selector: 'app-relatorio-atendimento-detalhe',
  templateUrl: './relatorio-atendimento-detalhe.component.html'
})
export class RelatorioAtendimentoDetalheComponent implements OnInit {
  codOS: number;
  codRAT: number;
  ordemServico: OrdemServico
  isPOS: boolean;
  loading: boolean;
  isAddMode: boolean;

  constructor(
    private _route: ActivatedRoute,
    private _utils: Utils,
    private _ordemServicoService: OrdemServicoService
  ) { }

  async ngOnInit() {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
		this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
		this.isAddMode = !this.codRAT;
    this.ordemServico = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
    this.isPOS = this._utils.isPOS(this.ordemServico.codEquip);
  }
}
