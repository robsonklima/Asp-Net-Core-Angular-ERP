import { Component, Input, OnInit } from '@angular/core';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';

@Component({
  selector: 'app-ordem-servico-historico',
  templateUrl: './ordem-servico-historico.component.html'
})
export class OrdemServicoHistoricoComponent implements OnInit
{
  ordensServico: OrdemServico[] = [];
  ordemServico: OrdemServico;
  @Input() codEquipContrato: number;
  @Input() codOS: number;

  constructor (
    private _ordemServicoService: OrdemServicoService
  ) { }

  ngOnInit(): void
  {
    this.obterDados();
  }

  private async obterDados()
  {
    const params: OrdemServicoParameters =
    {
      codEquipContrato: this.codEquipContrato,
      sortActive: "codOS",
      sortDirection: "desc",
      pageSize: 10
    }

    const data = await this._ordemServicoService.obterPorParametros(params).toPromise();
    this.ordensServico = data.items.filter(os => os.codOS !== this.codOS);
  }
}
