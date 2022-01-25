import { Component, OnInit } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { IndicadorService } from 'app/core/services/indicador.service';
import { FilialData } from 'app/core/types/filial.types';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
  selector: 'app-pecas-faltantes-filiais',
  templateUrl: './pecas-faltantes-filiais.component.html',
  styleUrls: ['./pecas-faltantes-filiais.component.css']
})
export class PecasFaltantesFiliaisComponent implements OnInit {
  public loading: boolean = true;
  public indicadoresPecas: Indicador[] = []
  public indicadoresFiliais: Indicador[] = []
  public nomesFiliais: string[] = []
  public pecasFaltantes: PecasFaltantesFiliaisModel[] = []
  public totalPecasFaltantes: TotalPecasFaltantes = new TotalPecasFaltantes();

  constructor(private _indicadorService: IndicadorService,
    private _filialService: FilialService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    //let dataInicio = moment().add(-30, 'days').format('yyyy-MM-DD HH:mm:ss'); // Ultimos 30 dias
    //let dataFim = moment().format('yyyy-MM-DD HH:mm:ss');
    let dataInicio = moment().add(-30, 'days').format('YYYY-MM-DD 00:00');
    let dataFim = moment().format('YYYY-MM-DD 23:59');

    this.indicadoresPecas = await this._indicadorService
      .obterPorParametros({
        tipo: IndicadorTipoEnum.PECA_FALTANTE,
        agrupador: IndicadorAgrupadorEnum.FILIAL,
        filterType: OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES,
        include: OrdemServicoIncludeEnum.OS_PECAS,
        dataInicio: dataInicio,
        dataFim: dataFim
      }).toPromise() as Indicador[];


    // Filiais
    this._filialService.obterPorParametros({ indAtivo: 1, sortActive: 'nomeFilial', sortDirection: 'asc', }).subscribe((data: FilialData) => {
      let filiais = data.items.filter((f) => f.codFilial != 7 && f.codFilial != 21 && f.codFilial != 33); // Remover EXP,OUT,IND

      for (let f of filiais) {
        this.nomesFiliais.push(f.nomeFilial);
      }
      this.nomesFiliais.push("Total");
      this.nomesFiliais.splice(0, 0, "Data");

      for (let data of this.indicadoresPecas) {

        let model: PecasFaltantesFiliaisModel = Enumerable.from(this.pecasFaltantes).firstOrDefault(f => f.data == data.label);

        if (!model) {
          model = new PecasFaltantesFiliaisModel();
          model.data = data.label;
          this.pecasFaltantes.push(model);
        }

        let filho = Enumerable.from(data.filho).firstOrDefault();

        if (filho.label == "FAM") {
          model.valorFAM == null ? model.valorFAM = filho.valor : model.valorFAM += filho.valor
          this.totalPecasFaltantes.totalFAM += filho.valor || 0;
        }

        if (filho.label == "FBA") {
          model.valorFBA == null ? model.valorFBA = filho.valor : model.valorFBA += filho.valor
          this.totalPecasFaltantes.totalFBA += filho.valor || 0;
        }

        if (filho.label == "FBU") {
          model.valorFBU == null ? model.valorFBU = filho.valor : model.valorFBU += filho.valor
          this.totalPecasFaltantes.totalFBU += filho.valor || 0;
        }
        if (filho.label == "FCE") {
          model.valorFCE == null ? model.valorFCE = filho.valor : model.valorFCE += filho.valor
          this.totalPecasFaltantes.totalFCE += filho.valor || 0;
        }
        if (filho.label == "FCP") {
          model.valorFCP == null ? model.valorFCP = filho.valor : model.valorFCP += filho.valor
          this.totalPecasFaltantes.totalFCP += filho.valor || 0;
        }
        if (filho.label == "FDF") {
          model.valorFDF == null ? model.valorFDF = filho.valor : model.valorFDF += filho.valor
          this.totalPecasFaltantes.totalFDF += filho.valor || 0;
        }
        if (filho.label == "FES") {
          model.valorFES == null ? model.valorFES = filho.valor : model.valorFES += filho.valor
          this.totalPecasFaltantes.totalFES += filho.valor || 0;
        }
        if (filho.label == "FGO") {
          model.valorFGO == null ? model.valorFGO = filho.valor : model.valorFGO += filho.valor
          this.totalPecasFaltantes.totalFGO += filho.valor || 0;
        }
        if (filho.label == "FMA") {
          model.valorFMA == null ? model.valorFMA = filho.valor : model.valorFMA += filho.valor
          this.totalPecasFaltantes.totalFMA += filho.valor || 0;
        }
        if (filho.label == "FMG") {
          model.valorFMG == null ? model.valorFMG = filho.valor : model.valorFMG += filho.valor
          this.totalPecasFaltantes.totalFMG += filho.valor || 0;
        }
        if (filho.label == "FMS") {
          model.valorFMS == null ? model.valorFMS = filho.valor : model.valorFMS += filho.valor
          this.totalPecasFaltantes.totalFMS += filho.valor || 0;
        }
        if (filho.label == "FMT") {
          model.valorFMT == null ? model.valorFMT = filho.valor : model.valorFMT += filho.valor
          this.totalPecasFaltantes.totalFMT += filho.valor || 0;
        }
        if (filho.label == "FPA") {
          model.valorFPA == null ? model.valorFPA = filho.valor : model.valorFPA += filho.valor
          this.totalPecasFaltantes.totalFPA += filho.valor || 0;
        }
        if (filho.label == "FPE") {
          model.valorFPE == null ? model.valorFPE = filho.valor : model.valorFPE += filho.valor
          this.totalPecasFaltantes.totalFPE += filho.valor || 0;
        }
        if (filho.label == "FPR") {
          model.valorFPR == null ? model.valorFPR = filho.valor : model.valorFPR += filho.valor
          this.totalPecasFaltantes.totalFPR += filho.valor || 0;
        }
        if (filho.label == "FRJ") {
          model.valorFRJ == null ? model.valorFRJ = filho.valor : model.valorFRJ += filho.valor
          this.totalPecasFaltantes.totalFRJ += filho.valor || 0;
        }
        if (filho.label == "FRN") {
          model.valorFRN == null ? model.valorFRN = filho.valor : model.valorFRN += filho.valor
          this.totalPecasFaltantes.totalFRN += filho.valor || 0;
        }
        if (filho.label == "FRO") {
          model.valorFRO == null ? model.valorFRO = filho.valor : model.valorFRO += filho.valor
          this.totalPecasFaltantes.totalFRO += filho.valor || 0;
        }
        if (filho.label == "FRS") {
          model.valorFRS == null ? model.valorFRS = filho.valor : model.valorFRS += filho.valor
          this.totalPecasFaltantes.totalFRS += filho.valor || 0;
        }
        if (filho.label == "FSC") {
          model.valorFSC == null ? model.valorFSC = filho.valor : model.valorFSC += filho.valor
          this.totalPecasFaltantes.totalFSC += filho.valor || 0;
        }
        if (filho.label == "FSP") {
          model.valorFSP == null ? model.valorFSP = filho.valor : model.valorFSP += filho.valor
          this.totalPecasFaltantes.totalFSP += filho.valor || 0;
        }
        if (filho.label == "FTO") {
          model.valorFTO == null ? model.valorFTO = filho.valor : model.valorFTO += filho.valor
          this.totalPecasFaltantes.totalFTO += filho.valor || 0;
        }

        model.total += filho.valor;
        this.totalPecasFaltantes.total += filho.valor || 0;
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