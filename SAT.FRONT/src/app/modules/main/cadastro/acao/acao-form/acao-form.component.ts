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
import { AcaoService } from 'app/core/services/acao.service';
import { Acao } from 'app/core/types/acao.types';

@Component({
  selector: 'app-acao-form',
  templateUrl: './acao-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class AcaoFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codAcao: number;
  isAddMode: boolean;
  form: FormGroup;
  acao: Acao;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _acaoService: AcaoService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codAcao = +this._route.snapshot.paramMap.get('codAcao');
    this.isAddMode = !this.codAcao;

    this.form = this._formBuilder.group({
      codAcao: [
        {
          value: undefined,
          disabled: true
        },
      ],
      codEAcao: ['', [Validators.required, Validators.maxLength(3)]],
      nomeAcao: ['', Validators.required],
      indAtivo: [statusConst.INATIVO]
    });

    if (!this.isAddMode) {
      this._acaoService.obterPorCodigo(this.codAcao)
        .pipe(first())
        .subscribe(data => {
          this.form.patchValue(data);
          this.acao = data;
        })
    }
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.acao,
      ...form,
      ...{
        indAtivo: +form.indAtivo,
      }
    };

    this._acaoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    })
  }

  criar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.acao,
      ...form,
      ...{
        indAtivo: +form.indAtivo,
      }
    };
    
    this._acaoService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Registro criado com sucesso!", "success");
      this._location.back();
    })

  }
  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
