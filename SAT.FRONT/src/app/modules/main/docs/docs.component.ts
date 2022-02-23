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
                    }
                ]
            },
            {
                id      : 'sistema',
                title   : 'Sistema',
                type    : 'group',
                children: [
                    {
                        id   : 'autenticacao',
                        title: 'Autenticação',
                        type : 'basic',
                        link : '/docs/autenticacao'
                    },
                    {
                        id   : 'ordem-servico',
                        title: 'Ordem de Serviço',
                        type : 'basic',
                        link : '/docs/ordem-servico'
                    },
                    {
                        id   : 'versoes',
                        title: 'Versões',
                        type : 'basic',
                        link : '/docs/inicio/versoes'
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
