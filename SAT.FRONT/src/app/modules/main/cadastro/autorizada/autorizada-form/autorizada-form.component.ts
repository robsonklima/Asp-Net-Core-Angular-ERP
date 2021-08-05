import { Location } from '@angular/common';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Autorizada } from 'app/core/types/autorizada.types';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ReplaySubject, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { AutorizadaService } from 'app/core/services/autorizada.service';

@Component({
  selector: 'app-autorizada-form',
  templateUrl: './autorizada-form.component.html',
  encapsulation: ViewEncapsulation.None
})

export class AutorizadaFormComponent implements OnInit {
  codAutorizada: number;
  isAddMode: boolean;
  form: FormGroup;
  autorizada: Autorizada;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _autorizadaService: AutorizadaService,    
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.codAutorizada = +this._route.snapshot.paramMap.get('codAutorizada');
    this.isAddMode = !this.codAutorizada;

    this.form = this._formBuilder.group({
      codAutorizada: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      codEGrupoEquip: ['', [Validators.required, Validators.maxLength(5)]],
      nomeGrupoEquip: ['', Validators.required],
      codTipoEquip: ['', Validators.required]
    });

    if (!this.isAddMode) {
      this._autorizadaService.obterPorCodigo(this.codAutorizada)
      .pipe(first())
      .subscribe(data => {
        this.form.patchValue(data);
        this.autorizada = data;
      });
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();
    
    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean" 
        ? this.autorizada[key] = +form[key]
        : this.autorizada[key] = form[key];
    });

    this._autorizadaService.atualizar(this.autorizada).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    });
  }

  criar(): void {
    const form: any = this.form.getRawValue();
    
    Object.keys(form).forEach((key, i) => {
      typeof form[key] == "boolean" ? form[key] = +form[key] : form[key] = form[key];
    });

    this._autorizadaService.criar(form).subscribe(() => {
      this._snack.exibirToast("Registro inserido com sucesso!", "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
