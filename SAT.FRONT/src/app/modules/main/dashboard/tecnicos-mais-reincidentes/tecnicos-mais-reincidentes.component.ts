import { Component, OnInit } from '@angular/core';
import { TecnicoService } from 'app/core/services/tecnico.service';

@Component({
  selector: 'app-tecnicos-mais-reincidentes',
  templateUrl: './tecnicos-mais-reincidentes.component.html'
})
export class TecnicosMaisReincidentesComponent implements OnInit {

  public loading: boolean = true;

  constructor(private _tecnicoService: TecnicoService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    let listaTecnicos = (await this._tecnicoService
      .obterPorParametros({ indAtivo: 1 })
      .toPromise()).items;
  }

}
