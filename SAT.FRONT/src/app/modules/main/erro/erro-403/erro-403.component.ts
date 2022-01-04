import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';

@Component({
    selector       : 'erro-403',
    templateUrl    : './erro-403.component.html',
    encapsulation  : ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class Erro403Component
{
    /**
     * Constructor
     */
    constructor()
    {
    }
}
