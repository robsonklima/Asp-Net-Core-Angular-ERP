import { Component } from '@angular/core';
import { DocsComponent } from '../../docs.component';


@Component({
    selector   : 'app-introducao',
    templateUrl: './introducao.component.html',
    styles     : ['']
})
export class IntroducaoComponent
{
    constructor(
      private _guidesComponent: DocsComponent
    ) {}

    toggleDrawer(): void
    {
      this._guidesComponent.matDrawer.toggle();
    }
}
