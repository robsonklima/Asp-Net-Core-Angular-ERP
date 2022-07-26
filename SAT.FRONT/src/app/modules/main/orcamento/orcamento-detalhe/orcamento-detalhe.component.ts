import { ChangeDetectorRef, Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { Orcamento, OrcamentoDadosLocal, OrcamentoDadosLocalEnum, OrcamentoDeslocamento, OrcamentoMotivo, OrcamentoStatus } from 'app/core/types/orcamento.types';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FilialService } from 'app/core/services/filial.service';
import { OrcamentoMotivoService } from 'app/core/services/orcamento-motivo.service';
import { OrcamentoStatusService } from 'app/core/services/orcamento-status.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Filial } from 'app/core/types/filial.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { EmailDialogComponent } from 'app/shared/email-dialog/email-dialog.component';
import { Subject } from 'rxjs';
import _ from 'lodash';
import { FormBuilder, FormGroup } from '@angular/forms';
import moment from 'moment';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
	selector: 'app-orcamento-detalhes',
	templateUrl: './orcamento-detalhe.component.html',
	providers: [
		{
			provide: LOCALE_ID,
			useValue: 'pt'
		}
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class OrcamentoDetalheComponent implements OnInit {
	codOrc: number;
	orcamento: Orcamento;
	os: OrdemServico;
	filial: Filial;
	userSession: UsuarioSessao;
	isLoading: boolean = false;
	status: OrcamentoStatus[] = [];
	motivos: OrcamentoMotivo[] = [];
	oldItem: any;
	isEditing: boolean;
	form: FormGroup;

	dadosLocalFaturamento: OrcamentoDadosLocal;
	dadosLocalEnvioNF: OrcamentoDadosLocal;
	dadosLocalAtendimento: OrcamentoDadosLocal;

	public orcamentoDeslocamentoChanged: Subject<OrcamentoDeslocamento[]> = new Subject<OrcamentoDeslocamento[]>();

	constructor(
		private _dialog: MatDialog,
		private _route: ActivatedRoute,
		private _router: Router,
		private _userService: UserService,
		private _osService: OrdemServicoService,
		private _orcamentoService: OrcamentoService,
		private _filialService: FilialService,
		private _orcService: OrcamentoService,
		private _orcMotivoService: OrcamentoMotivoService,
		private _orcStatusService: OrcamentoStatusService,
		private _cdRef: ChangeDetectorRef,
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService
	) {
		this.codOrc = +this._route.snapshot.paramMap.get('codOrc');
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		await this.obterStatus();
		await this.obterMotivos();
		await this.obterDados();
	}

	private async obterDados() {
		this.isLoading = true;
		this.orcamento = await this._orcamentoService.obterPorCodigo(this.codOrc).toPromise();
		this.os = await this._osService.obterPorCodigo(this.orcamento.codigoOrdemServico).toPromise();
		this.filial = await this._filialService.obterPorCodigo(this.orcamento.codigoFilial).toPromise();

		this.form = this._formBuilder.group({
			codOrcMotivo: this.orcamento.orcamentoMotivo.codOrcMotivo,
			detalhe: this.orcamento.detalhe,
			codOrcStatus: this.orcamento.orcamentoStatus.codOrcStatus,
			data: [
				{
					value: moment(this.orcamento?.dataCadastro).format('DD/MM/yyyy'),
					disabled: true,
				}
			],
			utilizaListaEspecifica: [
				{
					value: this.orcamento?.isMaterialEspecifico ? 'SIM' : 'NÃO',
					disabled: true
				}
			],
			filial: this.filial.nomeFilial,
			endereco: [
				{
					value: this.filial?.endereco,
					disabled: true
				}
			],
			fone: [
				{
					value: this.filial?.fone,
					disabled: true
				}
			],
			cnpj: [
				{
					value: this.filial?.cnpj,
					disabled: true
				}
			],
			valorTotalDesconto: [
				{
					value: this.orcamento?.valorTotalDesconto,
					disabled: true
				}
			],
			valorTotal: [
				{
					value: this.orcamento?.valorTotal,
					disabled: true
				}
			],
			validade: [
				{
					value: '3 dias',
					disabled: true
				}
			],
			impostosInclusos: [
				{
					value: 'SIM',
					disabled: true
				}
			],
		});

		this.obterLocais();
		this.isLoading = false;
	}

	private obterLocais() {
		this.dadosLocalFaturamento = {
			tipo: OrcamentoDadosLocalEnum.FATURAMENTO,
			codLocalEnvioNFFaturamento: this.orcamento?.localEnvioNFFaturamento?.codLocalEnvioNFFaturamento,
			razaoSocial: this.os?.cliente?.razaoSocial,
			cnpj: this.os?.cliente?.cnpj,
			inscricaoEstadual: this.os.cliente?.inscricaoEstadual,
			responsavel: this.orcamento?.localEnvioNFFaturamento?.responsavelFaturamento,
			email: this.orcamento?.localEnvioNFFaturamento?.emailFaturamento,
			fone: this.orcamento?.localEnvioNFFaturamento?.foneFaturamento,
			endereco: this.orcamento?.localEnvioNFFaturamento?.enderecoFaturamento,
			numero: this.orcamento?.localEnvioNFFaturamento?.numeroFaturamento,
			bairro: this.orcamento?.localEnvioNFFaturamento?.bairroFaturamento,
			cep: this.orcamento?.localEnvioNFFaturamento?.cepFaturamento,
			complemento: this.orcamento?.localEnvioNFFaturamento?.complementoFaturamento,
			cidade: this.orcamento?.localEnvioNFFaturamento?.cidadeEnvioNF?.nomeCidade,
			uf: this.orcamento?.localEnvioNFFaturamento?.cidadeFaturamento?.unidadeFederativa?.siglaUF
		}

		this.dadosLocalEnvioNF = {
			tipo: OrcamentoDadosLocalEnum.NOTA_FISCAL,
			codLocalEnvioNFFaturamento: this.orcamento?.localEnvioNFFaturamento?.codLocalEnvioNFFaturamento,
			razaoSocial: this.os?.cliente?.razaoSocial,
			cnpj: this.os?.cliente?.cnpj,
			inscricaoEstadual: this.os.cliente?.inscricaoEstadual,
			responsavel: this.orcamento?.localEnvioNFFaturamento?.responsavelEnvioNF,
			email: this.orcamento?.localEnvioNFFaturamento?.emailEnvioNF,
			fone: this.orcamento?.localEnvioNFFaturamento?.foneEnvioNF,
			endereco: this.orcamento?.localEnvioNFFaturamento?.enderecoEnvioNF,
			numero: this.orcamento?.localEnvioNFFaturamento?.numeroEnvioNF,
			bairro: this.orcamento?.localEnvioNFFaturamento?.bairroEnvioNF,
			cep: this.orcamento?.localEnvioNFFaturamento?.cepEnvioNF,
			complemento: this.orcamento?.localEnvioNFFaturamento?.complementoEnvioNF,
			cidade: this.orcamento?.localEnvioNFFaturamento?.cidadeEnvioNF?.nomeCidade,
			uf: this.orcamento?.localEnvioNFFaturamento?.cidadeEnvioNF?.unidadeFederativa?.siglaUF
		}

		this.dadosLocalAtendimento = {
			tipo: OrcamentoDadosLocalEnum.ATENDIMENTO,
			codLocalEnvioNFFaturamento: this.orcamento?.localEnvioNFFaturamento?.codLocalEnvioNFFaturamento,
			nroContrato: this.os?.equipamentoContrato?.contrato?.nroContrato,
			nomeLocal: this.os?.localAtendimento?.nomeLocal,
			oscliente: this.os?.numOSCliente,
			osPerto: this.os?.codOS.toString(),
			agencia: this.os?.localAtendimento?.numAgencia + '/' + this.os?.localAtendimento?.dcPosto,
			endereco: this.os?.localAtendimento?.endereco,
			bairro: this.os?.localAtendimento?.bairro,
			cep: this.os?.localAtendimento?.cep,
			complemento: this.os?.localAtendimento?.enderecoComplemento,
			cidade: this.os?.localAtendimento?.cidade?.nomeCidade,
			uf: this.os?.localAtendimento?.cidade?.unidadeFederativa?.siglaUF,
			modelo: this.os?.equipamento?.nomeEquip,
			nroSerie: this.os?.equipamentoContrato?.numSerie,
			motivoOrcamento: this.orcamento?.orcamentoMotivo?.descricao,
			cnpj: this.os?.cliente?.cnpj,
		}
	}

	enviarEmail() {
		this._dialog.open(EmailDialogComponent, {
			width: '600px',
		});
	}

	trocarTab(tab: any) {
		if (tab.index == 0)
			this.obterDados();

		if (tab.index !== 5 || !this.orcamento)
			return;
	}

	editar(): void {
		this.isEditing = true;
		this.oldItem = Object.assign({}, this.orcamento);
	}

	async salvar() {
		const form = this.form.getRawValue();
		this.orcamento.detalhe = form.detalhe;
		this.orcamento.codigoStatus = form.codOrcStatus;
		this.orcamento.codigoMotivo = form.codOrcMotivo;
		this.orcamento = await this._orcService.atualizar(this.orcamento).toPromise();
		this._snack.exibirToast('Orçamento atualizado com sucesso!', 'success');
	}

	cancelar(): void {
		this.isEditing = false;
		this.orcamento = Object.assign({}, this.oldItem);
		this._cdRef.detectChanges();
	}

	excluir() {

        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data: {
                titulo: 'Confirmação',
                message: 'Deseja excluir este orçamento?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
				
				this._orcamentoService.deletar(this.codOrc).subscribe(() => {
					
					this._snack.exibirToast('Orçamento Excluído','success');
					this._router.navigate(['/orcamento/lista']);
				});
            }
        });

	}	

	isEqual(): boolean {
		return _.isEqual(this.orcamento, this.oldItem);
	}

	isInvalid(): boolean {
		return false;
	}

	changeMotivo(value) {
		var parsedValue = parseInt(value);
		this.orcamento.codigoMotivo = parsedValue;
		this.orcamento.orcamentoMotivo = this.motivos.find(i => i.codOrcMotivo == parsedValue);
	}

	changeStatus(value) {
		var parsedValue = parseInt(value);
		this.orcamento.codigoStatus = parsedValue;
		this.orcamento.orcamentoStatus = this.status.find(i => i.codOrcStatus == parsedValue);
	}

	private async obterStatus() {
		this.status = (await this._orcStatusService.obterPorParametros({}).toPromise()).items;
	}

	private async obterMotivos() {
		this.motivos = (await this._orcMotivoService.obterPorParametros({}).toPromise()).items;
	}
}