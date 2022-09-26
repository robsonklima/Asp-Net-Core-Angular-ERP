import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Foto } from 'app/core/types/foto.types';
import { AuditoriaFotoComponent } from '../auditoria-foto.component';

@Component({
  selector: 'app-auditoria-foto',
  templateUrl: './auditoria-foto-dialog.component.html'
})
export class AuditoriaFotoDialogComponent implements OnInit {
  foto: Foto;

  constructor(
    public dialogRef: MatDialogRef<AuditoriaFotoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.foto = data.foto;
  }

  ngOnInit(): void {
  }
}
