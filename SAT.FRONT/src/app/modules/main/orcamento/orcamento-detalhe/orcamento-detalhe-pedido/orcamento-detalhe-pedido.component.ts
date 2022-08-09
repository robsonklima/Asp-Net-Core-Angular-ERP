import { Component, Input, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray, CdkDrag } from '@angular/cdk/drag-drop';

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
	@Input() materiais: any;


	isLoading: boolean;

	constructor() { }

	ngOnInit(): void {

	}

	drop(event: CdkDragDrop<unknown>) {
		moveItemInArray(this.materiais, event.previousIndex, event.currentIndex);
	}

	sortPredicate(index: number, item: CdkDrag<number>) {
		return (index + 1) % 2 === item.data % 2;
	}


}
