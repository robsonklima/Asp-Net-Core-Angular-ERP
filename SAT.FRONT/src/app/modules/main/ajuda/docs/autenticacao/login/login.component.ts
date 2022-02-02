import { Component } from '@angular/core';
import { DocsComponent } from '../../docs.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  constructor(
    private _guidesComponent: DocsComponent
  ) {}

  toggleDrawer(): void
  {
    this._guidesComponent.matDrawer.toggle();
  }
}
