import { Inject, Component, LOCALE_ID, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import 'leaflet';
import 'leaflet-routing-machine';
import _ from 'lodash';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import moment from 'moment';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Location } from '@angular/common';
import { BancadaLaboratorio } from 'app/core/types/bancada-laboratorio.types';
import { Usuario, UsuarioParameters } from 'app/core/types/usuario.types';
import { UsuarioService } from 'app/core/services/usuario.service';
import { statusConst } from 'app/core/types/status-types';
import { BancadaLaboratorioService } from 'app/core/services/bancada-laboratorio.service';
declare var L: any;

@Component({
  selector: 'app-laboratorio-bancada-dialog',
  templateUrl: './laboratorio-bancada-dialog.component.html',
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class LaboratorioBancadaDialogComponent implements OnInit {
  form: FormGroup;
  userSession: UserSession;
  codBancadaLaboratorio: number;
  bancada: BancadaLaboratorio;
  bancadas: BancadaLaboratorio[] = [];
  mapsPlaceholder: any = [];
  usuarios: Usuario[] = [];
  protected _onDestroy = new Subject<void>();
  usuarioFiltro: FormControl = new FormControl();
  searching: boolean;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _userSvc: UserService,
    private _bancadaLaboratorioService: BancadaLaboratorioService,
    private _usuarioService: UsuarioService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private dialogRef: MatDialogRef<LaboratorioBancadaDialogComponent>) {
      if (data)
      {
          this.bancadas = data;
          console.log(data);
          
      }
  
      this.userSession = JSON.parse(this._userSvc.userSession);
    }
  
  async ngOnInit() {
    this.obterUsuarios();
    this.criarForm();
    this.registrarEmitters();
  }

  criarForm(){
    this.form = this._formBuilder.group({
          codUsuario: [undefined, Validators.required],
          numBancada: [undefined, Validators.required],
      });
  }

  async registrarEmitters(){
    this.usuarioFiltro.valueChanges
			.pipe(filter(query => !!query),
				tap(() => this.searching = true),
				takeUntil(this._onDestroy),
				debounceTime(700),
				map(async query => {
					const data = await this._usuarioService.obterPorParametros({
						sortActive: 'NomeUsuario',
						sortDirection: 'asc',
            codPerfis: "61,63",
						filter: query,
						pageSize: 100,
					}).toPromise();

					return data.items.slice();
				}),
				delay(500),
				takeUntil(this._onDestroy)
			)
			.subscribe(async data => {
				this.searching = false;
				this.usuarios = await data;
			},
				() => {
					this.searching = false;
				}
			);
  }
  private async obterUsuarios() {
    const params: UsuarioParameters = {
        sortActive: 'nomeUsuario',
        codPerfis: "61,63",
        sortDirection: 'asc',
        indAtivo: statusConst.ATIVO,
        pageSize: 100
    }
    const data = await this._usuarioService.obterPorParametros(params).toPromise();
    this.usuarios = data.items;
    }

  salvar(){
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.bancada,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
      }
    };

    this._bancadaLaboratorioService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Usuário inserido com sucesso!", "success");
      this.dialogRef.close(true);
    }, e => {
      this.form.enable();
    });

    this.dialogRef.close(true);
  }

}