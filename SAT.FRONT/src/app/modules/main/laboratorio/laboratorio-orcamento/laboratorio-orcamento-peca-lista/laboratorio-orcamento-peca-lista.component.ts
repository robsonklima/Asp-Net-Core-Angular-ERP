import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoPecasEspecService } from 'app/core/services/orcamento-pecas-espec.service';
import { OrcamentoPecasEspecData, OrcamentoPecasEspecParameters } from 'app/core/types/orcamento-pecas-espec.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
	selector: 'app-laboratorio-orcamento-peca-lista',
	templateUrl: './laboratorio-orcamento-peca-lista.component.html',
	styles: [
        /* language=SCSS */
        `.list-grid-u {
            grid-template-columns: 10% auto 10% 15% ;
        }`
    ],
	animations: fuseAnimations,
	encapsulation: ViewEncapsulation.None,
})
export class LaboratorioOrcamentoPecaListaComponent implements AfterViewInit {
	@Input() codOrcamento: number;
    dataSourceData: OrcamentoPecasEspecData;
	userSession: UsuarioSessao;
	isLoading: boolean = false;

	constructor(
		private _userService: UserService,
		private _cdr: ChangeDetectorRef,
        private _orcamentoPecasEspecService: OrcamentoPecasEspecService,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngAfterViewInit(): void {
        this.obterDados();

		this._cdr.detectChanges();
	}

    private async obterDados(){
        const parametros: OrcamentoPecasEspecParameters = {
            ...{
                sortActive: 'codPeca',
                sortDirection: 'desc',
                codOrcamento: this.codOrcamento
            }
        }

        const data = await this._orcamentoPecasEspecService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;
        
        this.isLoading = false;
        this._cdr.detectChanges();
    }
    
    paginar() {
        this.obterDados();
    }
}