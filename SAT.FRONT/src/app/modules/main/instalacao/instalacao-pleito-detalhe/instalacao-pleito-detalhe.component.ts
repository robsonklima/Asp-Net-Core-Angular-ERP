import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-instalacao-pleito-detalhe',
  templateUrl: './instalacao-pleito-detalhe.component.html'
})
export class InstalacaoPleitoDetalheComponent implements OnInit {
  codInstalPleito: number;
  isAddMode: boolean;
  isLoading: boolean = true;
  userSession: UserSession;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _route: ActivatedRoute,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.isLoading = true;
		this.codInstalPleito = +this._route.snapshot.paramMap.get('codInstalPleito');
		this.isAddMode = !this.codInstalPleito;
		this.inicializarForm();
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
