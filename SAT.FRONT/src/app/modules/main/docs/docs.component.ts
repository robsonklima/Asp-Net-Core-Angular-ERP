import {
    ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy,
    OnInit, ViewChild, ViewEncapsulation
} from '@angular/core';
import { MatDrawer } from '@angular/material/sidenav';
import { FuseNavigationItem } from '@fuse/components/navigation';
import { takeUntil } from 'rxjs/operators';
import { FuseMediaWatcherService } from '@fuse/services/media-watcher';
import { Subject } from 'rxjs';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
    selector: 'app-docs',
    templateUrl: './docs.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DocsComponent implements OnInit, OnDestroy {
    @ViewChild('matDrawer', { static: true }) matDrawer: MatDrawer;
    drawerMode: 'side' | 'over';
    drawerOpened: boolean;
    menuData: FuseNavigationItem[];
    userSession: UserSession;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseMediaWatcherService: FuseMediaWatcherService,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {
        this.obterMenus();

        this._fuseMediaWatcherService.onMediaChange$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({ matchingAliases }) => {

                // Set the drawerMode and drawerOpened
                if (matchingAliases.includes('md')) {
                    this.drawerMode = 'side';
                    this.drawerOpened = true;
                }
                else {
                    this.drawerMode = 'over';
                    this.drawerOpened = false;
                }

                this._changeDetectorRef.markForCheck();
            });
    }

    private obterMenus(): void {
        this.menuData = [
            {
                id: 'inicio',
                title: 'Início',
                type: 'group',
                children: [
                    {
                        id: 'introducao',
                        title: 'Introdução',
                        type: 'basic',
                        link: '/docs/inicio/introducao'
                    },
                    {
                        id: 'versoes',
                        title: 'Versões',
                        type: 'basic',
                        link: '/docs/inicio/versoes'
                    }
                ]
            },
            {
                id: 'sistema',
                title: 'Sistema',
                type: 'group',
                children: [
                    {
                        id: 'autenticacao',
                        title: 'Autenticação',
                        type: 'basic',
                        link: '/docs/autenticacao'
                    },
                    {
                        id: 'ordem-servico',
                        title: 'Ordem de Serviço',
                        type: 'basic',
                        link: '/docs/ordem-servico'
                    },
                    {
                        id: 'app-tecnicos',
                        title: 'App Técnicos',
                        type: 'basic',
                        link: '/docs/app-tecnicos'
                    }
                ]
            },
            {
                id: 'desenvolvedor',
                title: 'Desenvolvedor',
                type: 'group',
                children: [
                    {
                        id: 'build',
                        title: 'Build',
                        type: 'basic',
                        link: '/docs/build'
                    }
                ]
            },
            {
                id: 'documentacao',
                title: 'Documentação',
                type: 'group',
                children: [
                    {
                        id: 'docs-intro',
                        title: 'Introdução',
                        type: 'basic',
                        link: '/docs/documentacao/intro'
                    },
                    {
                        id: 'docs-termo-abertura',
                        title: 'Termo de Abertura',
                        type: 'basic',
                        link: '/docs/documentacao/termo-abertura/'
                    },
                    {
                        id: 'docs-escopo',
                        title: 'Escopo',
                        type: 'basic',
                        link: '/docs/documentacao/escopo'
                    },
                    {
                        id: 'docs-custo',
                        title: 'Custo',
                        type: 'basic',
                        link: '/docs/documentacao/custo'
                    },
                    {
                        id: 'docs-tempo',
                        title: 'Tempo',
                        type: 'basic',
                        link: '/docs/documentacao/tempo'
                    }
                ]
            }
        ];
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
