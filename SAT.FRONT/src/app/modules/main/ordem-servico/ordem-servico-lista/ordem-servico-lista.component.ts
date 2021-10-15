import { FileService } from './../../../../core/services/file.service';
import
{
    AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { MatSort } from '@angular/material/sort';
import { OrdemServico, OrdemServicoData, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { MatSidenav } from '@angular/material/sidenav';
import { FileMime } from 'app/core/types/file.types';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
    selector: 'ordem-servico-lista',
    templateUrl: './ordem-servico-lista.component.html',
    styles: [`
        .list-grid-ordem-servico {
            grid-template-columns: 48px 72px 72px 72px 20px 48px 72px 36px auto 56px 56px 100px 50px 36px 145px 24px;
            
            @screen sm {
                grid-template-columns:  48px 72px 92px 92px 36px 36px auto 56px;
            }
        
            @screen md {
                grid-template-columns: 48px 92px 92px 92px 38px 36px auto 58px 58px 58px 58px 58px;
            }
        
            @screen lg {
                grid-template-columns: 48px 72px 72px 72px  20px 48px 72px 36px auto 56px 56px 100px 50px 36px 145px 24px;
            }
        }
    `],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class OrdemServicoListaComponent implements AfterViewInit
{
    @ViewChild('sidenav') sidenav: MatSidenav;
    userSession: UserSession;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) private sort: MatSort;
    dataSourceData: OrdemServicoData;
    selectedItem: OrdemServico | null = null;
    filtro: any;
    isLoading: boolean = false;
    @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
    protected _onDestroy = new Subject<void>();

    constructor (
        private _cdr: ChangeDetectorRef,
        private _ordemServicoService: OrdemServicoService,
        private _userService: UserService,
        private _fileService: FileService
    )
    {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngAfterViewInit(): void
    {
        this.carregarFiltro();
        interval(5 * 60 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._onDestroy)
            )
            .subscribe(() =>
            {
                this.obterOrdensServico();
            });

        this.registrarEmitters();

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
                this._userService.atualizarPropriedade(this.filtro?.nome, "sortActive", this.sort.active);
                this._userService.atualizarPropriedade(this.filtro?.nome, "sortDirection", this.sort.direction);
                this.paginator.pageIndex = 0;
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
            sortActive: this.filtro?.parametros?.sortActive || this.sort.active || 'codOS',
            sortDirection: this.filtro?.parametros?.direction || this.sort.direction || 'desc',
            pageSize: this.filtro?.parametros?.qtdPaginacaoLista ?? this.paginator?.pageSize,
            filter: filter
        };

        const data: OrdemServicoData = await this._ordemServicoService
            .obterPorParametros({
                ...params,
                ...this.filtro?.parametros
            })
            .toPromise();

        this.dataSourceData = data;
        this.isLoading = false;
    }

    private registrarEmitters(): void
    {
        this.sidenav.closedStart.subscribe(() =>
        {
            this.paginator.pageIndex = 0;
            this.carregarFiltro();
            this.obterOrdensServico();
        })
    }

    private carregarFiltro(): void
    {
        this.filtro = this._userService.obterFiltro('ordem-servico');
        if (!this.filtro)
        {
            return;
        }

        // Filtro obrigatorio de filial quando o usuario esta vinculado a uma filial
        if (this.userSession?.usuario?.codFilial)
        {
            this.filtro.parametros.codFiliais = [this.userSession.usuario.codFilial]
        }

        Object.keys(this.filtro?.parametros).forEach((key) =>
        {
            if (this.filtro?.parametros[key] instanceof Array)
            {
                this.filtro.parametros[key] = this.filtro.parametros[key].join()
            };
        });
    }

    public async exportar()
    {
        this.isLoading = true;
        const params: OrdemServicoParameters = {
            sortDirection: 'desc',
            pageSize: 100000,
        };

        window.open(await this._fileService.downloadLink("OrdemServico", FileMime.Excel, {
            ...this.filtro?.parametros,
            ...params
        }));
        this.isLoading = false;
    }

    paginar() 
    {
        this._userService.atualizarPropriedade(this.filtro?.nome, "qtdPaginacaoLista", this.paginator?.pageSize);
        this.obterOrdensServico();
    }

    ngOnDestroy()
    {
        this._onDestroy.next();
        this._onDestroy.complete();
    }

    statusSLADescricao(os: OrdemServico)
    {
        if (os.prazosAtendimento == null)
        {
            return "-";
        }
        else if (os.statusServico?.codStatusServico == 3 && os.prazosAtendimento?.length > 0)
        {
            if (os.dataHoraFechamento < os.prazosAtendimento[os.prazosAtendimento.length - 1]?.dataHoraLimiteAtendimento)
                return "DENTRO";
            return "FORA";
        }
        else if (os.prazosAtendimento?.length > 0)
        {
            var now = moment();
            var limit = moment(os.prazosAtendimento[os.prazosAtendimento.length - 1]?.dataHoraLimiteAtendimento);
            if (now < limit)
                return "DENTRO";
            return "FORA";
        }
        return "-";
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

            if (pecas.length > 0) description = description + "\nPEÇAS: " + pecas.join(", ");
        }

        return description;
    }
}