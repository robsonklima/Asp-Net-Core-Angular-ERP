import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { ContratoEquipamentoService } from 'app/core/services/contrato-equipamento.service';
import { InstalacaoPleitoInstalService } from 'app/core/services/instalacao-pleito-instal.service';
import { InstalacaoService } from 'app/core/services/instalacao.service';
import { InstalacaoPleitoInstal } from 'app/core/types/instalacao-pleito-instal.types';
import { InstalacaoPleito } from 'app/core/types/instalacao-pleito.types';
import { Instalacao, InstalacaoData, InstalacaoParameters } from 'app/core/types/instalacao.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-pleito-instalacao-lista',
  templateUrl: './instalacao-pleito-instalacao-lista.component.html',
  styles: [
    `.list-grid-instalacao-pleito-instal {
          grid-template-columns: 42px 42px 180px 150px 120px 72px auto 100px 36px 72px 72px 36px 42px 110px 150px 36px;
      }`
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class InstalacaoPleitoInstalacaoListaComponent implements AfterViewInit {
  @Input() instalPleito: InstalacaoPleito;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: InstalacaoData;
  instalPleitoInstal: InstalacaoPleitoInstal;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _instalacaoSvc: InstalacaoService,
    private _instalPleitoInstalSvc: InstalacaoPleitoInstalService,
    private _contratoEquipamentoSvc: ContratoEquipamentoService,
    private _userSvc: UserService
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  async ngAfterViewInit() {
    this.obterEquipamentosContrato();
    this.obterDados();
    this.registerEmitters();
  }

  async obterDados(filtro: string = '') {
    this.isLoading = true;
    this._cdr.detectChanges();

    const codEquips = await this.obterEquipamentosContrato();

    const parametros: InstalacaoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort.active || 'CodInstalacao',
      sortDirection: this.sort.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      codContrato: this.instalPleito?.codContrato,
      codEquips: codEquips,
      filter: filtro
    }

    const data: InstalacaoData = await this._instalacaoSvc.obterPorParametros({
      ...parametros
    }).toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
  }

  async obterEquipamentosContrato(): Promise<string> {
    return new Promise((resolve, reject) => {
      this._contratoEquipamentoSvc
        .obterPorParametros({ codContrato: this.instalPleito?.codContrato }).subscribe((data) => {
          resolve(data.items.map(c => c.codEquip).join(','));
        }, () => {
          reject(null);
        });
    });
  }

  registerEmitters(): void {
    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(1000)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      this.paginator.pageIndex = 0;
      this.obterDados(text);
    });

    if (this.sort && this.paginator) {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  async onChange($event: MatSlideToggleChange, instalacao: Instalacao) {
    if ($event.checked) {
      this._instalPleitoInstalSvc.criar({
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        codInstalPleito: this.instalPleito.codInstalPleito,
        codInstalacao: instalacao.codInstalacao,
        codEquipContrato: instalacao.codEquipContrato
      }).subscribe();
    } else {
      this._instalPleitoInstalSvc.deletar(instalacao.codInstalacao, this.instalPleito.codInstalPleito).subscribe();
    }
  }

  isInstalacaoPleito(codInstalacao: number) {
    if (this.instalPleito.instalacoesPleitoInstal.find(i => i.codInstalacao == codInstalacao)) {        
			return true;
    }
    
		return false
	}

  paginar() {
    this.obterDados();
  }
}
