import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { OrcamentoDadosLocal, OrcamentoDadosLocalEnum } from 'app/core/types/orcamento.types';

@Component({
	selector: 'app-orcamento-detalhe-local',
	templateUrl: './orcamento-detalhe-local.component.html'
})
export class OrcamentoDetalheLocalComponent implements OnInit {
	@Input() dadosLocal: OrcamentoDadosLocal;
	form: FormGroup;

	constructor(
		private _formBuilder: FormBuilder
	) { }

	ngOnInit(): void {

		console.log(this.dadosLocal);
		

		this.form = this._formBuilder.group({
			nomeLocal: [{ value: this.dadosLocal.nomeLocal ? this.dadosLocal.nomeLocal : this.dadosLocal.razaoSocial , disabled: true }],
			nroContrato: [{ value: this.dadosLocal.nroContrato, disabled: true }],
			agencia: [{ value: this.dadosLocal.agencia, disabled: true }],
			oscliente: [{ value: this.dadosLocal.oscliente, disabled: true }],
			osPerto: [{ value: this.dadosLocal.osPerto, disabled: true }],
			razaoSocial: [{ value: this.dadosLocal.razaoSocial, disabled: true }],
			cnpj: [{ value: this.dadosLocal.cnpj, disabled: true }],
			inscricaoEstadual: [{ value: this.dadosLocal.inscricaoEstadual, disabled: true }],
			responsavel: [{ value: this.dadosLocal.responsavel, disabled: true }],
			email: [{ value: this.dadosLocal.email, disabled: true }],
			fone: [{ value: this.dadosLocal.fone, disabled: true }],
			endereco: [{ value: this.dadosLocal.endereco, disabled: true }],
			numero: [{ value: this.dadosLocal.numero, disabled: true }],
			bairro: [{ value: this.dadosLocal.bairro, disabled: true }],
			cep: [{ value: this.dadosLocal.cep, disabled: true }],
			cidade: [{ value: this.dadosLocal.cidade, disabled: true }],
			complemento: [{ value: this.dadosLocal.complemento, disabled: true }],
			uf: [{ value: this.dadosLocal.uf, disabled: true }],
			modelo: [{ value: this.dadosLocal.modelo, disabled: true }],
			nroSerie: [{ value: this.dadosLocal.nroSerie, disabled: true }],
		});
	}

	obterTitulo() {
		switch (this.dadosLocal.tipo) {
			case OrcamentoDadosLocalEnum.ATENDIMENTO:
				return "Atendimento/Ocorrência";
			case OrcamentoDadosLocalEnum.FATURAMENTO:
				return "Faturamento";
			case OrcamentoDadosLocalEnum.NOTA_FISCAL:
				return "Nota Fiscal";
		}
	}


}