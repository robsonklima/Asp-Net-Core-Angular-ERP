import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { GrupoEquipamentoService } from 'app/core/services/grupo-equipamento.service';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';
import { GrupoEquipamento } from 'app/core/types/grupo-equipamento.types';
import { TipoEquipamento, TipoEquipamentoData } from 'app/core/types/tipo-equipamento.types';
import { ReplaySubject, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-grupo-equipamento-form',
  templateUrl: './grupo-equipamento-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class GrupoEquipamentoFormComponent implements OnInit {
  codGrupoEquip: number;
  isAddMode: boolean;
  form: FormGroup;
  grupoEquipamento: GrupoEquipamento;
  public tipoEquipamentoFiltro: FormControl = new FormControl();
  public tiposEquipamento: ReplaySubject<TipoEquipamento[]> = new ReplaySubject<TipoEquipamento[]>(1);

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _grupoEquipamentoService: GrupoEquipamentoService,
    private _tipoEquipamentoService: TipoEquipamentoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.codGrupoEquip = +this._route.snapshot.paramMap.get('codGrupoEquip');
    this.isAddMode = !this.codGrupoEquip;
    this.obterTiposEquipamento();

    this.form = this._formBuilder.group({
      codGrupoEquip: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      codEGrupoEquip: ['', [Validators.required, Validators.maxLength(5)]],
      nomeGrupoEquip: ['', Validators.required],
      codTipoEquip: ['', Validators.required]
    });

    this.tipoEquipamentoFiltro.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterTiposEquipamento(this.tipoEquipamentoFiltro.value);
      });

    if (!this.isAddMode) {
      this._grupoEquipamentoService.obterPorCodigo(this.codGrupoEquip)
      .pipe(first())
      .subscribe(data => {
        this.form.patchValue(data);
        this.grupoEquipamento = data;
        this.obterTiposEquipamento(this.grupoEquipamento.tipoEquipamento.nomeTipoEquip);
      });
    }
  }

  obterTiposEquipamento(filter: string = ''): void {
    this._tipoEquipamentoService.obterPorParametros({
      sortActive: 'nomeTipoEquip',
      sortDirection: 'asc',
      filter: filter,
      pageSize: 50
    }).subscribe((data: TipoEquipamentoData) => {
      if (data.tiposEquipamento.length) this.tiposEquipamento.next(data.tiposEquipamento.slice());
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();
    
    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean" 
        ? this.grupoEquipamento[key] = +form[key]
        : this.grupoEquipamento[key] = form[key];
    });

    this._grupoEquipamentoService.atualizar(this.grupoEquipamento).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    });
  }

  criar(): void {
    const form: any = this.form.getRawValue();
    
    Object.keys(form).forEach((key, i) => {
      typeof form[key] == "boolean" ? form[key] = +form[key] : form[key] = form[key];
    });

    this._grupoEquipamentoService.criar(form).subscribe(() => {
      this._snack.exibirToast("Registro inserido com sucesso!", "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
