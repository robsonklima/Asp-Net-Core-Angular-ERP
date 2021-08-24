import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { UsuarioData, UsuarioParameters } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-usuario-lista',
  templateUrl: './usuario-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-u {
          grid-template-columns: 142px auto 25% 25% 42px;
          
          @screen sm {
              grid-template-columns: 142px auto 25% 25% 42px;
          }
      
          @screen md {
              grid-template-columns: 142px auto 25% 25% 42px;
          }
      
          @screen lg {
              grid-template-columns: 142px auto 25% 25% 42px;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class UsuarioListaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: UsuarioData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _usuarioService: UserService,
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

  async obterDados() {
    this.isLoading = true;
    
    const params: UsuarioParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'nomeUsuario',
      sortDirection: this.sort?.direction || 'asc',
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    };

    const data = await this._userService
      .obterPorParametros(params)
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}
