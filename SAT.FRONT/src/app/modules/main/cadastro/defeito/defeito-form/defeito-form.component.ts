import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from './../../../../../core/services/custom-snackbar.service';
import { DefeitoService } from './../../../../../core/services/defeito.service';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UserService } from '../../../../../core/user/user.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Defeito } from 'app/core/types/defeito.types';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { statusConst } from 'app/core/types/status-types';

@Component({
  selector: 'app-defeito-form',
  templateUrl: './defeito-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class DefeitoFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codDefeito: number;
  isAddMode: boolean;
  form: FormGroup;
  defeito: Defeito;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _defeitoService: DefeitoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codDefeito = +this._route.snapshot.paramMap.get('codDefeito');
    this.isAddMode = !this.codDefeito;

    this.form = this._formBuilder.group({
      codDefeito: [
        {
          value: undefined,
          disabled: true
        }, 
      ],
      codEDefeito: ['', [Validators.required, Validators.maxLength(3)]],
      nomeDefeito: ['', Validators.required],
      indAtivo: [ statusConst.INATIVO ]
    });

    if (!this.isAddMode) {
      this._defeitoService.obterPorCodigo(this.codDefeito)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.defeito = data;
        })
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean"
      ? this.defeito[key] = +form[key]
      : this.defeito[key] = form[key]
    })
    
    this._defeitoService.atualizar(this.defeito).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    })
  }

  criar(): void{
    const form: any = this.form.getRawValue();

    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean"
      ? this.form[key] = +form[key]
      : this.form[key] = form[key]
    })

    this._defeitoService.criar(form).subscribe(() => {
      this._snack.exibirToast("Registro criado com sucesso!", "successs");
      this._location.back();
    })
   
  }
  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
