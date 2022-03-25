import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { GrupoEquipamentoService } from 'app/core/services/grupo-equipamento.service';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';
import { GrupoEquipamento } from 'app/core/types/grupo-equipamento.types';
import { TipoEquipamento } from 'app/core/types/tipo-equipamento.types';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-grupo-equipamento-form',
  templateUrl: './grupo-equipamento-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class GrupoEquipamentoFormComponent implements OnInit, OnDestroy {
  codGrupoEquip: number;
  codTipoEquip: number;
  isAddMode: boolean;
  form: FormGroup;
  grupoEquipamento: GrupoEquipamento;
  public tiposEquipamento: TipoEquipamento[] = [];

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _grupoEquipamentoService: GrupoEquipamentoService,
    private _tipoEquipamentoService: TipoEquipamentoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
  ) { }

  async ngOnInit() {
    this.codGrupoEquip = +this._route.snapshot.paramMap.get('codGrupoEquip');
    this.codTipoEquip = +this._route.snapshot.paramMap.get('codTipoEquip');
    this.isAddMode = !this.codGrupoEquip;

    this.form = this._formBuilder.group({
      codGrupoEquip: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      codEGrupoEquip: ['', [Validators.required, Validators.maxLength(5)]],
      nomeGrupoEquip: ['', [Validators.required, Validators.maxLength(50)]],
      codTipoEquip: ['', Validators.required]
    });

    this.tiposEquipamento = (await this._tipoEquipamentoService.obterPorParametros({
      sortActive: 'nomeTipoEquip',
      sortDirection: 'asc',
      pageSize: 50
    }).toPromise()).items;

    if (!this.isAddMode) {
      this._grupoEquipamentoService.obterPorCodigo(this.codGrupoEquip, this.codTipoEquip)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.grupoEquipamento = data;
        });
    }
  }

  salvar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.grupoEquipamento,
      ...form
    };

    if (this.isAddMode) {
      this._grupoEquipamentoService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Grupo Equipamento ${obj.nomeEquip} adicionado com sucesso!`, "success");
        this._location.back();
      });
    }
    else {
      this._grupoEquipamentoService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Grupo Equipamento ${obj.nomeEquip} atualizado com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
