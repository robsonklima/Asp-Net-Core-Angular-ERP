import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import packageInfo from '../../../../../package.json';

@Component({
    selector: 'version',
    templateUrl: './version.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'version'
})
export class VersionComponent implements OnInit {
    versao: string = packageInfo.version;
    
    constructor(
        
    ) {
        
    }

    async ngOnInit()
    {
        
    }
}
