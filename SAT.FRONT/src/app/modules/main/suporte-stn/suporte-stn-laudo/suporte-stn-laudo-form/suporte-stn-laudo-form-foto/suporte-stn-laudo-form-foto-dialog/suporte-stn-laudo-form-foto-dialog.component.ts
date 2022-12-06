import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Foto } from 'app/core/types/foto.types';
import { SuporteStnLaudoFormFotoComponent } from '../suporte-stn-laudo-form-foto.component';

@Component({
    selector: 'app-suporte-stn-laudo-form-foto-dialog',
    templateUrl: './suporte-stn-laudo-form-foto-dialog.component.html'
})
export class SuporteStnLaudoFormFotoDialogComponent implements OnInit {
  foto: Foto;

  constructor(
    public dialogRef: MatDialogRef<SuporteStnLaudoFormFotoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.foto = data.foto;
  }

  ngOnInit(): void {
  }
}
