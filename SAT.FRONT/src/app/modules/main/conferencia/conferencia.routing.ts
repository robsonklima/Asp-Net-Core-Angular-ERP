import { Route } from '@angular/router';
import { JitsiComponent } from './jitsi/jitsi.component';

export const conferenciaRoutes: Route[] = [
    {
        path     : 'jitsi',
        component: JitsiComponent
    }
];
