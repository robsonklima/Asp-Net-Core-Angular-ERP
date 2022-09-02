import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';

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
		private _auditoriaService: AuditoriaService
	) { }

	async ngOnInit(): Promise<void> {

	}

	reciverFeedback(codFilho) {
		this.nroAuditoria = codFilho;
	}

	salvar(){}

}
