import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Foto, FotoModalidadeEnum } from 'app/core/types/foto.types';
import { OrdemServicoFotoComponent } from '../ordem-servico-foto/ordem-servico-foto.component';

@Component({
  selector: 'app-ordem-servico-fotos',
  templateUrl: './ordem-servico-fotos.component.html'
})
export class OrdemServicoFotosComponent implements OnInit {
  @Input() fotos: Foto[];

  constructor(
    private _dialog: MatDialog
  ) { }

  ngOnInit(): void {
  }

  ampliarFoto(foto: Foto) {
		this._dialog.open(OrdemServicoFotoComponent, {
			data: { foto: foto}
		});
	}

  formatarModalidadeFoto(modalidade: string): string {
    return FotoModalidadeEnum[modalidade];
  }
}
