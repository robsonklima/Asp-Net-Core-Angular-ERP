import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ConferenciaService } from 'app/core/services/conferencia.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ConferenciaListaComponent } from '../conferencia-lista/conferencia-lista.component';

@Component({
  selector: 'app-conferencia-form',
  templateUrl: './conferencia-form.component.html'
})
export class ConferenciaFormComponent implements OnInit {
  @Input() public codUsuario: string;
  @Input() public fotoUsuario: string;

  loading: boolean = false;
  extensoes: string[] = ['image/png', 'image/jpeg'];
  tamanhoMaximo: number = 2097152; //2mb

  constructor(
    private _snack: CustomSnackbarService,
    private _conferenciaService: ConferenciaService,
    public dialogRef: MatDialogRef<ConferenciaListaComponent>,
    @Inject(MAT_DIALOG_DATA) protected data: any,
  ) { }

  ngOnInit(): void {
    
  }
}
