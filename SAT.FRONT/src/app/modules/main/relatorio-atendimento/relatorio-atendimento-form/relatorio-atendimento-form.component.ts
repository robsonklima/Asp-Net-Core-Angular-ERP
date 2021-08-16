import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { StatusServico, StatusServicoData } from 'app/core/types/status-servico.types';
import { RelatorioAtendimentoDetalhe } from 'app/core/types/relatorio-atendimento-detalhe.type';
import { RelatorioAtendimentoDetalheFormComponent } from '../relatorio-atendimento-detalhe-form/relatorio-atendimento-detalhe-form.component';
import { RelatorioAtendimentoDetalhePecaFormComponent } from '../relatorio-atendimento-detalhe-peca-form/relatorio-atendimento-detalhe-peca-form.component';
import { Tecnico, TecnicoData } from 'app/core/types/tecnico.types';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { MatSidenav } from '@angular/material/sidenav';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';

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
    private _router: Router,
    private _snack: CustomSnackbarService,
    private _relatorioAtendimentoService: RelatorioAtendimentoService,
    private _userService: UserService,
    private _statusServicoService: StatusServicoService,
    private _tecnicoService: TecnicoService,
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
      this.relatorioAtendimento = await this._relatorioAtendimentoService
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
    this.relatorioAtendimento.relatorioAtendimentoDetalhes = this.relatorioAtendimento
      .relatorioAtendimentoDetalhes.filter(d => d.dataHoraCad !== detalhe.dataHoraCad);
  }

  inserirPeca(detalhe: RelatorioAtendimentoDetalhe): void {
    const dialogRef = this._dialog.open(RelatorioAtendimentoDetalhePecaFormComponent);

    dialogRef.afterClosed().subscribe(raDetalhePeca => {
      if (raDetalhePeca) {
        let index = this.relatorioAtendimento.relatorioAtendimentoDetalhes
          .findIndex(
            d => d.codServico === detalhe.codServico && d.codAcao === detalhe.codAcao &&
            d.codCausa === detalhe.codCausa && d.codDefeito === detalhe.codDefeito
          );

        this.relatorioAtendimento
          .relatorioAtendimentoDetalhes[index]
          .relatorioAtendimentoDetalhePecas
          .push(raDetalhePeca);
      }
    });
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

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar(): void {
    this.form.disable();

    const form: any = this.form.getRawValue();
    let obj = {
      ...this.relatorioAtendimento,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.sessionData.usuario.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._relatorioAtendimentoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast('Relatório de atendimento atualizado com sucesso!', 'success');
      this._router.navigate([`/ordem-servico/detalhe/${this.codOS}`]);
    }, e => {
      this.form.enable();
      this._snack.exibirToast('Não foi possível atualizar o relatório de atendimento', 'error');
    });
  }

  private criar(): void {
    this.form.disable();

    const stepperForm: any = this.form.getRawValue();
    const data = stepperForm.step1.data.format('YYYY-MM-DD');
    const horaInicio = stepperForm.step1.horaInicio;
    const horaFim = stepperForm.step1.horaFim;

    let obj = {
      ...this.relatorioAtendimento,
      ...stepperForm.step1,
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

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._relatorioAtendimentoService.criar(obj).subscribe(() => {
      this._snack.exibirToast('Relatório de atendimento adicionado com sucesso!', 'success');
      this._router.navigate([`/ordem-servico/detalhe/${this.codOS}`]);
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
