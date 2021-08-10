import { AfterViewInit, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Foto } from 'app/core/types/foto.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import * as L from 'leaflet';
import { MatDialog } from '@angular/material/dialog';
import { OrdemServicoAgendamentoComponent } from '../ordem-servico-agendamento/ordem-servico-agendamento.component';
import { AgendamentoService } from 'app/core/services/agendamento.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';

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
  
  constructor(
    private _route: ActivatedRoute,
    private _ordemServicoService: OrdemServicoService,
    private _agendamentoService: AgendamentoService,
    private _snack: CustomSnackbarService,
    private _cdr: ChangeDetectorRef,
    private _dialog: MatDialog
  ) { }

  ngAfterViewInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.obterDadosOrdemServico();
    this._cdr.detectChanges();
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

    L.marker([
      +this.os.localAtendimento.latitude, 
      +this.os.localAtendimento.longitude
    ])
      .addTo(this.map)
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
}