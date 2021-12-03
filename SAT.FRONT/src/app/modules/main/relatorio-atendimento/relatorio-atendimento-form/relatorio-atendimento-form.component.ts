import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, delay, map, takeUntil, tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { StatusServico, statusServicoConst } from 'app/core/types/status-servico.types';
import { RelatorioAtendimentoDetalhe } from 'app/core/types/relatorio-atendimento-detalhe.type';
import { RelatorioAtendimentoDetalheService } from 'app/core/services/relatorio-atendimento-detalhe.service';
import { RelatorioAtendimentoDetalhePecaService } from 'app/core/services/relatorio-atendimento-detalhe-peca.service';
import { RelatorioAtendimentoDetalhePecaFormComponent } from '../relatorio-atendimento-detalhe-peca-form/relatorio-atendimento-detalhe-peca-form.component';
import { RelatorioAtendimentoDetalheFormComponent } from '../relatorio-atendimento-detalhe-form/relatorio-atendimento-detalhe-form.component';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { MatSidenav } from '@angular/material/sidenav';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TimeValidator } from 'app/core/validators/time.validator';
import { Agendamento } from 'app/core/types/agendamento.types';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';


@Component({
  selector: 'app-relatorio-atendimento-form',
  templateUrl: './relatorio-atendimento-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class RelatorioAtendimentoFormComponent implements OnInit, OnDestroy
{
  sidenav: MatSidenav;
  sessionData: UsuarioSessao;
  codOS: number;
  ordemServico: OrdemServico;
  codRAT: number;
  relatorioAtendimento: RelatorioAtendimento;
  agendamentos: Agendamento;
  form: FormGroup;
  isAddMode: boolean;
  tecnicosFiltro: FormControl = new FormControl();
  statusServicoFilterCtrl: FormControl = new FormControl();
  tecnicos: Tecnico[] = [];
  statusServicos: StatusServico[] = [];
  searching: boolean;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _raService: RelatorioAtendimentoService,
    private _ordemServicoService: OrdemServicoService,
    private _raDetalheService: RelatorioAtendimentoDetalheService,
    private _raDetalhePecaService: RelatorioAtendimentoDetalhePecaService,
    private _userService: UserService,
    private _statusServicoService: StatusServicoService,
    private _tecnicoService: TecnicoService,
    private _snack: CustomSnackbarService,
    private _router: Router,
    private _dialog: MatDialog
  )
  {
    this.sessionData = JSON.parse(this._userService.userSession);
  }

  async ngOnInit()
  {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
    this.isAddMode = !this.codRAT;
    this.inicializarForm();

    this.ordemServico = await this._ordemServicoService
      .obterPorCodigo(this.codOS)
      .toPromise();

    if (!this.isAddMode)
    {
      this.relatorioAtendimento = await this._raService
        .obterPorCodigo(this.codRAT)
        .toPromise();

      this.form.controls['data'].setValue(moment(this.relatorioAtendimento.dataHoraInicio));
      this.form.controls['horaInicio'].setValue(moment(this.relatorioAtendimento.dataHoraInicio).format('HH:mm'));
      this.form.controls['horaFim'].setValue(moment(this.relatorioAtendimento.dataHoraSolucao).format('HH:mm'));
      this.form.patchValue(this.relatorioAtendimento);
    } else
    {
      this.relatorioAtendimento = { relatorioAtendimentoDetalhes: [] } as RelatorioAtendimento;
      this.configuraForm(this.ordemServico);
    }

    this.form.controls['data'].valueChanges.subscribe((data) =>
    {
      this.validaDataHoraRAT();
    })

    this.form.controls['horaInicio'].valueChanges.subscribe((horaInicio) =>
    {
      this.validaDataHoraRAT();
    })

    this.form.controls['horaFim'].valueChanges.subscribe((horaFim) =>
    {
      this.validaDataHoraRAT();
    })

    this.form.controls['codStatusServico'].valueChanges.subscribe(() =>
    {
      this.validaBloqueioStatus();
    })

    this.statusServicos = (await this._statusServicoService.obterPorParametros({
      indAtivo: 1,
      pageSize: 100,
      sortActive: 'nomeStatusServico',
      sortDirection: 'asc'
    }).toPromise()).items.filter(o => o.codStatusServico !== statusServicoConst.CANCELADO);

    this.tecnicos = (await this._tecnicoService.obterPorParametros({
      indAtivo: 1,
      pageSize: 100,
      sortActive: 'nome',
      sortDirection: 'asc',
      codFiliais: this.ordemServico?.filial?.codFilial.toString()
    }).toPromise()).items;

    this.tecnicosFiltro.valueChanges
      .pipe(
        tap(() => this.searching = true),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query =>
        {
          const data = await this._tecnicoService.obterPorParametros({
            sortActive: 'nome',
            sortDirection: 'asc',
            indAtivo: 1,
            filter: query,
            pageSize: 100,
            codFiliais: this.ordemServico?.filial?.toString()
          }).toPromise();

          return data.items.slice();
        }),
        delay(500),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data =>
      {
        this.searching = false;
        this.tecnicos = await data;
      },
        () =>
        {
          this.searching = false;
        }
      );
  }

  private configuraForm(ordemServico: OrdemServico)
  {
    // Se o status for transferido, carrega o técnico
    if (this.bloqueiaFormTecnico(ordemServico))
    {
      this.form.controls['codTecnico'].setValue(ordemServico.codTecnico);
      this.form.controls['codTecnico'].disable();
    }
  }

  inserirDetalhe()
  {
    const dialogRef = this._dialog.open(RelatorioAtendimentoDetalheFormComponent);

    dialogRef.afterClosed().subscribe((detalhe: RelatorioAtendimentoDetalhe) =>
    {
      if (detalhe)
        this.relatorioAtendimento.relatorioAtendimentoDetalhes.push(detalhe);
    });
  }

  removerDetalhe(detalhe: RelatorioAtendimentoDetalhe): void
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja excluir este detalhe?',
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
        const i = this.relatorioAtendimento.relatorioAtendimentoDetalhes
          .map(function (d) { return d; })
          .indexOf(detalhe);

        this.relatorioAtendimento.relatorioAtendimentoDetalhes[i].removido = true;
      }
    });
  }

  removerPeca(detalhe: RelatorioAtendimentoDetalhe, iDetalhePeca: number): void
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja excluir esta peça?',
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
        const iDetalhe = this.relatorioAtendimento.relatorioAtendimentoDetalhes
          .map(function (d) { return d; })
          .indexOf(detalhe);

        this.relatorioAtendimento
          .relatorioAtendimentoDetalhes[iDetalhe]
          .relatorioAtendimentoDetalhePecas[iDetalhePeca].removido = true;
      }
    });
  }

  inserirPeca(detalhe: RelatorioAtendimentoDetalhe): void
  {
    const dialogRef = this._dialog.open(RelatorioAtendimentoDetalhePecaFormComponent);

    dialogRef.afterClosed().subscribe(raDetalhePeca =>
    {
      if (raDetalhePeca)
      {
        const i = this.relatorioAtendimento.relatorioAtendimentoDetalhes
          .map(function (d) { return d; })
          .indexOf(detalhe);

        this.relatorioAtendimento
          .relatorioAtendimentoDetalhes[i]
          .relatorioAtendimentoDetalhePecas
          .push(raDetalhePeca);
      }
    });
  }

  obterTotalDetalhesNaoRemovidos(): number
  {
    return this.relatorioAtendimento
      ?.relatorioAtendimentoDetalhes
      ?.filter(d => !d.removido).length || 0;
  }

  existeDetalheSemPeca(): boolean
  {
    for (const detalhe of this.relatorioAtendimento.relatorioAtendimentoDetalhes)
    {
      if ((detalhe.codAcao === 19 || detalhe.codAcao === 26) && detalhe.relatorioAtendimentoDetalhePecas
        .filter(dp => !dp.removido).length === 0)
      {
        return true;
      }
    }

    return false;
  }

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      codRAT: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      numRAT: [undefined],
      codTecnico: [undefined, [Validators.required]],
      codStatusServico: [undefined, [Validators.required]],
      nomeAcompanhante: [undefined],
      data: [
        {
          value: undefined,
          disabled: false,
        }, [Validators.required]
      ],
      horaInicio: [undefined, [TimeValidator(), Validators.required]],
      horaFim: [undefined, [TimeValidator(), Validators.required,]],
      obsRAT: [undefined],
    });
  }

  private validaDataHoraRAT(): void
  {
    let horaInicio = moment(this.form.controls['horaInicio'].value, 'h:mm A');
    let horaFim = moment(this.form.controls['horaFim'].value, 'h:mm A');

    const duracaoEmMinutos = moment.duration(horaFim.diff(horaInicio)).asMinutes();

    this.form.controls['horaFim'].setErrors(null)

    if (duracaoEmMinutos < 20)
    {
      this.form.controls['horaInicio'].setErrors({
        'periodoInvalido': true
      });
    } else
    {
      this.form.controls['horaInicio'].setErrors(null)
    }

    if (duracaoEmMinutos < 20)
    {
      this.form.controls['horaFim'].setErrors({
        'periodoInvalido': true
      });
    } else
    {
      this.form.controls['horaFim'].setErrors(null)
    }

    let dataHoraRAT = moment(this.form.controls['data'].value).set({ h: horaInicio.hours(), m: horaInicio.minutes() });
    let dataHoraOS = moment(this.ordemServico.dataHoraAberturaOS);
    let dataHoraAgendamento = moment(this.ordemServico.dataHoraAberturaOS);

    if ((dataHoraRAT < dataHoraOS) && (this.form.controls['horaInicio'].value) && (this.form.controls['horaFim'].value)) 
    {
      this.form.controls['data'].setErrors({
        'dataRATInvalida': true
      })
    } else
    {
      this.form.controls['data'].setErrors(null)
    }

    if ((dataHoraRAT < dataHoraAgendamento) && (this.form.controls['horaInicio'].value) && (this.form.controls['horaFim'].value))
    {
      this.form.controls['data'].setErrors({
        'dataRATInvalida': true
      })
    } else
    {
      this.form.controls['data'].setErrors(null)
    }
  }

  private validaBloqueioStatus(): void
  {
    let bloqueioReincidencia = this.ordemServico.indBloqueioReincidencia;

    if (bloqueioReincidencia > 0 && this.form.controls['codStatusServico'].value !== statusServicoConst.TRANSFERIDO)
    {
      this.form.controls['codStatusServico'].setErrors({
        'bloqueioReincidencia': true
      })
    } else
    {
      this.form.controls['codStatusServico'].setErrors(null)
    }

    if (
      (
        this.ordemServico.tipoIntervencao.codTipoIntervencao === TipoIntervencaoEnum.ORCAMENTO ||
        this.ordemServico.tipoIntervencao.codTipoIntervencao === TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE ||
        this.ordemServico.tipoIntervencao.codTipoIntervencao === TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO
      )
      && this.form.controls['codStatusServico'].value === statusServicoConst.FECHADO
    )
    {
      this.form.controls['codStatusServico'].setErrors({
        'bloqueioOrcamento': true
      })
    }
  }

  async salvar()
  {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private async criar()
  {
    this.form.disable();

    const form: any = this.form.getRawValue();
    const data = form.data.format('YYYY-MM-DD');
    const horaInicio = form.horaInicio;
    const horaFim = form.horaFim;

    let ra: RelatorioAtendimento = {
      ...this.relatorioAtendimento,
      ...form,
      ...{
        codOS: this.codOS,
        dataHoraInicio: moment(`${data} ${horaInicio}`).format('YYYY-MM-DD HH:mm:ss'),
        dataHoraInicioValida: moment(`${data} ${horaInicio}`).format('YYYY-MM-DD HH:mm:ss'),
        dataHoraSolucao: moment(`${data} ${horaFim}`).format('YYYY-MM-DD HH:mm:ss'),
        dataHoraSolucaoValida: moment(`${data} ${horaFim}`).format('YYYY-MM-DD HH:mm:ss'),
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.sessionData.usuario.codUsuario,
        codUsuarioCadastro: this.sessionData.usuario.codUsuario
      }
    };

    Object.keys(ra).forEach((key) =>
    {
      typeof ra[key] == "boolean" ? ra[key] = +ra[key] : ra[key] = ra[key];
    });

    const raRes = await this._raService.criar(ra).toPromise();
    ra.codRAT = raRes.codRAT;

    for (let d of ra.relatorioAtendimentoDetalhes)
    {
      d.codRAT = raRes.codRAT;
      d.codOS = this.codOS;
      const detalheRes = await this._raDetalheService.criar(d).toPromise();

      for (let dp of d.relatorioAtendimentoDetalhePecas)
      {
        dp.codRATDetalhe = detalheRes.codRATDetalhe;
        await this._raDetalhePecaService.criar(dp).toPromise();
      }
    }

    // Formata Relato Solução
    ra.relatoSolucao = this.formatarRelatoSolucao(ra.relatorioAtendimentoDetalhes);
    await this._raService.atualizar(ra).toPromise();

    // Atualizar OS
    const os: OrdemServico = {
      ...this.ordemServico,
      ...{
        codStatusServico: form.codStatusServico,
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.sessionData.usuario.codUsuario,
      }
    };
    await this._ordemServicoService.atualizar(os).toPromise();

    this._snack.exibirToast('Relatório de atendimento inserido com sucesso!', 'success');
    this._router.navigate(['ordem-servico/detalhe/' + this.codOS]);
  }

  private async atualizar()
  {
    this.form.disable();
    const form: any = this.form.getRawValue();

    var horaInicio = form.horaInicio.toString().split(":");

    let ra: RelatorioAtendimento = {
      ...this.relatorioAtendimento,
      ...form,
      ...{
        dataHoraInicio: moment(form.data).set({ 'hour': horaInicio[0], 'minute': horaInicio[1] }).format('YYYY-MM-DD HH:mm:ss'),
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.sessionData.usuario.codUsuario
      }
    };

    Object.keys(ra).forEach((key) =>
    {
      typeof ra[key] == "boolean" ? ra[key] = +ra[key] : ra[key] = ra[key];
    });

    ra.relatoSolucao = this.formatarRelatoSolucao(ra.relatorioAtendimentoDetalhes);
    await this._raService.atualizar(ra).toPromise();

    for (let detalhe of this.relatorioAtendimento.relatorioAtendimentoDetalhes)
    {
      // Remover Detalhes e Peças
      if (detalhe.removido && detalhe.codRATDetalhe)
      {
        for (let dPeca of detalhe.relatorioAtendimentoDetalhePecas)
          await this._raDetalhePecaService.deletar(dPeca.codRATDetalhePeca).toPromise();

        await this._raDetalheService.deletar(detalhe.codRATDetalhe).toPromise();
      }

      // Adicionar Detalhes e Peças
      if (!detalhe.removido && !detalhe.codRATDetalhe)
      {
        detalhe.codRAT = this.relatorioAtendimento.codRAT;
        const detalheRes = await this._raDetalheService.criar(detalhe).toPromise();

        for (let peca of detalhe.relatorioAtendimentoDetalhePecas)
        {
          peca.codRATDetalhe = detalheRes.codRATDetalhe;

          await this._raDetalhePecaService.criar(peca).toPromise();
        }
      }

      // Adicionar Pecas
      if (!detalhe.removido && detalhe.codRATDetalhe)
      {
        for (let dPeca of detalhe.relatorioAtendimentoDetalhePecas)
        {
          if (!dPeca.codRATDetalhePeca && !dPeca.removido)
          {
            dPeca.codRATDetalhe = detalhe.codRATDetalhe;

            await this._raDetalhePecaService.criar(dPeca).toPromise();
          } else if (dPeca.codRATDetalhePeca && dPeca.removido)
          {
            await this._raDetalhePecaService.deletar(dPeca.codRATDetalhePeca).toPromise();
          }
        }
      }
    }

    // Atualizar OS
    const os: OrdemServico = {
      ...this.ordemServico,
      ...{
        codStatusServico: form.codStatusServico,
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.sessionData.usuario.codUsuario,
      }
    };
    await this._ordemServicoService.atualizar(os).toPromise();

    this._snack.exibirToast('Relatório de atendimento atualizado com sucesso!', 'success');
    this._router.navigate(['ordem-servico/detalhe/' + this.codOS]);
  }

  private formatarRelatoSolucao(relatorioAtendimentoDetalhes: RelatorioAtendimentoDetalhe[])
  {
    let retorno = "";

    for (let detalhe of relatorioAtendimentoDetalhes)
    {
      const maquina = detalhe.tipoServico?.codETipoServico.substring(0, 1);

      retorno += ` ITEM: CAUSA ${maquina == "1" ? "Máquina" : "Extra-Máquina"}, ${detalhe.acao?.nomeAcao?.replace("'", "")} do(a) ${detalhe.causa?.nomeCausa?.replace("'", "")} `;
    }

    return retorno;
  }

  public bloqueiaFormTecnico(ordemServico: OrdemServico)
  {
    return (ordemServico?.codStatusServico == 8 && ordemServico?.codTecnico != null);
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
