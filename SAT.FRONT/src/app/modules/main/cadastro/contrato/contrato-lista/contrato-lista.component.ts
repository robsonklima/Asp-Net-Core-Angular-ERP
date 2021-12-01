import { ContratoParameters } from './../../../../../core/types/contrato.types';
import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ContratoService } from 'app/core/services/contrato.service';
import { ContratoData, Contrato } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { FileMime } from 'app/core/types/file.types';
import { FileService } from 'app/core/services/file.service';

@Component({
    selector: 'app-contrato-lista',
    templateUrl: './contrato-lista.component.html',
    styles: [`
        .list-grid-contrato {
            grid-template-columns: 48px 250px 150px auto 160px 30px;
            
            /* @screen sm {
               grid-template-columns: 48px 250px 150px auto 120px 30px;
            }
        
            @screen md{
                grid-template-columns: 48px 250px 150px auto 120px 30px;
            }
        
            @screen lg {
                grid-template-columns: 48px 250px 150px auto 120px 30px;
            }  */
        }
    `],
})
export class ContratoListaComponent implements AfterViewInit {
    @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
    userSession: UserSession;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) private sort: MatSort;
    dataSourceData: ContratoData;
    selectedItem: Contrato | null = null;
    filtro: any;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _cdr: ChangeDetectorRef,
        private _contratoService: ContratoService,
        private _userService: UserService,
        private _fileService: FileService
    ) {

        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {
    }

    ngAfterViewInit(): void {
        this.obterContratos();        

        fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
            map((event: any) => {
                return event.target.value;
            })
            , debounceTime(1000)
            , distinctUntilChanged()
        ).subscribe((text: string) => {
            this.paginator.pageIndex = 0;
            this.obterContratos(text);
        });

        if (this.sort && this.paginator) {

            this.sort.disableClear = true;
            this._cdr.markForCheck();

            this.sort.sortChange.subscribe(() => {
                this.paginator.pageIndex = 0;
                this.obterContratos();
            });
        }

        this._cdr.detectChanges();
    }

    public async obterContratos(filter: string = '') {
        this.isLoading = true;

        const params: ContratoParameters = {
            pageNumber: this.paginator.pageIndex + 1,
            sortActive: this.sort.active || 'codContrato',
            sortDirection: this.sort.direction || 'desc',
            pageSize: this.paginator?.pageSize,
            filter: filter
        };

        const data: ContratoData = await this._contratoService
            .obterPorParametros({
                ...params,
                ...this.filtro?.parametros
            })
            .toPromise();

        this.dataSourceData = data;
        this.isLoading = false;
    }
    

    public async exportar() {
        this.isLoading = true;
        const params: ContratoParameters = {
            sortDirection: 'desc',
            pageSize: 100000,
        };

        window.open(await this._fileService.downloadLink("Contrato", FileMime.Excel, {
            ...this.filtro?.parametros,
            ...params
        }));
        this.isLoading = false;
    }

    paginar() {
        this.obterContratos();
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
