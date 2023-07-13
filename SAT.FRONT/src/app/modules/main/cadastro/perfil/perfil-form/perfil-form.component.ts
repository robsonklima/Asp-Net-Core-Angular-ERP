import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { Location } from '@angular/common';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PerfilSetorService } from 'app/core/services/perfil-setor.service';
import { SetorService } from 'app/core/services/setor.service';
import { PerfilSetor } from 'app/core/types/perfil-setor.types';
import { Setor, SetorParameters } from 'app/core/types/setor.types';
import { statusConst } from 'app/core/types/status-types';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Perfil, PerfilParameters } from 'app/core/types/perfil.types';
import { PerfilService } from 'app/core/services/perfil.service';
@Component({
  selector: 'app-perfil-form',
  templateUrl: './perfil-form.component.html'
})
export class PerfilFormComponent implements OnInit {
  codSetor: number;
  codPerfil: number;
  perfilSetor: PerfilSetor;
  perfis: Perfil[] = [];
  setores: Setor[] = [];
  isAddMode: boolean;
  isLoading: boolean = false;
  userSession: UsuarioSessao;
  form: FormGroup;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _perfilSetorSrv: PerfilSetorService,
    private _setorSrv: SetorService,
    private _perfilSrv: PerfilService,
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _router: Router,
    public _location: Location,
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codPerfil = +this._route.snapshot.paramMap.get('codPerfil');
    this.codSetor = +this._route.snapshot.paramMap.get('codSetor');
    this.inicializarForm();
    this.obterSetores();
    this.obterPerfis();
    this.isAddMode = !this.codPerfil && !this.codSetor;

    if (!this.isAddMode)
    {
      this._perfilSetorSrv.obterPorParametros({
          codPerfil: this.codPerfil,
          codSetor: this.codSetor
      })
      .subscribe(perfil =>
      {
        this.perfilSetor = perfil.items.shift();
        this.form.patchValue(this.perfilSetor);
      });
    }
  }

  private inicializarForm()
  {
    this.form = this._formBuilder.group({
      codPerfil: [undefined],
      codSetor: [undefined],
     });
  }

  async obterPerfis(filtro: string = '') {
		let params: PerfilParameters = {
			filter: filtro,
			sortActive: 'nomePerfil',
			indAtivo: statusConst.ATIVO,
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._perfilSrv
			.obterPorParametros(params)
			.toPromise();
		this.perfis = data.items;
	}

  async obterSetores(filtro: string = '') {
		let params: SetorParameters = {
			filter: filtro,
			sortActive: 'nomeSetor',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._setorSrv
			.obterPorParametros(params)
			.toPromise();
		this.setores = data.items;
	}

  salvar(): void {

    let obj: PerfilSetor = {
            codPerfil: this.form.controls['codPerfil'].value,
            codSetor: this.form.controls['codSetor'].value,
            indAtivo: statusConst.ATIVO
    };
    
    this._perfilSetorSrv.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Perfil criado com sucesso!`, "success");
        this._router.navigate(['/perfil/form/' + obj.codPerfil + '/' + obj.codSetor]);
    });
}

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
