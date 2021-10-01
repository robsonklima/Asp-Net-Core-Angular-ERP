import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
    selector       : 'app-agenda-tecnico',
    templateUrl    : './agenda-tecnico.component.html',
    styleUrls      : ['./agenda-tecnico.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation  : ViewEncapsulation.None
})
export class AgendaTecnicoComponent implements OnInit {
    
    constructor(
        
    ) {
        
    }
    
    ngOnInit(): void
    {
        
    }
}