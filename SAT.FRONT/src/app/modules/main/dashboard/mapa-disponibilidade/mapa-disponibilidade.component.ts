import { AfterViewInit, Component } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { IndicadorService } from 'app/core/services/indicador.service';
import { DashboardDisponibilidade, DashboardTipoEnum } from 'app/core/types/dashboard.types';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';

export enum Region
{
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
  ]
})

export class MapaDisponibilidadeComponent implements AfterViewInit
{
  private statesByRegion: StateByRegion[] = [];
  private regionRects: RectByRegion[] = [];

  public loading: boolean = false;

  constructor (private _dashboardService: DashboardService,
    private _indicadorService: IndicadorService) { }

  ngAfterViewInit(): void
  {
    this.initializeMap();
  }

  public initializeMap(): void
  {
    this.getStates();
    this.getRects();
  }

  private getStates(): void
  {
    var states = Array.from(document.querySelector("#landmarks-brazil-disp").querySelectorAll("path")).filter(st => st.id.endsWith("disp"));
    states.forEach(s =>
    {
      const sbr: StateByRegion =
      {
        region: this.getRegion(this.getInitials(s.id)),
        state: s
      };

      this.statesByRegion.push(sbr);
      this.formatPaths(sbr);
    })
  }

  public formatPaths(st: StateByRegion): void
  {
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

  public getRects(): void
  {
    var rects =
      Array.from(document.querySelector("#landmarks-brazil-disp")
        .querySelectorAll("path"))
        .filter(r => r.id.startsWith("regiao"));

    this._indicadorService.obterPorParametros({
      tipo: IndicadorTipoEnum.DISPONIBILIDADE,
      agrupador: IndicadorAgrupadorEnum.REGIAO
    }).subscribe((data: Indicador[]) =>
    {
      debugger;
    });



    // let dados = this._dashboardService.obterPorParametros({
    //    tipo: DashboardTipoEnum.DISPONIBILIDADE_BBTS
    //   // agrupador: IndicadorAgrupadorEnum.TOP_PECAS_MAIS_FALTANTES,
    //   // filterType: OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES,
    //   // include: OrdemServicoIncludeEnum.OS_PECAS
    // }).toPromise();



    rects.forEach(r =>
    {

      const rbr: RectByRegion =
      {
        region: parseInt(r.id.split("-").pop()),
        rect: r
      };


      // DaDOS

      let d: MapaDisponibilidadeModel = new MapaDisponibilidadeModel();

      if (rbr.region == Region.NORTE)
      {
        d.mediaDisponibilidade = 'NORTE';
        d.SaldoHoras = 'R$100.00';
        d.BacklogOS = 'R$100.00';
        d.OSAbertas = 'R$100.00';
        d.OSFechadas = 'R$100.00';
      }

      if (rbr.region == Region.NORDESTE)
      {
        d.mediaDisponibilidade = 'NORDESTE';
        d.SaldoHoras = 'R$100.00';
        d.BacklogOS = 'R$100.00';
        d.OSAbertas = 'R$100.00';
        d.OSFechadas = 'R$100.00';
      }

      if (rbr.region == Region.CENTROESTE)
      {
        d.mediaDisponibilidade = 'CENTROESTE';
        d.SaldoHoras = 'R$100.00';
        d.BacklogOS = 'R$100.00';
        d.OSAbertas = 'R$100.00';
        d.OSFechadas = 'R$100.00';
      }

      if (rbr.region == Region.SUDESTE)
      {
        d.mediaDisponibilidade = 'SUDESTE';
        d.SaldoHoras = 'R$100.00';
        d.BacklogOS = 'R$100.00';
        d.OSAbertas = 'R$100.00';
        d.OSFechadas = 'R$100.00';
      }

      if (rbr.region == Region.SUL)
      {
        d.mediaDisponibilidade = 'SUL';
        d.SaldoHoras = 'R$100.00';
        d.BacklogOS = 'R$100.00';
        d.OSAbertas = 'R$100.00';
        d.OSFechadas = 'R$100.00';
      }


      this.regionRects.push(rbr);
      this.getRectTemplate(rbr, d);
    })
  }

  private getRectTemplate(rbr: RectByRegion, d: MapaDisponibilidadeModel)
  {
    var metricTitles: string[] = ["Média Disp.", "OS Abertas", "OS Fechadas", "Backlog OS", "Saldo Horas"];
    var b = rbr.rect.getBBox();

    var xLeft: number = (b.x + b.width / 2);
    var yLeft: number = b.y + 4000;

    metricTitles.forEach(metric =>
    {

      let value = metric == "Média Disp." ? d.mediaDisponibilidade : metric == "OS Abertas" ? d.OSAbertas : metric == "OS Fechadas" ?
        d.OSFechadas : metric == "Backlog OS" ? d.BacklogOS : d.SaldoHoras;

      this.createInnerRect(rbr.rect, metric, value, xLeft, yLeft);

      rbr.rect.setAttribute("fill", this.getRegionColor(rbr.region));
      yLeft += 6000;
    })
  }

  private getInitials(id: string): string
  {
    return id.toUpperCase().split("-").shift();
  }

  private getRegion(id: string): Region
  {
    if ("AM, RR, AC, PA, TO, AP, RO".includes(id)) return Region.NORTE;
    if ("RS, SC, PR".includes(id)) return Region.SUL;
    if ("SP, RJ, MG, ES".includes(id)) return Region.SUDESTE;
    if ("MT, MS, GO, DF".includes(id)) return Region.CENTROESTE;
    if ("MA, PI, PE, PB, RN, BA, SE, AL, CE".includes(id)) return Region.NORDESTE;
  }

  private getRegionColor(r: Region): string
  {
    return r == Region.SUL ? "#E0BBE4" :
      r == Region.NORDESTE ? "#F9F0C1" :
        r == Region.NORTE ? "#F4CDA6" :
          r == Region.CENTROESTE ? "#F6A8A6" : "#A5C8E4";
  }

  private createInnerRect(rect: SVGPathElement, title: string, value: string, xLeft: number, yLeft: number): void
  {
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

    // valor
    var v = document.createElementNS("http://www.w3.org/2000/svg", "text");
    v.setAttribute("transform", "translate(" + (xLeft - 3000) + " " + (yLeft + 2000) + ")");
    v.textContent = value;
    v.setAttribute("fill", "black");
    v.setAttribute("pointer-events", "none");
    v.setAttribute("text-align", "center");
    v.setAttribute('font-size', '2500');

    rect.parentNode.insertBefore(t, rect.nextSibling);
    rect.parentNode.insertBefore(v, rect.nextSibling);
    rect.parentNode.insertBefore(c, rect.nextSibling);
  }
}


export class MapaDisponibilidadeModel
{
  public mediaDisponibilidade: string;
  public OSAbertas: string;
  public OSFechadas: string;
  public BacklogOS: string;
  public SaldoHoras: string;
}