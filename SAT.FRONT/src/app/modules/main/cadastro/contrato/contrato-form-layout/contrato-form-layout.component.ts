import { ContratoService } from './../../../../../core/services/contrato.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Contrato } from 'app/core/types/contrato.types';

@Component({
	selector: 'app-contrato-form-layout',
	templateUrl: './contrato-form-layout.component.html',
})
export class ContratoFormLayoutComponent implements OnInit {

	public codContrato: number;
	public nroContrato: string;
	public contrato: Contrato;
	constructor(
		private _route: ActivatedRoute,
		private _contratoService: ContratoService
	) { }

	async ngOnInit(): Promise<void> {

	}

	reciverFeedback(codFilho) {
		this.nroContrato = codFilho;
	}

}
