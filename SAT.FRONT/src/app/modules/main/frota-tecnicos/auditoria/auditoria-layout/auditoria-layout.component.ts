import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Auditoria } from 'app/core/types/auditoria.types';

@Component({
	selector: 'app-auditoria-layout',
	templateUrl: './auditoria-layout.component.html',
})
export class AuditoriaLayoutComponent implements OnInit {

	public codAuditoria: number;
	public nroAuditoria: string;
	public Auditoria: Auditoria;
	constructor(
		private _route: ActivatedRoute,
	) { }

	async ngOnInit(): Promise<void> {
		this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
	}

	reciverFeedback(codFilho) {
		this.nroAuditoria = codFilho;
	}

	salvar(){}

}
