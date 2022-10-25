import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UsuarioService } from 'app/core/services/usuario.service';
import { statusConst } from 'app/core/types/status-types';
import { Usuario, UsuarioData, UsuarioParameters } from 'app/core/types/usuario.types';
import moment from 'moment';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-contato-lista',
	templateUrl: './contato-lista.component.html'
})
export class ContatoListaComponent implements OnInit {
	usuarios: Usuario[] = [];
	@ViewChild('searchInputControl') searchInputControl: ElementRef;

	constructor(
		private _usuarioService: UsuarioService
	) { }

	async ngOnInit() {
		this.usuarios = (await this.obterUsuarios()).items;
		
		fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(1000)
			, distinctUntilChanged()
		).subscribe(async (text: string) => {
			this.usuarios = (await this.obterUsuarios(text)).items;
		});
	}

	async obterUsuarios(filtro: string = ''): Promise<UsuarioData> {
		let params: UsuarioParameters = {
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeUsuario',
			sortDirection: 'asc',
			codPerfisNotIn: "34,81,87,90,93,97,98",
			filter: filtro,
			ultimoAcessoInicio: moment().subtract(1, 'year').format('YYYY-MM-DD HH:mm:ss')
		};

		return await this._usuarioService
			.obterPorParametros(params)
			.toPromise();
	}
}
