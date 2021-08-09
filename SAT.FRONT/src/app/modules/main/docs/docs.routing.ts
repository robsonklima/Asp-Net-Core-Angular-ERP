import { Route } from '@angular/router';
import { ChangelogComponent } from './changelog/changelog.component';

export const docsRoutes: Route[] = [
    {
        path     : 'changelog',
        component: ChangelogComponent
    }
];
