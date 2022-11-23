import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InstalacaoRessalvaService } from 'app/core/services/instalacao-ressalva.service';
import { InstalacaoRessalva } from 'app/core/types/instalacao-ressalva.types';
import { InstalacaoListaComponent } from '../instalacao-lista/instalacao-lista.component';

@Component({
  selector: 'app-instalacao-ressalva-dialog',
  templateUrl: './instalacao-ressalva-dialog.component.html'
})
export class InstalacaoRessalvaDialogComponent implements OnInit {
  codInstalacao: number;
  ressalvas: InstalacaoRessalva[] = [];
  form: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<InstalacaoListaComponent>,
    private _ressalvaService: InstalacaoRessalvaService,
    private _formBuilder: FormBuilder,
  ) { 
    this.codInstalacao = data?.codInstalacao;
  }

  async ngOnInit() {
    this.criarForms();
    this.obterRessalvas();
  }

  private criarForms() {
    this.form = this._formBuilder.group({
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
    
  }

  async obterRessalvas(){
    const data = await this._ressalvaService.obterPorParametros({
      codInstalacao: this.codInstalacao
    }).toPromise();

    console.log(data);    
  }

  fechar() {
    this.dialogRef.close();
  }
}
