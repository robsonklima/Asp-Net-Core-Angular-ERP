import { Component, OnInit  } from '@angular/core';

@Component({
  selector: 'app-mapa',
  templateUrl: './mapa.component.html',
  styleUrls: ['./mapa.component.css'
  ]
})

export class MapaComponent implements OnInit 
{
  ngOnInit(): void
  {
    this.initializeMap();
  }

  initializeMap(): void
  {
    var paths: SVGPathElement[] = Array.from(document.querySelector("#landmarks-brazil").querySelectorAll("path"));
    for (var p in paths)
    {
      this.addElements(paths[p]);
    }
  }

  addElements(p: SVGPathElement): void
  {
      var t = document.createElementNS("http://www.w3.org/2000/svg", "text");
      var b = p.getBBox();
      t.setAttribute("transform", "translate(" + (b.x + b.width/1.6) + " " + (b.y + b.height/1.7) + ")");
      t.textContent = p.id.toUpperCase();
      t.setAttribute("fill", "black");
      t.setAttribute("text-anchor", "middle");
      t.setAttribute("font-weight", "bold");
      t.setAttribute("cursor", "pointer");
      t.setAttribute("font-size", "12");
      p.parentNode.insertBefore(t, p.nextSibling);

      if (!p.id.startsWith("f")) return;

      var c = document.createElementNS("http://www.w3.org/2000/svg", "circle");
      c.cx.baseVal.value = 8;
      c.cy.baseVal.value = 8;
      c.r.baseVal.value = 8;
      c.style.fill = "lime";
      c.setAttribute("transform", "translate(" + (b.x + b.width/2) + " " + (b.y + b.height/2) + ")");
      p.parentNode.insertBefore(c, t);
  }
}