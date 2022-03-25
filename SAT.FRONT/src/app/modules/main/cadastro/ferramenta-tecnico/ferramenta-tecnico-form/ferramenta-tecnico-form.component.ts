import { ActivatedRoute } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FerramentaTecnico } from 'app/core/types/ferramenta-tecnico.types';
import { FerramentaTecnicoService } from 'app/core/services/ferramenta-tecnico.service';

@Component({
  selector: 'app-ferramenta-tecnico-form',
  templateUrl: './ferramenta-tecnico-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class FerramentaTecnicoFormComponent implements OnInit, OnDestroy {
  codFerramentaTecnico: number;
  isAddMode: boolean;
  form: FormGroup;
  ferramentaTecnico: FerramentaTecnico;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _ferramentaTecnico: FerramentaTecnicoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.codFerramentaTecnico = +this._route.snapshot.paramMap.get('codFerramentaTecnico');
    this.isAddMode = !this.codFerramentaTecnico;

    this.form = this._formBuilder.group({
      codFerramentaTecnico: [
        {
          value: undefined,
          disabled: true
        },
      ],
      nome: ['', [Validators.required, Validators.maxLength(80)]],
      status: [undefined]
    });

    if (!this.isAddMode) {
      this._ferramentaTecnico.obterPorCodigo(this.codFerramentaTecnico)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.ferramentaTecnico = data;
        })
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.ferramentaTecnico,
      ...form,
      status: +form.status
    };

    this._ferramentaTecnico.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    })
  }

  criar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.ferramentaTecnico,
      ...form,
      status: +form.status
    };

    this._ferramentaTecnico.criar(obj).subscribe(() => {
      this._snack.exibirToast("Registro criado com sucesso!", "success");
      this._location.back();
    })

  }
  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
