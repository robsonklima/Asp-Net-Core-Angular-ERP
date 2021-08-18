import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { StatusServico, StatusServicoData } from 'app/core/types/status-servico.types';
import { RelatorioAtendimentoDetalhe } from 'app/core/types/relatorio-atendimento-detalhe.type';
import { RelatorioAtendimentoDetalheService } from 'app/core/services/relatorio-atendimento-detalhe.service';
import { RelatorioAtendimentoDetalhePecaService } from 'app/core/services/relatorio-atendimento-detalhe-peca.service';
import { RelatorioAtendimentoDetalhePecaFormComponent } from '../relatorio-atendimento-detalhe-peca-form/relatorio-atendimento-detalhe-peca-form.component';
import { RelatorioAtendimentoDetalheFormComponent } from '../relatorio-atendimento-detalhe-form/relatorio-atendimento-detalhe-form.component';
import { Tecnico, TecnicoData } from 'app/core/types/tecnico.types';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { MatSidenav } from '@angular/material/sidenav';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';


@Component({
  selector: 'app-relatorio-atendimento-form',
  templateUrl: './relatorio-atendimento-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class RelatorioAtendimentoFormComponent implements OnInit, OnDestroy {
  sidenav: MatSidenav;
  sessionData: UsuarioSessionData;
  codOS: number;
  codRAT: number;
  relatorioAtendimento: RelatorioAtendimento;
  form: FormGroup;
  isAddMode: boolean;
  tecnicoFilterCtrl: FormControl = new FormControl();
  statusServicoFilterCtrl: FormControl = new FormControl();
  tecnicos: Tecnico[] = [];
  statusServicos: StatusServico[] = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _raService: RelatorioAtendimentoService,
    private _raDetalheService: RelatorioAtendimentoDetalheService,
    private _raDetalhePecaService: RelatorioAtendimentoDetalhePecaService,
    private _userService: UserService,
    private _statusServicoService: StatusServicoService,
    private _tecnicoService: TecnicoService,
    private _snack: CustomSnackbarService,
    private _router: Router,
    private _dialog: MatDialog
  ) {
    this.sessionData = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
    this.isAddMode = !this.codRAT;
    this.inicializarForm();
    this.registrarEmitters();

    if (!this.isAddMode) {
      this.relatorioAtendimento = await this._raService
        .obterPorCodigo(this.codRAT)
        .toPromise();

      this.form.controls['data'].setValue(moment(this.relatorioAtendimento.dataHoraInicio));
      this.form.controls['horaInicio'].setValue(moment(this.relatorioAtendimento.dataHoraInicio).format('HH:mm'));
      this.form.controls['horaFim'].setValue(moment(this.relatorioAtendimento.dataHoraSolucao).format('HH:mm'));
      this.form.patchValue(this.relatorioAtendimento);
    } else {
      this.relatorioAtendimento = { relatorioAtendimentoDetalhes: [] } as RelatorioAtendimento;
    }

    this.obterStatusServicos();
    this.obterTecnicos(this.relatorioAtendimento?.tecnico?.nome);
  }

  obterTecnicos(filter: string = ''): Promise<TecnicoData> {
    return new Promise((resolve, reject) => {
      this._tecnicoService.obterPorParametros({
        filter: filter,
        indAtivo: 1,
        pageSize: 50,
        sortActive: 'nome',
        sortDirection: 'asc'
      }).subscribe((data: TecnicoData) => {
        this.tecnicos = data.tecnicos;
        resolve(data);
      }, () => {
        reject();
      });
    });
  }

  obterStatusServicos(filter: string = ''): Promise<StatusServicoData> {
    return new Promise((resolve, reject) => {
      this._statusServicoService.obterPorParametros({
        indAtivo: 1,
        sortActive: 'nomeStatusServico',
        sortDirection: 'asc',
        pageSize: 50,
        filter: filter
      }).subscribe((data: StatusServicoData) => {
        this.statusServicos = data.statusServico
          .filter(s => s.codStatusServico !== 2 && s.codStatusServico !== 1);
        resolve(data);
      }, () => {
        reject();
      });
    });
  }

  inserirDetalhe() {
    const dialogRef = this._dialog.open(RelatorioAtendimentoDetalheFormComponent);

    dialogRef.afterClosed().subscribe(detalhe => {
      if (detalhe) {
        this.relatorioAtendimento.relatorioAtendimentoDetalhes.push(detalhe);
      }
    });
  }

  removerDetalhe(detalhe: RelatorioAtendimentoDetalhe): void {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
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
      if ((detalhe.codAcao === 19 || detalhe.codAcao === 26) && detalhe.relatorioAtendimentoDetalhePecas
        .filter(dp => !dp.removido).length === 0) {
        return true;
      }
    }

    return false;
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codRAT: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      numRAT: [undefined, [Validators.required]],
      codTecnico: [undefined, [Validators.required]],
      codStatusServico: [undefined, [Validators.required]],
      nomeAcompanhante: [undefined],
      data: [
        {
          value: undefined,
          disabled: false,
        }, [Validators.required]
      ],
      horaInicio: [undefined, [Validators.required]],
      horaFim: [undefined, [Validators.required]],
      obsRAT: [undefined],
    });
  }

  private registrarEmitters(): void {
    this.tecnicoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged(),
      ).subscribe((query) => {
        if (query) {
          this.obterTecnicos(this.tecnicoFilterCtrl.value);
        }
      });
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

    Object.keys(ra).forEach((key) => {
      typeof ra[key] == "boolean" ? ra[key] = +ra[key] : ra[key] = ra[key];
    });

    const raRes = await this._raService.criar(ra).toPromise();
    ra.codRAT = raRes.codRAT;

    for (let d of ra.relatorioAtendimentoDetalhes) {
      d.codRAT = raRes.codRAT;
      const detalheRes = await this._raDetalheService.criar(d).toPromise();

      for (let dp of d.relatorioAtendimentoDetalhePecas) {
        dp.codRATDetalhe = detalheRes.codRATDetalhe;
        await this._raDetalhePecaService.criar(dp).toPromise();
      }
    }

    this._snack.exibirToast('Relatório de atendimento inserido com sucesso!', 'success');
    this._router.navigate(['ordem-servico/detalhe' + this.codOS]);
  }

  private async atualizar() {
    this.form.disable();

    const form: any = this.form.getRawValue();
    let ra: RelatorioAtendimento = {
      ...this.relatorioAtendimento,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.sessionData.usuario.codUsuario
      }
    };

    Object.keys(ra).forEach((key) => {
      typeof ra[key] == "boolean" ? ra[key] = +ra[key] : ra[key] = ra[key];
    });

    await this._raService.atualizar(ra).toPromise();

    for (let detalhe of this.relatorioAtendimento.relatorioAtendimentoDetalhes) {
      // Remover Detalhes e Peças
      if (detalhe.removido && detalhe.codRATDetalhe) {
        for (let dPeca of detalhe.relatorioAtendimentoDetalhePecas) {
          await this._raDetalhePecaService.deletar(dPeca.codRATDetalhePeca).toPromise();
        }

        await this._raDetalheService.deletar(detalhe.codRATDetalhe).toPromise();
      }

      // Adicionar Detalhes e Peças
      if (!detalhe.removido && !detalhe.codRATDetalhe) {
        detalhe.codRAT = this.relatorioAtendimento.codRAT;
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
          } else if (dPeca.codRATDetalhePeca && dPeca.removido) {
            await this._raDetalhePecaService.deletar(dPeca.codRATDetalhePeca).toPromise();
          }
        }
      }
    }

    this._snack.exibirToast('Relatório de atendimento inserido com sucesso!', 'success');
    this._router.navigate(['ordem-servico/detalhe' + this.codOS]);
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
