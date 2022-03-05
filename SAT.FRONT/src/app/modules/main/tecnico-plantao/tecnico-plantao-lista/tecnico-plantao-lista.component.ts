import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PlantaoTecnicoService } from 'app/core/services/plantao-tecnico.service';
import { PlantaoTecnico, PlantaoTecnicoData } from 'app/core/types/plantao-tecnico.types';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { TecnicoPlantaoInformacoesComponent } from '../tecnico-plantao-informacoes/tecnico-plantao-informacoes.component';

@Component({
  selector: 'app-tecnico-plantao-lista',
  templateUrl: './tecnico-plantao-lista.component.html',
  styles: [
    `
      .list-grid-plantao-tecnico {
          grid-template-columns: 72px auto 240px 160px 120px 260px 50px 220px;
          
          @screen sm {
              grid-template-columns: 72px auto 240px 160px 120px 50px 220px;
          }
      
          @screen md {
              grid-template-columns: 72px auto 240px 160px 120px 50px 220px;
          }
      
          @screen lg {
              grid-template-columns: 72px auto 240px 160px 120px 50px 220px;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class TecnicoPlantaoListaComponent implements AfterViewInit {
  public plantoesTecnico: PlantaoTecnico[] = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: PlantaoTecnicoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;
  
  constructor(
    private _cdr: ChangeDetectorRef,
    private _dialog: MatDialog,
    private _plantaoTecnicoService: PlantaoTecnicoService,
    private _snack: CustomSnackbarService
  ) { }
 
  async ngAfterViewInit()
  {
    this.obterPlantoesTecnico();
    this.registrarEmitters();
    this._cdr.detectChanges();
  }

  registrarEmitters()
  {
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
        this.obterPlantoesTecnico();
      });

      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterPlantoesTecnico();
      });
    }
  }

  private async obterPlantoesTecnico()
  {
    this.isLoading = true;
    this._plantaoTecnicoService.obterPorParametros({
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active || 'DataPlantao',
      sortDirection: this.sort.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    }).subscribe((data: PlantaoTecnicoData) => {
      this.dataSourceData = data;
      console.log(data);
      
      this.isLoading = false;
      this._cdr.detectChanges();
    }, e => {
      this._snack.exibirToast(e.error.message, 'error');
      this.isLoading = false;
    });
  }

  onInformacoesDoTecnico(plantaoTecnico: PlantaoTecnico) {
    this._dialog.open(TecnicoPlantaoInformacoesComponent, {
      data: {
        plantaoTecnico: plantaoTecnico
      }
    });
  }

  onRegioesDoTecnico(plantaoTecnico: PlantaoTecnico) {

  }

  onClientesDoTecnico(plantaoTecnico: PlantaoTecnico) {

  }

  onRemoverPlantao(plantaoTecnico: PlantaoTecnico) {

  }

  paginar()
  {
    this.obterPlantoesTecnico();
  }
}
