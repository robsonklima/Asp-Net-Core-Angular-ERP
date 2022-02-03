import { Component } from '@angular/core';
import { DocsComponent } from '../../docs.component';

@Component({
  selector: 'app-listagem',
  templateUrl: './listagem.component.html'
})
export class ListagemComponent {
  constructor(
    private _guidesComponent: DocsComponent
  ) {}

  toggleDrawer(): void
  {
    this._guidesComponent.matDrawer.toggle();
  }
}
