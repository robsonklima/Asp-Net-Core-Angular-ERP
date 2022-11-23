import { AfterViewInit, Component } from '@angular/core';
import { Angulartics2GoogleAnalytics } from 'angulartics2';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})

export class AppComponent implements AfterViewInit
{
    constructor(angulartics2GoogleAnalytics: Angulartics2GoogleAnalytics) {
        angulartics2GoogleAnalytics.startTracking();
    }

    ngAfterViewInit() {}
}