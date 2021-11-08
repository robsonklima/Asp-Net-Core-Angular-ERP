import { AfterViewInit, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { PontoUsuarioDataService } from 'app/core/services/ponto-usuario-data.service';
import { PontoUsuarioService } from 'app/core/services/ponto-usuario.service';
import { PontoUsuarioDataDivergencia } from 'app/core/types/ponto-usuario-data-divergencia.types';
import { PontoUsuarioData, PontoUsuarioDataData, PontoUsuarioDataParameters } from 'app/core/types/ponto-usuario-data.types';
import { PontoUsuario, PontoUsuarioParameters } from 'app/core/types/ponto-usuario.types';
import { UsuarioData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { PontoRelatoriosAtendimentoComponent } from '../ponto-relatorios-atendimento/ponto-relatorios-atendimento.component';

@Component({
  selector: 'app-ponto-horarios-lista',
  templateUrl: './ponto-horarios-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-pud {
          grid-template-columns: 80px 186px 112px 250px 64px auto 196px;
          
          @screen sm {
              grid-template-columns: 80px 186px 112px 250px 64px auto 196px;
          }
      
          @screen md {
              grid-template-columns: 80px 186px 112px 250px 64px auto 196px;
          }
      
          @screen lg {
              grid-template-columns: 80px 186px 112px 250px 64px auto 196px;
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
    private _dialog: MatDialog,
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

  visualizarRelatoriosAtendimento(dataRegistro: string, codUsuario: string): void {
    const dialogRef = this._dialog.open(PontoRelatoriosAtendimentoComponent, {
      data: {
        dataRegistro: dataRegistro,
        codUsuario: codUsuario
      },
      width: '960px',
    });

    dialogRef.afterClosed().subscribe((data: any) =>
    {
      
    });
  }

  paginar() {
    this.obterDados();
  }
}

