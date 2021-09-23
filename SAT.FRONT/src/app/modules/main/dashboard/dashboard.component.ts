import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  
})
export class DashboardComponent implements AfterViewInit {
  @ViewChild('sidenav') sidenav: MatSidenav;
  usuarioSessao: UsuarioSessao;
  visaoSelecionada: string = 'Performances das Filiais e Resultado Geral do DSS';
  filtro: any;

  constructor(
    private _userService: UserService,
    private _cdr: ChangeDetectorRef
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.configurarFiltro();
    this._cdr.detectChanges();

    this.sidenav.closedStart.subscribe(() => {
      this.configurarFiltro();
    })
  }

  private configurarFiltro(): void {
    this.filtro = this._userService.obterFiltro('dashboard');

    if (!this.filtro) {
        return;
    }

    // Filtro obrigatorio de filial quando o usuario esta vinculado a uma filial
    if (this.usuarioSessao?.usuario?.codFilial) {
        this.filtro.parametros.codFiliais = [this.usuarioSessao.usuario.codFilial]
    }

    Object.keys(this.filtro?.parametros).forEach((key) => {
        if (this.filtro.parametros[key] instanceof Array) {
            this.filtro.parametros[key] = this.filtro.parametros[key].join()
        };
    });
  }
}
