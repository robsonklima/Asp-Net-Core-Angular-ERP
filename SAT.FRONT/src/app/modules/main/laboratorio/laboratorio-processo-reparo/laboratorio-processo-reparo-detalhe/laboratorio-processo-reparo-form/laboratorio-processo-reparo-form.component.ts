import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ORItem } from 'app/core/types/or-item.types';
import { ORTempoReparo } from 'app/core/types/or-tempo-reparo.types';
import _ from 'lodash';

@Component({
  selector: 'app-laboratorio-processo-reparo-form',
  templateUrl: './laboratorio-processo-reparo-form.component.html'
})
export class LaboratorioProcessoReparoFormComponent implements OnInit {
  @Input() item: ORItem;
  tempoReparo: ORTempoReparo;
  form: FormGroup;

  constructor(
    private _formBuilder: FormBuilder
  ) { }

  async ngOnInit() {
    this.criarForm();

    this.tempoReparo = _.last(this.item.temposReparo);

    this.form.patchValue({
      codMagnus: this.item?.peca?.codMagnus,
      descricao: this.item?.peca?.nomePeca,
      numSerie: this.item?.numSerie,
      usuarioTecnico: this.item?.usuarioTecnico?.nomeUsuario,
      status: this.item?.statusOR?.descStatus,
    });
  }

  criarForm() {
    this.form = this._formBuilder.group({
      codMagnus: [
        {
          value: '',
          disabled: true
        },
      ],
      descricao: [
        {
          value: '',
          disabled: true
        },
      ],
      numSerie: [
        {
          value: '',
          disabled: false
        },
      ],
      usuarioTecnico: [
        {
          value: '',
          disabled: true
        },
      ],
      status: [
        {
          value: '',
          disabled: true
        },
      ],
      nivel: [undefined]
    });
  }

  salvar() {
    
  }
}
