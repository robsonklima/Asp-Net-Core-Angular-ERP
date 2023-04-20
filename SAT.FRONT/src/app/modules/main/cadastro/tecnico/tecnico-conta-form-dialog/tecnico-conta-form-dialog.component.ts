import { Component, Inject, OnInit } from '@angular/core';
import { TecnicoContaService } from 'app/core/services/tecnico-conta-service';
import { TecnicoContaListaComponent } from '../tecnico-conta-lista/tecnico-conta-lista.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TecnicoConta } from 'app/core/types/tecnico.types';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-tecnico-conta-form',
  templateUrl: './tecnico-conta-form-dialog.component.html'
})
export class TecnicoContaFormDialogComponent implements OnInit {
  conta: TecnicoConta;
  form: FormGroup;

  constructor(
    private _tecnicoContaService: TecnicoContaService,
    private dialogRef: MatDialogRef<TecnicoContaListaComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
  ) {
    this.conta = data?.conta;
  }

  ngOnInit(): void {

  }

  salvar() {
    
  }
}
