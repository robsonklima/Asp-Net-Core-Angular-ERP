import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Usuario, UsuarioParameters, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { statusConst } from 'app/core/types/status-types';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculo } from 'app/core/types/auditoria-veiculo.types';
import { UsuarioService } from 'app/core/services/usuario.service';

@Component({
	selector: 'app-auditoria-detalhe',
	templateUrl: './auditoria-detalhe.component.html',
})
export class AuditoriaDetalheComponent implements OnInit {

	
	codAuditoria: number;
  	codAuditoriaVeiculo: number;
	auditoria: Auditoria;
  	auditoriaVeiculo: AuditoriaVeiculo;
	form: FormGroup;
	isAddMode: boolean;
	isLoading: boolean;
	userSession: UsuarioSessao;
	usuarios: Usuario[] = [];
	finalidade: string;
	searching: boolean;
	protected _onDestroy = new Subject<void>();
	clienteFilterCtrl: FormControl = new FormControl();

	constructor(
		private _formBuilder: FormBuilder,
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _auditoriaService: AuditoriaService,
    private _usuarioService: UsuarioService,

	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
        this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
        this.codAuditoriaVeiculo = +this._route.snapshot.paramMap.get('codAuditoriaVeiculo');
        this.inicializarForm();
        this.obterUsuarios();

        this._auditoriaService.obterPorCodigo(this.codAuditoria)
				.pipe(first())
				.subscribe(data => {
					this.form.patchValue(data);
					this.auditoria = data;
					this.validarFinalidade(this.auditoria);
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

    private inicializarForm(): void {
		this.form = this._formBuilder.group({
			codAuditoria: [undefined],
			codAuditoriaVeiculo: [undefined],
            nomeUsuario: [undefined],
            nomeFilial: [undefined],
            cpf: [undefined],
            placa: [undefined],
            cnh: [undefined],
            nome: [undefined],
            numCracha: [undefined],
            rg: [undefined],
            cnhCategorias: [undefined],
            finalidadesUso: [undefined],
            cnhValidade: [undefined],
		});
	}

	private validarFinalidade(auditoria){
		if(auditoria?.usuario?.tecnico?.codFrotaFinalidadeUso === 1 )
		{ 
			this.finalidade = "Apenas Trabalho";
		}
		else
		{
			this.finalidade = "Trabalho/Particular";
		}
		console.log(auditoria?.usuario?.tecnico?.codFrotaFinalidadeUso);

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}