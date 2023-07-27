import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ContratoEquipamentoService } from 'app/core/services/contrato-equipamento.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { InstalacaoPagtoInstalService } from 'app/core/services/instalacao-pagto-instal.service';

import { InstalacaoPagtoService } from 'app/core/services/instalacao-pagto-service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { InstalacaoPagto } from 'app/core/types/instalacao-pagto.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-instalacao-pagto-detalhe',
  templateUrl: './instalacao-pagto-detalhe.component.html'
})
export class InstalacaoPagtoDetalheComponent implements OnInit {
  codInstalPagto: number;
  instalPagto: InstalacaoPagto;
  isAddMode: boolean;
  isLoading: boolean = true;
  userSession: UserSession;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _route: ActivatedRoute,
    private _instalPagtoService: InstalacaoPagtoService,
    private _contratoEquipamentoSvc: ContratoEquipamentoService,
    private _instalPagtoInstalSvc: InstalacaoPagtoInstalService,
    private _exportacaoService: ExportacaoService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.isLoading = true;
    this.codInstalPagto = +this._route.snapshot.paramMap.get('codInstalPagto');
    this.isAddMode = !this.codInstalPagto;

    if (!this.isAddMode)
      this.instalPagto = await this._instalPagtoService
        .obterPorCodigo(this.codInstalPagto)
        .toPromise();

    this.isLoading = false;
  }

  async exportar() {
    this.isLoading = true;

    const exportacaoParam: Exportacao = {
      formatoArquivo: ExportacaoFormatoEnum.EXCEL,
      tipoArquivo: ExportacaoTipoEnum.INSTALACAO_PAGTO,
      entityParameters: {
        codInstalPagto: this.codInstalPagto
      }
    }

    await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
    this.isLoading = false;
  }

  async obterEquipamentosContrato(): Promise<string> {
    return new Promise((resolve, reject) => {
      this._contratoEquipamentoSvc
        .obterPorParametros({ codContrato: this.instalPagto?.codContrato }).subscribe((data) => {
          resolve(data.items.map(c => c.codEquip)?.join(','));
        }, () => {
          reject(null);
        });
    });
  }

  async obterInstalacoesPagto(): Promise<string> {
    return new Promise((resolve, reject) => {
      this._instalPagtoInstalSvc
        .obterPorParametros({ codInstalPagto: this.instalPagto?.codInstalPagto }).subscribe((data) => {
          resolve(data.items.map(c => c.codInstalacao)?.join(','));
        }, () => {
          reject(null);
        });
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
