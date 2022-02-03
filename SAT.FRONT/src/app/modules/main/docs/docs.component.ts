import { 
  ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy,
  OnInit, ViewChild, ViewEncapsulation
} from '@angular/core';
import { MatDrawer } from '@angular/material/sidenav';
import { FuseNavigationItem } from '@fuse/components/navigation';
import { takeUntil } from 'rxjs/operators';
import { FuseMediaWatcherService } from '@fuse/services/media-watcher';
import { Subject } from 'rxjs';

@Component({
    selector       : 'app-docs',
    templateUrl    : './docs.component.html',
    encapsulation  : ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DocsComponent implements OnInit, OnDestroy
{
    @ViewChild('matDrawer', {static: true}) matDrawer: MatDrawer;
    drawerMode: 'side' | 'over';
    drawerOpened: boolean;
    menuData: FuseNavigationItem[];
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseMediaWatcherService: FuseMediaWatcherService
    )
    {
        this.menuData = [
            {
                id      : 'inicio',
                title   : 'Início',
                type    : 'group',
                children: [
                    {
                        id   : 'introducao',
                        title: 'Introdução',
                        type : 'basic',
                        link : '/docs/inicio/introducao'
                    },
                    {
                        id   : 'versoes',
                        title: 'Versões',
                        type : 'basic',
                        link : '/docs/inicio/versoes'
                    }
                ]
            },
            {
                id      : 'autenticacao',
                title   : 'Autenticação',
                type    : 'group',
                children: [
                    {
                        id   : 'login',
                        title: 'Login',
                        type : 'basic',
                        link : '/docs/autenticacao/login'
                    },
                    {
                        id   : 'duas-etaoas',
                        title: 'Duas Etapas',
                        type : 'basic',
                        link : '/docs/autenticacao/duas-etapas'
                    }
                ]
            },
            {
                id      : 'ordem-servico',
                title   : 'Ordem de Serviço',
                type    : 'group',
                children: [
                    {
                        id   : 'lista',
                        title: 'Lista',
                        type : 'basic',
                        link : '/docs/ordem-servico/listagem'
                    },
                    {
                        id   : 'filtro',
                        title: 'Filtros',
                        type : 'basic',
                        link : '/docs/ordem-servico/filtro'
                    },
                    {
                        id   : 'exportacao',
                        title: 'Exportação',
                        type : 'basic',
                        link : '/docs/ordem-servico/exportacao'
                    },
                    {
                        id   : 'novo',
                        title: 'Novo',
                        type : 'basic',
                        link : '/docs/ordem-servico/novo'
                    }
                ]
            }
        ];
    }

    ngOnInit(): void
    {
        // Subscribe to media query change
        this._fuseMediaWatcherService.onMediaChange$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({matchingAliases}) => {

                // Set the drawerMode and drawerOpened
                if ( matchingAliases.includes('md') )
                {
                    this.drawerMode = 'side';
                    this.drawerOpened = true;
                }
                else
                {
                    this.drawerMode = 'over';
                    this.drawerOpened = false;
                }

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
