import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Foto } from 'app/core/types/foto.types';
import { OrdemServico, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import * as L from 'leaflet';
import { MatDialog } from '@angular/material/dialog';
import { OrdemServicoAgendamentoComponent } from '../ordem-servico-agendamento/ordem-servico-agendamento.component';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { AgendamentoService } from 'app/core/services/agendamento.service';
import { MatSidenav } from '@angular/material/sidenav';
import { StatusServico, statusServicoConst } from 'app/core/types/status-servico.types';
import moment from 'moment';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import Enumerable from 'linq';
import { RoleEnum } from 'app/core/user/user.types';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { NotificacaoService } from 'app/core/services/notificacao.service';
import { Notificacao } from 'app/core/types/notificacao.types';
import { OrdemServicoCancelamentoComponent } from '../ordem-servico-cancelamento/ordem-servico-cancelamento.component';
import { OrdemServicoEmailDialogComponent } from '../ordem-servico-email-dialog/ordem-servico-email-dialog.component';
import { FotoService } from 'app/core/services/foto.service';
import { OrdemServicoHistoricoData } from 'app/core/types/ordem-servico-historico.types';
import { OrdemServicoHistoricoService } from 'app/core/services/ordem-servico-historico.service';
import { fuseAnimations } from '@fuse/animations';
import { statusConst } from 'app/core/types/status-types';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';

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
export class OrdemServicoDetalheComponent implements AfterViewInit
{
	@ViewChild('sidenav') sidenav: MatSidenav;
	codOS: number;
	os: OrdemServico;
	statusServico: StatusServico;
	perfis: any;
	userSession: UsuarioSessao;
	qtdFotos: number = 0;
	ultimoAgendamento: string;
	histAgendamento: string = 'Agendamentos: \n';
	isLoading: boolean = false;

	public get perfilEnum(): typeof RoleEnum
	{
		return RoleEnum;
	}

	constructor (
		private _route: ActivatedRoute,
		private _ordemServicoService: OrdemServicoService,
		private _agendamentoService: AgendamentoService,
		private _userService: UserService,
		private _snack: CustomSnackbarService,
		private _cdr: ChangeDetectorRef,
		private _dialog: MatDialog,
		private _agendaTecnicoService: AgendaTecnicoService,
		private _ordemServicoHistoricoSvc: OrdemServicoHistoricoService,
		private _fotoService: FotoService,
		private _notificacaoService: NotificacaoService
	)
	{
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngAfterViewInit(): void
	{
		this.codOS = +this._route.snapshot.paramMap.get('codOS');

		if (this.codOS) {
			this.obterDadosOrdemServico();

			this.perfis = RoleEnum;

			this.sidenav.closedStart.subscribe(() =>
			{
				this.obterDadosOrdemServico();
			});
		}

		this._cdr.detectChanges();
	}

	private async obterDadosOrdemServico()
	{
		this.isLoading = true;

		await this.obterOS();
		this.obterAgendamentos();
		this.obterFotosRAT();
		
		this.isLoading = false;
	}

	private async obterOS()
	{
		this.os = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
	}

	private obterHistoricoOS(codOS: number): Promise<OrdemServicoHistoricoData>
	{
		return new Promise((resolve, reject) =>
		{
			this._ordemServicoHistoricoSvc
				.obterPorParametros({ codOS: codOS })
				.subscribe((historico: OrdemServicoHistoricoData) =>
				{
					resolve(historico);
				}, () =>
				{
					reject();
				});
		})
	}

	private async obterAgendamentos()
	{
		if (this.os.agendamentos?.length)
		{
			var agendamentos = Enumerable.from(this.os.agendamentos)
				.orderByDescending(a => a.codAgendamento);

			this.histAgendamento += agendamentos
				.select(e => moment(e.dataAgendamento)
					.format('DD/MM HH:mm'))
				.toJoinedString(", \n");

			this.ultimoAgendamento = agendamentos
				.select(a => a.dataAgendamento)
				.first();
		}
	}

	async agendar()
	{
		const dialogRef = this._dialog.open(OrdemServicoAgendamentoComponent, {
			data: {
				codOS: this.os.codOS
			}
		});

		dialogRef.afterClosed().subscribe((data: any) =>
		{
			if (data)
			{

				if (data.agendamento.dataAgendamento < moment().format('YYYY-MM-DD HH:mm:ss'))
				{

					this._snack.exibirToast('O Chamado não deve ser agendado em datas retroativas', 'error');
					return;
				}

				this._agendamentoService.criar(data.agendamento).subscribe(
					result =>
					{
						this.os.dataHoraSolicitacao = data.agendamento.dataAgendamento;

						this._ordemServicoService.atualizar(this.os).subscribe(
							result =>
							{
								this._snack.exibirToast('Chamado agendado com sucesso!', 'success');
								this.createAgendaTecnico();
								this.obterDadosOrdemServico();
							},
							error =>
							{
								this._snack.exibirToast('Erro ao agendar chamado.', 'error');
							});
					},
					error =>
					{
						this._snack.exibirToast('Erro ao agendar chamado.', 'error');
					});
			}
		});
	}

	openEmailDialog()
	{
		this._dialog.open(OrdemServicoEmailDialogComponent, {
			width: '600px',
			data: { os: this.os }
		});
	}

	cancelar()
	{
		const dialogRef = this._dialog.open(OrdemServicoCancelamentoComponent, {
			width: '400px',
			data: { os: this.os }
		});

		dialogRef.afterClosed().subscribe((os: any) =>
		{
			if (os)
			{
				this.obterDadosOrdemServico();
			}
		});
	}

	async cancelarTransferencia()
	{
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

		dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
		{
			if (confirmacao)
			{
				var ultimoStatus = statusServicoConst.ABERTO;

				this.obterHistoricoOS(this.codOS).then((historico) =>
				{
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
							codStatusServico: ultimoStatus,
							codTecnico: null
						}
					};

					Object.keys(obj).forEach((key) => typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key]);

					this._ordemServicoService.atualizar(obj).subscribe(
						() =>
						{
							this._snack.exibirToast("Transferência cancelada com sucesso!", "success");
							this.deleteAgendaTecnico();
							this.obterDadosOrdemServico();
						},
						() =>
						{
							this._snack.exibirToast("Erro ao cancelar transferência!", "error");
						});
				});
			}
		});
	}

	private async obterFotosRAT()
	{
		for (const [i, rat] of this.os.relatoriosAtendimento.entries()) {
			this.os.relatoriosAtendimento[i].fotos = 
				(await this._fotoService.obterPorParametros(
					{
						codOS: rat.codOS,
						numRAT: rat.numRAT,
						sortActive: 'CodRATFotoSmartphone',
						sortDirection: 'desc'
					}
				).toPromise()).items.filter(f => !f.modalidade.includes('ASSINATURA'));

			this.qtdFotos = this.qtdFotos + this.os.relatoriosAtendimento[i].fotos.length;
		}
	}

	filtrarFotosRAT(tipo: string, fotos: Foto[]): Foto[] {
		let fotosFiltered: Foto[];

		if (tipo === 'RAT') {
			fotosFiltered = fotos.filter(f => !f.modalidade.includes('LAUDO'));
		} else if (tipo === 'LAUDO') {
			fotosFiltered = fotos.filter(f => f.modalidade.includes('LAUDO'));
		}

		return fotosFiltered;
	}

	private createAgendaTecnico()
	{
		if (this.os.codTecnico == null) return;

		this._agendaTecnicoService.criarAgendaTecnico(this.os.codOS, this.os.codTecnico).toPromise()
			.then(s =>
			{
				if (s)
				{
					var notificacao: Notificacao =
					{
						titulo: "Agenda Técnico",
						descricao: `O chamado ${this.os.codOS} foi alocado na Agenda Técnico.`,
						link: './#/agenda-tecnico',
						useRouter: true,
						lida: 0,
						indAtivo: statusConst.ATIVO,
						codUsuario: this.userSession.usuario.codUsuario
					};
					this._notificacaoService.criar(notificacao).toPromise();
				}
			}).catch(
				e =>
				{
					var notificacao: Notificacao =
					{
						titulo: "Agenda Técnico",
						descricao: `Ocorreu um erro ao alocar o chamado ${this.os.codOS} na Agenda Técnico.`,
						lida: 0,
						indAtivo: statusConst.ATIVO,
						codUsuario: this.userSession.usuario.codUsuario
					};
					this._notificacaoService.criar(notificacao).toPromise();
				});
	}

	private deleteAgendaTecnico()
	{
		this._agendaTecnicoService.deletarAgendaTecnico(this.os.codOS).toPromise()
			.then(s =>
			{
				var notificacao: Notificacao =
				{
					titulo: "Agenda Técnico",
					descricao: `O chamado ${this.os.codOS} foi removido da Agenda Técnico.`,
					link: './#/agenda-tecnico',
					useRouter: true,
					lida: 0,
					indAtivo: statusConst.ATIVO,
					codUsuario: this.userSession.usuario.codUsuario
				};
				this._notificacaoService.criar(notificacao).toPromise();
			}).catch(
				e =>
				{
					var notificacao: Notificacao =
					{
						titulo: "Agenda Técnico",
						descricao: `Ocorreu um erro ao remover o chamado ${this.os.codOS} da Agenda Técnico.`,
						lida: 0,
						indAtivo: statusConst.ATIVO,
						codUsuario: this.userSession.usuario.codUsuario
					};
					this._notificacaoService.criar(notificacao).toPromise();
				});
	}

	isOrcamento()
	{
		var orcamentos = [
			TipoIntervencaoEnum.ORCAMENTO,
			TipoIntervencaoEnum.ORC_APROVADO,
			TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE,
			TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO,
			TipoIntervencaoEnum.ORC_REPROVADO]

		return orcamentos.includes(this.os?.codTipoIntervencao);
	}

	getTimeFromMins(mins)
	{
		var h = mins / 60 | 0,
			m = mins % 60 | 0;
		return moment.utc().hours(h).minutes(m).format("HH:mm");
	}

	trocarTab(tab: any)
	{
	}
}