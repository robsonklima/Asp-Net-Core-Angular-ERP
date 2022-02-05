import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
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
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { OrdemServico, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TimeValidator } from 'app/core/validators/time.validator';
import { Agendamento } from 'app/core/types/agendamento.types';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { Foto, FotoModalidadeEnum } from 'app/core/types/foto.types';
import { FotoService } from 'app/core/services/foto.service';
import Enumerable from 'linq';
import { statusConst } from 'app/core/types/status-types';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { AcaoEnum } from 'app/core/types/acao.types';


@Component({
  selector: 'app-relatorio-atendimento-form',
  templateUrl: './relatorio-atendimento-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class RelatorioAtendimentoFormComponent implements OnInit, OnDestroy {
  snackConfigError: MatSnackBarConfig = { duration: 2000, panelClass: 'error', verticalPosition: 'bottom', horizontalPosition: 'center' };
  snackConfigSuccess: MatSnackBarConfig = { duration: 2000, panelClass: 'success', verticalPosition: 'top', horizontalPosition: 'right' };

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
  loading: boolean = true;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _raService: RelatorioAtendimentoService,
    private _ordemServicoService: OrdemServicoService,
    private _fotoSvc: FotoService,
    private _raDetalheService: RelatorioAtendimentoDetalheService,
    private _raDetalhePecaService: RelatorioAtendimentoDetalhePecaService,
    private _userService: UserService,
    private _statusServicoService: StatusServicoService,
    private _tecnicoService: TecnicoService,
    private _matSnackBar: MatSnackBar,
    private _cdr: ChangeDetectorRef,
    private _router: Router,
    private _dialog: MatDialog
  ) {
    this.sessionData = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.loading = true;
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
    this.isAddMode = !this.codRAT;
    this.inicializarForm();

    await this.obterOrdemServico();
    await this.obterRelatorioAtendimento();

    this.form.controls['data'].valueChanges.subscribe((data) => {
      this.validaDataHoraRAT();
    })

    this.form.controls['horaInicio'].valueChanges.subscribe((horaInicio) => {
      this.validaDataHoraRAT();
    })

    this.form.controls['horaFim'].valueChanges.subscribe((horaFim) => {
      this.validaDataHoraRAT();
    })

    this.form.controls['codStatusServico'].valueChanges.subscribe(() => {
      this.validaBloqueioStatus();
    })

    this.statusServicos = (await this._statusServicoService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      pageSize: 100,
      sortActive: 'nomeStatusServico',
      sortDirection: 'asc'
    }).toPromise()).items.filter(o => o.codStatusServico !== statusServicoConst.CANCELADO);

    this.tecnicos = (await this._tecnicoService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      sortActive: 'nome',
      sortDirection: 'asc',
      codFiliais: this.ordemServico?.codFilial.toString()
    }).toPromise()).items;

    this.tecnicosFiltro.valueChanges
      .pipe(
        tap(() => this.searching = true),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          const data = await this._tecnicoService.obterPorParametros({
            sortActive: 'nome',
            sortDirection: 'asc',
            indAtivo: statusConst.ATIVO,
            filter: query,
            codFiliais: this.ordemServico?.codFilial.toString()
          }).toPromise();

          return data.items.slice();
        }),
        delay(500),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.searching = false;
        this.tecnicos = await data;
      },
        () => {
          this.searching = false;
        }
      );

    this.loading = false;
  }

  private async obterOrdemServico() {
    this.ordemServico = await this._ordemServicoService
      .obterPorCodigo(this.codOS)
      .toPromise();
  }

  private async obterRelatorioAtendimento() {
    if (!this.isAddMode) {
      this.relatorioAtendimento = await this._raService
        .obterPorCodigo(this.codRAT)
        .toPromise();

      await this.obterFotos();

      this.form.controls['data'].setValue(moment(this.relatorioAtendimento.dataHoraInicio));
      this.form.controls['horaInicio'].setValue(moment(this.relatorioAtendimento.dataHoraInicio).format('HH:mm'));
      this.form.controls['horaFim'].setValue(moment(this.relatorioAtendimento.dataHoraSolucao).format('HH:mm'));

      var checkin = Enumerable.from(this.relatorioAtendimento.checkinsCheckouts)
        .where(i => i.tipo == 'CHECKIN').orderBy(i => i.codCheckInCheckOut).firstOrDefault();

      var checkout = Enumerable.from(this.relatorioAtendimento.checkinsCheckouts)
        .where(i => i.tipo == 'CHECKOUT').orderBy(i => i.codCheckInCheckOut).firstOrDefault();

      this.form.controls['checkin'].setValue(moment(checkin?.dataHoraCadSmartphone).format('HH:mm'));
      this.form.controls['checkout'].setValue(moment(checkout?.dataHoraCadSmartphone).format('HH:mm'));

      this.form.patchValue(this.relatorioAtendimento);

      if (this.ordemServico?.codStatusServico === 3 || this.ordemServico?.codStatusServico === 2)
        this.form.disable();
    }
    else {
      this.relatorioAtendimento = { relatorioAtendimentoDetalhes: [] } as RelatorioAtendimento;
      this.configuraForm();
    }
  }

  private async obterFotos() {
    const data = await this._fotoSvc.obterPorParametros({
      numRAT: this.relatorioAtendimento.numRAT,
      codOS: this.relatorioAtendimento.codOS
    }).toPromise();

    this.relatorioAtendimento.fotos = data.items;
  }

  private configuraForm() {
    // Se o status for transferido, carrega o técnico
    if (this.ordemServico?.codStatusServico == StatusServicoEnum.TRANSFERIDO && this.ordemServico?.codTecnico !== null)
      this.form.controls['codTecnico'].setValue(this.ordemServico?.codTecnico);
  }

  inserirDetalhe() {
    const dialogRef = this._dialog.open(RelatorioAtendimentoDetalheFormComponent);

    dialogRef.afterClosed().subscribe((detalhe: RelatorioAtendimentoDetalhe) => {
      if (detalhe)
        this.relatorioAtendimento.relatorioAtendimentoDetalhes.push(detalhe);
    });
  }

  removerDetalhe(detalhe: RelatorioAtendimentoDetalhe): void {
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

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        const i = this.relatorioAtendimento.relatorioAtendimentoDetalhes
          .map(function (d) { return d; })
          .indexOf(detalhe);

        this.relatorioAtendimento.relatorioAtendimentoDetalhes[i].removido = true;
      }
    });
  }

  removerPeca(detalhe: RelatorioAtendimentoDetalhe, iDetalhePeca: number): void {
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

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        const iDetalhe = this.relatorioAtendimento.relatorioAtendimentoDetalhes
          .map(function (d) { return d; })
          .indexOf(detalhe);

        this.relatorioAtendimento
          .relatorioAtendimentoDetalhes[iDetalhe]
          .relatorioAtendimentoDetalhePecas[iDetalhePeca].removido = true;

        // this._cdr.markForCheck();
      }
    });
  }

  inserirPeca(detalhe: RelatorioAtendimentoDetalhe): void {
    const dialogRef = this._dialog.open(RelatorioAtendimentoDetalhePecaFormComponent);

    dialogRef.afterClosed().subscribe(raDetalhePeca => {
      if (raDetalhePeca) {
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

  obterTotalDetalhesNaoRemovidos(): number {
    return this.relatorioAtendimento
      ?.relatorioAtendimentoDetalhes
      ?.filter(d => !d.removido).length || 0;
  }

  existeDetalheSemPeca(): boolean {
    for (const detalhe of this.relatorioAtendimento.relatorioAtendimentoDetalhes) {
      if ((detalhe.codAcao === AcaoEnum.PENDÊNCIA_DE_PEÇA || detalhe.codAcao === AcaoEnum.TROCA_SUBSTITUIÇÃO) && detalhe.relatorioAtendimentoDetalhePecas.filter(dp => !dp.removido).length === 0)
        return true;
    }

    return false;
  }

  formatarModalidadeFoto(modalidade: string): string {
    return FotoModalidadeEnum[modalidade];
  }

  removerFoto(codRATFotoSmartphone: number) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja excluir esta foto?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        this._fotoSvc.deletar(codRATFotoSmartphone).subscribe(() => {
          this.obterFotos();
        });
      }
    });
  }

  selecionarImagem(ev: any) {
    var files = ev.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();
      reader.onload = this.transformarBase64.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  private transformarBase64(readerEvt) {
    var binaryString = readerEvt.target.result;
    var base64textString = btoa(binaryString);

    const foto: Foto = {
      codOS: this.codOS,
      numRAT: this.relatorioAtendimento.numRAT,
      modalidade: 'RAT',
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
      nomeFoto: moment().format('YYYYMMDDHHmmss') + "_" + this.codOS + '_' + 'RAT.jpg',
      base64: base64textString
    }

    this._fotoSvc.criar(foto).subscribe(() => {
      this.obterFotos();
      this._matSnackBar.open("Imagem adicionada com sucesso!", null, this.snackConfigSuccess);
    });
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codRAT: [{ value: undefined, disabled: true }],
      numRAT: [undefined],
      codTecnico: [undefined, [Validators.required]],
      codStatusServico: [undefined, [Validators.required]],
      nomeAcompanhante: [undefined],
      data: [{ value: undefined, disabled: false }, [Validators.required]],
      horaInicio: [undefined, [TimeValidator(), Validators.required]],
      horaFim: [undefined, [TimeValidator(), Validators.required,]],
      horarioInicioIntervalo: [undefined],
      horarioTerminoIntervalo: [undefined],
      checkin: [undefined],
      checkout: [undefined],
      obsRAT: [undefined],
    });
  }

  private validaDataHoraRAT(): void {
    if (!this.form.controls['horaInicio'].value || !this.form.controls['horaFim'].value) {
      if (!this.form.controls['horaFim'].value)
        this.form.controls['horaFim'].setErrors({ 'incorrect': true });

      if (!this.form.controls['horaInicio'].value)
        this.form.controls['horaInicio'].setErrors({ 'incorrect': true });

      return;
    }

    let horaInicio = moment(this.form.controls['horaInicio'].value, 'h:mm A');
    let horaFim = moment(this.form.controls['horaFim'].value, 'h:mm A');

    const duracaoEmMinutos = moment.duration(horaFim.diff(horaInicio)).asMinutes();

    if (duracaoEmMinutos < 20)
      this.form.controls['horaInicio'].setErrors({ 'periodoInvalido': true });
    else
      this.form.controls['horaInicio'].setErrors(null)

    if (duracaoEmMinutos < 20)
      this.form.controls['horaFim'].setErrors({ 'periodoInvalido': true });
    else
      this.form.controls['horaFim'].setErrors(null)

    let dataHoraRAT = moment(this.form.controls['data'].value).set({ h: horaInicio.hours(), m: horaInicio.minutes() });
    let dataHoraOS = moment(this.ordemServico?.dataHoraAberturaOS);
    let dataHoraAgendamento = moment(this.ordemServico?.dataHoraAberturaOS);

    if ((dataHoraRAT < dataHoraOS) && (this.form.controls['horaInicio'].value) && (this.form.controls['horaFim'].value))
      this.form.controls['data'].setErrors({ 'dataRATInvalida': true });
    else
      this.form.controls['data'].setErrors(null);

    if ((dataHoraRAT < dataHoraAgendamento) && (this.form.controls['horaInicio'].value) && (this.form.controls['horaFim'].value))
      this.form.controls['data'].setErrors({ 'dataRATInvalida': true });
    else
      this.form.controls['data'].setErrors(null);
  }

  private validaBloqueioStatus(): void {
    let bloqueioReincidencia = this.ordemServico.indBloqueioReincidencia;

    if (bloqueioReincidencia > 0 && this.form.controls['codStatusServico'].value !== statusServicoConst.TRANSFERIDO)
      this.form.controls['codStatusServico'].setErrors({ 'bloqueioReincidencia': true });
    else
      this.form.controls['codStatusServico'].setErrors(null);

    if ((this.ordemServico?.codTipoIntervencao === TipoIntervencaoEnum.ORCAMENTO ||
      this.ordemServico?.codTipoIntervencao === TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE ||
      this.ordemServico?.codTipoIntervencao === TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO)
      && this.form.controls['codStatusServico'].value === statusServicoConst.FECHADO)
      this.form.controls['codStatusServico'].setErrors({ 'bloqueioOrcamento': true });
  }

  async salvar() {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private async criar() {
    this.form.disable();

    const form: any = this.form.getRawValue();
    const data = form.data.format('YYYY-MM-DD');
    const horaInicio = form.horaInicio;
    const horaFim = form.horaFim;
    const qtdeHorasTecnicas = moment.duration(moment(horaFim).diff(moment(horaInicio))).asMinutes();

    let ra: RelatorioAtendimento = {
      ...this.relatorioAtendimento,
      ...form,
      ...{
        codOS: this.codOS,
        dataHoraInicio: moment(`${data} ${horaInicio}`).format('YYYY-MM-DD HH:mm:ss'),
        dataHoraInicioValida: moment(`${data} ${horaInicio}`).format('YYYY-MM-DD HH:mm:ss'),
        dataHoraSolucao: moment(`${data} ${horaFim}`).format('YYYY-MM-DD HH:mm:ss'),
        dataHoraSolucaoValida: moment(`${data} ${horaFim}`).format('YYYY-MM-DD HH:mm:ss'),
        qtdeHorasTecnicas: qtdeHorasTecnicas,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.sessionData.usuario.codUsuario,
        codUsuarioCadastro: this.sessionData.usuario.codUsuario
      }
    };

    Object.keys(ra).forEach((key) => {
      typeof ra[key] == "boolean" ? ra[key] = +ra[key] : ra[key] = ra[key];
    });

    const raRes = await this._raService.criar(ra).toPromise();
    ra.codRAT = raRes.codRAT;

    for (let d of ra.relatorioAtendimentoDetalhes) {
      d.codRAT = raRes.codRAT;
      d.codOS = this.codOS;
      const detalheRes = await this._raDetalheService.criar(d).toPromise();

      for (let dp of d.relatorioAtendimentoDetalhePecas) {
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

    this._matSnackBar.open("Relatório de atendimento inserido com sucesso!", null, this.snackConfigSuccess);
    this._router.navigate(['ordem-servico/detalhe/' + this.codOS]);
  }

  private async atualizar() {
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

    Object.keys(ra).forEach((key) => {
      typeof ra[key] == "boolean" ? ra[key] = +ra[key] : ra[key] = ra[key];
    });

    ra.relatoSolucao = this.formatarRelatoSolucao(ra.relatorioAtendimentoDetalhes);
    await this._raService.atualizar(ra).toPromise();

    for (let detalhe of this.relatorioAtendimento.relatorioAtendimentoDetalhes) {
      // Remover Detalhes e Peças
      if (detalhe.removido && detalhe.codRATDetalhe) {
        for (let dPeca of detalhe.relatorioAtendimentoDetalhePecas)
          await this._raDetalhePecaService.deletar(dPeca.codRATDetalhePeca).toPromise();

        await this._raDetalheService.deletar(detalhe.codRATDetalhe).toPromise();
      }

      // Adicionar Detalhes e Peças
      if (!detalhe.removido && !detalhe.codRATDetalhe) {
        detalhe.codRAT = this.relatorioAtendimento.codRAT;
        detalhe.codOS = this.relatorioAtendimento.codOS;
        const detalheRes = await this._raDetalheService.criar(detalhe).toPromise();

        for (let peca of detalhe.relatorioAtendimentoDetalhePecas) {
          peca.codRATDetalhe = detalheRes.codRATDetalhe;

          await this._raDetalhePecaService.criar(peca).toPromise();
        }
      }

      // Adicionar Pecas
      if (!detalhe.removido && detalhe.codRATDetalhe) {
        for (let dPeca of detalhe.relatorioAtendimentoDetalhePecas) {
          if (!dPeca.codRATDetalhePeca && !dPeca.removido) {
            dPeca.codRATDetalhe = detalhe.codRATDetalhe;
            await this._raDetalhePecaService.criar(dPeca).toPromise();
          }
          else if (dPeca.codRATDetalhePeca && dPeca.removido) {
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

    this._matSnackBar.open("Relatório de atendimento atualizado com sucesso!", null, this.snackConfigSuccess);
    this._router.navigate(['ordem-servico/detalhe/' + this.codOS]);
  }

  private formatarRelatoSolucao(relatorioAtendimentoDetalhes: RelatorioAtendimentoDetalhe[]) {
    let retorno = "";

    for (let detalhe of relatorioAtendimentoDetalhes) {
      const maquina = detalhe.tipoServico?.codETipoServico.substring(0, 1);
      retorno += ` ITEM: CAUSA ${maquina == "1" ? "Máquina" : "Extra-Máquina"}, ${detalhe.acao?.nomeAcao?.replace("'", "")} do(a) ${detalhe.causa?.nomeCausa?.replace("'", "")} `;
    }

    return retorno;
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
