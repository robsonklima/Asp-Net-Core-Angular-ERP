import { ActivatedRoute } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { Location } from '@angular/common';
import { UserService } from 'app/core/user/user.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ANSService } from 'app/core/services/ans.service';
import { ANS } from 'app/core/types/ans.types';

@Component({
  selector: 'app-ans-form',
  templateUrl: './ans-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class ANSFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codANS: number;
  isAddMode: boolean;
  form: FormGroup;
  ans: ANS;
  loading: boolean;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _ansService: ANSService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codANS = +this._route.snapshot.paramMap.get('codANS');
    this.isAddMode = !this.codANS;

    this.form = this._formBuilder.group({
      nomeANS: ['', [Validators.required, Validators.maxLength(20)]],
      descANS: ['', Validators.required],
      sabado: [''],
      domingo: [''],
      feriado: [''],
      permiteAgendamento: [''],
    });

    if (!this.isAddMode) {
      this.ans = await this._ansService.obterPorCodigo(this.codANS).toPromise();
      this.form.patchValue(this.ans);
      this.form.controls['sabado'].setValue(this.ans.sabado == 'SIM' ? true : false);
      this.form.controls['domingo'].setValue(this.ans.domingo == 'SIM' ? true : false);
      this.form.controls['feriado'].setValue(this.ans.feriado == 'SIM' ? true : false);
      this.form.controls['permiteAgendamento'].setValue(this.ans.permiteAgendamento == 'SIM' ? true : false);
    }
  }

  salvar(): void {
    this.loading = true;

    const form: any = this.form.getRawValue();

    let obj = {
      ...this.ans,
      ...form,
      ...{
        sabado: form.sabado ? 'SIM' : 'NAO',
        domingo: form.domingo ? 'SIM' : 'NAO',
        feriado: form.feriado ? 'SIM' : 'NAO',
        permiteAgendamento: form.permiteAgendamento ? 'SIM' : 'NAO',
        indAtivo: +form.indAtivo,
      }
    };

    if (this.isAddMode) {
      this._ansService.criar(obj).subscribe(() => {
        this._snack.exibirToast("Registro criado com sucesso!", "success");

        this._location.back();
      }, () => {
        this._snack.exibirToast("Erro ao criar registro!", "error");
      });
    } else {
      this._ansService.atualizar(obj)
        .subscribe(() => {
          this._snack.exibirToast("Registro atualizado com sucesso!", "success");

          this._location.back();
        }, () => {
          this._snack.exibirToast("Erro ao criar registro!", "error");
        });
    }
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
