import { Route } from '@angular/router';
import { ImportacaoComponent } from './importacao.component';
import { ImportacaoArquivoAdendoComponent } from './importacao-arquivo-adendo/importacao-arquivo-adendo.component';


export const ImportacaoRoutes: Route[] = [
    {
        path: '',
        component: ImportacaoComponent
    },
    
    {
        path: 'arquivo',
        component: ImportacaoArquivoAdendoComponent,
    },
];
