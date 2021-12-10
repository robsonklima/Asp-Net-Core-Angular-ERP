import { 
    ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy,
    OnInit, TemplateRef, ViewChild, ViewContainerRef, ViewEncapsulation
} from '@angular/core';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatButton } from '@angular/material/button';
import { interval, Subject } from 'rxjs';
import { Notificacao } from 'app/layout/common/notifications/notifications.types';
import { NotificationsService } from 'app/layout/common/notifications/notifications.service';
import moment from 'moment';
import { startWith } from 'rxjs/operators';

@Component({
    selector: 'notifications',
    templateUrl: './notifications.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'notifications'
})
export class NotificationsComponent implements OnInit, OnDestroy {
    @ViewChild('notificationsOrigin') private _notificationsOrigin: MatButton;
    @ViewChild('notificationsPanel') private _notificationsPanel: TemplateRef<any>;

    notifications: Notificacao[];
    unreadCount: number = 0;
    private _overlayRef: OverlayRef;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _notificationsService: NotificationsService,
        private _overlay: Overlay,
        private _viewContainerRef: ViewContainerRef
    ) { }

    ngOnInit(): void {
        interval(5 * 60 * 1000)
            .pipe(startWith(0))
            .subscribe(() => {
                this.obterNotificacoes();
                this._calculateUnreadCount();
                this._changeDetectorRef.markForCheck();
            });
    }

    private obterNotificacoes(): any {
        this.notifications = [
            {
                codNotificacao: 1,
                icone: 'heroicons_solid:star',
                titulo: 'Tarefa Concluída',
                descricao: 'Sua tarefa de suporte <strong>3881</strong> foi finalizada',
                dataHoraCad: moment().subtract(25, 'minutes').toISOString(), // 25 minutes ago
                lida: 0,
                indAtivo: 1,
            },
            {
                codNotificacao: 2,
                icone: 'heroicons_solid:clipboard',
                titulo: 'Chamado Aberto',
                descricao: '<strong>João</strong> abriu um chamado para o contrato <em>Banco do Brasil Acompanhamento</em>',
                dataHoraCad: moment().subtract(50, 'minutes').toISOString(), // 50 minutes ago
                lida: 0,
                link: '/ordem-servico/lista',
                useRouter: 1,
                indAtivo: 1
            }
        ];
    }

    openPanel(): void {
        // Return if the notifications panel or its origin is not defined
        if (!this._notificationsPanel || !this._notificationsOrigin) {
            return;
        }

        // Create the overlay if it doesn't exist
        if (!this._overlayRef) {
            this._createOverlay();
        }

        // Attach the portal to the overlay
        this._overlayRef.attach(new TemplatePortal(this._notificationsPanel, this._viewContainerRef));
    }

    closePanel(): void {
        this._overlayRef.detach();
    }

    markAllAsRead(): void {
        // Mark all as read
        this.notifications.forEach((notification, index) => {
            this.notifications[index].lida = 1;
        });

        this._calculateUnreadCount();
        this._changeDetectorRef.markForCheck();
    }

    toggleRead(notification: Notificacao): void {
        // Toggle the read status
        notification.lida = +!notification.lida;

        const index = this.notifications.findIndex(item => item.codNotificacao === notification.codNotificacao);

        this.notifications[index] = notification;
        this._calculateUnreadCount();
        this._changeDetectorRef.markForCheck();
    }

    delete(notification: Notificacao): void {
        // Delete the notification
        this._notificationsService.delete(notification.codNotificacao).subscribe();

        const index = this.notifications.findIndex(item => item.codNotificacao === notification.codNotificacao);
        this.notifications.splice(index, 1);

        this._calculateUnreadCount();
        this._changeDetectorRef.markForCheck();
    }

    trackByFn(index: number, item: any): any {
        return item.id || index;
    }

    private _createOverlay(): void {
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
        this._overlayRef.backdropClick().subscribe(() => {
            this._overlayRef.detach();
        });
    }

    private _calculateUnreadCount(): void {
        let count = 0;

        if (this.notifications && this.notifications.length) {
            count = this.notifications.filter(notification => !notification.lida).length;
        }

        this.unreadCount = count;
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();

        if (this._overlayRef) {
            this._overlayRef.dispose();
        }
    }
}
