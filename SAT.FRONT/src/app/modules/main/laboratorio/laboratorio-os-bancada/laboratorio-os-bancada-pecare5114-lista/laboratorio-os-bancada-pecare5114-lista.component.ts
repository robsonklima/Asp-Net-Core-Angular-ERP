import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { PecaRE5114Service } from 'app/core/services/pecaRE5114.service';
import { PecaRE5114, PecaRE5114Data, PecaRE5114Parameters } from 'app/core/types/pecaRE5114.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { LaboratorioOSBancadaPecaRE5114DialogComponent } from '../laboratorio-os-bancada-pecare5114-dialog/laboratorio-os-bancada-pecare5114-dialog.component';
import { LaboratorioOSBancadaPecasDialogComponent } from '../laboratorio-os-bancada-pecas-dialog/laboratorio-os-bancada-pecas-dialog.component';

@Component({
	selector: 'app-laboratorio-os-bancada-pecare5114-lista',
	templateUrl: './laboratorio-os-bancada-pecare5114-lista.component.html',
	styles: [
        /* language=SCSS */
        `.list-grid-u {
            grid-template-columns: 10% 10% 50% ;
        }`
    ],
	animations: fuseAnimations,
	encapsulation: ViewEncapsulation.None,
})
export class LaboratorioOSBancadaPecaRE5114ListaComponent implements AfterViewInit {
	@Input() codOsbancada: number;
    dataSourceData: PecaRE5114Data;
	userSession: UsuarioSessao;
	isLoading: boolean = false;

	constructor(
		private _userService: UserService,
		private _cdr: ChangeDetectorRef,
        private _pecaRE5114Service: PecaRE5114Service,
		private _dialog: MatDialog,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngAfterViewInit(): void {
        this.obterDados();

		this._cdr.detectChanges();
	}

    private async obterDados(){
        const parametros: PecaRE5114Parameters = {
            ...{
                sortActive: 'numRe5114',
                sortDirection: 'desc',
                codOsbancada: this.codOsbancada
            }
        }

        const data = await this._pecaRE5114Service.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;
        
        this.isLoading = false;
        this._cdr.detectChanges();
    }

	editarPeca(pecaRE5114: PecaRE5114) {
        this._dialog.open(LaboratorioOSBancadaPecaRE5114DialogComponent, {
          data: { pecaRE5114: pecaRE5114 }
        });
      }
    
    paginar() {
        this.obterDados();
    }
}