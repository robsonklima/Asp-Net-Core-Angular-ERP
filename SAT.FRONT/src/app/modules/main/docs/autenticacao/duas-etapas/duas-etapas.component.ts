import { Component } from '@angular/core';
import { DocsComponent } from '../../docs.component';

@Component({
  selector: 'app-duas-etapas',
  templateUrl: './duas-etapas.component.html'
})
export class DuasEtapasComponent {
  constructor(
    private _guidesComponent: DocsComponent
  ) {}

  toggleDrawer(): void
  {
    this._guidesComponent.matDrawer.toggle();
  }
}
