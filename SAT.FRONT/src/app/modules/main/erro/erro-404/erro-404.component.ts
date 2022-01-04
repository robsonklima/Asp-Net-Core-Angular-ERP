import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';

@Component({
    selector       : 'erro-404',
    templateUrl    : './erro-404.component.html',
    encapsulation  : ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class Erro404Component
{
    /**
     * Constructor
     */
    constructor()
    {
    }
}
