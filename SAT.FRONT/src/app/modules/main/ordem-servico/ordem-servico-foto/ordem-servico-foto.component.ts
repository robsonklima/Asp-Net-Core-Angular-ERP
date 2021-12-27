import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Foto } from 'app/core/types/foto.types';
import { OrdemServicoFotosComponent } from '../ordem-servico-fotos/ordem-servico-fotos.component';

@Component({
  selector: 'app-ordem-servico-foto',
  templateUrl: './ordem-servico-foto.component.html'
})
export class OrdemServicoFotoComponent implements OnInit {
  foto: Foto;

  constructor(
    public dialogRef: MatDialogRef<OrdemServicoFotosComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.foto = data.foto;
  }

  ngOnInit(): void {
  }
}
