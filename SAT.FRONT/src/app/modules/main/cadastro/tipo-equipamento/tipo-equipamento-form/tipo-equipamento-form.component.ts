import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';
import { TipoEquipamento } from 'app/core/types/tipo-equipamento.types';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-tipo-equipamento-form',
  templateUrl: './tipo-equipamento-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class TipoEquipamentoFormComponent implements OnInit, OnDestroy {
  codTipoEquip: number;
  isAddMode: boolean;
  form: FormGroup;
  tipoEquipamento: TipoEquipamento;
  public tiposEquipamento: TipoEquipamento[] = [];

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _tipoEquipamentoService: TipoEquipamentoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
  ) { }

  async ngOnInit() {
    this.codTipoEquip = +this._route.snapshot.paramMap.get('codTipoEquip');
    this.isAddMode = !this.codTipoEquip;

    this.form = this._formBuilder.group({
      codTipoEquip: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      codETipoEquip: ['', [Validators.required, Validators.maxLength(2)]],
      nomeTipoEquip: ['', [Validators.required, Validators.maxLength(50)]]
    });

    this.tiposEquipamento = (await this._tipoEquipamentoService.obterPorParametros({
      sortActive: 'nomeTipoEquip',
      sortDirection: 'asc',
      pageSize: 50
    }).toPromise()).items;

    if (!this.isAddMode) {
      this._tipoEquipamentoService.obterPorCodigo(this.codTipoEquip)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.tipoEquipamento = data;
        });
    }
  }

  salvar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.tipoEquipamento,
      ...form
    };

    if (this.isAddMode) {
      this._tipoEquipamentoService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Tipo Equipamento ${obj.nomeEquip} adicionado com sucesso!`, "success");
        this._location.back();
      });
    }
    else {
      this._tipoEquipamentoService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Tipo Equipamento ${obj.nomeEquip} atualizado com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
