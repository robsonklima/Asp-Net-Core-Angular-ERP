import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Foto } from 'app/core/types/foto.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import * as L from 'leaflet';
import { MatDialog } from '@angular/material/dialog';
import { appConfig as c } from 'app/core/config/app.config'
import { OrdemServicoAgendamentoComponent } from '../ordem-servico-agendamento/ordem-servico-agendamento.component';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { AgendamentoService } from 'app/core/services/agendamento.service';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import moment from 'moment';
import { Tecnico } from 'app/core/types/tecnico.types';
import { MatSidenav } from '@angular/material/sidenav';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-ordem-servico-detalhe',
  templateUrl: './ordem-servico-detalhe.component.html',
  styleUrls: ['./ordem-servico-detalhe.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class OrdemServicoDetalheComponent implements AfterViewInit {
  codOS: number;
  os: OrdemServico;
  fotos: Foto[] = [];
  map: L.Map;
  ultimoAgendamento: string;
  sessionData: UsuarioSessionData;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  sidenav: MatSidenav;
  tecnicos: Tecnico[] = [];

  constructor(
    private _route: ActivatedRoute,
    private _ordemServicoService: OrdemServicoService,
    private _agendamentoService: AgendamentoService,
    private _tecnicoService: TecnicoService,
    private _snack: CustomSnackbarService,
    private _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _dialog: MatDialog
  ) {
    this.sessionData = JSON.parse(this._userService.userSession);
   }

  ngAfterViewInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.obterDadosOrdemServico();
    this.obterTecnicos();
    this.registrarEmitters();
    this._cdr.detectChanges();
  }

  async obterTecnicos() {
    const params = {
      indAtivo: 1,
      sortActive: 'nome',
      sortDirection: 'asc',
      codFilial: this.sessionData?.usuario?.filial?.codFilial,
      filter: this.searchInputControl.nativeElement.val,
      pageSize: 10
    }
        
    const data = await this._tecnicoService
      .obterPorParametros(params)
      .toPromise();
    
    this.tecnicos = data.tecnicos;
  }

  trocarTab(tab: any) {
    if (tab.index !== 3 || !this.os) {
      return;
    }

    this.map = L.map('map', {
      scrollWheelZoom: false,
    }).setView([
      +this.os.localAtendimento.latitude,
      +this.os.localAtendimento.longitude
    ], 14);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: 'SAT 2.0'
    }).addTo(this.map);

    var icon = new L.Icon.Default();
    icon.options.shadowSize = [0, 0];

    L.marker([
      +this.os.localAtendimento.latitude,
      +this.os.localAtendimento.longitude
    ])
      .addTo(this.map)
      .setIcon(icon)
      .bindPopup(this.os.localAtendimento.nomeLocal);

    this.map.invalidateSize();
  }

  private async obterDadosOrdemServico() {
    this.os = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
    if (this.os.agendamentos.length) {
      this.ultimoAgendamento = this.os.agendamentos
        .reduce((max, p) => p.dataAgendamento > max ? p.dataAgendamento : max, this.os.agendamentos[0].dataAgendamento);
    }
  }

  agendar() {
    const dialogRef = this._dialog.open(OrdemServicoAgendamentoComponent, {
      data: {
        codOS: this.os.codOS
      }
    });

    dialogRef.afterClosed().subscribe((data: any) => {
      if (data) {
        this._agendamentoService.criar(data.agendamento).subscribe(() => {
          this._snack.exibirToast('Chamado agendado com sucesso!', 'success');
          this.obterDadosOrdemServico();
        }, e => {
          this._snack.exibirToast(e?.error, 'success');
        });
      }
    });
  }

  private registrarEmitters(): void {
    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(700)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      this.searchInputControl.nativeElement.val = text;
      this.obterTecnicos();
    });
  }

  transferir(tecnico: Tecnico): void {
    this.os.codTecnico = tecnico.codTecnico;
    this.os.codUsuarioManut = this.sessionData.usuario.codUsuario;
    this.os.codStatusServico = c.status_servico.transferido;
    this.os.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
    this._ordemServicoService.atualizar(this.os).subscribe(() => {
      this._snack.exibirToast(`Chamado transferido para ${tecnico.nome.replace(/ .*/, '')}`, 'success');
      this.obterDadosOrdemServico();
      this.sidenav.close();
    }, error => {
      this._snack.exibirToast(error, 'error');
    });
  }
}