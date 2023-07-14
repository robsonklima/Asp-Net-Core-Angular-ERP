import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { RecursoBloqueadoService } from 'app/core/services/recurso-bloqueado.service';
import { PerfilSetor } from 'app/core/types/perfil-setor.types';
import { RecursoBloqueado, RecursoBloqueadoData, RecursoBloqueadoParameters } from 'app/core/types/recurso-bloqueado.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import _ from 'lodash';
import { Subject } from 'rxjs';
import { PerfilFormRecursosBloqueadosDialogComponent } from './perfil-form-recursos-bloqueados-dialog/perfil-form-recursos-bloqueados-dialog.component';

@Component({
  selector: 'app-perfil-form-recursos-bloqueados',
  templateUrl: './perfil-form-recursos-bloqueados.component.html',
  styles: [
    /* language=SCSS */
    `.list-grid-u {
        grid-template-columns: 200px 150px 150px 150px 150px auto;
    }`
],
animations: fuseAnimations,
encapsulation: ViewEncapsulation.None,
})
export class PerfilFormRecursosBloqueadosComponent implements OnInit {
  @Input() perfilSetor: PerfilSetor;

  recursosBloqueados: RecursoBloqueadoData;
  userSession: UsuarioSessao;
  isAddMode: boolean;
  searching: boolean;
  flagConsultar: boolean;
  flagEditar: boolean;
  flagCriar: boolean;
  flagExcluir: boolean;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _recursoBloqueadoSrv: RecursoBloqueadoService,
    private _dialog: MatDialog,
    private _userService: UserService  
    ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.obterDados();
  }

  async obterDados(){
    this.obterRecursoBloqueado();
  }

  async obterRecursoBloqueado(filtro: string = '') {
		
    let params: RecursoBloqueadoParameters = {
			filter: filtro,
			sortActive: 'codRecursoBloqueado',
      codPerfil: this.perfilSetor.codPerfil,
      codSetor: this.perfilSetor.codSetor,
			indAtivo: statusConst.ATIVO,
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._recursoBloqueadoSrv
			.obterPorParametros(params)
			.toPromise();

		this.recursosBloqueados = data;
	}

  validarAcao(recurso: RecursoBloqueado, acao: string) {
    switch(acao)
    {
      case "consultar":
        if(recurso.claims.includes("CanReadResource"))
          {
            this.flagConsultar = true;
            return true;
          }

      case "criar":
        if(recurso.claims.includes("CanAddResource"))
        {
          this.flagCriar = true;
          return true;
        }

      case "editar":
        if(recurso.claims.includes("CanEditResource"))
        {
          this.flagEditar = true;
          return true;
        }

      case "excluir":
        if(recurso.claims.includes("CanDeleteResource"))
        {
          this.flagExcluir = true;
          return true;
        }
    }

    return false;
  }

  remover(recurso: RecursoBloqueado){
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: `Deseja remover o atendimento?`,
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                await this._recursoBloqueadoSrv.deletar(recurso.codRecursoBloqueado).toPromise();
                this.ngOnInit();
            }
        });
    }
  
  adicionar(){
    const dialogRef = this._dialog.open(PerfilFormRecursosBloqueadosDialogComponent, {
      data: { perfilSetor: this.perfilSetor }
    });

    dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
        if (confirmacao) {
            this.ngOnInit();
        }
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}