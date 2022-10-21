import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ORService } from 'app/core/services/or.service';
import { OR } from 'app/core/types/OR.types';

@Component({
  selector: 'app-laboratorio-processo-reparo-form',
  templateUrl: './laboratorio-processo-reparo-form.component.html'
})
export class LaboratorioProcessoReparoFormComponent implements OnInit {
  or: OR;
  form: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private _orService: ORService
  ) {}

  ngOnInit(): void {
    this.criarForm();
  }

  criarForm() {
    this.form = this._formBuilder.group({
      
    });
  }

  salvar() {
    
  }
}
