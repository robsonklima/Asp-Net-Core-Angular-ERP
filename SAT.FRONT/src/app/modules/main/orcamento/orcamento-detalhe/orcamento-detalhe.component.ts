import { Cidade } from 'app/core/types/cidade.types';
import { EmailService } from 'app/core/services/email.service';
import { ExportacaoService } from './../../../../core/services/exportacao.service';
import { ChangeDetectorRef, Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { Orcamento, OrcamentoDadosLocal, OrcamentoDadosLocalEnum, OrcamentoDeslocamento,
		 OrcamentoMotivo, OrcamentoStatus } from 'app/core/types/orcamento.types';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoMotivoService } from 'app/core/services/orcamento-motivo.service';
import { OrcamentoStatusService } from 'app/core/services/orcamento-status.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { EmailDialogComponent } from 'app/shared/email-dialog/email-dialog.component';
import { Subject } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { FileMime } from 'app/core/types/file.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { environment } from 'environments/environment';
import moment from 'moment';
import _ from 'lodash';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { PerfilEnum } from 'app/core/types/perfil.types';

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
	codOS: number;
	orcamento: Orcamento;
	os: OrdemServico;
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
	possuiPermissaoExclusao: boolean = false;
	possuiPermissaoFaturar: boolean = false;
	public orcamentoDeslocamentoChanged: Subject<OrcamentoDeslocamento[]> = new Subject<OrcamentoDeslocamento[]>();

	constructor(
		private _dialog: MatDialog,
		private _route: ActivatedRoute,
		private _router: Router,
		private _userService: UserService,
		private _osService: OrdemServicoService,
		private _orcamentoService: OrcamentoService,
		private _orcService: OrcamentoService,
		private _orcMotivoService: OrcamentoMotivoService,
		private _orcStatusService: OrcamentoStatusService,
		private _cdRef: ChangeDetectorRef,
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService,
		private _exportacaoService: ExportacaoService,
		private _emailService: EmailService
	) {
		this.codOrc = +this._route.snapshot.paramMap.get('codOrc');
		this.codOS = +this._route.snapshot.paramMap.get('codOS');
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.obterDados();
		this.obterStatus();
		this.obterMotivos();
		this.verificarPermissaoExclusao();
		this.verificarPermissaoFaturar();
	}

	verificarPermissaoFaturar() {
		this.possuiPermissaoFaturar = this.orcamento?.indFaturamento == 1 || 
			(this.os?.codStatusServico !== 3 && this.os?.codTipoIntervencao !== 17) || 
			(this.os?.equipamentoContrato?.contrato?.indPedido && !this.orcamento?.numPedido);
	}

	private verificarPermissaoExclusao() {
		this.possuiPermissaoExclusao = this.userSession?.usuario?.codPerfil == PerfilEnum.FINANCEIRO_COORDENADOR_CREDITO || 
			this.userSession?.usuario?.codPerfil == PerfilEnum.ADM_DO_SISTEMA || 
			this.userSession?.usuario?.codPerfil == PerfilEnum.FINANCEIRO_COORDENADOR_PONTO || 
			this.userSession?.usuario?.codPerfil == PerfilEnum.FINANCEIRO_COORDENADOR || 
			this.userSession?.usuario?.codPerfil == PerfilEnum.PV_COORDENADOR_DE_CONTRATO
	}

	private async obterDados() {
		this.isLoading = true;
		this.orcamento = await this._orcamentoService.obterPorCodigo(this.codOrc).toPromise();
		this.os = await this._osService.obterPorCodigo(this.codOS).toPromise();
		this.inicializarForm();
		this.formatarLocais();
		this.isLoading = false;
	}

	private inicializarForm() {
		this.form = this._formBuilder.group({
			codOrcMotivo: this.orcamento?.orcamentoMotivo?.codOrcMotivo,
			detalhe: this.orcamento?.detalhe,
			codOrcStatus: this.orcamento?.orcamentoStatus?.codOrcStatus,
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
			filial: this.orcamento?.filial?.nomeFilial,
			endereco: [
				{
					value: this.orcamento?.filial?.endereco,
					disabled: true
				}
			],
			fone: [
				{
					value: this.orcamento?.filial?.fone,
					disabled: true
				}
			],
			cnpj: [
				{
					value: this.orcamento?.filial?.cnpj,
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
	}

	private formatarLocais() {
		this.dadosLocalFaturamento = {
			tipo: OrcamentoDadosLocalEnum.FATURAMENTO,
			codLocalEnvioNFFaturamento: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.codLocalEnvioNFFaturamento,
			razaoSocial: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.razaoSocialFaturamento,
			cnpj: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cnpjFaturamento,
			inscricaoEstadual: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.inscricaoEstadualFaturamento,
			responsavel: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.responsavelFaturamento,
			email: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.emailFaturamento,
			fone: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.foneFaturamento,
			endereco: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.enderecoFaturamento,
			numero: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.numeroFaturamento,
			bairro: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.bairroFaturamento,
			cep: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cepFaturamento,
			complemento: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.complementoFaturamento,
			cidade: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cidadeEnvioNF?.nomeCidade,
			uf: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cidadeFaturamento?.unidadeFederativa?.siglaUF
		}

		this.dadosLocalEnvioNF = {
			tipo: OrcamentoDadosLocalEnum.NOTA_FISCAL,
			codLocalEnvioNFFaturamento: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.codLocalEnvioNFFaturamento,
			razaoSocial: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.razaoSocialEnvioNF,
			cnpj: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cnpjEnvioNF,
			inscricaoEstadual: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.inscricaoEstadualEnvioNF,
			responsavel: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.responsavelEnvioNF,
			email: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.emailEnvioNF,
			fone: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.foneEnvioNF,
			endereco: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.enderecoEnvioNF,
			numero: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.numeroEnvioNF,
			bairro: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.bairroEnvioNF,
			cep: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cepEnvioNF,
			complemento: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.complementoEnvioNF,
			cidade: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cidadeEnvioNF?.nomeCidade,
			uf: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cidadeEnvioNF?.unidadeFederativa?.siglaUF
		}

		this.dadosLocalAtendimento = {
			tipo: OrcamentoDadosLocalEnum.ATENDIMENTO,
			codLocalEnvioNFFaturamento: this.orcamento?.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.codLocalEnvioNFFaturamento,
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
			cnpj: this.os?.localAtendimento?.cnpj
		}
	}

	enviarEmail(tipoEnvio: number) {

		let dados;
		switch (tipoEnvio) {
			case 1:
				dados = {
					destinatarios: this.obterEmailsFaturamentoNf(),
					assuntoEmail: `PERTO ${this.orcamento?.numero} ${this.orcamento?.codigoOrdemServico} ${moment().format('DD/MM/YY HH:mm')}`,
					conteudoEmail:
						`
							Prezado Cliente,
							<br>
							A PERTO encaminha no anexo orçamento para sua apreciação, solicitamos gentilmente acessar o link abaixo, para aprovação ou reprovação.
							<br>
							Alertamos ao prazo de validade do mesmo, constante no rodapé do orçamento, findo este prazo e não obtido retorno será considerado reprovado e o chamado fechado.
							<br>
							No caso de aprovação a Nota Fiscal Fatura será emitida e encaminhada conforme os dados abaixo, caso seja necessário alteração por favor comunicar o setor de orçamentos da PERTO.
							<br>
							<a href="${environment.clientUrl}/#/orcamento-aprovacao/${this.codOrc}">CLIQUE AQUI</a> para Aprovar ou Reprovar este orçamento !!!
							<br>
							Ou, se preferir, utilize seu recurso de 'Responder a todos' da sua ferramenta de e-mail, e nos escreva aprovado ou reprovado!!!
							<br>
							<div class="tg-wrap">							
								<table style="undefined;table-layout: fixed; width: 1008px">
									<colgroup>
										<col style="width: 104px">
										<col style="width: 370px">
										<col style="width: 108px">
										<col style="width: 249px">
										<col style="width: 80px">
										<col style="width: 80px">
									</colgroup>
									<thead>
										<tr>
											<th colspan="6" style="border-bottom: 2px solid white;">Dados do local para envio de Nota Fiscal</th>
										</tr>
									</thead>
									<tbody>
										<tr>
											<td style="font-weight: bold;">Razão Social:</td>
											<td colspan="3">${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.razaoSocialEnvioNF?? "---"}</td>
											<td style="font-weight: bold;">CEP:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cepEnvioNF?? "---"}</td>
										</tr>
										<tr>
											<td style="font-weight: bold;">Endereço:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.enderecoEnvioNF?? "---"}</td>
											<td style="font-weight: bold;">Complemento:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.complementoEnvioNF?? "---"}</td>
											<td style="font-weight: bold;">Número:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.numeroEnvioNF?? "---"}</td>
										</tr>
										<tr>
											<td style="font-weight: bold;">Bairro:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.bairroEnvioNF?? "---"}</td>
											<td style="font-weight: bold;">Cidade:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cidadeEnvioNF?.nomeCidade?? "---"}</td>
											<td style="font-weight: bold;">UF:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cidadeEnvioNF?.unidadeFederativa?.siglaUF?? "---"}</td>
										</tr>
										<tr>
											<td style="font-weight: bold;">CNPJ:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cnpjEnvioNF?? "---"}</td>
											<td style="font-weight: bold;">Ins. Estadual:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.inscricaoEstadualEnvioNF?? "---"}</td>
											<td></td>
											<td></td>
										</tr>
										<tr>
											<td style="font-weight: bold;">Responsável:</td>
											<td colspan="5">${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.responsavelEnvioNF?? "---"}</td>
										</tr>
										<tr>
											<td style="font-weight: bold;">E-mail:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.emailEnvioNF?? "---"}</td>
											<td style="font-weight: bold;">DDD+Fone:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.foneEnvioNF?? "---"}</td>
											<td></td>
											<td></td>
										</tr>
									</tbody>
								</table>
							<br>
							<div class="tg-wrap">							
								<table style="undefined;table-layout: fixed; width: 1008px">
									<colgroup>
										<col style="width: 104px">
										<col style="width: 370px">
										<col style="width: 108px">
										<col style="width: 249px">
										<col style="width: 80px">
										<col style="width: 80px">
									</colgroup>
									<thead>
										<tr>
											<th colspan="6" style="border-bottom: 2px solid white;">Dados do local para envio de Faturamento</th>
										</tr>
									</thead>
									<tbody>
										<tr>
											<td style="font-weight: bold;">Razão Social:</td>
											<td colspan="3">${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.razaoSocialFaturamento?? "---"}</td>
											<td style="font-weight: bold;">CEP:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cepFaturamento?? "---"}</td>
										</tr>
										<tr>
											<td style="font-weight: bold;">Endereço:</td>
											<td >${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.enderecoFaturamento?? "---"}</td>
											<td style="font-weight: bold;">Complemento:</td>
											<td> ${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.complementoFaturamento?? "---"}</td>
											<td style="font-weight: bold;">Número:</td>
											<td >${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.numeroFaturamento?? "---"}</td>
										</tr>
										<tr>
											<td style="font-weight: bold;">Bairro:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.bairroFaturamento?? "---"}</td>
											<td style="font-weight: bold;">Cidade:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cidadeFaturamento?.nomeCidade?? "---"}</td>
											<td style="font-weight: bold;">UF:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cidadeFaturamento?.unidadeFederativa?.siglaUF?? "---"}</td>
										</tr>
										<tr>
											<td style="font-weight: bold;">CNPJ:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.cnpjFaturamento?? "---"}</td>
											<td style="font-weight: bold;">Ins. Estadual:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.inscricaoEstadualFaturamento?? "---"}</td>
											<td></td>
											<td></td>
										</tr>
										<tr>
											<td style="font-weight: bold;">Responsável:</td>
											<td colspan="5">${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.responsavelFaturamento?? "---"}</td>
										</tr>
										<tr>
											<td style="font-weight: bold;">E-mail:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.emailFaturamento?? "---"}</td>
											<td style="font-weight: bold;">DDD+Fone:</td>
											<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.foneFaturamento?? "---"}</td>
											<td></td>
											<td></td>
										</tr>
									</tbody>
								</table>
							</div>
						`,
					indOrcamento: this.orcamento.ordemServico?.relatoriosAtendimento.pop()?.laudos.pop()?.codLaudoStatus == 2 ? true : false
				};
				break;
			case 2:
				dados = {
					destinatarios: ['dss.orcamentos@perto.com.br'],
					assuntoEmail: `Perto - Solicitação de Abertura Técnica`,
					conteudoEmail:
						`
							<p>Solicito cotação para o atendimento abaixo</p> 
							<table> 
								<tr> 
									<td><b>CHAMADO PERTO:</b></td> 
									<td>${this.orcamento.codigoOrdemServico}</td> 
								</tr> 
								<tr> 
									<td><b>CLIENTE:</b></td> 
									<td>${this.orcamento.localEnvioNFFaturamentoVinculado?.localEnvioNFFaturamento?.razaoSocialFaturamento}</td> 
								</tr> 
								<tr> 
									<td><b>LOCAL:</b></td> 
									<td>${this.orcamento.ordemServico?.localAtendimento?.nomeLocal}</td> 
								</tr> 
								<tr> 
									<td><b>CEP:</b></td> 
									<td>${this.orcamento.ordemServico?.localAtendimento?.cep}</td> 
								</tr> 
								<tr> 
									<td><b>ENDEREÇO:</b></td> 
									<td>${this.orcamento.ordemServico?.localAtendimento?.endereco}</td> 
								</tr> 
								<tr> 
									<td><b>BAIRRO:</b></td> 
									<td>${this.orcamento.ordemServico?.localAtendimento?.bairro}</td> 
								</tr> 
								<tr> 
									<td><b>CIDADE:</b></td> 
									<td>${this.orcamento.ordemServico?.localAtendimento?.cidade?.nomeCidade}</td> 
								</tr> 
								<tr> 
									<td><b>UF:</b></td> 
									<td>${this.orcamento.ordemServico?.localAtendimento?.cidade?.unidadeFederativa?.siglaUF}</td> 
								</tr> 
								<tr> 
									<td><b>MÁQUINA:</b></td> 
									<td>${this.orcamento.ordemServico?.equipamento?.nomeEquip}</td> 
								</tr> 
								<tr> 
									<td><b>SÉRIE:</b></td> 
									<td>${this.orcamento.ordemServico?.equipamentoContrato?.numSerie}</td> 
								</tr> 
								<tr> 
									<td><b>OBS:</b></td> 
									<td>${this.orcamento.outrosServicos?.descricao?? ''}</td> 
								</tr> 
							</table> 
						`,
				};
				break;
			default:
				break;
		}

		const dialogRef = this._dialog.open(EmailDialogComponent, {
			width: '600px',
			data: dados
		});

		dialogRef.afterClosed().subscribe(async (data: any) => {
			debugger

			if (data) {
				this.orcamento.indEnvioCliente = 1;
				this.orcamento.codusuarioEnvioCliente = this.userSession.usuario.codUsuario;
				this.orcamento.dataHoraEnvioCliente = moment().format('yyyy-MM-DD HH:mm:ss');
				this._orcamentoService.atualizar(this.orcamento).subscribe();

				switch (tipoEnvio) {
					case 1:
						let exportacaoParam: Exportacao = {
							email: data,
							formatoArquivo: ExportacaoFormatoEnum.PDF,
							tipoArquivo: ExportacaoTipoEnum.ORCAMENTO,
							entityParameters: {
								codOrc: this.codOrc,
								incluirLaudoExportacao: data.incluirLaudoExportacao
							}
						}
						this._exportacaoService.exportar(FileMime.PDF, exportacaoParam);
						this.atualizaIntervencaoOS();
						break;

					case 2:
						debugger

						this._emailService.enviarEmail(
							{
								assunto: data.assunto,
								corpo: data.corpo,
								emailDestinatarios: data.emailDestinatarios
							}
						).subscribe(() => {
							this._snack.exibirToast('E-mail enviado','success')
						});
						break;

					default:
						break;
				}
			}
		});
	}

	private obterEmailsFaturamentoNf() {

		return [
			...this.dadosLocalEnvioNF?.email?.split(';').map(function (item) {
				return item.trim();
			}),
			...this.dadosLocalFaturamento?.email?.split(';').map(function (item) {
				return item.trim();
			})
		];
	}

	exportar() {
		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.PDF,
			tipoArquivo: ExportacaoTipoEnum.ORCAMENTO,
			entityParameters: {
				codOrc: this.codOrc
			}
		}

		this._exportacaoService
			.exportar(FileMime.PDF, exportacaoParam)
			.catch(e => { this._snack.exibirToast(`Não foi possível realizar o download ${e.message}`) });
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

					this._snack.exibirToast('Orçamento Excluído', 'success');
					this._router.navigate(['/orcamento/lista']);
				});
			}
		});

	}

	faturar() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja faturar este orçamento?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao) {
				this.orcamento.indFaturamento = 1;
				this.orcamento.dataHoraFaturamento = moment().format('yyyy-MM-DD HH:mm:ss');
				this.orcamento.codUsuarioFaturamento = this.userSession.usuario.codUsuario;

				this._orcamentoService.atualizar(this.orcamento).subscribe(() => {
					this._snack.exibirToast('Orçamento Faturado com Sucesso', 'success');
				}, error => {
					this._snack.exibirToast(error?.error?.message || error?.message, 'error');
				});
			}
		});
	}

	atualizaIntervencaoOS() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja alterar chamado para PENDENTE APROVAÇÃO CLIENTE?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao) {
				this.orcamento.ordemServico.codTipoIntervencao = TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE;
				this.orcamento.ordemServico.dataHoraManut = moment().format('yyyy-MM-DD HH:mm:ss');
				this.orcamento.ordemServico.codUsuarioManut = this.userSession.usuario.codUsuario;

				this._osService.atualizar(this.orcamento.ordemServico).subscribe(() => {
					this._snack.exibirToast('Chamado Atualizado com Sucesso', 'success');
				}, error => {
					this._snack.exibirToast(error?.error?.message || error?.message, 'error');
				});
			}
		});
	}

	clonarOS() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja clonar a OS?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao) {
				this.isLoading = true;

				this._osService.clonar(this.os).subscribe((os: OrdemServico) => {
					this._snack.exibirAlerta(`OS ${os.codOS} clonada com sucesso!`);

					this.isLoading = false;
				}, e => {
					this._snack.exibirToast(`Erro ao clonar a OS ${this.os.codOS}!`);

					this.isLoading = false;
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