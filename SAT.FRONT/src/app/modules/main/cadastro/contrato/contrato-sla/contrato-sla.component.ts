import { SLAService } from './../../../../../core/services/sla.service';
import { SLA, SLAData } from './../../../../../core/types/sla.types';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import Enumerable from 'linq';

@Component({
	selector: 'app-contrato-sla',
	templateUrl: './contrato-sla.component.html',
})
export class ContratoSlaComponent implements OnInit {

	form: FormGroup;
	isAddMode: boolean;
	isLoading: boolean;
	selectedSla: any;
	slaSicoob: SLA[] = [];
	slaGeral: SLA[] = [];
	
	constructor(
		private _formBuilder: FormBuilder,
		private _slaService: SLAService
	) { }

	ngOnInit(): void {
		this.inicializarForm();
		this.obterDados();
	}

	async obterDados(){
		const slas = (await this._slaService.obterPorParametros({}).toPromise()).items;
		this.slaSicoob = Enumerable.from(slas).where( sla => sla.nomeSLA.startsWith('67.LOC')).toArray();
		this.slaGeral = Enumerable.from(slas).except(this.slaSicoob).orderBy(s => s.nomeSLA).toArray();
		
	}

	inicializarForm() {
		this.form = this._formBuilder.group({
			tags: [[]],
		});
	}

	toggleDetails(sla: any) {
		// If the product is already selected...
		if (this.selectedSla === sla) {
			// Close the details
			this.selectedSla = null;
			return;
		}
		this.selectedSla = sla;

		// Get the product by id
		// this._inventoryService.getProductById(productId)
		//   .subscribe((product) => {

		//     // Set the selected product
		//     this.selectedProduct = product;

		//     // Fill the form
		//     this.selectedProductForm.patchValue(product);

		//     // Mark for check
		//     this._changeDetectorRef.markForCheck();
		//   });
	}
}
