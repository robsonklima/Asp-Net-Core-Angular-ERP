import { FileService } from './../../../../core/services/file.service';
import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { MatSort } from '@angular/material/sort';
import { OrdemServico, OrdemServicoData, OrdemServicoFilterEnum, OrdemServicoIncludeEnum, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { MatSidenav } from '@angular/material/sidenav';
import { FileMime } from 'app/core/types/file.types';
import Enumerable from 'linq';
import moment from 'moment';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { StringExtensions } from 'app/core/extensions/string-extensions';

@Component({
    selector: 'ordem-servico-lista',
    templateUrl: './ordem-servico-lista.component.html',
    styles: [`
        .list-grid-ordem-servico {
            grid-template-columns: 42px 65px 80px 80px 20px 48px 50px 30px auto 120px auto 40px 120px 50px 100px 10px;
            
            @screen sm {
                grid-template-columns:  48px 80px 92px 92px 36px 36px 56px auto;
            }
        
            @screen md {
                grid-template-columns: 48px 92px 92px 92px 38px 36px 58px auto 58px 58px 58px 10px;
            }
        
            @screen lg {
                grid-template-columns: 42px 65px 80px 80px 20px 48px 50px 30px 120px auto 40px 120px 50px 100px 30px;
            }
        }
    `],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})

export class OrdemServicoListaComponent extends Filterable implements AfterViewInit, IFilterable
{
    @ViewChild('sidenav') sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild('searchInputControl') searchInputControl: ElementRef;

    @ViewChild(MatSort) sort: MatSort;

    dataSourceData: OrdemServicoData;
    selectedItem: OrdemServico | null = null;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();

    constructor (
        private _cdr: ChangeDetectorRef,
        private _ordemServicoService: OrdemServicoService,
        protected _userService: UserService,
        private _stringExtensions: StringExtensions,
        private _fileService: FileService
    )
    {
        super(_userService, 'ordem-servico')
    }

    ngAfterViewInit(): void
    {
        interval(5 * 60 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._onDestroy)
            )
            .subscribe(() =>
            {
                this.obterOrdensServico();
            });

        this.registerEmitters();

        fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
            map((event: any) =>
            {
                return event.target.value;
            })
            , debounceTime(1000)
            , distinctUntilChanged()
        ).subscribe((text: string) =>
        {
            this.paginator.pageIndex = 0;
            this.obterOrdensServico(text);
        });

        if (this.sort && this.paginator)
        {
            this.sort.disableClear = true;
            this._cdr.markForCheck();

            this.sort.sortChange.subscribe(() =>
            {
                this.onSortChanged();
                this.obterOrdensServico();
            });
        }

        this._cdr.detectChanges();
    }

    async obterOrdensServico(filter: string = '')
    {
        this.isLoading = true;

        const params: OrdemServicoParameters = {
            pageNumber: this.paginator.pageIndex + 1,
            sortActive: this.filter?.parametros?.sortActive || this.sort.active || 'codOS',
            sortDirection: this.filter?.parametros?.direction || this.sort.direction || 'desc',
            pageSize: this.filter?.parametros?.qtdPaginacaoLista ?? this.paginator?.pageSize,
            include: OrdemServicoIncludeEnum.OS_LISTA,
            filter: filter
        };

        if (!this._stringExtensions.isEmptyOrWhiteSpace(filter))
            params.filterType = OrdemServicoFilterEnum.FILTER_GENERIC_TEXT;

        const data: OrdemServicoData = await this._ordemServicoService
            .obterPorParametros({
                ...params,
                ...this.filter?.parametros
            })
            .toPromise();

        this.dataSourceData = data;
        this.isLoading = false;
    }

    registerEmitters(): void
    {
        this.sidenav.closedStart.subscribe(() =>
        {
            this.onSidenavClosed();
            this.obterOrdensServico();
        })
    }

    loadFilter(): void
    {
        super.loadFilter();

        // Filtro obrigatorio de filial quando o usuario esta vinculado a uma filial
        if (this.userSession?.usuario?.codFilial && this.filter)
            this.filter.parametros.codFiliais = this.userSession?.usuario?.codFilial;
    }

    public async exportar()
    {
        this.isLoading = true;

        const params: OrdemServicoParameters = {
            sortDirection: 'desc',
            pageSize: 100000,
        };

        window.open(await this._fileService.downloadLink("OrdemServico", FileMime.Excel, {
            ...this.filter?.parametros,
            ...params
        }));

        this.isLoading = false;
    }

    paginar()
    {
        this.onPaginationChanged();
        this.obterOrdensServico();
    }

    ngOnDestroy()
    {
        this._onDestroy.next();
        this._onDestroy.complete();
    }

    tooltipSLA(os: OrdemServico)
    {
        if (os.equipamentoContrato == null || os.equipamentoContrato?.acordoNivelServico == null) return null;

        return os.equipamentoContrato?.acordoNivelServico?.nomeSLA + " - " +
            os.equipamentoContrato?.acordoNivelServico?.descSLA;
    }

    statusSLADescricao(os: OrdemServico)
    {
        if (os.prazosAtendimento == null)
        {
            return "---";
        }
        else if (os.statusServico?.codStatusServico == 3 && os.prazosAtendimento?.length > 0)
        {
            var solucao = Enumerable.from(os.relatoriosAtendimento).orderBy(i => i.codRAT).firstOrDefault()?.dataHoraSolucao || os.dataHoraFechamento;
            if (solucao < os.prazosAtendimento[0]?.dataHoraLimiteAtendimento)
                return "DENTRO";
            return "FORA";
        }
        else if (os.prazosAtendimento?.length > 0)
        {
            var now = moment();
            var limit = moment(os.prazosAtendimento[0]?.dataHoraLimiteAtendimento);
            if (now < limit)
                return "DENTRO";
            return "FORA";
        }
        return "---";
    }

    statusServicoDescricao(os: OrdemServico)
    {
        var description = os.statusServico?.nomeStatusServico;

        if (os.statusServico?.codStatusServico == 7 || os.statusServico?.codStatusServico == 10)
        {
            var pecas = Enumerable.from(os.relatoriosAtendimento)
                .selectMany(rat => Enumerable.from(rat.relatorioAtendimentoDetalhes)
                    .selectMany(d => Enumerable.from(d.relatorioAtendimentoDetalhePecas)
                        .select(dp => dp.peca?.codMagnus))).toArray();

            if (pecas?.length > 0) description = description + "\nPEÇAS: " + pecas.join(", ");
        }

        return description;
    }

    tecnicoDescricao(os: OrdemServico)
    {
        var description = os.tecnico?.nome;
        description += '\n' + 'TRANSFERIDO EM: ';
        description += os.dataHoraTransf ? moment(os.dataHoraTransf).format('DD/MM HH:mm') + '\n' : 'NÃO DISPONÍVEL\n';
        description += 'VISUALIZADO EM: ';
        description += os.dataHoraOSMobileLida ? moment(os.dataHoraOSMobileLida).format('DD/MM HH:mm') : 'NÃO VISUALIZADO';
        return description;
    }

    alternarDetalhes(id: number): void
    {
        this.isLoading = true;

        if (this.selectedItem && this.selectedItem.codOS === id)
        {
            this.isLoading = false;
            this.fecharDetalhes();
            return;
        }

        this._ordemServicoService.obterPorCodigo(id)
            .subscribe((item) =>
            {
                this.selectedItem = item;
                this.isLoading = false;
                this._cdr.markForCheck();
            });
    }

    fecharDetalhes(): void
    {
        this.selectedItem = null;
    }
}