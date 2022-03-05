import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PlantaoTecnicoClienteService } from 'app/core/services/plantao-tecnico-cliente.service';
import { PlantaoTecnicoRegiaoService } from 'app/core/services/plantao-tecnico-regiao.service';
import { PlantaoTecnicoService } from 'app/core/services/plantao-tecnico.service';
import { PlantaoTecnico, PlantaoTecnicoData } from 'app/core/types/plantao-tecnico.types';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { TecnicoPlantaoClientesComponent } from '../tecnico-plantao-clientes/tecnico-plantao-clientes.component';
import { TecnicoPlantaoInformacoesComponent } from '../tecnico-plantao-informacoes/tecnico-plantao-informacoes.component';
import { TecnicoPlantaoRegioesComponent } from '../tecnico-plantao-regioes/tecnico-plantao-regioes.component';

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
    private _plantaoTecnicoRegiaoService: PlantaoTecnicoRegiaoService,
    private _plantaoTecnicoClienteService: PlantaoTecnicoClienteService,
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
    this._dialog.open(TecnicoPlantaoRegioesComponent, {
      data: {
        plantaoTecnico: plantaoTecnico
      }
    });
  }

  onClientesDoTecnico(plantaoTecnico: PlantaoTecnico) {
    this._dialog.open(TecnicoPlantaoClientesComponent, {
      data: {
        plantaoTecnico: plantaoTecnico
      }
    });
  }

  async onRemoverPlantao(plantaoTecnico: PlantaoTecnico) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: `Deseja remover o plantão do técnico ${plantaoTecnico.tecnico?.nome}?`,
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) =>
		{
			if (confirmacao)
			{
        this.isLoading = true;
        
        for (const plantaoRegiao of plantaoTecnico.plantaoRegioes) {
          await this._plantaoTecnicoRegiaoService.deletar(plantaoRegiao.codPlantaoTecnicoRegiao).toPromise();
          console.log('deletando regiao');
          
        }
        
        for (const plantaoCliente of plantaoTecnico.plantaoClientes) {
          await this._plantaoTecnicoClienteService.deletar(plantaoCliente.codPlantaoTecnicoCliente).toPromise();
          console.log('deletando cliente');
        }

        await this._plantaoTecnicoService.deletar(plantaoTecnico.codPlantaoTecnico).toPromise();
        this._snack.exibirToast(`Plantão para o técnico ${plantaoTecnico.tecnico?.nome} removido`, 'success');
        this.isLoading = false;
        this.obterPlantoesTecnico();
      }
    });
  }

  paginar()
  {
    this.obterPlantoesTecnico();
  }
}
