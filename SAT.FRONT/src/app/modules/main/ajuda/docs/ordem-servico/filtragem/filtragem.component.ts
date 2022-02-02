import { Component } from '@angular/core';
import { DocsComponent } from '../../docs.component';

@Component({
  selector: 'app-filtragem',
  templateUrl: './filtragem.component.html'
})
export class FiltragemComponent {
  constructor(
    private _guidesComponent: DocsComponent
  ) {}

  toggleDrawer(): void
  {
    this._guidesComponent.matDrawer.toggle();
  }
}
