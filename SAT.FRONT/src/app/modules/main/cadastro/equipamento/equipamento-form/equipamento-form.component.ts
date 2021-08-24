import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Equipamento } from 'app/core/types/equipamento.types';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';

@Component({
  selector: 'app-equipamento-form',
  templateUrl: './equipamento-form.component.html'
})
export class EquipamentoFormComponent implements OnInit, OnDestroy {
  codEquip: number;
  equipamento: Equipamento;
  isAddMode: boolean;
  form: FormGroup;
  userSession: UsuarioSessionData;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _equipamentoService: EquipamentoService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codEquip = +this._route.snapshot.paramMap.get('codEquip');
    this.isAddMode = !this.codEquip;
    this.inicializarForm();
    
    if (!this.isAddMode) {
      this._equipamentoService.obterPorCodigo(this.codEquip)
      .pipe(first())
      .subscribe(data => {
        this.form.patchValue(data);
        this.equipamento = data;
      });
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codEquip: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nomeEquip: [undefined, Validators.required],
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.equipamento,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario
      }
    };
    
    // this._equipamentoService.atualizar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Equipamento ${obj.nomeEquip} atualizado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.equipamento,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario
      }
    };

    // this._equipamentoService.criar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Equipamento ${obj.nomeEquip} adicionado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
