import { AfterViewInit, Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardDisponibilidadeBBTSMapaRegioes, ViewDashboardDisponibilidadeBBTSMultasRegioes } from 'app/core/types/dashboard.types';
import Enumerable from 'linq';

export enum Region {
  NORTE = 1,
  CENTROESTE = 2,
  SUL = 3,
  SUDESTE = 4,
  NORDESTE = 5
}

export type StateByRegion =
  {
    region: Region;
    state: SVGPathElement;
  };

export type RectByRegion =
  {
    region: Region;
    rect: SVGPathElement;
  };

@Component({
  selector: 'app-mapa-disponibilidade',
  templateUrl: './mapa-disponibilidade.component.html',
  styleUrls: ['./mapa-disponibilidade.component.css'
  ],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None,
})

export class MapaDisponibilidadeComponent implements OnInit, AfterViewInit {

  private rectDados: { rect: RectByRegion, box: SVGRect }[] = [];
  private viewDados: ViewDashboardDisponibilidadeBBTSMapaRegioes[] = [];
  private viewDadosMultas: ViewDashboardDisponibilidadeBBTSMultasRegioes[] = [];

  public loading: boolean = false;
  @Input() bbtsRegiaoMulta: boolean;

  constructor(private _dashboardService: DashboardService) { }

  ngAfterViewInit() {
    debugger;
    let u = document.querySelector("#landmarks-brazil-disp");
    this.prepararTextos();
    this.loading = true;
  }

  async ngOnInit(): Promise<void> {
    await this.preparaDadosMapa().then(async callback => {
      this.inserirDadosMapa();
    });
  }

  private async preparaDadosMapa() {
    if (this.bbtsRegiaoMulta) {
      this.viewDadosMultas = (await (this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.BBTS_MULTA_REGIOES }))
        .toPromise()).viewDashboardDisponibilidadeBBTSMultasRegioes;
    } else {
      this.viewDados = (await (this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.BBTS_MAPA_REGIOES }))
        .toPromise()).viewDashboardDisponibilidadeBBTSMapaRegioes;
    }
  }

  private prepararTextos(): void {
    // Inicia os nomes dos estados
    var states = Array.from(document.querySelector("#landmarks-brazil-disp").querySelectorAll("path")).filter(st => st.id.endsWith("disp"));
    states.forEach(s => {
      const sbr: StateByRegion =
      {
        region: this.getRegion(this.getInitials(s.id)),
        state: s
      };
      // debugger;
      this.formatPaths(sbr);
    });

    // Inicia os textos de dados
    let rects = Array.from(document.querySelector("#landmarks-brazil-disp").querySelectorAll("path")).filter(r => r.id.startsWith("regiao"));
    rects.forEach(r => {
      //  debugger;
      const rbr: RectByRegion =
      {
        region: parseInt(r.id.split("-").pop()),
        rect: r
      };
      this.rectDados.push({ rect: rbr, box: rbr.rect.getBBox() });
      this.startLoadingRectTemplate(rbr);
    });
  }

  public formatPaths(st: StateByRegion): void {
    var t = document.createElementNS("http://www.w3.org/2000/svg", "text");
    var b = st.state.getBBox();
    t.setAttribute("transform", "translate(" + (b.x + b.width / 1.6) + " " + (b.y + b.height / 1.7) + ")");
    t.textContent = this.getInitials(st.state.id);
    t.setAttribute("fill", "black");
    t.setAttribute("pointer-events", "none");
    t.setAttribute("text-anchor", "middle");
    t.setAttribute("font-weight", "bold");
    t.setAttribute("font-size", "12");
    st.state.setAttribute("fill", this.getRegionColor(st.region));
    st.state.parentNode.insertBefore(t, st.state.nextSibling);
  }

  private async inserirDadosMapa(): Promise<void> {
    this.loading = false;
    // Remove os textos de "carregando"
    this.removerRectsIniciais();

    if (!this.bbtsRegiaoMulta) {
      this.rectDados.forEach(dados => {
        let d: MapaDisponibilidadeModel = new MapaDisponibilidadeModel();

        if (dados.rect.region == Region.NORTE) {
          let dadosNorte = Enumerable.from(this.viewDados).firstOrDefault(f => f.regiao == 'Norte');
          d.mediaDisponibilidade = dadosNorte.mediaDisponibilidade + '%';
          d.SaldoHoras = dadosNorte.saldoHoras;
          d.BacklogOS = dadosNorte.backlogOS;
          d.OSAbertas = dadosNorte.qtdOSAbertas;
          d.OSFechadas = dadosNorte.qtdOSFechadas;
        }

        if (dados.rect.region == Region.NORDESTE) {
          let dadosNordeste = Enumerable.from(this.viewDados).firstOrDefault(f => f.regiao == 'Nordeste');
          d.mediaDisponibilidade = dadosNordeste.mediaDisponibilidade + '%';
          d.SaldoHoras = dadosNordeste.saldoHoras;
          d.BacklogOS = dadosNordeste.backlogOS;
          d.OSAbertas = dadosNordeste.qtdOSAbertas;
          d.OSFechadas = dadosNordeste.qtdOSFechadas;
        }

        if (dados.rect.region == Region.CENTROESTE) {
          let dadosCentroeste = Enumerable.from(this.viewDados).firstOrDefault(f => f.regiao == 'Centro-Oeste');
          d.mediaDisponibilidade = dadosCentroeste.mediaDisponibilidade + '%';
          d.SaldoHoras = dadosCentroeste.saldoHoras;
          d.BacklogOS = dadosCentroeste.backlogOS;
          d.OSAbertas = dadosCentroeste.qtdOSAbertas;
          d.OSFechadas = dadosCentroeste.qtdOSFechadas;
        }

        if (dados.rect.region == Region.SUDESTE) {
          let dadosSudeste = Enumerable.from(this.viewDados).firstOrDefault(f => f.regiao == 'Sudeste');
          d.mediaDisponibilidade = dadosSudeste.mediaDisponibilidade + '%';
          d.SaldoHoras = dadosSudeste.saldoHoras;
          d.BacklogOS = dadosSudeste.backlogOS;
          d.OSAbertas = dadosSudeste.qtdOSAbertas;
          d.OSFechadas = dadosSudeste.qtdOSFechadas;
        }

        if (dados.rect.region == Region.SUL) {
          let dadosSul = Enumerable.from(this.viewDados).firstOrDefault(f => f.regiao == 'Sul');
          d.mediaDisponibilidade = dadosSul.mediaDisponibilidade + '%';
          d.SaldoHoras = dadosSul.saldoHoras;
          d.BacklogOS = dadosSul.backlogOS;
          d.OSAbertas = dadosSul.qtdOSAbertas;
          d.OSFechadas = dadosSul.qtdOSFechadas;
        }

        // this.regionRects.push(rbr);
        this.getRectTemplateDisponibilidade(dados.rect, dados.box, d);
      });
    } else {
      this.rectDados.forEach(dados => {
        debugger;
        let d: MapaMultaModel = new MapaMultaModel();

        if (dados.rect.region == Region.NORTE) {
          d.multa11 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Norte' && f.criticidade == '11').multa;
          d.multa12 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Norte' && f.criticidade == '12').multa;
          d.multa13 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Norte' && f.criticidade == '13').multa;
          d.multa14 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Norte' && f.criticidade == '14').multa;
          d.multa15 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Norte' && f.criticidade == '15').multa;
        }

        if (dados.rect.region == Region.NORDESTE) {
          d.multa11 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Nordeste' && f.criticidade == '11').multa;
          d.multa12 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Nordeste' && f.criticidade == '12').multa;
          d.multa13 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Nordeste' && f.criticidade == '13').multa;
          d.multa14 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Nordeste' && f.criticidade == '14').multa;
          d.multa15 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Nordeste' && f.criticidade == '15').multa;
        }

        if (dados.rect.region == Region.CENTROESTE) {
          d.multa11 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Centro-Oeste' && f.criticidade == '11').multa;
          d.multa12 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Centro-Oeste' && f.criticidade == '12').multa;
          d.multa13 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Centro-Oeste' && f.criticidade == '13').multa;
          d.multa14 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Centro-Oeste' && f.criticidade == '14').multa;
          d.multa15 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Centro-Oeste' && f.criticidade == '15').multa;
        }

        if (dados.rect.region == Region.SUDESTE) {
          d.multa11 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sudeste' && f.criticidade == '11').multa;
          d.multa12 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sudeste' && f.criticidade == '12').multa;
          d.multa13 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sudeste' && f.criticidade == '13').multa;
          d.multa14 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sudeste' && f.criticidade == '14').multa;
          d.multa15 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sudeste' && f.criticidade == '15').multa;
        }

        if (dados.rect.region == Region.SUL) {
          d.multa11 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sul' && f.criticidade == '11').multa;
          d.multa12 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sul' && f.criticidade == '12').multa;
          d.multa13 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sul' && f.criticidade == '13').multa;
          d.multa14 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sul' && f.criticidade == '14').multa;
          d.multa15 = Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == 'Sul' && f.criticidade == '15').multa;
        }

        this.getRectTemplateMultas(dados.rect, dados.box, d);
      });
    }

    this.loading = false;
  }

  private removerRectsIniciais() {
    for (let i = 1; i <= 5; i++) {
      document.querySelector("#textTitulo" + i).remove();
      document.querySelector("#textDescricao" + i).remove();
    }
  }

  private startLoadingRectTemplate(rbr: RectByRegion) {
    var b = rbr.rect.getBBox();
    var xLeft: number = (b.x + b.width / 2.5);
    var yLeft: number = b.y + 16000;

    this.createInnerRect(rbr.rect, "Carregando dados", "Aguarde", xLeft, yLeft, rbr.region, true);
    rbr.rect.setAttribute("fill", this.getRegionColor(rbr.region));
    yLeft += 6000;
  }

  private getRectTemplateDisponibilidade(rbr: RectByRegion, box: SVGRect, d: MapaDisponibilidadeModel) {
    var metricTitles: string[] = ["Média Disp.", "OS Abertas", "OS Fechadas", "Backlog OS", "Saldo Horas"];
    var xLeft: number = (box.x + box.width / 2);
    var yLeft: number = box.y + 4000;

    metricTitles.forEach(metric => {

      let value = metric == "Média Disp." ? d.mediaDisponibilidade : metric == "OS Abertas" ? d.OSAbertas : metric == "OS Fechadas" ?
        d.OSFechadas : metric == "Backlog OS" ? d.BacklogOS : d.SaldoHoras;

      this.createInnerRect(rbr.rect, metric, value.toString(), xLeft, yLeft, rbr.region, false);

      rbr.rect.setAttribute("fill", this.getRegionColor(rbr.region));
      yLeft += 6000;
    })
  }

  private getRectTemplateMultas(rbr: RectByRegion, box: SVGRect, d: MapaMultaModel) {
    var metricTitles: string[] = ["Criticidade 11", "Criticidade 12", "Criticidade 13", "Criticidade 14", "Criticidade 15"];
    var xLeft: number = (box.x + box.width / 2);
    var yLeft: number = box.y + 4000;

    metricTitles.forEach(metric => {

      let value = metric == "Criticidade 11" ? d.multa11 : metric == "Criticidade 12" ? d.multa12 : metric == "Criticidade 13" ?
        d.multa13 : metric == "Criticidade 14" ? d.multa14 : d.multa15;

      this.createInnerRect(rbr.rect, metric, value.toString(), xLeft, yLeft, rbr.region, false);

      rbr.rect.setAttribute("fill", this.getRegionColor(rbr.region));
      yLeft += 6000;
    })
  }

  private getInitials(id: string): string {
    return id.toUpperCase().split("-").shift();
  }

  private getRegion(id: string): Region {
    if ("AM, RR, AC, PA, TO, AP, RO".includes(id)) return Region.NORTE;
    if ("RS, SC, PR".includes(id)) return Region.SUL;
    if ("SP, RJ, MG, ES".includes(id)) return Region.SUDESTE;
    if ("MT, MS, GO, DF".includes(id)) return Region.CENTROESTE;
    if ("MA, PI, PE, PB, RN, BA, SE, AL, CE".includes(id)) return Region.NORDESTE;
  }

  private getRegionColor(r: Region): string {
    return r == Region.SUL ? "#E0BBE4" :
      r == Region.NORDESTE ? "#F9F0C1" :
        r == Region.NORTE ? "#F4CDA6" :
          r == Region.CENTROESTE ? "#F6A8A6" : "#A5C8E4";
  }

  private createInnerRect(rect: SVGPathElement, title: string, value: string, xLeft: number, yLeft: number, id: number, startValues: boolean): void {

    // elipse
    var c = document.createElementNS("http://www.w3.org/2000/svg", "ellipse");
    c.rx.baseVal.value = 16000;
    c.ry.baseVal.value = 3000;
    c.style.fill = "none";
    c.setAttribute("pointer-events", "none");
    c.setAttribute("opacity", "0.5");
    c.setAttribute("stroke-width", "250");
    c.setAttribute("text-anchor", "center");
    c.setAttribute("transform", "translate(" + xLeft + " " + yLeft + ")");

    // titulo
    var t = document.createElementNS("http://www.w3.org/2000/svg", "text");
    t.setAttribute("transform", "translate(" + (xLeft - 6500) + " " + (yLeft - 850) + ")");
    t.textContent = title;
    t.setAttribute("fill", "black");
    t.setAttribute("pointer-events", "none");
    t.setAttribute("text-align", "center");
    t.setAttribute("font-weight", "bold");
    t.setAttribute('font-size', '2500');
    t.setAttribute("id", "textTitulo" + id);

    // valor
    var v = document.createElementNS("http://www.w3.org/2000/svg", "text");
    v.setAttribute("transform", "translate(" + (startValues ? xLeft : xLeft - 3000) + " " + (yLeft + 2000) + ")");
    v.textContent = value;
    v.setAttribute("fill", "black");
    v.setAttribute("pointer-events", "none");
    v.setAttribute("text-align", "center");
    v.setAttribute('font-size', '2500');
    v.setAttribute("id", "textDescricao" + id);

    rect.parentNode.insertBefore(t, rect.nextSibling);
    rect.parentNode.insertBefore(v, rect.nextSibling);
    rect.parentNode.insertBefore(c, rect.nextSibling);
  }
}


// Models

export class MapaDisponibilidadeModel {
  public mediaDisponibilidade: string;
  public OSAbertas?: number;
  public OSFechadas?: number;
  public BacklogOS?: number;
  public SaldoHoras: string;
}

export class MapaMultaModel {
  public multa11?: number;
  public multa12?: number;
  public multa13?: number;
  public multa14?: number;
  public multa15?: number;
}