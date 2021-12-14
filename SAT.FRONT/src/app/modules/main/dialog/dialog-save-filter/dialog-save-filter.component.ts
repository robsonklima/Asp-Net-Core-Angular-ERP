import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FilterBase } from 'app/core/filters/filter-base';

@Component({
  selector: 'app-dialog-save-filter',
  templateUrl: './dialog-save-filter.component.html'
})
export class DialogSaveFilterComponent implements OnInit {

  form: FormGroup;

  constructor(private _formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<FilterBase>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    this.inicializarForm();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      nomeFiltro: [undefined, [Validators.required]]
    });
  }

  salvar(): void {
    const form = this.form.getRawValue();
    this.dialogRef.close(form);
  }

}