import { ChangeDetectorRef, Component, Input, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { IEditableFuseCard } from 'app/core/base-components/interfaces/ieditable-fuse-card';
import { OrcamentoMotivoService } from 'app/core/services/orcamento-motivo.service';
import { OrcamentoStatusService } from 'app/core/services/orcamento-status.service';
import { Orcamento, OrcamentoMotivo, OrcamentoStatus } from 'app/core/types/orcamento.types';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-orcamento-status',
  templateUrl: './orcamento-status.component.html',
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoStatusComponent implements OnInit, IEditableFuseCard
{
  @Input() orcamento: Orcamento;
  status: OrcamentoStatus[] = [];
  motivos: OrcamentoMotivo[] = [];
  oldItem: any;
  userSession: UserSession
  isLoading: boolean;
  isEditing: boolean;

  constructor (
    private _cdRef: ChangeDetectorRef,
    private _orcStatusService: OrcamentoStatusService,
    private _orcMotivoService: OrcamentoMotivoService)
  { }

  editar(): void
  {
    this.isEditing = true;
    this.oldItem = Object.assign({}, this.orcamento);
  }

  salvar(): void
  {
    // atribui 
    this.isEditing = false;
    this.isLoading = true;
    this.isLoading = false;
  }

  cancelar(): void
  {
    this.isEditing = false;
    this.orcamento = Object.assign({}, this.oldItem);
    this._cdRef.detectChanges();
  }

  isEqual(): boolean
  {
    return false;
  }

  isInvalid(): boolean
  {
    return false;
  }

  private async obterStatus()
  {
    this.status = (await this._orcStatusService.obterPorParametros({}).toPromise()).items;
  }

  private async obterMotivos()
  {
    this.motivos = (await this._orcMotivoService.obterPorParametros({}).toPromise()).items;
  }

  ngOnInit(): void
  {
    this.obterStatus();
    this.obterMotivos();
  }
}