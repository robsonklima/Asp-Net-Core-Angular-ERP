import { AfterViewInit, Component  } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';

export enum Region
{
  NORTE,
  SUL,
  SUDESTE,
  CENTROESTE,
  NORDESTE
}

export type StateByRegion =
{
  region: Region;
  state: SVGPathElement;
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

  constructor(private _indicadorService: IndicadorService) { }

  ngAfterViewInit(): void
  {
    this.getStates();
    this.initializeMap();
  }

  public initializeMap(): void
  {
    this.getStates();
    this.addRects();
  }

  public async getData(): Promise<void>
  {
    
  }

  private getStates(): void
  {
    var states = Array.from(document.querySelector("#landmarks-brazil-disp").querySelectorAll("path"));

    states.forEach(s =>
    {
      const sbr: StateByRegion =
      {
        region: this.getRegion(this.getInitials(s.id)),
        state: s
      };

      this.statesByRegion.push(sbr);
      this.addElements(sbr.region, s);
    })
  }

  private addRects(): void
  {
    // NORTE
    // var norte = this.statesByRegion.find(i => i.state.id.startsWith("rn"));
    // var t = document.createElementNS("http://www.w3.org/2000/svg", "ellipse");
    // t.cx.baseVal.value = 80;
    // t.cy.baseVal.value = 80;
    // t.rx.baseVal.value = 80;
    // t.ry.baseVal.value = 80;
    // var b = norte.state.getBBox();
    // t.setAttribute("transform", "translate(" + (b.x + b.width/0.5) + " " + (b.y + b.height/4) + ")");
    // t.setAttribute("fill", this.getRegionColor(norte.region));
    // norte.state.parentNode.insertBefore(t, norte.state.nextSibling);
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
    return r == Region.SUL ? "#e77094" :
              r == Region.NORDESTE ? "#e89d7b" : 
                r == Region.NORTE ? "#7823a2" : 
                  r == Region.CENTROESTE ? "#08ddc8" : "#c8dc72";
  }

  public addElements(r: Region, p: SVGPathElement): void
  {
      var t = document.createElementNS("http://www.w3.org/2000/svg", "text");
      var b = p.getBBox();
      t.setAttribute("transform", "translate(" + (b.x + b.width/1.6) + " " + (b.y + b.height/1.7) + ")");
      t.textContent = this.getInitials(p.id);
      t.setAttribute("fill", "black");
      t.setAttribute("pointer-events", "none");
      t.setAttribute("text-anchor", "middle");
      t.setAttribute("font-weight", "bold");
      t.setAttribute("font-size", "12");
      p.setAttribute("fill", this.getRegionColor(r));
      p.parentNode.insertBefore(t, p.nextSibling);
  }

  private getInitials(id: string): string
  {
    return id.toUpperCase().split("-").shift();
  }
}