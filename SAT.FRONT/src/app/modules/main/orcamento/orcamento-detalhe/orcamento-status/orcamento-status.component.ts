import { ChangeDetectorRef, Component, Input, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { IEditableFuseCard } from 'app/core/base-components/interfaces/ieditable-fuse-card';
import { OrcamentoMotivoService } from 'app/core/services/orcamento-motivo.service';
import { OrcamentoStatusService } from 'app/core/services/orcamento-status.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { Orcamento, OrcamentoMotivo, OrcamentoStatus } from 'app/core/types/orcamento.types';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';

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
    private _orcMotivoService: OrcamentoMotivoService,
    private _orcService: OrcamentoService)
  { }

  editar(): void
  {
    this.isEditing = true;
    this.oldItem = Object.assign({}, this.orcamento);
  }

  async salvar(): Promise<void>
  {
    this.orcamento =
      (await this._orcService.atualizar(this.orcamento).toPromise());

    this.oldItem = Object.assign({}, this.orcamento);

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
    return _.isEqual(this.orcamento, this.oldItem);
  }

  isInvalid(): boolean
  {
    return false;
  }

  changeMotivo(value)
  {
    var parsedValue = parseInt(value);
    this.orcamento.codigoMotivo = parsedValue;
    this.orcamento.orcamentoMotivo = this.motivos.find(i => i.codOrcMotivo == parsedValue);
  }

  changeStatus(value)
  {
    var parsedValue = parseInt(value);
    this.orcamento.codigoStatus = parsedValue;
    this.orcamento.orcamentoStatus = this.status.find(i => i.codOrcStatus == parsedValue);
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