import { OverlayRef } from '@angular/cdk/overlay';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { VersaoService } from 'app/core/services/versao.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { interval, Subject } from 'rxjs';
import { startWith } from 'rxjs/operators';
import packageInfo from '../../../../../package.json';

@Component({
    selector: 'version',
    templateUrl: './version.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'version'
})
export class VersionComponent implements OnInit, OnDestroy {
    versao: string = packageInfo.version;

    private _overlayRef: OverlayRef;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _versaoService: VersaoService,
        private _cdr: ChangeDetectorRef,
        private _dialog: MatDialog,
    ) {}

    async ngOnInit()
    {
        interval(30 * 60 * 1000)
            .pipe(startWith(0))
            .subscribe(() => {
                 this._versaoService.obterPorParametros({ 
                    sortActive: 'codSatVersao',
                    sortDirection: 'desc',
                    pageSize: 1
                }).subscribe((versao) => {
                    if (versao.items.shift().nome !== this.versao) {
                        this.atualizar();
                        this._cdr.markForCheck();
                    }
                });
            });
    }

    atualizar() {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Nova Versão',
				message: 'Uma nova versão do sistema foi encontrada! Deseja atualizar seu aplicativo agora?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
		{
			if (confirmacao)
			{
                window.location.reload();
            }
        });
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
