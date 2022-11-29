import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORStatusService } from 'app/core/services/or-status.service';
import { ORItem } from 'app/core/types/or-item.types';
import { ORStatus, ORStatusParameters } from 'app/core/types/or-status.types';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-laboratorio-processo-reparo-form',
  templateUrl: './laboratorio-processo-reparo-form.component.html'
})
export class LaboratorioProcessoReparoFormComponent implements OnInit {
  @Input() item: ORItem;
  status: ORStatus[] = [];
  form: FormGroup;
  statusFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _orItemService: ORItemService,
    private _snack: CustomSnackbarService,
    private _orStatusService: ORStatusService
  ) { }

  async ngOnInit() {
    this.criarForm();
    this.obterStatus();

    this.form.patchValue({
      codMagnus: this.item?.peca?.codMagnus,
      descricao: this.item?.peca?.nomePeca,
      numSerie: this.item?.numSerie,
      usuarioTecnico: this.item?.usuarioTecnico?.nomeUsuario,
      codStatus: this.item?.codStatus,
    });

    this.registrarEmmiters();
  }

  registrarEmmiters() {
    this.form.controls['numSerie'].valueChanges.pipe(
      filter(v => !!v),
      tap(() => { }),
      debounceTime(700),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => {
      this.salvar();
    });

    this.form.controls['codStatus'].valueChanges.pipe(
      filter(v => !!v),
      tap(() => { }),
      debounceTime(700),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => {
      this.salvar();
    });
  }

  criarForm() {
    this.form = this._formBuilder.group({
      codMagnus: [{value: '', disabled: true }],
      descricao: [{value: '', disabled: true }],
      numSerie: [{value: '', disabled: false }],
      usuarioTecnico: [{value: '', disabled: true }],
      codStatus: [{value: '', disabled: false }],
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
    const f = this.form.getRawValue();

    this._orItemService.atualizar({
      ...this.item,
      ...this.form.getRawValue()
    }).subscribe(() => {
      this._snack.exibirToast('Processo de reparo atualizado', 'success')
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
