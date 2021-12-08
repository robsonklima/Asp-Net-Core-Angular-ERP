import { Component, OnInit } from '@angular/core';
import { environment } from 'environments/environment.prod';
import { VersionCheckService } from './core/version-control/version-control.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit
{
    // constructor (private _versionCheckService: VersionCheckService) { }
    constructor () { }

    ngOnInit(): void
    {
        // this._versionCheckService.initVersionCheck(environment.versionCheckURL);
    }
}