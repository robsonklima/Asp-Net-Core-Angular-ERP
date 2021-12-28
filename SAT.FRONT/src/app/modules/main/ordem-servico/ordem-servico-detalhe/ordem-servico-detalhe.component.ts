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
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import Enumerable from 'linq';
import { RoleEnum } from 'app/core/user/user.types';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { NotificacaoService } from 'app/core/services/notificacao.service';
import { Notificacao } from 'app/core/types/notificacao.types';
import { OrdemServicoCancelamentoComponent } from '../ordem-servico-cancelamento/ordem-servico-cancelamento.component';
import { OrdemServicoEmailDialogComponent } from '../ordem-servico-email-dialog/ordem-servico-email-dialog.component';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { FotoService } from 'app/core/services/foto.service';
import { OrdemServicoHistoricoData } from 'app/core/types/ordem-servico-historico.types';
import { OrdemServicoHistoricoService } from 'app/core/services/ordem-servico-historico.service';
import { fuseAnimations } from '@fuse/animations';

@Component({
	selector: 'app-ordem-servico-detalhe',
	templateUrl: './ordem-servico-detalhe.component.html',
	styleUrls: ['./ordem-servico-detalhe.component.scss'],
	animations   : fuseAnimations,
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
	fotos: Foto[] = [];
	map: L.Map;
	ultimoAgendamento: string;
	histAgendamento: string = 'Agendamentos: \n';
	usuarioCadastro: Usuario;
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
		this.obterDadosOrdemServico();

		this.perfis = RoleEnum;

		this.sidenav.closedStart.subscribe(() =>
		{
			this.obterDadosOrdemServico();
		})

		this._cdr.detectChanges();
	}

	trocarTab(tab: any)
	{
		if (tab.index !== 5 || !this.os)
		{
			return;
		}

		this.map = L.map('map', {
			scrollWheelZoom: false,
		}).setView([
			+this.os.localAtendimento.latitude,
			+this.os.localAtendimento.longitude
		], 14);

		L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
			attribution: 'SAT 2.0'
		}).addTo(this.map);

		var icon = new L.Icon.Default();
		icon.options.shadowSize = [0, 0];

		L.marker([
			+this.os.localAtendimento.latitude,
			+this.os.localAtendimento.longitude
		])
			.addTo(this.map)
			.setIcon(icon)
			.bindPopup(this.os.localAtendimento.nomeLocal);

		this.map.invalidateSize();
	}

	private async obterDadosOrdemServico()
	{
		this.isLoading = true;

		await this.obterOS();
		await this.obterUsuarioCadastro();
		await this.obterFotos();
		await this.obterAgendamentos();

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

	private async obterUsuarioCadastro()
	{
		if (this.os?.codUsuarioCad != null)
			this.usuarioCadastro =
				(await this._userService.obterPorCodigo(this.os.codUsuarioCad).toPromise());
	}

	private async obterFotos()
	{
		this.os.fotos =
			(await this._fotoService.obterPorParametros(
				{
					codOS: this.codOS
				}
			).toPromise()).items;
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
		const dialogRef = this._dialog.open(OrdemServicoEmailDialogComponent, {
			width: '400px',
			data: { os: this.os }
		});

		dialogRef.afterClosed().subscribe((confirmacao: any) =>
		{
			if (confirmacao)
			{
				this._snack.exibirToast('E-mail enviado com sucesso.', 'success');
			}
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

	getCheckin(relatorioAtendimento: RelatorioAtendimento): string
	{
		return Enumerable.from(relatorioAtendimento?.checkinsCheckouts)
			.where(i => i.tipo == 'CHECKIN').orderBy(i => i.codCheckInCheckOut).firstOrDefault()?.dataHoraCadSmartphone;
	}

	getCheckout(relatorioAtendimento: RelatorioAtendimento): string
	{
		return Enumerable.from(relatorioAtendimento?.checkinsCheckouts)
			.where(i => i.tipo == 'CHECKOUT').orderBy(i => i.codCheckInCheckOut).firstOrDefault()?.dataHoraCadSmartphone;
	}

	getFotos(): Foto[]
	{
		return Enumerable.from(this.os?.fotos)
			.where(i => !i.modalidade.includes("LAUDO"))
			.toArray();
	}

	getLaudos(): Foto[]
	{
		return Enumerable.from(this.os?.fotos)
			.where(i => i.modalidade.includes("LAUDO"))
			.toArray();
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
						indAtivo: 1,
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
						indAtivo: 1,
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
					indAtivo: 1,
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
						indAtivo: 1,
						codUsuario: this.userSession.usuario.codUsuario
					};
					this._notificacaoService.criar(notificacao).toPromise();
				});
	}

	isUserPosVenda()
	{
		var userRole: RoleEnum = this.userSession?.usuario?.codPerfil;

		if (userRole == RoleEnum.PV_COORDENADOR_DE_CONTRATO || userRole == RoleEnum.ADMIN) return true;

		return false;
	}
}