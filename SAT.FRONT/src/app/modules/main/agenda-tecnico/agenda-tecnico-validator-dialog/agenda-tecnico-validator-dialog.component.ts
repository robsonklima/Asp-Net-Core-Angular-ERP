import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-agenda-tecnico-validator-dialog',
  templateUrl: './agenda-tecnico-validator-dialog.component.html'
})
export class AgendaTecnicoValidatorDialogComponent implements OnInit
{

  message: string;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<AgendaTecnicoValidatorDialogComponent>)
  {
    if (data)
      this.message = data.message;
  }

  ngOnInit(): void { }

}
