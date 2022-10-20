import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, TemplateRef, ViewChild, ViewContainerRef, ViewEncapsulation } from '@angular/core';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatButton } from '@angular/material/button';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { MensagemService } from 'app/core/services/mensagem.service';
import { Mensagem, MensagemData } from 'app/core/types/mensagem.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { MatDialog } from '@angular/material/dialog';
import { MessageFormDialogComponent } from './message-form-dialog/message-form-dialog.component';

@Component({
    selector: 'messages',
    templateUrl: './messages.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'messages'
})
export class MessagesComponent implements OnInit, OnDestroy {
    @ViewChild('messagesOrigin') private _messagesOrigin: MatButton;
    @ViewChild('messagesPanel') private _messagesPanel: TemplateRef<any>;

    messages: Mensagem[];
    unreadCount: number = 0;
    userSession: UserSession;
    private _overlayRef: OverlayRef;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _mensagemService: MensagemService,
        private _userService: UserService,
        private _overlay: Overlay,
        private _viewContainerRef: ViewContainerRef,
        private _dialog: MatDialog
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {
        interval(1 * 60 * 1000)
            .pipe(startWith(0))
            .subscribe(() => {
                this._mensagemService
                    .obterPorParametros({ 
                        codUsuarioDestinatario: this.userSession.usuario.codUsuario,
                        sortActive: 'codMsg',
                        sortDirection: 'desc',
                        pageSize: 100
                    })
                    .pipe(takeUntil(this._unsubscribeAll))
                    .subscribe((messages: MensagemData) => {
                        this.messages = messages.items;
                        this._calculateUnreadCount();
                        this._changeDetectorRef.markForCheck();        
                    });
            });
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();

        if (this._overlayRef)
        {
            this._overlayRef.dispose();
        }
    }

    openPanel(): void {
        if (!this._messagesPanel || !this._messagesOrigin)
        {
            return;
        }

        if (!this._overlayRef)
        {
            this._createOverlay();
        }

        this._overlayRef.attach(new TemplatePortal(this._messagesPanel, this._viewContainerRef));
    }

    closePanel(): void {
        this._overlayRef.detach();
    }

    markAllAsRead(): void {
        for (const msg of this.messages) {
            msg.indLeitura = 1;
            msg.dataHoraLeitura = moment().format('YYYY-MM-DD HH:mm:ss');

            this._mensagemService.atualizar(msg);
        }

        this._calculateUnreadCount();
        this._changeDetectorRef.markForCheck();
    }

    toggleRead(message: Mensagem): void {
        message.indLeitura = +!message.indLeitura;
        message.dataHoraLeitura = moment().format('YYYY-MM-DD HH:mm:ss');
        
        const index = this.messages.findIndex(item => item.codMsg === message.codMsg);
        
        this._mensagemService.atualizar(message).subscribe(() => {
            this.messages[index] = message;
            this._calculateUnreadCount();
            this._changeDetectorRef.markForCheck();
        });
    }

    delete(message: Mensagem): void {
        const index = this.messages.findIndex(item => item.codMsg === message.codMsg);

        this._mensagemService.deletar(message.codMsg).subscribe(() =>
        {
            this.messages.splice(index, 1);
            this._calculateUnreadCount();
            this._changeDetectorRef.markForCheck();
        });
    }

    trackByFn(index: number, item: any): any {
        return item.id || index;
    }
    
    private _createOverlay(): void {
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

        this._overlayRef.backdropClick().subscribe(() => {
            this._overlayRef.detach();
        });
    }

    onSendMessage(mensagem: Mensagem = null) {
        this._dialog.open(MessageFormDialogComponent, {
            width: '600px',
            data: {
                mensagem: mensagem
            }
        });
    }
    
    private _calculateUnreadCount(): void {
        let count = 0;

        if (this.messages && this.messages.length)
        {
            count = this.messages.filter(message => !message.indLeitura).length;
        }

        this.unreadCount = count;
    }
}
