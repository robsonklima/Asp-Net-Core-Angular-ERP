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
import { PerfilEnum } from 'app/core/types/perfil.types';

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
    userSession: UserSession;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseMediaWatcherService: FuseMediaWatcherService,
        private _userService: UserService
    )
    {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void
    {
        this.obterMenus();

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

    private obterMenus(): void {
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
                    }
                ]
            }
        ];

        if (this.userSession.usuario.codPerfil === PerfilEnum.ADM_DO_SISTEMA) {
            this.menuData.push({
                id      : 'desenvolvimento',
                title   : 'Desenvolvimento',
                type    : 'group',
                children: [
                    {
                        id   : 'arquitetura',
                        title: 'Arquitetura',
                        type : 'basic',
                        link : '/docs/arquitetura'
                    },
                    {
                        id   : 'publicacao',
                        title: 'Publicação',
                        type : 'basic',
                        link : '/docs/publicacao'
                    }
                ]
            });
        }
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
