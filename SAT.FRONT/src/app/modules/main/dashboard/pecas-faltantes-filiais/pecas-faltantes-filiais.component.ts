import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { FilialService } from 'app/core/services/filial.service';
import { DashboardViewEnum, ViewDashboardPecasFaltantes } from 'app/core/types/dashboard.types';
import { FilialData } from 'app/core/types/filial.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-pecas-faltantes-filiais',
  templateUrl: './pecas-faltantes-filiais.component.html'
})
export class PecasFaltantesFiliaisComponent implements OnInit {
  public loading: boolean = true;
  public indicadoresPecas: ViewDashboardPecasFaltantes[] = [];
  public nomesFiliais: string[] = [];
  public pecasFaltantes: PecasFaltantesFiliaisModel[] = [];
  public totalPecasFaltantes: TotalPecasFaltantes = new TotalPecasFaltantes();

  constructor(private _dashboardService: DashboardService,
    private _filialService: FilialService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    this.indicadoresPecas = Enumerable.from((await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.PECAS_FALTANTES }).toPromise())
      .viewDashboardPecasFaltantes).orderBy(ord => ord.dataFaltante).toArray();;

    // Filiais
    this._filialService.obterPorParametros({ indAtivo: 1, sortActive: 'nomeFilial', sortDirection: 'asc', }).subscribe((data: FilialData) => {
      let filiais = data.items.filter((f) => f.codFilial != 7 && f.codFilial != 21 && f.codFilial != 33); // Remover EXP,OUT,IND

      for (let f of filiais) {
        this.nomesFiliais.push(f.nomeFilial);
      }
      this.nomesFiliais.push("Total");
      this.nomesFiliais.splice(0, 0, "Data");

      for (let data of this.indicadoresPecas) {

        let model: PecasFaltantesFiliaisModel = Enumerable.from(this.pecasFaltantes).firstOrDefault(f => f.data == data.dataFaltante);

        if (!model) {
          model = new PecasFaltantesFiliaisModel();
          model.data = data.dataFaltante;
          this.pecasFaltantes.push(model);
        }

        if (data.nomeFilial == "FAM") {
          model.valorFAM == null ? model.valorFAM = data.qtd : model.valorFAM += data.qtd
          this.totalPecasFaltantes.totalFAM += data.qtd || 0;
        }

        if (data.nomeFilial == "FBA") {
          model.valorFBA == null ? model.valorFBA = data.qtd : model.valorFBA += data.qtd
          this.totalPecasFaltantes.totalFBA += data.qtd || 0;
        }

        if (data.nomeFilial == "FBU") {
          model.valorFBU == null ? model.valorFBU = data.qtd : model.valorFBU += data.qtd
          this.totalPecasFaltantes.totalFBU += data.qtd || 0;
        }
        if (data.nomeFilial == "FCE") {
          model.valorFCE == null ? model.valorFCE = data.qtd : model.valorFCE += data.qtd
          this.totalPecasFaltantes.totalFCE += data.qtd || 0;
        }
        if (data.nomeFilial == "FCP") {
          model.valorFCP == null ? model.valorFCP = data.qtd : model.valorFCP += data.qtd
          this.totalPecasFaltantes.totalFCP += data.qtd || 0;
        }
        if (data.nomeFilial == "FDF") {
          model.valorFDF == null ? model.valorFDF = data.qtd : model.valorFDF += data.qtd
          this.totalPecasFaltantes.totalFDF += data.qtd || 0;
        }
        if (data.nomeFilial == "FES") {
          model.valorFES == null ? model.valorFES = data.qtd : model.valorFES += data.qtd
          this.totalPecasFaltantes.totalFES += data.qtd || 0;
        }
        if (data.nomeFilial == "FGO") {
          model.valorFGO == null ? model.valorFGO = data.qtd : model.valorFGO += data.qtd
          this.totalPecasFaltantes.totalFGO += data.qtd || 0;
        }
        if (data.nomeFilial == "FMA") {
          model.valorFMA == null ? model.valorFMA = data.qtd : model.valorFMA += data.qtd
          this.totalPecasFaltantes.totalFMA += data.qtd || 0;
        }
        if (data.nomeFilial == "FMG") {
          model.valorFMG == null ? model.valorFMG = data.qtd : model.valorFMG += data.qtd
          this.totalPecasFaltantes.totalFMG += data.qtd || 0;
        }
        if (data.nomeFilial == "FMS") {
          model.valorFMS == null ? model.valorFMS = data.qtd : model.valorFMS += data.qtd
          this.totalPecasFaltantes.totalFMS += data.qtd || 0;
        }
        if (data.nomeFilial == "FMT") {
          model.valorFMT == null ? model.valorFMT = data.qtd : model.valorFMT += data.qtd
          this.totalPecasFaltantes.totalFMT += data.qtd || 0;
        }
        if (data.nomeFilial == "FPA") {
          model.valorFPA == null ? model.valorFPA = data.qtd : model.valorFPA += data.qtd
          this.totalPecasFaltantes.totalFPA += data.qtd || 0;
        }
        if (data.nomeFilial == "FPE") {
          model.valorFPE == null ? model.valorFPE = data.qtd : model.valorFPE += data.qtd
          this.totalPecasFaltantes.totalFPE += data.qtd || 0;
        }
        if (data.nomeFilial == "FPR") {
          model.valorFPR == null ? model.valorFPR = data.qtd : model.valorFPR += data.qtd
          this.totalPecasFaltantes.totalFPR += data.qtd || 0;
        }
        if (data.nomeFilial == "FRJ") {
          model.valorFRJ == null ? model.valorFRJ = data.qtd : model.valorFRJ += data.qtd
          this.totalPecasFaltantes.totalFRJ += data.qtd || 0;
        }
        if (data.nomeFilial == "FRN") {
          model.valorFRN == null ? model.valorFRN = data.qtd : model.valorFRN += data.qtd
          this.totalPecasFaltantes.totalFRN += data.qtd || 0;
        }
        if (data.nomeFilial == "FRO") {
          model.valorFRO == null ? model.valorFRO = data.qtd : model.valorFRO += data.qtd
          this.totalPecasFaltantes.totalFRO += data.qtd || 0;
        }
        if (data.nomeFilial == "FRS") {
          model.valorFRS == null ? model.valorFRS = data.qtd : model.valorFRS += data.qtd
          this.totalPecasFaltantes.totalFRS += data.qtd || 0;
        }
        if (data.nomeFilial == "FSC") {
          model.valorFSC == null ? model.valorFSC = data.qtd : model.valorFSC += data.qtd
          this.totalPecasFaltantes.totalFSC += data.qtd || 0;
        }
        if (data.nomeFilial == "FSP") {
          model.valorFSP == null ? model.valorFSP = data.qtd : model.valorFSP += data.qtd
          this.totalPecasFaltantes.totalFSP += data.qtd || 0;
        }
        if (data.nomeFilial == "FTO") {
          model.valorFTO == null ? model.valorFTO = data.qtd : model.valorFTO += data.qtd
          this.totalPecasFaltantes.totalFTO += data.qtd || 0;
        }

        model.total += data.qtd;
        this.totalPecasFaltantes.total += data.qtd || 0;
      }
    });

    this.loading = false;
  }
}

export class PecasFaltantesFiliaisModel {
  public data: string;
  public total: number = 0;
  public valorFAM?: number;
  public valorFBA?: number;
  public valorFGO?: number;
  public valorFMS?: number;
  public valorFSP?: number;
  public valorFSC?: number;
  public valorFCP?: number;
  public valorFBU?: number;
  public valorFES?: number;
  public valorFRS?: number;
  public valorFMT?: number;
  public valorFPA?: number;
  public valorFMA?: number;
  public valorFCE?: number;
  public valorFTO?: number;
  public valorFRO?: number;
  public valorFRN?: number;
  public valorFPE?: number;
  public valorFMG?: number;
  public valorFRJ?: number;
  public valorFDF?: number;
  public valorFPR?: number;
}

export class TotalPecasFaltantes {
  public total: number = 0;
  public totalFAM?: number = 0;
  public totalFBA?: number = 0;
  public totalFGO?: number = 0;
  public totalFMS?: number = 0;
  public totalFSP?: number = 0;
  public totalFSC?: number = 0;
  public totalFCP?: number = 0;
  public totalFBU?: number = 0;
  public totalFES?: number = 0;
  public totalFRS?: number = 0;
  public totalFMT?: number = 0;
  public totalFPA?: number = 0;
  public totalFMA?: number = 0;
  public totalFCE?: number = 0;
  public totalFTO?: number = 0;
  public totalFRO?: number = 0;
  public totalFRN?: number = 0;
  public totalFPE?: number = 0;
  public totalFMG?: number = 0;
  public totalFRJ?: number = 0;
  public totalFDF?: number = 0;
  public totalFPR?: number = 0;
}