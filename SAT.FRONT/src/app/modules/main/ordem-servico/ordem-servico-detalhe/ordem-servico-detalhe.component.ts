import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { AgendamentoService } from 'app/core/services/agendamento.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DispBBBloqueioOSService } from 'app/core/services/disp-bb-bloqueio-os.service';
import { IntegracaoCobraService } from 'app/core/services/integracao-cobra.service';
import { OrdemServicoHistoricoService } from 'app/core/services/ordem-servico-historico.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { AgendaTecnico, AgendaTecnicoTipoEnum } from 'app/core/types/agenda-tecnico.types';
import { CheckinCheckout } from 'app/core/types/checkin-checkout.types';
import { DispBBBloqueioOS, DispBBBloqueioOSParameters } from 'app/core/types/DispBBBloqueioOS.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { OrdemServicoAgendamentoComponent } from '../ordem-servico-agendamento/ordem-servico-agendamento.component';
import { OrdemServicoCancelamentoComponent } from '../ordem-servico-cancelamento/ordem-servico-cancelamento.component';
import { OrdemServicoEmailDialogComponent } from '../ordem-servico-email-dialog/ordem-servico-email-dialog.component';
import { ExportacaoService } from './../../../../core/services/exportacao.service';
import { FileMime } from 'app/core/types/file.types';
import { Orcamento } from 'app/core/types/orcamento.types';
import { OrdemServico, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { StatusServico, statusServicoConst } from 'app/core/types/status-servico.types';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { RoleEnum } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import Enumerable from 'linq';
import moment from 'moment';
import { mensagensConst, toastTypesConst } from 'app/core/types/generic.types';
import { SetorEnum } from 'app/core/types/setor.types';

@Component({
	selector: 'app-ordem-servico-detalhe',
	templateUrl: './ordem-servico-detalhe.component.html',
	styles: [`
		.tool-tip{
			white-space: pre-line;
		}
	`],
	animations: fuseAnimations,
	encapsulation: ViewEncapsulation.None,
})
export class OrdemServicoDetalheComponent implements AfterViewInit {
	@ViewChild('sidenav') sidenav: MatSidenav;
	codOS: number;
	os: OrdemServico;
	orcamentos: Orcamento[] = [];
	statusServico: StatusServico;
	perfis: any;
	userSession: UsuarioSessao;
	qtdFotos: number = 0;
	qtdLaudos: number = 0;
	ultimoAgendamento: string;
	isLoading: boolean = false;
	historico: any[] = [];
	dispBBBloqueioOS: DispBBBloqueioOS[] = [];
	checkinsCheckouts: CheckinCheckout[] = [];
	perfilCliente: boolean;

	public get tipoIntervencaoEnum(): typeof TipoIntervencaoEnum {
		return TipoIntervencaoEnum;
	}

	public get perfilEnum(): typeof RoleEnum {
		return RoleEnum;
	}

	constructor(
		private _route: ActivatedRoute,
		private _ordemServicoService: OrdemServicoService,
		private _osHistoricoService: OrdemServicoHistoricoService,
		private _agendamentoService: AgendamentoService,
		private _userService: UserService,
		private _snack: CustomSnackbarService,
		private _cdr: ChangeDetectorRef,
		private _dialog: MatDialog,
		private _agendaTecnicoService: AgendaTecnicoService,
		private _dispBBBloqueioOSService: DispBBBloqueioOSService,
		private _integracaoCobraService: IntegracaoCobraService,
		private _exportacaoService: ExportacaoService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngAfterViewInit(): void {
		this.codOS = +this._route.snapshot.paramMap.get('codOS');
		this.perfilCliente = this._userService.isCustomer;

		if (this.codOS) {
			this.obterDados();
			this.perfis = RoleEnum;
			this.sidenav.closedStart.subscribe(() => this.obterDados());
		}

		this._cdr.detectChanges();
	}

	public exportar() {
		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.PDF,
			tipoArquivo: ExportacaoTipoEnum.ORDEM_SERVICO,
			entityParameters: {
				codOS: this.codOS
			}
		}
		this._exportacaoService.exportar(FileMime.PDF, exportacaoParam);
	}

	private async obterDados() {
		this.isLoading = true; 0
		await this.obterOS();
		this.obterDispBBBloqueioOS();
		this.isLoading = false;
	}

	private async obterOS() {
		this.os = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
	}

	async agendar() {
		const dialogRef = this._dialog.open(OrdemServicoAgendamentoComponent, {
			data: {
				codOS: this.os.codOS
			}
		});

		dialogRef.afterClosed().subscribe(async (data: any) => {
			if (data) {
				if (data.agendamento.dataAgendamento < moment().format('YYYY-MM-DD HH:mm:ss')) {
					this._snack.exibirToast('O Chamado não pode ser agendado para datas retroativas', 'error');
					return;
				}

				this._agendamentoService.criar(data.agendamento).subscribe(async () => {
					this.os.dataHoraSolicitacao = data.agendamento.dataAgendamento;

					this._ordemServicoService.atualizar(this.os).subscribe(async () => {
						this._snack.exibirToast('Chamado agendado com sucesso!', 'success');
						await this.criarAgendaTecnico();
						this.obterDados();
					}, () => {
						this._snack.exibirToast('Erro ao agendar chamado.', 'error');
					});
				},
					() => {
						this._snack.exibirToast('Erro ao agendar chamado.', 'error');
					});
			}
		});
	}

	openEmailDialog() {
		const parametros = {
			width: '600px',
			data: { os: this.os }
		};

		this._dialog.open(OrdemServicoEmailDialogComponent, parametros);
	}

	cancelar() {
		const dialogRef = this._dialog.open(OrdemServicoCancelamentoComponent, {
			width: '400px',
			data: { os: this.os }
		});

		dialogRef.afterClosed().subscribe((os: any) => {
			if (os) {
				this.obterDados();
			}
		});
	}

	public verificarPermissaoCancelamento(): boolean {
		if (this.userSession.usuario.codPerfil === PerfilEnum.ADM_DO_SISTEMA && this.os?.codStatusServico !== StatusServicoEnum.FECHADO)
			return false;

		if ((this.userSession.usuario.codPerfil === PerfilEnum.PV_COORDENADOR_DE_CONTRATO
			|| this.userSession.usuario.codSetor === SetorEnum.COORDENACAO_DE_CONTRATOS
			|| this.userSession.usuario.codSetor === SetorEnum.HELPDESK_NOC)
			&& this.os?.codStatusServico !== StatusServicoEnum.FECHADO)
			return false;

		if (
			(
				this.userSession.usuario.codPerfil === PerfilEnum.LIDER ||
				this.userSession.usuario.codPerfil === PerfilEnum.COORDENADOR ||
				this.userSession.usuario.codPerfil === PerfilEnum.SUPERVISOR
			) &&
			this.os?.codTipoIntervencao === TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO &&
			this.os?.codStatusServico !== StatusServicoEnum.FECHADO &&
			this.os?.codStatusServico !== StatusServicoEnum.CANCELADO
		)
			return false;

		return true;
	}

	public verificarPermissaoReabertura(): boolean {
		if (this.os?.codStatusServico == StatusServicoEnum.FECHADO)
			return true;

		if (this.os?.codStatusServico == StatusServicoEnum.CANCELADO)
			return true;

		if (this.userSession.usuario.perfil?.codPerfil === this.perfilEnum.PV_COORDENADOR_DE_CONTRATO)
			return true;

		if (this.userSession.usuario.perfil?.codPerfil === this.perfilEnum.ADM_DO_SISTEMA)
			return true;

		if (this.os?.codTipoIntervencao === this.tipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO)
			return true;

		return false;
	}

	public verificarPermissaoCriacaoRAT(): boolean {
		if (this.userSession.usuario.codPerfil === PerfilEnum.FILIAL_TECNICO_DE_CAMPO
			|| this.userSession.usuario.codPerfil === PerfilEnum.TECNICO_OPERACOES
			|| this.userSession.usuario.codSetor === SetorEnum.HELPDESK_NOC
			|| (this.userSession.usuario.codPerfil === PerfilEnum.COORDENADOR && this.userSession.usuario.codSetor === SetorEnum.CENTRO_TECNICO_OPERACIONAL)
			|| (this.userSession.usuario.codPerfil === PerfilEnum.SUPERVISOR && this.userSession.usuario.codSetor === SetorEnum.CENTRO_TECNICO_OPERACIONAL)
			|| (this.userSession.usuario.codPerfil === PerfilEnum.COORDENADOR && this.userSession.usuario.codSetor === SetorEnum.OPERACAO_DE_CAMPO)
			|| (this.userSession.usuario.codPerfil === PerfilEnum.SUPERVISOR && this.userSession.usuario.codSetor === SetorEnum.OPERACAO_DE_CAMPO)
			|| this.userSession.usuario.codPerfil === PerfilEnum.ADM_DO_SISTEMA)
			return true;

		else
			return false;
	}

	public verificarPermissaoEdicaoOS(): boolean {
		if (this.userSession.usuario.codPerfil !== PerfilEnum.FILIAL_TECNICO_DE_CAMPO
			&& this.userSession.usuario.codSetor !== SetorEnum.CLIENTE
			&& this.userSession.usuario.codSetor !== SetorEnum.FINANCEIRO
			&& this.userSession.usuario.codSetor !== SetorEnum.LABORATORIO_TECNICO
			&& this.userSession.usuario.codSetor !== SetorEnum.LOGISTICA
			&& this.userSession.usuario.codSetor !== SetorEnum.PRESTADOR_DE_SERVICO
			&& this.userSession.usuario.codSetor !== SetorEnum.FISCAL
			&& this.userSession.usuario.codSetor !== SetorEnum.ENGENHARIA_GARANTIA_E_QUALIDADE
		)
			return true;

		else
			return false;
	}

	public verificarPermissaoAgendamento(): boolean {
		if ((this.userSession.usuario.codSetor === SetorEnum.HELPDESK_NOC
			|| this.userSession.usuario.codSetor === SetorEnum.COORDENACAO_DE_CONTRATOS
			|| this.userSession.usuario.codSetor === SetorEnum.CENTRO_TECNICO_OPERACIONAL
			|| this.userSession.usuario.codSetor === SetorEnum.OPERACAO_DE_CAMPO
			|| this.userSession.usuario.codPerfil === PerfilEnum.ADM_DO_SISTEMA)
			&& (this.userSession.usuario.codPerfil !== PerfilEnum.FILIAL_TECNICO_DE_CAMPO))
			return true;

		else
			return false;
	}

	public verificarPermissaoTransferencia(): boolean {
		if ((this.userSession.usuario.codSetor === SetorEnum.HELPDESK_NOC
			|| this.userSession.usuario.codSetor === SetorEnum.CENTRO_TECNICO_OPERACIONAL
			|| this.userSession.usuario.codSetor === SetorEnum.OPERACAO_DE_CAMPO
			|| this.userSession.usuario.codPerfil === PerfilEnum.ADM_DO_SISTEMA)
			&& (this.userSession.usuario.codPerfil !== PerfilEnum.FILIAL_TECNICO_DE_CAMPO))
			return true;

		else
			return false;
	}

	async cancelarTransferencia() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja cancelar a transferência deste chamado?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao) {
				var ultimoStatus = statusServicoConst.ABERTO;

				this._osHistoricoService.obterPorParametros({ codOS: this.codOS }).subscribe(async (historico) => {
					const historicoOS = historico.items.filter(h => h.codStatusServico != StatusServicoEnum.TRANSFERIDO);

					if (historicoOS.length)
						ultimoStatus = Enumerable.from(historicoOS)
							.orderByDescending(i => i.codHistOS)
							.where(i => i.codStatusServico !== StatusServicoEnum.FECHADO)
							.firstOrDefault()
							.codStatusServico;

					let obj = {
						...this.os,
						...{
							dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
							codUsuarioManut: this.userSession.usuario.codUsuario,
							codUsuarioManutencao: this.userSession.usuario.codUsuario,
							codStatusServico: ultimoStatus,
							codTecnico: null
						}
					};

					Object.keys(obj).forEach((key) => typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key]);
					this._ordemServicoService.atualizar(obj).subscribe(
						async () => {
							this._snack.exibirToast("Transferência cancelada com sucesso!", "success");
							this.obterDados();
						},
						() => {
							this._snack.exibirToast("Erro ao cancelar transferência!", "error");
						});
				});
			}
		});
	}

	async reabrir() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja reabrir este chamado?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
			if (confirmacao) {
				let obj: OrdemServico = {
					...this.os,
					...{
						codStatusServico: statusServicoConst.ABERTO,
						dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
						codUsuarioManut: this.userSession.usuario?.codUsuario,
						codUsuarioManutencao: this.userSession.usuario?.codUsuario
					}
				};

				Object.keys(obj).forEach((key) => {
					typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
				});

				this._ordemServicoService.atualizar(obj).subscribe((os) => {
					this._snack.exibirToast("Ordem de serviço reaberta!", "success");
					this.obterOS();
				});
			}
		});
	}

	private criarAgendaTecnico() {
		if (this.os.codTecnico == null) return;

		const agenda: AgendaTecnico = {
			codTecnico: this.os.codTecnico,
			codOS: this.os.codOS,
			codUsuarioCad: this.userSession.usuario.codUsuario,
			dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
			indAtivo: 1,
			inicio: null,
			fim: null,
			indAgendamento: 0,
			tipo: AgendaTecnicoTipoEnum.OS
		}

		this._agendaTecnicoService.criar(agenda).subscribe(() => { });
	}

	public async bloquearDesbloquearChamado() {
		this.isLoading = true;

		this.os.indBloqueioReincidencia = this.os.indBloqueioReincidencia ? 0 : 1;
		this.os.codUsuarioManut = this.userSession.usuario.codUsuario;
		this.os.codUsuarioManutencao = this.userSession.usuario.codUsuario;
		this.os.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

		await this._ordemServicoService.atualizar(this.os).toPromise();
		await this.obterDados();

		this.isLoading = false;
	}

	private async obterDispBBBloqueioOS() {
		let params: DispBBBloqueioOSParameters = {
			indAtivo: 1,
			codOS: this.os.codOS
		}

		const data = await this._dispBBBloqueioOSService
			.obterPorParametros(params)
			.toPromise();

		this.dispBBBloqueioOS = data.items;
	}

	public async desbloqueioRATBBTS() {
		this.isLoading = true;

		const osBBBloqueio = (await this._dispBBBloqueioOSService.obterPorParametros({ codOS: this.codOS }).toPromise()).items.shift();
		osBBBloqueio.indAtivo = 0;

		this._dispBBBloqueioOSService.atualizar(osBBBloqueio).subscribe(r => {
			this._snack.exibirToast('Desbloqueio Realizado!', 'success');
		});

		await this.obterDados();

		this.isLoading = false;
	}

	public async reenvioIntegracaoBBTS() {
		this.isLoading = true;

		this._integracaoCobraService.deletar(this.codOS).subscribe((r) => {
			this._snack.exibirToast('Reenvio realizado!', 'success');
		}, (error) => {
			this._snack.exibirToast('Erro ao reenviar!', 'error');
		}
		);

		await this.obterDados();
		this.isLoading = false;
	}

	public reprocessarIntegracaoBRB() {
		this.isLoading = true;

		let obj: OrdemServico = {
			...this.os,
			...{
				dataHoraIntegracaoRevisao: null,
				dataHoraIntegracaoRevisaoAgendamento: null,
				dataHoraIntegracaoRevisaoV2: null,
				dataHoraManut: moment().format('yyyy-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario.codUsuario,
				dataHoraManutencao: moment().format('yyyy-MM-DD HH:mm:ss'),
				codUsuarioManutencao: this.userSession.usuario.codUsuario
			}
		};

		this._ordemServicoService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast("Sua solicitação foi realizada com sucesso! O chamado será reprocessado dentro de alguns minutos", 'success');
			this.isLoading = false;
		}, e => {
			this._snack.exibirToast("Erro ao realizar sua solicitação!", 'error');
			this.isLoading = false;
		})
	}

	verificaPermissaoBB() {
		if (((this.userSession.usuario.codPerfil == PerfilEnum.PV_COORDENADOR_DE_CONTRATO ||
			this.userSession.usuario.codPerfil == PerfilEnum.ADM_DO_SISTEMA) &&
			this.os?.codCliente == 1)) {
			return true;
		}
		return false
	}

	isOrcamento() {
		var orcamentos = [
			TipoIntervencaoEnum.ORCAMENTO,
			TipoIntervencaoEnum.ORC_APROVADO,
			TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE,
			TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO,
			TipoIntervencaoEnum.ORC_REPROVADO]

		return orcamentos.includes(this.os?.codTipoIntervencao);
	}

	async reprocessarSLA() {
		this._ordemServicoService.atualizar(this.os).toPromise();

		this._snack.exibirToast(mensagensConst.REPROCESSADO_COM_SUCESSO, toastTypesConst.SUCCESS);
	}

	trocarTab(tab: any) {

	}
}