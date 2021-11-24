import { AfterViewInit, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoUsuarioDataService } from 'app/core/services/ponto-usuario-data.service';
import { PontoUsuarioService } from 'app/core/services/ponto-usuario.service';
import { pontoPeriodoModoAprovacaoConst } from 'app/core/types/ponto-periodo-modo-aprovacao.types';
import { pontoPeriodoStatusConst } from 'app/core/types/ponto-periodo-status.types';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
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
  }

  async obterHorarios() {
    this.isLoading = true;
    this.usuario = await this._userSvc.obterPorCodigo(this.codUsuario).toPromise(); 
    this._cdr.detectChanges();

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

    const pontos = await this._pontoUsuarioSvc
      .obterPorParametros({
        codPontoPeriodo: this.codPontoPeriodo,
        codUsuario: this.codUsuario,
      })
      .toPromise();   
    
    for (let i = 0; i < datas.items.length; i++) {
      const pontoUsuarioData = datas.items[i];

      datas.items[i].pontosUsuario = this.obterPontosPorData(pontoUsuarioData, pontos.items);
      datas.items[i].horasExtras = this.calculaHorasExtras(datas.items[i]);
      this.permiteInconsistirAutomaticamente(pontoUsuarioData, this.pontoPeriodo);
    }

    this.dataSourceData = datas;
    this.isLoading = false;
  }

  private obterPontosPorData(pontoUsuarioData: PontoUsuarioData, pontos: PontoUsuario[]): PontoUsuario[] {
    return pontos.filter(p => 
      moment(p.dataHoraRegistro).format('yyyy-MM-DD') == moment(pontoUsuarioData.dataRegistro).format('yyyy-MM-DD')
    ).sort((a, b) => {
      return moment(a.dataHoraRegistro) < moment(b.dataHoraRegistro) ? -1 : 1
    })
  }

  private calculaHorasExtras(data: PontoUsuarioData): string {
    if (data.pontosUsuario.length == 2 || data.pontosUsuario.length == 4) {
      const horarioJornadaDiaria = moment().set({ hour: 8, minute: 48 });
      const minutosTolerancia = 5;
      const somaHorasRegistradas = moment().set({ hour: 0, minute: 0 });

      for (let i = 0; i < data.pontosUsuario.length; i=i+2) { 
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
      )
      {
        return false;
      }
      
      return true;
    } else {
      return false;
    }
  }

  private inconsistirAutomaticamente(): boolean {
    return false;
  }

  public conferirPontoData(pontoData: PontoUsuarioData) {
    debugger;

    if (pontoData.codPontoUsuarioDataStatus == pontoUsuarioDataStatusConst.CONFERIDO)
    {
        this._snack.exibirToast('Você já conferiu esta data');
        return;
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
        const ultimoPonto = moment(pontoData.pontosUsuario[pontoData.pontosUsuario.length-1].dataHoraRegistro);

        let intervaloEntrePontos = moment.duration(primeiroPonto.diff(ultimoPonto)).asHours();

        switch (pontoData.pontosUsuario.length)
        {
          case 2:
            if (intervaloEntrePontos > 6)
            {
              validaInconsistencia = true;
            }
            break;

          case 4:
            if (intervaloEntrePontos > 6) {
              const saidaIntervalo = moment(pontoData.pontosUsuario[1].dataHoraRegistro);
              const retornoIntervalo = moment(pontoData.pontosUsuario[2].dataHoraRegistro);

              intervaloEntrePontos = moment.duration(saidaIntervalo.diff(retornoIntervalo)).asHours();

              if (intervaloEntrePontos < 1)
              {
                  validaInconsistencia = true;
              }
              else if (intervaloEntrePontos > 2)
              {
                  validaInconsistencia = true;
              }
            }
            break;
        }
      }
    }

    if (validaInconsistencia) {
      //new clsPontoUsuarioData().ValidarInconsistencia(CodPontoUsuarioData, usuario, -1, -1);
    }
    
    // TimeSpan he = TimeSpan.Zero;

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

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
      {
        this.conferirPontoData(pontoData);
      }
    });
  }

  informarInconsistencia(pontoUsuarioData: PontoUsuarioData) {
    const dialogRef = this._dialog.open(PontoInconsistenciaFormComponent, {
      data: {
        pontoUsuarioData: pontoUsuarioData
      }
    });

    dialogRef.afterClosed().subscribe(() => {});
  }

  visualizarRelatoriosAtendimento(dataRegistro: string, codUsuario: string): void {
    const dialogRef = this._dialog.open(PontoRelatoriosAtendimentoComponent, {
      data: {
        dataRegistro: dataRegistro,
        codUsuario: codUsuario
      },
      width: '1040px',
    });

    dialogRef.afterClosed().subscribe((data: any) =>
    {
      
    });
  }

  paginar() {
    this.obterHorarios();
  }
}

