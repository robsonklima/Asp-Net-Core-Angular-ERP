import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { GrupoEquipamentoService } from 'app/core/services/grupo-equipamento.service';
import { GrupoEquipamento, GrupoEquipamentoData } from 'app/core/types/grupo-equipamento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-grupo-equipamento-lista',
  templateUrl: './grupo-equipamento-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-ge {
          grid-template-columns: 72px 156px auto 256px 72px;
          
          @screen sm {
              grid-template-columns: 72px auto 72px;
          }
      
          @screen md {
              grid-template-columns: 72px 156px auto 256px 72px;
          }
      
          @screen lg {
              grid-template-columns: 72px 156px auto 256px 72px;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class GrupoEquipamentoListaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: GrupoEquipamentoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  selectedItem: GrupoEquipamento | null = null;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _grupoEquipamentoService: GrupoEquipamentoService,
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngAfterViewInit(): void {
    this.obterDados();

    if (this.sort && this.paginator) {
      fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
        map((event: any) => {
          return event.target.value;
        })
        , debounceTime(700)
        , distinctUntilChanged()
      ).subscribe((text: string) => {
        this.paginator.pageIndex = 0;
        this.searchInputControl.nativeElement.val = text;
        this.obterDados();
      });

      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  obterDados(): void {
    this.isLoading = true;
    this._grupoEquipamentoService.obterPorParametros({
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'nomeGrupoEquip',
      sortDirection: this.sort?.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    }).subscribe((data: GrupoEquipamentoData) => {
      this.dataSourceData = data;
      this.isLoading = false;
      this._cdr.detectChanges();
    });
  }

  paginar() {
    this.obterDados();
  }
}
