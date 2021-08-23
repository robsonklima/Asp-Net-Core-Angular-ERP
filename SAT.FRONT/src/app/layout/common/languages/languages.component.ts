import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { elementAt, take } from 'rxjs/operators';
import { AvailableLangs, TranslocoService } from '@ngneat/transloco';
import { FuseNavigationService, FuseVerticalNavigationComponent } from '@fuse/components/navigation';
import { cloneDeep } from 'lodash';

@Component({
    selector: 'languages',
    templateUrl: './languages.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'languages'
})
export class LanguagesComponent implements OnInit, OnDestroy {
    availableLangs: AvailableLangs;
    activeLang: string;
    flagCodes: any;

    constructor(
        private _fuseNavigationService: FuseNavigationService,
        private _translocoService: TranslocoService,
    ) { }

    ngOnInit(): void {
        this.availableLangs = this._translocoService.getAvailableLangs();
        this._translocoService.langChanges$.subscribe((activeLang) => {
            this.activeLang = activeLang;
            this._updateNavigation(activeLang);
        });

        this.flagCodes = {
            'en': 'us',
            'pt': 'br'
        };
    }

    ngOnDestroy(): void {
    }

    setActiveLang(lang: string): void {
        this._translocoService.setActiveLang(lang);
    }

    trackByFn(index: number, item: any): any {
        return item.id || index;
    }

    private _updateNavigation(lang: string): void {
        const navComponent = this._fuseNavigationService.getComponent<FuseVerticalNavigationComponent>('mainNavigation');
        const navigation = navComponent.navigation;

        if (!navComponent) {
            return null;
        }

        navComponent.navigation?.forEach(el => {
            let nav = this._fuseNavigationService.getItem(el.id, navigation);

            if (nav) {
                this._translocoService.selectTranslate(el.id).pipe(take(1))
                    .subscribe((translation) => {
                        nav.title = translation;
                        navComponent.refresh();
                    });

                el.children?.forEach(ch => {
                    let chNav = this._fuseNavigationService
                        .getItem(ch.id, navigation);

                    if (chNav) {
                        this._translocoService.selectTranslate(ch.id).pipe(take(1))
                            .subscribe((translation) => {
                                chNav.title = translation;
                                navComponent.refresh();
                            });
                    }
                });
            }
        });
    }
}
