import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InstalacaoPleitoService } from 'app/core/services/instalacao-pleito.service';
import { InstalacaoPleito } from 'app/core/types/instalacao-pleito.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-instalacao-pleito-detalhe',
  templateUrl: './instalacao-pleito-detalhe.component.html'
})
export class InstalacaoPleitoDetalheComponent implements OnInit {
  codInstalPleito: number;
  instalPleito: InstalacaoPleito;
  isAddMode: boolean;
  isLoading: boolean = true;
  userSession: UserSession;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _route: ActivatedRoute,
    private _instalPleitoService: InstalacaoPleitoService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.isLoading = true;
		this.codInstalPleito = +this._route.snapshot.paramMap.get('codInstalPleito');
		this.isAddMode = !this.codInstalPleito;

    if (!this.isAddMode)
      await this._instalPleitoService
        .obterPorCodigo(this.codInstalPleito)
        .subscribe((test) => {
          let a = test
          console.log(test)
        });

		this.inicializarForm();
    this.isLoading = false;
  }

  inicializarForm() {
    
  }

  exportar() {
    
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
