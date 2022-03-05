import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PlantaoTecnicoClienteService } from 'app/core/services/plantao-tecnico-cliente.service';
import { PlantaoTecnicoRegiaoService } from 'app/core/services/plantao-tecnico-regiao.service';
import { PlantaoTecnicoService } from 'app/core/services/plantao-tecnico.service';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { Location } from '@angular/common';
import { Subject } from 'rxjs';
import { Regiao } from 'app/core/types/regiao.types';
import { Cliente } from 'app/core/types/cliente.types';

@Component({
  selector: 'app-tecnico-plantao-form',
  templateUrl: './tecnico-plantao-form.component.html'
})
export class TecnicoPlantaoFormComponent implements OnInit {
  userSession: UserSession;
  form: FormGroup;
  isAddMode: boolean;
  regioes: Regiao[] = [];
  clientes: Cliente[] = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _plantaoTecnicoService: PlantaoTecnicoService,
    private _plantaoTecnicoRegiaoService: PlantaoTecnicoRegiaoService,
    private _plantaoTecnicoClienteService: PlantaoTecnicoClienteService,
    private _location: Location,
    private _snack: CustomSnackbarService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.inicializarForm();
  }

  private inicializarForm()
  {
    this.form = this._formBuilder.group({
      codTecnico: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      dataPlantao: [undefined, Validators.required],
      codRegioes: [undefined, Validators.required],
      codClientes: [undefined, Validators.required]
    });
  }

  salvar(): void
  {
    this.form.disable();
    this.isAddMode ? this.criar() : null;
  }

  private criar(): void
  {
    const form = this.form.getRawValue();

    let obj = {
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: 1
      }
    };

    this._plantaoTecnicoService.criar(obj).subscribe(() =>
    {
      this._snack.exibirToast("Cidade inserida com sucesso!", "success");
      this._location.back();
    }, e =>
    {
      this.form.enable();
    });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
