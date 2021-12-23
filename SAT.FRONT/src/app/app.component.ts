import { AfterViewInit, Component } from '@angular/core';
import { SwUpdate } from '@angular/service-worker';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})

export class AppComponent implements AfterViewInit
{
    constructor (private swUpdate: SwUpdate) { }

    ngAfterViewInit()
    {
        if (this.swUpdate.isEnabled)
        {
            this.swUpdate.versionUpdates
                .subscribe(() =>
                {
                    this.swUpdate
                        .activateUpdate()
                        .then(() =>
                        {
                            window.location.reload();
                        });
                });
        }
    }
}