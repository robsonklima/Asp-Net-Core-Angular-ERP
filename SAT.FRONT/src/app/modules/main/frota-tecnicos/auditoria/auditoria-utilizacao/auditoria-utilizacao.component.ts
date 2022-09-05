import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Usuario, UsuarioParameters, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { statusConst } from 'app/core/types/status-types';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculo, AuditoriaVeiculoParameters } from 'app/core/types/auditoria-veiculo.types';
import { UsuarioService } from 'app/core/services/usuario.service';
import { AuditoriaVeiculoService } from 'app/core/services/auditoria-veiculo.service';
import { AuditoriaVeiculoTanque, AuditoriaVeiculoTanqueParameters } from 'app/core/types/auditoria-veiculo-tanque.types';
import { AuditoriaVeiculoTanqueService } from 'app/core/services/auditoria-veiculo-tanque.service';

@Component({
	selector: 'app-auditoria-utilizacao',
	templateUrl: './auditoria-utilizacao.component.html',
})
export class AuditoriaUtilizacaoComponent implements OnInit {

	
	codAuditoria: number;
  codAuditoriaVeiculo: number;
	auditoria: Auditoria;
  auditoriaVeiculo: AuditoriaVeiculo;
	form: FormGroup;
	isAddMode: boolean;
	isLoading: boolean;
	userSession: UsuarioSessao;
	usuarios: Usuario[] = [];
  auditoriaVeiculos: AuditoriaVeiculo[] = [];
	searching: boolean;
  veiculos: AuditoriaVeiculo[];
  qtdLitros: number;
  tanques: AuditoriaVeiculoTanque [] = [];
	protected _onDestroy = new Subject<void>();
	
	constructor(
		private _formBuilder: FormBuilder,
		private _route: ActivatedRoute,
		private _router: Router,
		private _userService: UserService,
		private _auditoriaService: AuditoriaService,
    private _usuarioService: UsuarioService,
		private _snack: CustomSnackbarService,
    private _auditoriaVeiculoService: AuditoriaVeiculoService,
    private _auditoriaVeiculoTanqueService: AuditoriaVeiculoTanqueService,
    private _location: Location,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
    this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
    this.codAuditoriaVeiculo = +this._route.snapshot.paramMap.get('codAuditoriaVeiculo');
    this.inicializarForm();
    this.obterUsuarios();
    this.obterVeiculos();
    this.obterTanques();
    this.CalcularQtdLitros();

    this._auditoriaService.obterPorCodigo(this.codAuditoria)
			.pipe(first())
			.subscribe(data => {
				console.log(data);
				
				this.form.patchValue(data);
				this.auditoria = data;
			});
	}

    private async obterUsuarios()
    {
      const params: UsuarioParameters = {
        sortActive: 'nomeUsuario',
        sortDirection: 'asc',
        indAtivo: statusConst.ATIVO,
        pageSize: 100
      }
      const data = await this._usuarioService.obterPorParametros(params).toPromise();
      this.usuarios = data.items;
    }

    private async obterVeiculos()
    {
      const params: AuditoriaVeiculoParameters = {
        sortActive: 'codAuditoriaVeiculo',
        sortDirection: 'asc',
        pageSize: 100
      }
      const data = await this._auditoriaVeiculoService.obterPorParametros(params).toPromise();
      this.veiculos = data.items;
    }

    private async obterTanques()
    {
      const params: AuditoriaVeiculoTanqueParameters = {
        sortActive: 'codAuditoriaVeiculoTanque',
        sortDirection: 'asc',
      }
      const data = await this._auditoriaVeiculoTanqueService.obterPorParametros(params).toPromise();
      this.tanques = data.items;
    }

	  private inicializarForm(): void 
    {
		  this.form = this._formBuilder.group
      ({
			  codAuditoria: [undefined],
        codAuditoriaVeiculoTanque: [undefined],
			  dataHoraRetiradaVeiculo:[undefined],
			  dataHoraCad: [undefined],
			  totalDiasEmUso:[undefined],
			  odometroInicialRetirada:[undefined],
			  odometroPeriodoAuditado:[undefined],
        despesasSAT: [undefined],
			  kmFerias:[undefined],
			  creditosCartao:[undefined],
			  saldoCartao:[undefined],
			  observacoes:[undefined],
		  });
	  }

    private async CalcularQtdLitros()
    {
      
      switch (this.veiculos[0].codAuditoriaVeiculoTanque.toString())
            {
                case "1":
                  this.qtdLitros = 5;
                    break;
                case "2":
                  this.qtdLitros = 13.75;
                    break;
                case "3":
                  this.qtdLitros = 27.50;
                    break;
                case "4":
                  this.qtdLitros = 41.25;
                    break;
                case "5":
                  this.qtdLitros = 55.00;
                    break;
                default:
                  this.qtdLitros = 0;
                    break;
            }
    }

    salvar(): void
    {
      this.form.disable();
      this.isAddMode ? this.criar() : this.atualizar();
    }
  
    private atualizar(): void
    {
      const form: any = this.form.getRawValue();
  
      let obj = {
        ...this.auditoria,
        ...form,
        ...{
          dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
          codUsuarioManut: this.userSession.usuario.codUsuario,
        }
      };
  
      this._usuarioService.atualizar(obj).subscribe(() =>
      {
        this._snack.exibirToast("Usuario atualizada com sucesso!", "success");
        this._location.back();
      }, e =>
      {
        this.form.enable();
      });
    }
  
    private criar(): void
    {
      const form = this.form.getRawValue();
  
      let obj = {
        ...this.auditoria,
        ...form,
        ...{
          dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
          codUsuarioCad: this.userSession.usuario.codUsuario,
        }
      };
  
      this._auditoriaService.criar(obj).subscribe(() =>
      {
        this._snack.exibirToast("Auditoria inserida com sucesso!", "success");
        this._location.back();
      }, e =>
      {
        this.form.enable();
      });
    }

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}