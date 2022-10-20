import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Usuario } from 'app/core/types/usuario.types';

@Component({
  selector: 'app-utilizacao-usuarios-dialog',
  templateUrl: './utilizacao-usuarios-dialog.component.html'
})
export class UtilizacaoUsuariosDialogComponent implements OnInit {
  usuarios: Usuario[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
  ) {
    this.usuarios = data?.usuarios;
  }

  ngOnInit(): void {
  }
}
