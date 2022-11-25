import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InstalacaoMotivoResService } from 'app/core/services/instalacao-motivo-res.service';
import { InstalacaoRessalvaService } from 'app/core/services/instalacao-ressalva.service';
import { InstalacaoMotivoRes, InstalacaoMotivoResData, InstalacaoMotivoResParameters } from 'app/core/types/instalacao-ressalva-motivo.types';
import { InstalacaoRessalva, InstalacaoRessalvaData } from 'app/core/types/instalacao-ressalva.types';
import { statusConst } from 'app/core/types/status-types';
import { InstalacaoListaComponent } from '../instalacao-lista/instalacao-lista.component';

@Component({
  selector: 'app-instalacao-ressalva-dialog',
  templateUrl: './instalacao-ressalva-dialog.component.html'
})
export class InstalacaoRessalvaDialogComponent implements OnInit {
  codInstalacao: number;
  ressalvas: InstalacaoRessalva[] = [];
  motivos: InstalacaoMotivoRes[] = [];
  formRessalva: FormGroup;
  dataSourceData: InstalacaoRessalvaData;

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<InstalacaoListaComponent>,
    private _ressalvaService: InstalacaoRessalvaService,
    private _motivoService: InstalacaoMotivoResService,
    private _formBuilder: FormBuilder,
  ) { 
    this.codInstalacao = data?.codInstalacao;
  }

  async ngOnInit() {
    this.criarForms();
    this.motivos = (await this.obterMotivos()).items;
    this.obterRessalvas();
  }

  private criarForms() {
    this.formRessalva = this._formBuilder.group({
      codInstalMotivoRes: [undefined],
      codInstalRessalva: [undefined],
      codInstalacao: [undefined],
      codUsuarioCad: [undefined],
      codUsuarioManut: [undefined],
      comentario: [undefined],
      dataHoraCad: [undefined],
      dataHoraManut: [undefined],
      dataOcorrencia: [undefined],
      indAtivo: [undefined],
      indJustificativa: [undefined]
    });    
  }

  async salvar() {
    console.log(this.formRessalva);    
  }

  async obterRessalvas(){
    const data = await this._ressalvaService.obterPorParametros({
      codInstalacao: this.codInstalacao
    }).toPromise();

    this.dataSourceData = data;
  }

  async obterMotivos(): Promise<InstalacaoMotivoResData> {
    let params: InstalacaoMotivoResParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'DescMotivoRes',
      sortDirection: 'asc'
    };

    return await this._motivoService
      .obterPorParametros(params)
      .toPromise();
  }

  fechar() {
    this.dialogRef.close();
  }
}
