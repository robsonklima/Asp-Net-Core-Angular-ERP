import { AfterViewInit, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { PontoUsuarioDataService } from 'app/core/services/ponto-usuario-data.service';
import { PontoUsuarioService } from 'app/core/services/ponto-usuario.service';
import { PontoUsuarioData, PontoUsuarioDataData, PontoUsuarioDataParameters } from 'app/core/types/ponto-usuario-data.types';
import { PontoUsuario, PontoUsuarioParameters } from 'app/core/types/ponto-usuario.types';
import { UsuarioData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';

@Component({
  selector: 'app-ponto-horarios-lista',
  templateUrl: './ponto-horarios-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-pud {
          grid-template-columns: 80px 186px 112px auto 72px 198px 112px;
          
          @screen sm {
              grid-template-columns: 80px 186px 112px auto 72px 198px 112px;
          }
      
          @screen md {
              grid-template-columns: 80px 186px 112px auto 72px 198px 112px;
          }
      
          @screen lg {
              grid-template-columns: 80px 186px 112px auto 72px 198px 112px;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PontoHorariosListaComponent implements AfterViewInit {
  codPontoPeriodo: number;
  codUsuario: string;
  dataSourceData: PontoUsuarioDataData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _pontoUsuarioDataSvc: PontoUsuarioDataService,
    private _pontoUsuarioSvc: PontoUsuarioService,
    private _cdr: ChangeDetectorRef,
    private _userSvc: UserService,
    private _route: ActivatedRoute
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.codPontoPeriodo = +this._route.snapshot.paramMap.get('codPontoPeriodo');
    this.codUsuario = this._route.snapshot.paramMap.get('codUsuario');
    this.obterDados();
  }

  async obterDados() {
    this.isLoading = true;
    this._cdr.detectChanges();

    const datas = await this._pontoUsuarioDataSvc
      .obterPorParametros({
        codPontoPeriodo: this.codPontoPeriodo,
        codUsuario: this.codUsuario,
        sortActive: 'DataRegistro',
        sortDirection: 'desc'
      })
      .toPromise();

    const pontos = await this._pontoUsuarioSvc
      .obterPorParametros({
        codPontoPeriodo: this.codPontoPeriodo,
        codUsuario: this.codUsuario,
      })
      .toPromise();   
    
    for (let i = 0; i < datas.items.length; i++) {
      const pontoUsuarioData = datas.items[i];
      datas.items[i].pontosUsuario = this.obterPontosPorData(pontoUsuarioData, pontos.items);
    }

    this.dataSourceData = datas;
    this.isLoading = false;
  }

  private obterPontosPorData(pontoUsuarioData: PontoUsuarioData, pontos: PontoUsuario[]): PontoUsuario[] {
    return pontos.filter(
      p => moment(p.dataHoraRegistro).format('yyyy-MM-DD') == moment(pontoUsuarioData.dataRegistro).format('yyyy-MM-DD')
    );
  }

  paginar() {
    this.obterDados();
  }
}

