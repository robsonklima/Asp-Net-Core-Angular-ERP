import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FilialService } from 'app/core/services/filial.service';
import { InstalacaoService } from 'app/core/services/instalacao.service';
import { Filial, FilialData, FilialParameters } from 'app/core/types/filial.types';
import { Instalacao } from 'app/core/types/instalacao.types';
import { statusConst } from 'app/core/types/status-types';
import { InstalacaoListaComponent } from '../instalacao-lista.component';

@Component({
  selector: 'app-instalacao-lista-mais-opcoes',
  templateUrl: './instalacao-lista-mais-opcoes.component.html'
})
export class InstalacaoListaMaisOpcoesComponent implements OnInit {
  itens: Instalacao[] = [];
  filiais: Filial[] = [];
  form: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<InstalacaoListaComponent>,
    private _filialService: FilialService,
    private _instalacaoService: InstalacaoService,
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
      codFilial: [undefined]
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
