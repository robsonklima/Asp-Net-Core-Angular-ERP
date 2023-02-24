import { AfterViewInit, ChangeDetectorRef, Component, Input, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { OSBancadaPecasService } from 'app/core/services/os-bancada-pecas.service';
import { OSBancadaPecas, OSBancadaPecasData, OSBancadaPecasParameters } from 'app/core/types/os-bancada-pecas.types';
import { PecaRE5114 } from 'app/core/types/pecaRE5114.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { LaboratorioOSBancadaPecaRE5114DialogComponent } from '../laboratorio-os-bancada-pecare5114-dialog/laboratorio-os-bancada-pecare5114-dialog.component';

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
    dataSourceData: OSBancadaPecasData;
	userSession: UsuarioSessao;
	isLoading: boolean = false;

	constructor(
		private _userService: UserService,
		private _cdr: ChangeDetectorRef,
        private _osBancadaPecasService: OSBancadaPecasService,
		private _dialog: MatDialog,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngAfterViewInit(): void {
        this.obterDados();

		this._cdr.detectChanges();
	}

    private async obterDados(){
        const parametros: OSBancadaPecasParameters = {
            ...{
                sortActive: 'codPecaRe5114',
                sortDirection: 'desc',
                codOsbancadas: this.codOsbancada.toString()
            }
        }

        const data = await this._osBancadaPecasService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;
        
        this.isLoading = false;
        this._cdr.detectChanges();
    }

	editarPeca(osBancadaPecas: OSBancadaPecas) {
        this._dialog.open(LaboratorioOSBancadaPecaRE5114DialogComponent, {
          data: { osBancadaPecas: osBancadaPecas }
        });
      }
    
    paginar() {
        this.obterDados();
    }
}