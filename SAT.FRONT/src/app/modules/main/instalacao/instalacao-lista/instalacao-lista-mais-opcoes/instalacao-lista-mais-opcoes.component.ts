import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FilialService } from 'app/core/services/filial.service';
import { InstalacaoService } from 'app/core/services/instalacao.service';
import { TransportadoraService } from 'app/core/services/transportadora.service';
import { Filial, FilialData, FilialParameters } from 'app/core/types/filial.types';
import { Instalacao } from 'app/core/types/instalacao.types';
import { statusConst } from 'app/core/types/status-types';
import { Transportadora } from 'app/core/types/transportadora.types';
import { InstalacaoListaComponent } from '../instalacao-lista.component';

@Component({
  selector: 'app-instalacao-lista-mais-opcoes',
  templateUrl: './instalacao-lista-mais-opcoes.component.html'
})
export class InstalacaoListaMaisOpcoesComponent implements OnInit {
  itens: Instalacao[] = [];
  filiais: Filial[] = [];
  transportadoras: Transportadora[] = [];
  form: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<InstalacaoListaComponent>,
    private _filialService: FilialService,
    private _instalacaoService: InstalacaoService,
    private _transportadoraService: TransportadoraService,    
    private _snack: CustomSnackbarService,
    private _formBuilder: FormBuilder
  ) {
    if (data) this.itens = data.itens;
   }

  async ngOnInit(){
    this.criarForm();
    this.filiais = (await this.obterFiliais()).items;
  }

  private criarForm() {
    this.form = this._formBuilder.group({
      codFilial: [undefined],
      nomeLote: [undefined],
      dataRecLote: [undefined],
      nroContrato: [undefined],
      pedidoCompra: [undefined],
      superE: [undefined],
      csl:  [undefined],
      codInstalacao: [''],
      codTransportadora: [''],
      nomeFilial: [{ value: '', disabled: true }],
      csoServ: [''],
      supridora: [''],
      mst606TipoNovo: [''],
      nomeEquip: [{ value: '', disabled: true }],
      numSerie: [{ value: '', disabled: true }],
      numSerieCliente: [{ value: '', disabled: true }],
      prefixosb: [''],
      nomeLocal: [''],
      cnpj: [{ value: '', disabled: true }],
      endereco: [{ value: '', disabled: true }],
      nomeCidade: [{ value: '', disabled: true }],
      siglaUF: [{ value: '', disabled: true }],
      cep: [{ value: '', disabled: true }],
      dataLimiteEnt: [{ value: '', disabled: true }],
      dataSugEntrega: [''],
      dataConfEntrega: [''],
      nfRemessa: [{ value: '', disabled: true }],
      dataNFRemessa: [{ value: '', disabled: true }],
      dataExpedicao: [''],
      nomeTransportadora: [''],
      agenciaEnt: [''],
      nomeLocalEnt: [''],
      dtbCliente: [''],
      faturaTranspReEntrega: [''],
      dtReEntrega: [''],
      responsavelRecebReEntrega: [''],
      dataHoraChegTranspBT: [''],
      ressalvaEnt: [''],
      nomeRespBancoBT: [''],
      numMatriculaBT: [''],
      indBTOrigEnt: [{ value: '', disabled: true }],
      indBTOK: [{ value: '', disabled: true }],
      nfRemessaConferida: [''],
      dataLimiteIns: [''],
      dataSugInstalacao: [''],
      dataConfInstalacao: [''],
      os: [{ value: '', disabled: true }],
      dataHoraOS: [''],
      instalStatus: [{ value: '', disabled: true }],
      numRAT: [''],
      agenciaIns: [''],
      nomeLocalIns: [{ value: '', disabled: true }],
      dataBI: [''],
      qtdParaboldBI: [''],
      ressalvaIns: [''],
      indEquipRebaixadoBI: [''],
      ressalvaInsR: [{ value: '', disabled: true }]
    });
  }

  async obterFiliais(): Promise<FilialData> {
		let params: FilialParameters = {
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFilial',
			sortDirection: 'asc'
		};

		return await this._filialService
			.obterPorParametros(params)
			.toPromise();
	}  

  private async obterTransportadoras(filter: string = '') {
    const data = await this._transportadoraService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      sortActive: 'NomeTransportadora',
      sortDirection: 'asc',
      filter: filter
    }).toPromise();

    this.transportadoras = data.items;
  }

  async salvar() {
    const form = this.form.getRawValue();

    for (const item of this.itens) {
      this._instalacaoService
        .atualizar({
          ...item,
          ...{
            codFilial: form.codFilial || item.codFilial
          }
        })
        .subscribe(() => {
          this._snack.exibirToast('Registros atualizados com sucesso', 'success');
          this.dialogRef.close(true);
        }, () => {
          this._snack.exibirToast('Erro ao atualizar os registros', 'error');
          this.dialogRef.close(true);
        });
    }
  }

}
