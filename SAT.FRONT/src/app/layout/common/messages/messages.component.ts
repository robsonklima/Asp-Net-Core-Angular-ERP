import
{
    ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy,
    OnInit, TemplateRef, ViewChild, ViewContainerRef, ViewEncapsulation
} from '@angular/core';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatButton } from '@angular/material/button';
import { Notificacao } from 'app/core/types/notificacao.types';
import { Subject } from 'rxjs';

@Component({
    selector: 'messages',
    templateUrl: './messages.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'messages'
})
export class MessagesComponent implements OnInit, OnDestroy
{
    @ViewChild('messagesOrigin') private _messagesOrigin: MatButton;
    @ViewChild('messagesPanel') private _messagesPanel: TemplateRef<any>;

    messages: any[] = [];
    unreadCount: number = 0;
    private _overlayRef: OverlayRef;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor (
        private _changeDetectorRef: ChangeDetectorRef,
        private _overlay: Overlay,
        private _viewContainerRef: ViewContainerRef
    )
    {
    }

    ngOnInit(): void
    {
    }

    private async obterNotificacoes()
    {
        
        this.messages = [];
        this._calculateUnreadCount();
        this._changeDetectorRef.markForCheck();
    }

    openPanel(): void
    {
        // Return if the messages panel or its origin is not defined
        if (!this._messagesPanel || !this._messagesOrigin)
        {
            return;
        }

        // Create the overlay if it doesn't exist
        if (!this._overlayRef)
        {
            this._createOverlay();
        }

        // Attach the portal to the overlay
        this._overlayRef.attach(new TemplatePortal(this._messagesPanel, this._viewContainerRef));
    }

    closePanel(): void
    {
        this._overlayRef.detach();
    }

    markAllAsRead(): void
    {
        // Mark all as read
        this.messages.forEach((notification, index) =>
        {
            this.messages[index].lida = 1;
        });

        this._calculateUnreadCount();
        this._changeDetectorRef.markForCheck();
    }

    async toggleRead(notification: Notificacao)
    {
        notification.lida = +!notification.lida;

        const index = this.messages.findIndex(item => item.codNotificacao === notification.codNotificacao);
    }

    async delete(notification: Notificacao)
    {
        const index = this.messages.findIndex(item => item.codNotificacao === notification.codNotificacao);
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
                .flexibleConnectedTo(this._messagesOrigin._elementRef.nativeElement)
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

        if (this.messages && this.messages.length)
        {
            count = this.messages.filter(notification => !notification.lida).length;
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
