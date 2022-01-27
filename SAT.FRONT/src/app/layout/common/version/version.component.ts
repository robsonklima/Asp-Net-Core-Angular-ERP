import { OverlayRef } from '@angular/cdk/overlay';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { VersaoService } from 'app/core/services/versao.service';
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
    ultimaVersao: string;

    private _overlayRef: OverlayRef;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _versaoService: VersaoService,
        private _cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void
    {
        interval(15 * 60 * 1000)
            .pipe(startWith(0))
            .subscribe(() => {
                this.obterUltimaVersao();
            });
    }

    private async obterUltimaVersao()
    {
        const data = await this._versaoService.obterPorParametros({ 
            sortActive: 'codSatVersao',
            sortDirection: 'desc',
            pageSize: 1
        }).toPromise();

        this.ultimaVersao = data.items.shift().nome;
        this._cdr.markForCheck();
    }

    atualizar() {
        window.location.reload();
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
