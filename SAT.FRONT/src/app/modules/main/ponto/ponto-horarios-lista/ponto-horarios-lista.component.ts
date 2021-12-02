import { AfterViewInit, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoUsuarioDataDivergenciaService } from 'app/core/services/ponto-usuario-data-divergencia.service';
import { PontoUsuarioDataService } from 'app/core/services/ponto-usuario-data.service';
import { PontoUsuarioService } from 'app/core/services/ponto-usuario.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { pontoPeriodoModoAprovacaoConst } from 'app/core/types/ponto-periodo-modo-aprovacao.types';
import { pontoPeriodoStatusConst } from 'app/core/types/ponto-periodo-status.types';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
import { PontoUsuarioDataDivergencia } from 'app/core/types/ponto-usuario-data-divergencia.types';
import { pontoUsuarioDataMotivoDivergenciaConst } from 'app/core/types/ponto-usuario-data-motivo-divergencia.types';
import { pontoUsuarioDataStatusConst } from 'app/core/types/ponto-usuario-data-status.types';
import { PontoUsuarioData, PontoUsuarioDataData } from 'app/core/types/ponto-usuario-data.types';
import { PontoUsuario } from 'app/core/types/ponto-usuario.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';
import { PontoInconsistenciaFormComponent } from '../ponto-inconsistencia-form/ponto-inconsistencia-form.component';
import { PontoRelatoriosAtendimentoComponent } from '../ponto-relatorios-atendimento/ponto-relatorios-atendimento.component';

@Component({
  selector: 'app-ponto-horarios-lista',
  templateUrl: './ponto-horarios-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-relatorios {
          grid-template-columns: 80px 186px 112px 250px 72px auto 196px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PontoHorariosListaComponent implements AfterViewInit {
  codPontoPeriodo: number;
  pontoPeriodo: PontoPeriodo;
  codUsuario: string;
  usuario: Usuario;
  dataSourceData: PontoUsuarioDataData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _pontoUsuarioDataSvc: PontoUsuarioDataService,
    private _pontoUsuarioSvc: PontoUsuarioService,
    private _pontoPeriodoSvc: PontoPeriodoService,
    private _relatorioAtendimentoSvc: RelatorioAtendimentoService,
    private _pontoUsuarioDataDivergenciaSvc: PontoUsuarioDataDivergenciaService,
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

    const datas = await this._pontoUsuarioDataSvc
      .obterPorParametros({
        codPontoPeriodo: this.codPontoPeriodo,
        codUsuario: this.codUsuario,
        sortActive: 'DataRegistro',
        sortDirection: 'desc'
      })
      .toPromise();

    for (var [i, pontoData] of datas.items.entries()) {
      //datas.items[i].pontosUsuario = await this.obterPontosPorData(pontoData, pontos.items);
      //datas.items[i].horasExtras = await this.calculaHorasExtras(datas.items[i]);
      //await this.inconsistirAutomaticamente(pontoData, this.pontoPeriodo);
    }

    this.dataSourceData = datas;
    this.isLoading = false;
  }

  private obterPontosPorData(pontoUsuarioData: PontoUsuarioData, pontos: PontoUsuario[]): PontoUsuario[] {
    return pontos.filter(p =>
      moment(p.dataHoraRegistro).format('yyyy-MM-DD') == moment(pontoUsuarioData.dataRegistro).format('yyyy-MM-DD')
    ).sort((a, b) => {
      return moment(a.dataHoraRegistro) < moment(b.dataHoraRegistro) ? -1 : 1
    });
  }

  private calculaHorasExtras(data: PontoUsuarioData): string {
    if (data.pontosUsuario.length == 2 || data.pontosUsuario.length == 4) {
      const horarioJornadaDiaria = moment().set({ hour: 8, minute: 48 });
      const minutosTolerancia = 5;
      const somaHorasRegistradas = moment().set({ hour: 0, minute: 0 });

      for (let i = 0; i < data.pontosUsuario.length; i = i + 2) {
        const horarioInicio = moment(data.pontosUsuario[i].dataHoraRegistro);
        const horarioFim = moment(data.pontosUsuario[i + 1].dataHoraRegistro);

        const totalRealizado = horarioFim.subtract(horarioInicio.format('HH:mm')).format('HH:mm')
        somaHorasRegistradas.add(totalRealizado);
      }

      const horasExtrasEmMinutos = moment.duration(somaHorasRegistradas.diff(horarioJornadaDiaria)).asMinutes();

      if (horasExtrasEmMinutos > minutosTolerancia) {
        return moment().startOf('day').add(horasExtrasEmMinutos, 'minutes').format('HH:mm');
      } else {
        return '';
      }
    }
  }

  private permiteInconsistirAutomaticamente(pontoData: PontoUsuarioData, pontoPeriodo: PontoPeriodo): boolean {
    if (
      pontoPeriodo.codPontoPeriodoStatus != pontoPeriodoStatusConst.CONSOLIDADO &&
      pontoPeriodo.pontoPeriodoModoAprovacao.codPontoPeriodoModoAprovacao == pontoPeriodoModoAprovacaoConst.DIARIO ||
      pontoPeriodo.codPontoPeriodoStatus == pontoPeriodoStatusConst.EM_ANALISE
    ) {
      const data = moment(pontoData.dataRegistro);

      if (
        pontoData.codPontoUsuarioDataStatus == pontoUsuarioDataStatusConst.INCONSISTENTE ||
        pontoData.codPontoUsuarioDataStatus == pontoUsuarioDataStatusConst.CONFERIDO ||
        data.weekday() == 7 || data.weekday() == 1 || data >= moment()
      ) {
        return false;
      }

      return true;
    } else {
      return false;
    }
  }

  private async inconsistirAutomaticamente(pontoData: PontoUsuarioData, periodo: PontoPeriodo) {
    if (!this.permiteInconsistirAutomaticamente(pontoData, periodo)) {
      return false;
    }
            
    let motivoDivergencia: number = -1;

    if (pontoData.pontosUsuario.length % 2 == 1) {
      motivoDivergencia = pontoUsuarioDataMotivoDivergenciaConst.FALTA_MARCACAO;
    } else {
      if (pontoData.pontosUsuario.length >= 2) {
        const primeiroPonto = moment(pontoData.pontosUsuario[0].dataHoraRegistro);
        const ultimoPonto = moment(pontoData.pontosUsuario[pontoData.pontosUsuario.length - 1].dataHoraRegistro);
        let intervaloEntrePontosEmHoras = moment.duration(primeiroPonto.diff(ultimoPonto)).asHours();

        switch (pontoData.pontosUsuario.length) {
          case 2:
            if (intervaloEntrePontosEmHoras > 6) {
              motivoDivergencia = pontoUsuarioDataMotivoDivergenciaConst.AUSENCIA_DE_INTERVALO;
            }
            break;

          case 4:
            if (intervaloEntrePontosEmHoras > 6) {
              intervaloEntrePontosEmHoras = moment
                .duration(moment(pontoData.pontosUsuario[1].dataHoraRegistro)
                .diff(moment(pontoData.pontosUsuario[2].dataHoraRegistro)))
                .asHours();

              if (intervaloEntrePontosEmHoras < 1) {
                motivoDivergencia = pontoUsuarioDataMotivoDivergenciaConst.INTERVALO_MIN_1_HORA_NAO_REALIZADO;
              }
              else if (intervaloEntrePontosEmHoras > 2) {
                motivoDivergencia = pontoUsuarioDataMotivoDivergenciaConst.INTERVALO_MAX_2_HORAS_EXCEDIDO;
              }
            }
            break;

          default:
            break;
        }
      }
    }

    const usuarioTecnico = await this._userSvc
      .obterPorCodigo(pontoData.codUsuario)
      .toPromise();

    const relatorios = await this._relatorioAtendimentoSvc.obterPorParametros({
      codTecnicos: String(usuarioTecnico.codTecnico),
      dataInicio: moment(pontoData.dataRegistro).format('yyyy-MM-DD 00:00:00')
    }).toPromise();


    console.log(motivoDivergencia);
    
    return motivoDivergencia != -1;
  }

  private async conferirPontoData(pontoData: PontoUsuarioData) {
    if (pontoData.codPontoUsuarioDataStatus == pontoUsuarioDataStatusConst.CONFERIDO) {
      return this._snack.exibirToast('Você já conferiu esta data');
    }

    pontoData.codPontoUsuarioDataStatus = pontoUsuarioDataStatusConst.CONFERIDO;
    pontoData.codUsuarioManut = this.userSession.usuario.codUsuario;
    pontoData.dataHoraManut = moment().format('yyyy-mm-dd HH:mm');
    let validaInconsistencia: boolean;

    if (pontoData.pontosUsuario.length % 2 == 1) {
      validaInconsistencia = true;
    } else {
      if (pontoData.pontosUsuario.length >= 2) {
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
    }

    if (validaInconsistencia) {
      this.validarInconsistencia(pontoData);
    }

    const horasExtras = moment(pontoData.horasExtras);

    // if (he > TimeSpan.FromMinutes(1))
    // {
    //     clsPontoUsuarioData pontoData = new clsPontoUsuarioData() { HoraExtra = he };

    //     if (pontoData.IsHoraExtraMaisPermitida())
    //     {
    //       //ExibeMensagem("Data finalizada com sucesso. ATENÇÃO: DATA COM HORA EXTRA ULTRAPASSANDO A DIÁRIA TOTAL DE 1 HORA E 12 MINUTOS!");
    //       //ExibeMensagem("Data conferida com sucesso. ATENÇÃO: DATA COM HORA EXTRA ULTRAPASSANDO A DIÁRIA TOTAL DE 1 HORA E 12 MINUTOS!");
    //       this._snack.exibirToast("ATENÇÃO: JORNADA TRABALHO EXCEDIDA! HORA EXTRA SUPERIOR À 1hr e 12min. Data conferida com sucesso.");
    //     }
    //     else
    //     {
    //       //ExibeMensagem("Data finalizada com sucesso. ATENÇÃO: DATA COM HORA EXTRA!");
    //       //ExibeMensagem("Data conferida com sucesso. ATENÇÃO: DATA COM HORA EXTRA!");
    //       this._snack.exibirToast("ATENÇÃO: DATA COM HORA EXTRA! Data conferida com sucesso.");
    //     }
    // }
    // else
    // {
    //   //ExibeMensagem("Data finalizada com sucesso.");
    //   this._snack.exibirToast("Data conferida com sucesso.");
    // }
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
        this.obterHorarios();
      }
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

  paginar() {
    this.obterHorarios();
  }
}

