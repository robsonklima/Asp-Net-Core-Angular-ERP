import { Component, OnInit } from '@angular/core';
import Enumerable from 'linq';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardIndicadoresFiliais } from 'app/core/types/dashboard.types';
import * as L from "leaflet";
import { latLng, tileLayer, Map } from 'leaflet';
import 'leaflet.markercluster';
import { HttpClient } from '@angular/common/http';
import { FilialService } from 'app/core/services/filial.service';
import { Router } from '@angular/router';
import { Filial } from 'app/core/types/filial.types';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UserService } from 'app/core/user/user.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { MatDialog } from '@angular/material/dialog';
import { IndicadoresFiliaisDetalhadosDialogComponent } from '../indicadores-filiais-detalhados/indicadores-filiais-detalhados-dialog/indicadores-filiais-detalhados-dialog.component';
import { IndicadoresFiliaisOpcoesComponent } from './indicadores-filiais-opcoes/indicadores-filiais-opcoes.component';

@Component({
  selector: 'app-indicadores-filiais',
  templateUrl: './indicadores-filiais.component.html'
})

export class IndicadoresFiliaisComponent implements OnInit {
  public indicadoresFiliais: ViewDashboardIndicadoresFiliais[] = [];
  public indicadoresFiliaisTotal: ViewDashboardIndicadoresFiliais;
  private map: Map;
  private usuarioSessao: UsuarioSessao;
  public markerClusterGroup: L.MarkerClusterGroup;
  public markerClusterData = [];
  public filiais: Filial[] = [];
  public loading: boolean = true;

  public options = {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap'
      })
    ],
    zoom: 5,
    center: latLng([-15.7801, -47.9292])
  };

  constructor(
    private _filialService: FilialService,
    private _userService: UserService,
    private _http: HttpClient,
    private _dialog: MatDialog,
    private _snack: CustomSnackbarService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.loading = true;
    this.montaDashboard();
  }

  async onMapReady(map: Map) {
    this.map = map;
    this.markerClusterGroup = L.markerClusterGroup({ removeOutsideVisibleBounds: true });
    const component = this;
    this._http.get('assets/geojson/uf.json').subscribe((json: any) => {
      L.geoJSON(json, {
        style: {
          weight: 1,
          color: '#254441',
          fillColor: '#43AA8B'
        },
        onEachFeature: function onEachFeature(feature, layer) {
          layer.on('click', (e) => {
            const uf = e.target.feature.properties.UF_05;

            if (!component.usuarioSessao.usuario.codFilial)
              component.onIndicadoresDetalhados(uf);
          });

          layer.on('mouseover', function () {
            if (!component.usuarioSessao.usuario.codFilial) {
              this.setStyle({
                'fillColor': '#0000ff'
              });
            }
          });

          layer.on('mouseout', function () {
            if (!component.usuarioSessao.usuario.codFilial) {
              this.setStyle({
                'fillColor': '#43AA8B'
              });
            }
          });
        }
      }).addTo(map);
    });
  }

  private async montaDashboard(): Promise<void> {
    // this.indicadoresFiliais = (await this._dashboardService
    //   .obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.INDICADORES_FILIAL }).toPromise())
    //   .viewDashboardIndicadoresFiliais;

    this.indicadoresFiliais = [
      {
        "filial": "FAM",
        "sla": 100,
        "pendencia": 0,
        "reincidencia": 20,
        "spa": 100,
        "osMedTec": 0.92
      },
      {
        "filial": "FBA",
        "sla": 98.7,
        "pendencia": 0,
        "reincidencia": 2.99,
        "spa": 91.6,
        "osMedTec": 1.93
      },
      {
        "filial": "FBU",
        "sla": 97.8,
        "pendencia": 0,
        "reincidencia": 10.81,
        "spa": 80.81,
        "osMedTec": 1.66
      },
      {
        "filial": "FCE",
        "sla": 92.9,
        "pendencia": 3.85,
        "reincidencia": 5.66,
        "spa": 92.31,
        "osMedTec": 1.95
      },
      {
        "filial": "FCP",
        "sla": 98.1,
        "pendencia": 1.37,
        "reincidencia": 16.28,
        "spa": 90.24,
        "osMedTec": 1.91
      },
      {
        "filial": "FDF",
        "sla": 97.4,
        "pendencia": 2.29,
        "reincidencia": 20.72,
        "spa": 85.32,
        "osMedTec": 2.16
      },
      {
        "filial": "FES",
        "sla": 97,
        "pendencia": 5.93,
        "reincidencia": 11.49,
        "spa": 91.67,
        "osMedTec": 1.79
      },
      {
        "filial": "FGO",
        "sla": 100,
        "pendencia": 0,
        "reincidencia": 4.35,
        "spa": 97.14,
        "osMedTec": 1.61
      },
      {
        "filial": "FMA",
        "sla": 91.3,
        "pendencia": 3.85,
        "reincidencia": 6.25,
        "spa": 100,
        "osMedTec": 1.12
      },
      {
        "filial": "FMG",
        "sla": 96.2,
        "pendencia": 9.35,
        "reincidencia": 10.11,
        "spa": 83.89,
        "osMedTec": 1.46
      },
      {
        "filial": "FMS",
        "sla": 94.7,
        "pendencia": 5.56,
        "reincidencia": 6.25,
        "spa": 96,
        "osMedTec": 1.2
      },
      {
        "filial": "FMT",
        "sla": 85,
        "pendencia": 4.35,
        "reincidencia": 0,
        "spa": 100,
        "osMedTec": 1.27
      },
      {
        "filial": "FPA",
        "sla": 89.3,
        "pendencia": 3.23,
        "reincidencia": 4.55,
        "spa": 92.86,
        "osMedTec": 0.85
      },
      {
        "filial": "FPE",
        "sla": 98.6,
        "pendencia": 4.71,
        "reincidencia": 7.94,
        "spa": 96.74,
        "osMedTec": 1.85
      },
      {
        "filial": "FPR",
        "sla": 96.2,
        "pendencia": 6.3,
        "reincidencia": 5.26,
        "spa": 92.11,
        "osMedTec": 2.07
      },
      {
        "filial": "FRJ",
        "sla": 96,
        "pendencia": 1.99,
        "reincidencia": 18.49,
        "spa": 87.35,
        "osMedTec": 2.71
      },
      {
        "filial": "FRN",
        "sla": 100,
        "pendencia": 5.26,
        "reincidencia": 0,
        "spa": 90,
        "osMedTec": 1.06
      },
      {
        "filial": "FRO",
        "sla": 100,
        "pendencia": 0,
        "reincidencia": 7.69,
        "spa": 93.33,
        "osMedTec": 1.04
      },
      {
        "filial": "FRS",
        "sla": 88.1,
        "pendencia": 4.68,
        "reincidencia": 11.14,
        "spa": 90.13,
        "osMedTec": 2.19
      },
      {
        "filial": "FSC",
        "sla": 92.8,
        "pendencia": 1.17,
        "reincidencia": 9.09,
        "spa": 92.78,
        "osMedTec": 1.98
      },
      {
        "filial": "FSP",
        "sla": 98.9,
        "pendencia": 3.21,
        "reincidencia": 13.1,
        "spa": 88.03,
        "osMedTec": 2.29
      },
      {
        "filial": "FTO",
        "sla": 100,
        "pendencia": 0,
        "reincidencia": 0,
        "spa": 90.91,
        "osMedTec": 1
      },
      {
        "filial": "TOTAL",
        "sla": 93.8,
        "pendencia": 3.5,
        "reincidencia": 9.95,
        "spa": 89.81,
        "osMedTec": 1.86
      }
    ];

    this.indicadoresFiliaisTotal = Enumerable.from(this.indicadoresFiliais).firstOrDefault(q => q.filial == "TOTAL");
    this.indicadoresFiliais = Enumerable.from(this.indicadoresFiliais).where(f => f.filial != "TOTAL").distinct().toArray();
    this.indicadoresFiliais.sort((a, b) => (a.sla > b.sla ? -1 : 1));

    let filiaisData = await this._filialService.obterPorParametros({ indAtivo: 1 }).toPromise();
    this.filiais = filiaisData.items.filter((f) => f.codFilial != 7 && f.codFilial != 21 && f.codFilial != 33);

    this.filiais.forEach(async (filial) => {
      const valorIndicador = this.indicadoresFiliais?.find(f => f.filial == filial.nomeFilial)?.sla || 0;
      var icon = new L.Icon({
        iconUrl: this.obterIconeUrl(valorIndicador),
        iconSize: [32, 32],
        iconAnchor: [15, 32],
        popupAnchor: [1, -32]
      });

      const marker = new L.Marker([+filial.cidade.latitude, +filial.cidade.longitude], { icon: icon }).bindPopup(filial.nomeFilial);
      marker.addTo(this.map);
      this.map.invalidateSize();
    });


    this.loading = false;
  }

  public onIndicadoresDetalhados(nomeFilial: string) {
    const filial = this.filiais.filter(f => f.nomeFilial === "F" + nomeFilial).shift();

    if (filial) {
      if (filial.nomeFilial == 'FSP') {
        const dialogRef = this._dialog.open(IndicadoresFiliaisOpcoesComponent, {
          width: '450px'
        });
        
        dialogRef.afterClosed().subscribe(async (data: any) => {
          if (data) {   
            const filial = this.filiais.filter(f => f.nomeFilial === data).shift();
            
            this._dialog.open(IndicadoresFiliaisDetalhadosDialogComponent, {
              data: {
                codFilial: filial.codFilial
              }
            });
          }
        });
  
        return;
      }

      this._dialog.open(IndicadoresFiliaisDetalhadosDialogComponent, {
        data: {
          codFilial: filial.codFilial
        }
      });
    }
    else {
      this._snack.exibirToast("Não encontramos a filial selecionada", "warning");
    }
  }

  private obterIconeUrl(valor: number): string {
    if (valor >= 95)
      return 'assets/icons/marker-green-32.svg';
    else if (valor >= 92)
      return 'assets/icons/marker-yellow-32.svg';
    else
      return 'assets/icons/marker-red-32.svg';
  }
}