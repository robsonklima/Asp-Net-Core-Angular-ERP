import { Component } from '@angular/core';
import { DocsComponent } from '../docs.component';

@Component({
  selector: 'app-autenticacao',
  templateUrl: './autenticacao.component.html'
})
export class AutenticacaoComponent {
  constructor(
    private _guidesComponent: DocsComponent
  ) {}

  toggleDrawer(): void
  {
    this._guidesComponent.matDrawer.toggle();
  }
}
