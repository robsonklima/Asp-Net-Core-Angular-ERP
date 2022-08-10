import { Orcamento, OrcamentoMaterial } from 'app/core/types/orcamento.types';
import { Component, Input, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray, CdkDrag } from '@angular/cdk/drag-drop';
import { OrcamentoMaterialService } from 'app/core/services/orcamento-material.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import Enumerable from 'linq';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
	selector: 'app-orcamento-detalhe-pedido',
	templateUrl: './orcamento-detalhe-pedido.component.html',
	styles: [`
			.example-list {
				border: solid 1px #ccc;
				min-height: 60px;
				background: white;
				border-radius: 4px;
				overflow: hidden;
				display: block;
				width: 400px;
				max-width: 100%;
			}

			.example-box {
				padding: 20px 10px;
				border-bottom: solid 1px #ccc;
				color: rgba(0, 0, 0, 0.87);
				display: flex;
				flex-direction: row;
				align-items: center;
				justify-content: space-between;
				box-sizing: border-box;
				cursor: move;
				background: white;
				font-size: 12px;
			}

			.cdk-drag-preview {
				box-sizing: border-box;
				border-radius: 4px;
				box-shadow: 0 5px 5px -3px rgba(0, 0, 0, 0.2),
							0 8px 10px 1px rgba(0, 0, 0, 0.14),
							0 3px 14px 2px rgba(0, 0, 0, 0.12);
			}

			.cdk-drag-placeholder {
			opacity: 0;
			}

			.cdk-drag-animating {
			transition: transform 250ms cubic-bezier(0, 0, 0.2, 1);
			}

			.example-box:last-child {
			border: none;
			}

			.example-list.cdk-drop-list-dragging .example-box:not(.cdk-drag-placeholder) {
			transition: transform 250ms cubic-bezier(0, 0, 0.2, 1);
			}
	`],
})
export class OrcamentoDetalhePedidoComponent implements OnInit {
	@Input() codOrc: number;
	@Input() orcamento: Orcamento;

	materiais: OrcamentoMaterial[];
	isLoading: boolean;
	form: FormGroup;

	constructor(
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService,
		private _orcMaterialService: OrcamentoMaterialService,
		private _orcamentoService: OrcamentoService
		) { 

		}

	ngOnInit(): void {
		this.materiais = Enumerable.from(this.orcamento.materiais).orderBy(mat => mat.seqItemPedido).toArray();
		this.inicializaForm();
	}

	private inicializaForm() {
		this.form = this._formBuilder.group({
		  numPedido: [undefined, [Validators.required]],
		  obsPedido: [undefined, [Validators.required]]
		});

		this.form.patchValue(this.orcamento);
	  }

	salvarPedido(){
		this.atualizarOrcamento();
		this.atualizarOrcamentoMaterial();
	}

	atualizarOrcamentoMaterial(){
		this.materiais.forEach((mat,index) => {

			mat.seqItemPedido = index + 1;

			this._orcMaterialService.atualizar(mat).subscribe(m =>
				{
				  this._snack.exibirToast('Material atualizado com sucesso.');
				},
				  e =>
				  {
					this._snack.exibirToast('Erro ao atualizar o material.');
				  });
		});
	}



	atualizarOrcamento(){
		const form = this.form.getRawValue();
		
		this.orcamento.numPedido = form.numPedido;
		this.orcamento.obsPedido = form.obsPedido;
	
		this._orcamentoService.atualizar(this.orcamento).subscribe(() => {
			this._snack.exibirToast('Orcamento atualizado com sucesso.');
		});

	}

	drop(event: CdkDragDrop<unknown>) {
		moveItemInArray(this.materiais, event.previousIndex, event.currentIndex);
	}

	sortPredicate(index: number, item: CdkDrag<number>) {
		return (index + 1) % 2 === item.data % 2;
	}
}
