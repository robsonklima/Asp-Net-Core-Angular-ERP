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
    private _snack: CustomSnackbarService,
    private _dashboardService: DashboardService,
    private _router: Router
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

            if (!component.usuarioSessao.usuario.codFilial) {
              const filial = component.filiais.filter(f => f.nomeFilial === "F" + uf).shift();
              const url = '/dashboard/indicadores-filiais-detalhados/' + filial?.codFilial;
              component._router.navigate([url]);
            }
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
    this.indicadoresFiliais = (await this._dashboardService
      .obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.INDICADORES_FILIAL }).toPromise())
      .viewDashboardIndicadoresFiliais;

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


          }
        });

        return;
      }


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