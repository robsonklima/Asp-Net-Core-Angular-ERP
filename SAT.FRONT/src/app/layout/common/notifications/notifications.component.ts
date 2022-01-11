import
{
    ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy,
    OnInit, TemplateRef, ViewChild, ViewContainerRef, ViewEncapsulation
} from '@angular/core';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatButton } from '@angular/material/button';
import { interval, Subject } from 'rxjs';
import { startWith } from 'rxjs/operators';
import { Notificacao } from 'app/core/types/notificacao.types';
import { NotificacaoService } from 'app/core/services/notificacao.service';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
    selector: 'notifications',
    templateUrl: './notifications.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'notifications'
})
export class NotificationsComponent implements OnInit, OnDestroy
{
    @ViewChild('notificationsOrigin') private _notificationsOrigin: MatButton;
    @ViewChild('notificationsPanel') private _notificationsPanel: TemplateRef<any>;

    notifications: Notificacao[] = [];
    unreadCount: number = 0;
    private _overlayRef: OverlayRef;
    private userSession: UserSession;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor (
        private _changeDetectorRef: ChangeDetectorRef,
        private _notificacaoService: NotificacaoService,
        private _overlay: Overlay,
        private _userService: UserService,
        private _viewContainerRef: ViewContainerRef
    )
    {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void
    {
        interval(5 * 60 * 1000)
            .pipe(startWith(0))
            .subscribe(() => {
                this.obterNotificacoes();
            });
    }

    private async obterNotificacoes()
    {
        const data = this._notificacaoService.obterPorParametros({ codUsuario: this.userSession?.usuario?.codUsuario }).toPromise();
        this.notifications = (await data).items;
        this._calculateUnreadCount();
        this._changeDetectorRef.markForCheck();
    }

    openPanel(): void
    {
        // Return if the notifications panel or its origin is not defined
        if (!this._notificationsPanel || !this._notificationsOrigin)
        {
            return;
        }

        // Create the overlay if it doesn't exist
        if (!this._overlayRef)
        {
            this._createOverlay();
        }

        // Attach the portal to the overlay
        this._overlayRef.attach(new TemplatePortal(this._notificationsPanel, this._viewContainerRef));
    }

    closePanel(): void
    {
        this._overlayRef.detach();
    }

    markAllAsRead(): void
    {
        // Mark all as read
        this.notifications.forEach((notification, index) =>
        {
            this.notifications[index].lida = 1;
        });

        this._calculateUnreadCount();
        this._changeDetectorRef.markForCheck();
    }

    async toggleRead(notification: Notificacao)
    {
        // Toggle the read status
        notification.lida = +!notification.lida;

        const index = this.notifications.findIndex(item => item.codNotificacao === notification.codNotificacao);

        this._notificacaoService.atualizar(notification).subscribe(() =>
        {
            this.notifications[index] = notification;
            this._calculateUnreadCount();
            this._changeDetectorRef.markForCheck();
        });
    }

    async delete(notification: Notificacao)
    {
        const index = this.notifications.findIndex(item => item.codNotificacao === notification.codNotificacao);

        this._notificacaoService.deletar(notification.codNotificacao).subscribe(() =>
        {
            this.notifications.splice(index, 1);
            this._calculateUnreadCount();
            this._changeDetectorRef.markForCheck();
        });
    }

    trackByFn(index: number, item: any): any
    {
        return item.id || index;
    }

    private _createOverlay(): void
    {
        // Create the overlay
        this._overlayRef = this._overlay.create({
            hasBackdrop: true,
            backdropClass: 'fuse-backdrop-on-mobile',
            scrollStrategy: this._overlay.scrollStrategies.block(),
            positionStrategy: this._overlay.position()
                .flexibleConnectedTo(this._notificationsOrigin._elementRef.nativeElement)
                .withLockedPosition()
                .withPush(true)
                .withPositions([
                    {
                        originX: 'start',
                        originY: 'bottom',
                        overlayX: 'start',
                        overlayY: 'top'
                    },
                    {
                        originX: 'start',
                        originY: 'top',
                        overlayX: 'start',
                        overlayY: 'bottom'
                    },
                    {
                        originX: 'end',
                        originY: 'bottom',
                        overlayX: 'end',
                        overlayY: 'top'
                    },
                    {
                        originX: 'end',
                        originY: 'top',
                        overlayX: 'end',
                        overlayY: 'bottom'
                    }
                ])
        });

        // Detach the overlay from the portal on backdrop click
        this._overlayRef.backdropClick().subscribe(() =>
        {
            this._overlayRef.detach();
        });
    }

    private _calculateUnreadCount(): void
    {
        let count = 0;

        if (this.notifications && this.notifications.length)
        {
            count = this.notifications.filter(notification => !notification.lida).length;
        }

        this.unreadCount = count;
    }

    ngOnDestroy(): void
    {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();

        if (this._overlayRef)
        {
            this._overlayRef.dispose();
        }
    }
}
