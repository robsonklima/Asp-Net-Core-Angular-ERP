import { Component, Input, OnInit } from '@angular/core';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Tecnico } from 'app/core/types/tecnico.types';

@Component({
  selector: 'app-tecnico-conta-lista',
  templateUrl: './tecnico-conta-lista.component.html'
})
export class TecnicoContaListaComponent implements OnInit {
  @Input() codTecnico;
  tecnico: Tecnico;

  constructor(
    private _tecnicoService: TecnicoService
  ) { }

  async ngOnInit() {
    if (!this.codTecnico) return;

    this.tecnico = await this._tecnicoService.obterPorCodigo(this.codTecnico).toPromise();
  }
}
