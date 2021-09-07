import { Route } from '@angular/router';
import { AgendaTecnicoComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico.component';
import { AgendaTecnicoConfiguracoesComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico-configuracoes/agenda-tecnico-configuracoes.component';
import { CalendarCalendarsResolver, CalendarSettingsResolver, CalendarWeekdaysResolver } from 'app/modules/main/agenda-tecnico/agenda-tecnico.resolvers';

export const agendaTecnicoRoutes: Route[] = [
    {
        path     : '',
        component: AgendaTecnicoComponent,
        resolve  : {
            calendars: CalendarCalendarsResolver,
            settings : CalendarSettingsResolver,
            weekdays : CalendarWeekdaysResolver
        }
    },
    {
        path     : 'agenda-tecnico-configuracoes',
        component: AgendaTecnicoConfiguracoesComponent,
        resolve  : {
            settings: CalendarSettingsResolver
        }
    }
];
