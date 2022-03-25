import { Route } from '@angular/router';
import { FerramentaTecnicoFormComponent } from './ferramenta-tecnico-form/ferramenta-tecnico-form.component';
import { FerramentaTecnicoListaComponent } from './ferramenta-tecnico-lista/ferramenta-tecnico-lista.component';

export const ferramentaTecnicoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: FerramentaTecnicoListaComponent,
    },
    {
        path: 'form',
        component: FerramentaTecnicoFormComponent,
    },
    {
        path: 'form/:codFerramentaTecnico',
        component: FerramentaTecnicoFormComponent,
    },
];
