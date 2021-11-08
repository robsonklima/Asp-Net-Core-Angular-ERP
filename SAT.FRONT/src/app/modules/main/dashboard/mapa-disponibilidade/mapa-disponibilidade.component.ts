import { AfterViewInit, Component } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';

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
  ]
})

export class MapaDisponibilidadeComponent implements AfterViewInit {
  private statesByRegion: StateByRegion[] = [];
  private regionRects: RectByRegion[] = [];

  public loading: boolean = false;

  constructor(private _indicadorService: IndicadorService) { }

  ngAfterViewInit(): void {
    this.initializeMap();
  }

  public initializeMap(): void {
    this.getStates();
    this.getRects();
  }

  private getStates(): void {
    var states = Array.from(document.querySelector("#landmarks-brazil-disp").querySelectorAll("path")).filter(st => st.id.endsWith("disp"));
    states.forEach(s => {
      const sbr: StateByRegion =
      {
        region: this.getRegion(this.getInitials(s.id)),
        state: s
      };

      this.statesByRegion.push(sbr);
      this.formatPaths(sbr);
    })
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
    t.setAttribute("font-size", "18");
    st.state.setAttribute("fill", this.getRegionColor(st.region));
    st.state.parentNode.insertBefore(t, st.state.nextSibling);
  }

  public getRects(): void {
    var rects =
      Array.from(document.querySelector("#landmarks-brazil-disp")
        .querySelectorAll("path"))
        .filter(r => r.id.startsWith("regiao"));

    rects.forEach(r => {
      const rbr: RectByRegion =
      {
        region: parseInt(r.id.split("-").pop()),
        rect: r
      };

      this.regionRects.push(rbr);
      this.getRectTemplate(rbr);
    })
  }

  private getRectTemplate(rbr: RectByRegion) {
    var metricTitles: string[] = ["Média Disp.", "OS Abertas", "OS Fechadas", "Backlog OS", "Saldo Horas"];
    var b = rbr.rect.getBBox();

    var xLeft: number = (b.x + b.width / 5);
    var yLeft: number = b.y + 4000;

    metricTitles.forEach(metric => {
      this.createInnerRect(rbr.rect, metric, "sdfd0.00", xLeft, yLeft);
      
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

  private createInnerRect(rect: SVGPathElement, title: string, value: string, xLeft: number, yLeft: number): void {
    // elipse
  //   var c = document.createElementNS("http://www.w3.org/2000/svg", "ellipse");
  //   c.rx.baseVal.value = 16000;
  //   c.ry.baseVal.value = 3000;
  //   c.style.fill = "none";
  //   c.setAttribute("pointer-events", "none");
  //  // forma.setAttributeNS(null, "width",  50);
  //   c.setAttribute("opacity", "0.5");
  //   c.setAttribute("stroke-width", "250");
  //   c.setAttribute("text-anchor", "center");
  //   c.setAttribute("transform", "translate(" + xLeft + " " + yLeft + ")");

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
   // rect.parentNode.insertBefore(c, rect.nextSibling);
  }
}