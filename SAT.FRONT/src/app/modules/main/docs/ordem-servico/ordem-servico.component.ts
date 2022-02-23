import { Component } from '@angular/core';
import { DocsComponent } from '../docs.component';

@Component({
  selector: 'app-ordem-servico',
  templateUrl: './ordem-servico.component.html'
})
export class OrdemServicoComponent {

  constructor(
    private _guidesComponent: DocsComponent
  ) {}

  toggleDrawer(): void
  {
    this._guidesComponent.matDrawer.toggle();
  }
}
