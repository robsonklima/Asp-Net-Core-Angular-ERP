import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { setOptions, localePtBR, Notifications, MbscEventcalendarOptions } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, AgendaTecnicoTypeEnum, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico, OrdemServicoFilterEnum, OrdemServicoIncludeEnum, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import moment from 'moment';
import Enumerable from 'linq';
import { Subject } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { MatDialog } from '@angular/material/dialog';
import { RoteiroMapaComponent } from './roteiro-mapa/roteiro-mapa.component';
import { UserService } from 'app/core/user/user.service';
import { RoleEnum, UserSession } from 'app/core/user/user.types';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { AgendaTecnicoRealocacaoDialogComponent } from './agenda-tecnico-realocacao-dialog/agenda-tecnico-realocacao-dialog.component';
import { AgendaTecnicoValidator } from './agenda-tecnico.validator';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

setOptions({
  locale: localePtBR,
  theme: 'ios',
  themeVariant: 'light',
  clickToCreate: true,
  dragToCreate: true,
  dragToMove: true,
  dragToResize: true
});

@Component({
  selector: 'app-agenda-tecnico',
  templateUrl: './agenda-tecnico.component.html',
  styleUrls: ['./agenda-tecnico.component.scss'],
})

export class AgendaTecnicoComponent extends Filterable implements AfterViewInit, IFilterable
{
  loading: boolean;
  userSession: UserSession;
  tecnicos: Tecnico[] = [];
  events: MbscAgendaTecnicoCalendarEvent[] = [];
  intervalos: AgendaTecnico[] = [];
  agendaTecnicos: AgendaTecnico[] = [];
  chamados: OrdemServico[] = [];
  resources = [];
  weekStart = moment().clone().startOf('isoWeek').format('yyyy-MM-DD HH:mm:ss');
  weekEnd = moment().clone().startOf('isoWeek').add(7, 'days').format('yyyy-MM-DD HH:mm:ss');
  snackConfigInfo: MatSnackBarConfig = { duration: 2000, panelClass: 'info' };

  calendarOptions: MbscEventcalendarOptions = {
    view: {
      timeline: {
        type: 'week',
        allDay: false,
        startDay: 1,
        startTime: '07:00',
        endTime: '24:00',
      }
    },
    dragToMove: true,
    externalDrop: true,
    dragToResize: false,
    dragToCreate: false,
    clickToCreate: false,
    onEventCreate: (args, inst) =>
    {
      if (this._validator.hasOverlap(args, inst))
      {
        this._notify.toast({
          message: 'Os atendimentos não podem se sobrepor.',
          color: 'danger'
        });
        return false;
      }
      else if (this._validator.invalidInsert(args))
      {
        this._notify.toast({
          message: 'O atendimento não pode ser agendado para antes da linha do tempo.',
          color: 'danger'
        });
        return false;
      }

      this.validateNewEvent(args, inst);
    },
    onEventUpdate: (args, inst) =>
    {
      if (this._validator.hasOverlap(args, inst))
      {
        this._notify.toast({
          message: 'Os atendimentos não podem se sobrepor.',
          color: 'danger'
        });
        return false;
      }
      else if (this._validator.hasChangedResource(args))
      {
        return this.updateResourceChange(args);
      }
      else if (this._validator.invalidMove(args))
      {
        this._notify.toast({
          message: 'O atendimento não pode ser agendado para antes da linha do tempo.',
          color: 'danger'
        });
        return false;
      }
      return this.updateEvent(args);
    },
    onEventDoubleClick: (args, inst) =>
    {
      this.showOSInfo(args);
    },
    onCellDoubleClick: (args, inst) =>
    {
      this.realocarAgendamento(args);
    },
    onSelectedDateChange: (args, inst) =>
    {
      this.changeWeek(args, inst)
    }
  };

  @ViewChild('sidenavChamados') sidenavChamados: MatSidenav;
  @ViewChild('sidenav') sidenavAgenda: MatSidenav;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _notify: Notifications,
    private _tecnicoSvc: TecnicoService,
    private _osSvc: OrdemServicoService,
    private _cdr: ChangeDetectorRef,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    protected _userSvc: UserService,
    public _dialog: MatDialog,
    private _validator: AgendaTecnicoValidator,
    private _snack: MatSnackBar
  )
  {
    super(_userSvc, 'agenda-tecnico')
    this.obterDados();
  }

  registerEmitters(): void
  {
    this.sidenavAgenda.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterDados();
    });
  }

  ngAfterViewInit(): void 
  {
    this.registerEmitters();
  }

  private async obterDados()
  {
    this.loading = true;

    this.tecnicos = (await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codPerfil: RoleEnum.FILIAL_TECNICO_DE_CAMPO,
      codFiliais: this.filter?.parametros?.codFiliais,
      sortActive: 'nome',
      sortDirection: 'asc',
      codTecnicos: this.filter?.parametros?.codTecnicos
    }).toPromise()).items;

    this.agendaTecnicos = (await this._agendaTecnicoSvc.obterPorParametros({
      codFiliais: this.filter?.parametros?.codFiliais,
      codTecnicos: this.filter?.parametros?.codTecnicos,
      inicioPeriodoAgenda: this.weekStart,
      fimPeriodoAgenda: this.weekEnd,
      sortActive: 'nome',
      sortDirection: 'asc',
    }).toPromise());

    await this.carregaAgenda();
    await this.criaIntervalosDoDia();

    this.loading = false;
  }

  carregaAgenda()
  {
    this.resources = this.tecnicos.map(tecnico =>
    {
      return {
        id: tecnico.codTecnico,
        name: tecnico.nome.toUpperCase(),
        indFerias: this._validator.isOnVacation(tecnico),
        img: `https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/${tecnico.usuario.codUsuario}.jpg`,
      }
    });

    this.events = [];
    this.agendaTecnicos.forEach(ag =>
    {
      this.events = this.events.concat(
        {
          codAgendaTecnico: ag.codAgendaTecnico,
          codOS: ag.codOS,
          agendaTecnico: ag,
          start: ag.inicio,
          end: ag.fim,
          title: ag.titulo,
          color: ag.cor,
          editable: ag.indAgendamento == 0 ? true : false,
          resource: ag.codTecnico
        });
    });
  }

  private async checkForWarnings(ev, args, inst)
  {
    var isFromSameRegion = (await this._validator.isTecnicoDaRegiaoDoChamado(ev.ordemServico, ev.resource));

    if (!isFromSameRegion)
    {
      var message: string = `Você transferiu o chamado ${ev.ordemServico.codOS} para um técnico com a região diferente do chamado.`;
      await this._snack.open(message, null, this.snackConfigInfo).afterDismissed().toPromise();
    }

    this.createExternalEvent(ev, args, inst);
  }

  /**  */

  /** Mobiscroll */

  private async changeWeek(args, inst)
  {
    this.weekStart = moment(args.date).format('yyyy-MM-DD HH:mm:ss');
    this.weekEnd = moment(args.date).add(7, 'days').format('yyyy-MM-DD HH:mm:ss');
    await this.obterDados();
  }

  private async validateNewEvent(args, inst)
  {
    var ev = args.event;
    this.checkForWarnings(ev, args, inst);
  }

  private async createExternalEvent(ev, args, inst)
  {
    this.sidenavChamados.close();

    var agendaTecnico: AgendaTecnico =
    {
      inicio: moment(ev.start).format('yyyy-MM-DD HH:mm:ss'),
      fim: moment(ev.end).format('yyyy-MM-DD HH:mm:ss'),
      codOS: ev.ordemServico.codOS,
      codTecnico: ev.resource,
      titulo: ev.ordemServico.localAtendimento.nomeLocal.toUpperCase(),
      cor: this._validator.getTypeColor(AgendaTecnicoTypeEnum.OS),
      ultimaAtualizacao: moment().format('yyyy-MM-DD HH:mm:ss'),
      tipo: AgendaTecnicoTypeEnum.OS,
      indAgendamento: 0,
      usuarioCadastro: this.userSession.usuario.codUsuario,
      cadastro: moment().format('yyyy-MM-DD HH:mm:ss')
    }

    var ag = (await this._agendaTecnicoSvc.criar(agendaTecnico).toPromise());

    if (ag != null)
    {
      var os = (await this._osSvc.obterPorCodigo(ev.ordemServico.codOS).toPromise());

      os.codTecnico = ag.codTecnico;
      os.dataHoraTransf = ag.ultimaAtualizacao;
      os.codStatusServico = StatusServicoEnum.TRANSFERIDO;
      os.statusServico.codStatusServico = StatusServicoEnum.TRANSFERIDO;
      ev.codAgendaTecnico = ag.codAgendaTecnico;

      var os = (await this._osSvc.atualizar(os).toPromise());

      if (os)
      {
        this._notify.toast({
          message: 'Atendimento agendado com sucesso.',
          color: 'success'
        });

        return true;
      }
      else
      {
        this._notify.toast({
          message: 'Não foi possível fazer o agendamento.',
          color: 'danger'
        });
        this.deleteEvent(args, inst);
        return false;
      }
    }
    else
    {
      this._notify.toast({
        message: 'Não foi possível fazer o agendamento.',
        color: 'danger'
      });
      this.deleteEvent(args, inst);
      return false;
    }
  }

  private async updateResourceChange(args)
  {
    var ev = args.event;

    if (this._validator.isTechnicianInterval(args))
    {
      this._notify.toast({
        message: 'Não é possível transferir um intervalo.',
        color: 'danger'
      });
      return false;
    }
    else
    {
      var os = (await this._osSvc.obterPorCodigo(ev.codOS).toPromise());
      os.codTecnico = ev.resource;
      this._osSvc.atualizar(os).toPromise().then(() =>
      {
        return this.updateEvent(args);
      }).catch(() =>
      {
        return false;
      })
    }
  }

  private async updateEvent(args)
  {
    var ev = args.event;

    var agenda: AgendaTecnico = ev.agendaTecnico;
    agenda.codTecnico = ev.resource;
    agenda.inicio = moment(ev.start).format('yyyy-MM-DD HH:mm:ss');
    agenda.fim = moment(ev.end).format('yyyy-MM-DD HH:mm:ss');
    agenda.ultimaAtualizacao = moment().format('yyyy-MM-DD HH:mm:ss');
    agenda.usuarioAtualizacao = this.userSession.usuario.codUsuario;

    if (moment(ev.end) > moment() && ev.agendaTecnico.tipo == AgendaTecnicoTypeEnum.OS)
    {
      agenda.cor = this._validator.getTypeColor(AgendaTecnicoTypeEnum.OS);
      var event = Enumerable.from(this.events).firstOrDefault(e => e.codAgendaTecnico == args.event.codAgendaTecnico);
      event.color = agenda.cor;
    }

    this._agendaTecnicoSvc.atualizar(agenda).toPromise().then(() =>
    {
      this._notify.toast({
        message: 'Agendamento atualizado com sucesso.',
        color: 'success'
      });
      return true;
    }).catch(() =>
    {
      this._notify.toast({
        message: 'Não foi possível atualizar o agendamento.',
        color: 'danger'
      });
      return false;
    })
  }

  public async deleteEvent(args, inst)
  {
    var os = args.event.ordemServico;
    if (os?.codAgendaTecnico)
    {
      this._agendaTecnicoSvc.deletar(os?.codAgendaTecnico).toPromise().then(() =>
      {
        inst.removeEvent(args.event);
        return;
      })
    }
    inst.removeEvent(args.event);
  }

  private async criaIntervalosDoDia()
  {
    for (const tecnico of this.tecnicos) 
    {
      var eventosPorTecnico = Enumerable.from(this.events)
        .where(i => moment(i.start).format('yyyy-MM-DD') == moment().format('yyyy-MM-DD') && i.resource == tecnico.codTecnico).toArray();

      if (!Enumerable.from(eventosPorTecnico).any(i => i.agendaTecnico.tipo == AgendaTecnicoTypeEnum.INTERVALO))
      {
        var agendaTecnico: AgendaTecnico =
        {
          inicio: moment().set({ hour: 12, minute: 0, second: 0, millisecond: 0 }).format('yyyy-MM-DD HH:mm:ss'),
          fim: moment().set({ hour: 13, minute: 0, second: 0, millisecond: 0 }).format('yyyy-MM-DD HH:mm:ss'),
          codOS: 0,
          codTecnico: tecnico.codTecnico,
          titulo: "INTERVALO",
          cor: this._validator.getTypeColor(AgendaTecnicoTypeEnum.INTERVALO),
          ultimaAtualizacao: moment().format('yyyy-MM-DD HH:mm:ss'),
          tipo: AgendaTecnicoTypeEnum.INTERVALO,
          indAgendamento: 0,
          usuarioCadastro: "ADMIN",
          cadastro: moment().format('yyyy-MM-DD HH:mm:ss')
        }

        await this._agendaTecnicoSvc.criar(agendaTecnico).subscribe(intervalo =>
        {
          this.events = this.events.concat(
            {
              codAgendaTecnico: intervalo.codAgendaTecnico,
              codOS: intervalo.codOS,
              agendaTecnico: intervalo,
              start: intervalo.inicio,
              end: intervalo.fim,
              title: intervalo.titulo,
              color: intervalo.cor,
              editable: intervalo.indAgendamento == 0 ? true : false,
              resource: intervalo.codTecnico
            });
        });
      };
    }
  }

  private async realocarAgendamento(args)
  {
    var now = moment();
    var initialTime = moment(args.date).format('yyyy-MM-DD HH:mm:ss');
    var codTecnico = args.resource;

    if (moment(args.date) < now) return;

    var agendamentosTecnico = (await this._agendaTecnicoSvc.obterPorParametros({
      codFiliais: this.filter?.parametros?.codFiliais,
      inicioPeriodoAgenda: this.weekStart,
      fimPeriodoAgenda: this.weekEnd,
      sortActive: 'nome',
      sortDirection: 'asc',
      codTecnico: codTecnico
    }).toPromise());

    var atendimentosTecnico: MbscAgendaTecnicoCalendarEvent[] = [];

    Enumerable.from(agendamentosTecnico).where(i => i.indAgendamento == 0).forEach(i =>
    {
      atendimentosTecnico.push(
        {
          codAgendaTecnico: i.codAgendaTecnico,
          start: i.inicio,
          end: i.fim,
          codOS: i.codOS,
          title: i.titulo,
          color: this._validator.getTypeColor(AgendaTecnicoTypeEnum.OS),
          editable: true,
          resource: i.codTecnico
        });
    });

    if (!atendimentosTecnico.length) return;

    var dialog = this._dialog.open(AgendaTecnicoRealocacaoDialogComponent, {
      data:
      {
        agendamentos: atendimentosTecnico,
        initialTime: initialTime,
        codTecnico: codTecnico
      }
    });

    dialog.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        this.obterDados();
    });
  }

  private async showOSInfo(args)
  {
    var codOS = args.event.codOS;
    var os = (await this._osSvc.obterPorCodigo(codOS).toPromise());

    if (os == null) return;

    var text = "";
    if (os.localAtendimento?.nomeLocal) text += os.localAtendimento?.nomeLocal + '\n';
    if (os.tipoIntervencao?.nomTipoIntervencao) text += 'Intervenção ' + os.tipoIntervencao?.nomTipoIntervencao + '\n';

    this._notify.alert(
      {
        title: 'OS ' + os.codOS.toString(),
        message: text.toUpperCase(),
        display: 'center',
        cssClass: 'os_info'
      }
    );
  }

  /** */

  /** Mapa */
  public abrirMapa(codTecnico: number): void
  {
    const resource = this.resources.filter(r => r.id === codTecnico).shift();
    const chamados = this.chamados.filter(os => os.codTecnico === codTecnico);

    this._dialog.open(RoteiroMapaComponent, {
      width: '960px',
      height: '640px',
      data: {
        resource: resource,
        chamados: chamados
      }
    });
  }

  /**  */
}