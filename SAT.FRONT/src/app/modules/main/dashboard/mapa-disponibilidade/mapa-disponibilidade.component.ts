import { AfterViewInit, Component  } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { Indicador, IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { FilialEnum } from 'app/shared/enums/FilialEnum';
import moment from 'moment';

@Component({
  selector: 'app-mapa-disponibilidade',
  templateUrl: './mapa-disponibilidade.component.html',
  styleUrls: ['./mapa-disponibilidade.component.css'
  ]
})

export class MapaDisponibilidadeComponent implements AfterViewInit 
{
  private paths: SVGPathElement[] = [];
  private ellipses: SVGRectElement[] = [];
  private codFiliais: string[] = [];
  
  constructor(private _indicadorService: IndicadorService) { }
  ngAfterViewInit(): void
  {
    this.getPaths();
    this.getFiliais();
    this.initializeMap();
  }

  public addElements(p: SVGPathElement): void
  {
      var t = document.createElementNS("http://www.w3.org/2000/svg", "text");
      var b = p.getBBox();
      t.setAttribute("transform", "translate(" + (b.x + b.width/1.6) + " " + (b.y + b.height/1.7) + ")");
      t.textContent = p.id.toUpperCase().split("-").shift();
      t.setAttribute("fill", "black");
      t.setAttribute("pointer-events", "none");
      t.setAttribute("text-anchor", "middle");
      t.setAttribute("font-weight", "bold");
      t.setAttribute("font-size", "12");
      p.parentNode.insertBefore(t, p.nextSibling);

      if (p.id.startsWith("sul")){
        var c = document.createElementNS("http://www.w3.org/2000/svg", "rect");
        c.rx.baseVal.value = 80;
        c.ry.baseVal.value = 80;
        c.style.fill = "blue";
        c.setAttribute("cursor", "pointer");
        c.setAttribute("transform", "translate(" + (b.x + b.width/2) + " " + (b.y + b.height/2) + ")");
        c.setAttribute("id", p.id);
        p.parentNode.insertBefore(c, t);
      }
  }

  public initializeMap(): void
  {
    for (var p in this.paths)
      this.addElements(this.paths[p]);

    //this.getEllipses();
    this.getData();
  }

  public async getData(): Promise<void>
  {
    var params: IndicadorParameters = 
    {
      agrupador: IndicadorAgrupadorEnum.FILIAL,
      tipo: IndicadorTipoEnum.SLA,
      codFiliais: this.codFiliais.join(','),
      dataInicio: moment().startOf('month').toISOString(),
      dataFim: moment().endOf('month').toISOString()
    };

    var data = await this._indicadorService.obterPorParametros(params).toPromise();
    this.updateData(data);
  }

  private getPaths(): void
  {
    this.paths = Array.from(document.querySelector("#landmarks-brazil-disp").querySelectorAll("path"));
  }

  private getFiliais(): void
  {
    Object.keys(FilialEnum).filter((e) => isNaN(Number(e))).forEach((i) => this.codFiliais.push(this.parseFilialCod(i)));
  }

  private getEllipses(): void
  {
    this.ellipses = Array.from(document.querySelector("#landmarks-brazil-disp").querySelectorAll("rect"));
  }

  private updateData(data: Indicador[]): void
  {
    data.forEach(d => this.paintEllipse(d));
  }

  private paintEllipse(filial: Indicador)
  {
    var c = this.ellipses.find(c => c.id.toLocaleUpperCase() == filial.label.toUpperCase());
    c.style.fill = filial.valor >= 95 ? 'green' : filial.valor >= 90 ? 'yellow' : 'red';
  }

  private parseFilialCod(sigla: string): string
  {
    return FilialEnum[sigla].toString();
  }

  private selectedEllipse(this, ev: MouseEvent)
  {
    alert("oi, eu sou a filial " + (this as SVGEllipseElement).id + ", cod " + FilialEnum[(this as SVGEllipseElement).id.toUpperCase()]);
  }

  private highlightEllipse(this, ev: MouseEvent)
  {
    // todo
  }

  private unhighlightEllipse(this, ev: MouseEvent)
  {
    // todo
  }
}