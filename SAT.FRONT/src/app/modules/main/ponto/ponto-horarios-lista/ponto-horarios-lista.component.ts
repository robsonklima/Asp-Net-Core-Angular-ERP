import { AfterViewInit, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PontoPeriodoUsuarioService } from 'app/core/services/ponto-periodo-usuario.service';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoUsuarioDataDivergenciaService } from 'app/core/services/ponto-usuario-data-divergencia.service';
import { PontoUsuarioDataService } from 'app/core/services/ponto-usuario-data.service';
import { pontoPeriodoUsuarioStatusConst } from 'app/core/types/ponto-periodo-usuario-status.types';
import { PontoPeriodoUsuario } from 'app/core/types/ponto-periodo-usuario.types';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
import { PontoUsuarioDataDivergencia } from 'app/core/types/ponto-usuario-data-divergencia.types';
import { pontoUsuarioDataStatusAcessoConst } from 'app/core/types/ponto-usuario-data-status-acesso.types';
import { pontoUsuarioDataStatusConst } from 'app/core/types/ponto-usuario-data-status.types';
import { PontoUsuarioData, PontoUsuarioDataData } from 'app/core/types/ponto-usuario-data.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';
import { PontoInconsistenciaFormComponent } from '../ponto-inconsistencia-form/ponto-inconsistencia-form.component';
import { PontoRelatoriosAtendimentoComponent } from '../ponto-relatorios-atendimento/ponto-relatorios-atendimento.component';
import { PontoUsuarioFormComponent } from '../ponto-usuario-form/ponto-usuario-form.component';

@Component({
  selector: 'app-ponto-horarios-lista',
  templateUrl: './ponto-horarios-lista.component.html',
  styles: [`
      .list-grid-relatorios {
          grid-template-columns: 80px 186px 112px 250px 72px auto 196px;
      }
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PontoHorariosListaComponent implements AfterViewInit {
  codPontoPeriodo: number;
  pontoPeriodo: PontoPeriodo;
  pontoPeriodoUsuario: PontoPeriodoUsuario;
  codUsuario: string;
  usuario: Usuario;
  dataSourceData: PontoUsuarioDataData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _pontoUsuarioDataSvc: PontoUsuarioDataService,
    private _pontoPeriodoSvc: PontoPeriodoService,
    private _pontoUsuarioDataDivergenciaSvc: PontoUsuarioDataDivergenciaService,
    private _pontoPeriodoUsuarioSvc: PontoPeriodoUsuarioService,
    private _cdr: ChangeDetectorRef,
    private _dialog: MatDialog,
    private _userSvc: UserService,
    private _route: ActivatedRoute,
    private _snack: CustomSnackbarService
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.codPontoPeriodo = +this._route.snapshot.paramMap.get('codPontoPeriodo');
    this.codUsuario = this._route.snapshot.paramMap.get('codUsuario');
    this.obterHorarios();
    this._cdr.detectChanges();
  }

  async obterHorarios() {
    this.isLoading = true;
    this.usuario = await this._userSvc.obterPorCodigo(this.codUsuario).toPromise();

    this.pontoPeriodo = await this._pontoPeriodoSvc
      .obterPorCodigo(this.codPontoPeriodo)
      .toPromise();

    this.pontoPeriodoUsuario = (await this._pontoPeriodoUsuarioSvc
      .obterPorParametros({codUsuario: this.codUsuario, codPontoPeriodo: this.codPontoPeriodo})
      .toPromise())
      .items
      .shift();

    const datas = await this._pontoUsuarioDataSvc
      .obterPorParametros({
        codPontoPeriodo: this.codPontoPeriodo,
        codUsuario: this.codUsuario,
        sortActive: 'DataRegistro',
        sortDirection: 'desc'
      })
      .toPromise();

    this.dataSourceData = datas;
    this.isLoading = false;
  }

  private async conferirPontoData(pontoData: PontoUsuarioData) {
    if (pontoData.codPontoUsuarioDataStatus == pontoUsuarioDataStatusConst.CONFERIDO) {
      return this._snack.exibirToast('Você já conferiu esta data');
    }

    pontoData.codPontoUsuarioDataStatus = pontoUsuarioDataStatusConst.CONFERIDO;
    pontoData.codUsuarioManut = this.userSession.usuario.codUsuario;
    pontoData.dataHoraManut = moment().format('yyyy-MM-DD HH:mm');
    await this._pontoUsuarioDataSvc.atualizar(pontoData).toPromise();
    this.obterHorarios();
    this._snack.exibirToast('Ponto conferido com sucesso');

    let validaInconsistencia: boolean;

    if (pontoData.pontosUsuario.length % 2 == 1) {
      validaInconsistencia = true;
    } else if (pontoData.pontosUsuario.length >= 2) {
      const primeiroPonto = moment(pontoData.pontosUsuario[0].dataHoraRegistro);
      const ultimoPonto = moment(pontoData.pontosUsuario[pontoData.pontosUsuario.length - 1].dataHoraRegistro);

      let intervaloEntrePontos = moment.duration(primeiroPonto.diff(ultimoPonto)).asHours();

      switch (pontoData.pontosUsuario.length) {
        case 2:
          if (intervaloEntrePontos > 6) {
            validaInconsistencia = true;
          }
          break;

        case 4:
          if (intervaloEntrePontos > 6) {
            const saidaIntervalo = moment(pontoData.pontosUsuario[1].dataHoraRegistro);
            const retornoIntervalo = moment(pontoData.pontosUsuario[2].dataHoraRegistro);
            intervaloEntrePontos = moment.duration(saidaIntervalo.diff(retornoIntervalo)).asHours();

            if (intervaloEntrePontos < 1) {
              validaInconsistencia = true;
            }
            else if (intervaloEntrePontos > 2) {
              validaInconsistencia = true;
            }
          }
          break;
      }
    }

    if (validaInconsistencia) {
      this.validarInconsistencia(pontoData);
    }
  }

  private async validarInconsistencia(pontoData: PontoUsuarioData) {
    let divergencia: PontoUsuarioDataDivergencia;
    
    const divergencias = (
      await this._pontoUsuarioDataDivergenciaSvc
        .obterPorParametros({
          codPontoUsuarioData: pontoData.codPontoUsuarioData,
          sortActive: "DataHoraCad",
          sortDirection: "ASC",
        })
        .toPromise()
    ).items;

    if (divergencias.length) {
      divergencia.codPontoUsuarioDataDivergencia = divergencias.shift().codPontoUsuarioDataDivergencia;
      divergencia.codUsuarioCad = this.userSession.usuario.codUsuario;
      divergencia.dataHoraCad = moment().format('yyyy-MM-DD HH:mm');
      divergencia.codPontoUsuarioData = pontoData.codPontoUsuarioData;
      divergencia.divergenciaValidada = 1;

      this._pontoUsuarioDataDivergenciaSvc.atualizar(divergencia).subscribe();
    }
  }

  obterDivergenciasTooltip(pontoData:PontoUsuarioData): string {
    return pontoData.divergencias.map(d => {
        return `${d?.pontoUsuarioDataModoDivergencia?.descricao}: ${d?.pontoUsuarioDataMotivoDivergencia?.descricao}`;
    })?.join("\n");
  }

  conferir(pontoData: PontoUsuarioData) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja conferir este horário?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        this.conferirPontoData(pontoData);
        
        setTimeout(() => {
          this.obterHorarios();
          this.atualizarStatusPeriodo();
        }, 1200);
      }
    });
  }

  corrigirRegistros(pontoUsuarioData: PontoUsuarioData) {
    const dialogRef = this._dialog.open(PontoUsuarioFormComponent, {
      data: {
        pontoUsuarioData: pontoUsuarioData
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.obterHorarios();
      this.atualizarStatusPeriodo();
    });
  }

  informarInconsistencia(pontoUsuarioData: PontoUsuarioData) {
    const dialogRef = this._dialog.open(PontoInconsistenciaFormComponent, {
      data: {
        pontoUsuarioData: pontoUsuarioData
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.obterHorarios();
      this.atualizarStatusPeriodo();
    });
  }

  visualizarRelatoriosAtendimento(dataRegistro: string, codUsuario: string): void {
    const dialogRef = this._dialog.open(PontoRelatoriosAtendimentoComponent, {
      data: {
        dataRegistro: dataRegistro,
        codUsuario: codUsuario
      },
      width: '1040px',
    });

    dialogRef.afterClosed().subscribe((data: any) => {

    });
  }

  private verificarNovoPontoPeriodoUsuarioStatus(): number {
    const datas = this.dataSourceData.items.filter(d => moment(d.dataRegistro) >= moment(this.usuario.dataAdmissao));

    if (datas.filter(d => d.codPontoUsuarioDataStatus === pontoPeriodoUsuarioStatusConst.INCONSISTENTE).length) {
      return pontoPeriodoUsuarioStatusConst.INCONSISTENTE;
    }

    if (datas.filter(d => d.codPontoUsuarioDataStatus === pontoPeriodoUsuarioStatusConst.AGUARDANDO_CONFERENCIA).length) {
      return pontoPeriodoUsuarioStatusConst.AGUARDANDO_CONFERENCIA;
    }

    if (datas.length === datas.filter(d => d.codPontoUsuarioDataStatus === pontoPeriodoUsuarioStatusConst.SEM_REGISTRO).length) {
      return pontoPeriodoUsuarioStatusConst.SEM_REGISTRO;
    }

    if (datas.length === datas.filter(d => d.codPontoUsuarioDataStatus === pontoPeriodoUsuarioStatusConst.CONFERIDO).length) {
      return pontoPeriodoUsuarioStatusConst.CONFERIDO;
    }

    return this.pontoPeriodoUsuario.codPontoPeriodoUsuarioStatus;
  }

  private atualizarStatusPeriodo(): void {
    const novoStatus = this.verificarNovoPontoPeriodoUsuarioStatus();

    if (this.pontoPeriodoUsuario.codPontoPeriodoUsuarioStatus !== novoStatus) {
      this.pontoPeriodoUsuario.codPontoPeriodoUsuarioStatus = novoStatus;

      this._pontoPeriodoUsuarioSvc.atualizar(this.pontoPeriodoUsuario).toPromise();
    }
  }

  public async desbloquear(pontoUsuarioData: PontoUsuarioData) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja desbloquear este horário?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
      if (confirmacao) {
        const data = {
          ...pontoUsuarioData,
          ...{
            codUsuarioManut: this.userSession?.usuario?.codUsuario,
            dataHoraManut: moment().format('yyyy-MM-DD HH:mm:ss'),
            codPontoUsuarioDataStatusAcesso: pontoUsuarioDataStatusAcessoConst.DESBLOQUEADO
          }
        }
    
        await this._pontoUsuarioDataSvc.atualizar(data).toPromise();
        this._snack.exibirToast('Registro desbloquado com sucesso', 'success');
        this.obterHorarios();
      }
    });
  }
}