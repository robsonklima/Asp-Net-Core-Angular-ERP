import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { statusConst } from 'app/core/types/status-types';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Location } from '@angular/common';
import { Auditoria } from 'app/core/types/auditoria.types';
import { Usuario, UsuarioParameters } from 'app/core/types/usuario.types';
import { UsuarioService } from 'app/core/services/usuario.service';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { PerfilEnum } from 'app/core/types/perfil.types';
import moment from 'moment';

@Component({
  selector: 'app-auditoria-form',
  templateUrl: './auditoria-form.component.html'
})
export class AuditoriaFormComponent implements OnInit, OnDestroy {

  protected _onDestroy = new Subject<void>();
  public auditoria: Auditoria;
  public loading: boolean = true;
  public codAuditoria: number;
  public form: FormGroup;
  public usuarios: Usuario[] = [];
  usuarioFiltro: FormControl = new FormControl();
  searching: boolean;

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _usuarioService: UsuarioService,
    private _auditoriaService: AuditoriaService,
    public _location: Location,
  ) { }

  async ngOnInit() {

    this.inicializarForm();
    this.registrarEmitters();
    this.obterUsuarios();
  }

  private inicializarForm() {

    this.form = this._formBuilder.group({
      codAuditoria: [
        {
          value: undefined,
          disabled: true
        }
      ],
      codUsuario: [undefined, Validators.required],
      dataRetiradaVeiculo: [undefined],
    });

  }

  private registrarEmitters() {
		this.usuarioFiltro.valueChanges
			.pipe(filter(query => !!query),
				tap(() => this.searching = true),
				takeUntil(this._onDestroy),
				debounceTime(700),
				map(async query => {
					const data = await this._usuarioService.obterPorParametros({
						sortActive: 'NomeUsuario',
						sortDirection: 'asc',
            codPerfil: PerfilEnum.FILIAL_TECNICO_DE_CAMPO,
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
      codPerfil: PerfilEnum.FILIAL_TECNICO_DE_CAMPO,
			sortDirection: 'asc',
			indAtivo: statusConst.ATIVO,
			pageSize: 100
		}
    const data = await this._usuarioService.obterPorParametros(params).toPromise();
		this.usuarios = data.items;
	}

  public salvar(): void {

    const form = this.form.getRawValue();

    let obj = {
      ...this.auditoria,
      ...form,
      ...{
            dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
            codAuditoriaStatus: 1,    
        
      }
    };

      this._auditoriaService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Auditoria criada com sucesso!`, "success");
        this._location.back();
      });
    } 
    
  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

}
