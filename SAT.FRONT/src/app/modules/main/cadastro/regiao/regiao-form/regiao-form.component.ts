import { ActivatedRoute } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Regiao } from 'app/core/types/regiao.types';
import { RegiaoService } from 'app/core/services/regiao.service';

@Component({
  selector: 'app-regiao-form',
  templateUrl: './regiao-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class RegiaoFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codRegiao: number;
  isAddMode: boolean;
  form: FormGroup;
  regiao: Regiao;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _regiaoService: RegiaoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codRegiao = +this._route.snapshot.paramMap.get('codRegiao');
    this.isAddMode = !this.codRegiao;

    this.form = this._formBuilder.group({
      codRegiao: [
        {
          value: undefined,
          disabled: true
        },
      ],
      nomeRegiao: ['', Validators.required],
      indAtivo: [statusConst.INATIVO]
    });

    if (!this.isAddMode) {
      this._regiaoService.obterPorCodigo(this.codRegiao)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.regiao = data;
        })
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj =
    {
      ... this.regiao,
      ...form,
      indAtivo: +form.indAtivo
    }

    this._regiaoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    })
  }

  criar(): void {
    const form: any = this.form.getRawValue();

    let obj =
    {
      ... this.regiao,
      ...form,
      indAtivo: +form.indAtivo
    }

    this._regiaoService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Registro criado com sucesso!", "success");
      this._location.back();
    })

  }
  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
