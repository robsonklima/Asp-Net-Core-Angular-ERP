import { Component } from '@angular/core';
import { DocsComponent } from '../../docs.component';

@Component({
  selector: 'app-exportacao',
  templateUrl: './exportacao.component.html'
})
export class ExportacaoComponent {
  constructor(
    private _guidesComponent: DocsComponent
  ) {}

  toggleDrawer(): void
  {
    this._guidesComponent.matDrawer.toggle();
  }
}
