import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Foto } from 'app/core/types/foto.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-ordem-servico-detalhe',
  templateUrl: './ordem-servico-detalhe.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class OrdemServicoDetalheComponent implements OnInit {
  tecnicos: string[] = ['João da Silva', 'Maria Vasconcelos', 'Adriano Nunes'];
  codOS: number;
  os: OrdemServico;
  fotos: Foto[] = [];

  constructor(
    private _route: ActivatedRoute,
    private _ordemServicoService: OrdemServicoService,
  ) { }

  ngOnInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');

    if (this.codOS) {
      this.obterDadosOrdemServico();
    }
  }

  private obterDadosOrdemServico(): void {
    this._ordemServicoService.obterPorCodigo(this.codOS)
      .pipe(first())
      .subscribe(res => {
        this.os = res;
      });
  }
}