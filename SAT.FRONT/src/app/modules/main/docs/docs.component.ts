import {
    ChangeDetectionStrategy, Component,
    OnInit, ViewEncapsulation
} from '@angular/core';

@Component({
    selector: 'app-docs',
    templateUrl: './docs.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DocsComponent implements OnInit {

    constructor(
    ) {
    }

    ngOnInit(): void {
    }
}
