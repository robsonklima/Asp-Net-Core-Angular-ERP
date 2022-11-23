import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORStatusService } from 'app/core/services/or-status.service';
import { ORItem } from 'app/core/types/or-item.types';
import { ORStatus, ORStatusParameters } from 'app/core/types/or-status.types';
import { ORTempoReparo } from 'app/core/types/or-tempo-reparo.types';
import _ from 'lodash';

@Component({
  selector: 'app-laboratorio-processo-reparo-form',
  templateUrl: './laboratorio-processo-reparo-form.component.html'
})
export class LaboratorioProcessoReparoFormComponent implements OnInit {
  @Input() item: ORItem;
  tempoReparo: ORTempoReparo;
  status: ORStatus[] = [];
  form: FormGroup;
  statusFilterCtrl: FormControl = new FormControl();

  constructor(
    private _formBuilder: FormBuilder,
    private _orItemService: ORItemService,
    private _snack: CustomSnackbarService,
    private _orStatusService: ORStatusService
  ) { }

  async ngOnInit() {
    this.criarForm();
    this.obterStatus();

    this.tempoReparo = _.last(this.item.temposReparo);

    this.form.patchValue({
      codMagnus: this.item?.peca?.codMagnus,
      descricao: this.item?.peca?.nomePeca,
      numSerie: this.item?.numSerie,
      usuarioTecnico: this.item?.usuarioTecnico?.nomeUsuario,
      codStatus: this.item?.codStatus,
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
      codStatus: [
        {
          value: '',
          disabled: false
        },
      ],
      nivel: [undefined]
    });
  }

  async obterStatus(filtro: string = '') {
		let params: ORStatusParameters = {
			filter: filtro,
			sortActive: 'descStatus',
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._orStatusService
			.obterPorParametros(params)
			.toPromise();

		this.status = data.items;
	}

  salvar() {
    console.log({
      ...this.item,
      ...this.form.getRawValue()
    });
    

    this._orItemService.atualizar({
      ...this.item,
      ...this.form.getRawValue()
    }).subscribe(() => {
      this._snack.exibirToast('Processo de reparo atualizado', 'success')
    });
  }
}
