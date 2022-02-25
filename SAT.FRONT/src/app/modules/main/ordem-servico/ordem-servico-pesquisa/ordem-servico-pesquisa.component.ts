import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServicoData } from 'app/core/types/ordem-servico.types';
import _ from 'lodash';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-ordem-servico-pesquisa',
  templateUrl: './ordem-servico-pesquisa.component.html',
  styles: [`
    .list-grid-pesquisa {
      grid-template-columns: 72px 90px 158px 158px auto 218px;
      
      @screen sm {
          grid-template-columns: 72px 90px 158px 158px auto 218px;
      }

      @screen md {
          grid-template-columns: 72px 90px 158px 158px auto 218px;
      }

      @screen lg {
          grid-template-columns: 72px 90px 158px 158px auto 218px;
      }
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OrdemServicoPesquisaComponent implements OnInit, OnDestroy {
  @ViewChild(MatSort) private sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  form: FormGroup;
  dataSourceData: OrdemServicoData;
  isLoading: boolean = false;
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _cdr: ChangeDetectorRef,
    private _osSvc: OrdemServicoService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      codOS: [''],
      numSerie: [''],
      numOSCliente: [''],
      numOSQuarteirizada: [''],
      
      numRAT: [''],
      numAgencia: ['']
    });

    if (this.sort && this.paginator) {
      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.pesquisar();
      });
    }

    this._cdr.detectChanges();
  }

  pesquisar() {
    const form = this.form.getRawValue();
    const isEmpty = Object.values(form).every(x => x === null || x === '');
    if (isEmpty) {
      this._snack.exibirToast('Favor informar sua pesquisa', 'warning');
      return;
    }

    this.obterChamados();    
  }

  monitorarDigitacaoForm(e) {
    if(e.keyCode === 13){
      this.pesquisar()    
    }
  }

  private obterChamados() {
    const form = this.form.getRawValue();
    this.isLoading = true;

    this._osSvc.obterPorParametros({
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active || 'codOS',
      sortDirection: this.sort.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      codOS: form.codOS,
      numOSQuarteirizada: form.numOSQuarteirizada,
      numOSCliente: form.numOSCliente,
      numSerie: form.numSerie,
    }).subscribe((data: OrdemServicoData) => {
      if (data.items.length === 1) {
        this._router.navigate([`ordem-servico/detalhe/${data.items[0].codOS}`]);
        return;
      }

      if (data.items.length === 0) {
        this._snack.exibirToast('Nenhum chamado encontrado', 'error');
      }

      this.dataSourceData = data;
      this.isLoading = false;
      this._cdr.detectChanges();
    }, () => {
      this.isLoading = false;
    });
  }

  paginar() {
    this.pesquisar();
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
