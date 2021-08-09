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
import { Tecnico, TecnicoData } from 'app/core/types/tecnico.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { MatSidenav } from '@angular/material/sidenav';
import { MatDialog } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert/alert.types';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-relatorio-atendimento-form',
  templateUrl: './relatorio-atendimento-form.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class RelatorioAtendimentoFormComponent implements OnInit, OnDestroy {
  sidenav: MatSidenav;
  usuario: Usuario;
  codOS: number;
  codRAT: number;
  relatorioAtendimento: RelatorioAtendimento;
  stepperForm: FormGroup;
  isAddMode: boolean;
  tecnicoFilterCtrl: FormControl = new FormControl();
  statusServicoFilterCtrl: FormControl = new FormControl();
  tecnicos: Tecnico[] = [];
  statusServicos: StatusServico[] = [];
  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };
  showAlert: boolean = false;
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
    this.usuario = JSON.parse(this._userService.userSession).usuario;
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

      this.stepperForm.get('step1').get('horaInicio')
        .setValue(moment(this.relatorioAtendimento.dataHoraInicio).format('HH:mm'));
      this.stepperForm.get('step1').get('horaFim')
        .setValue(moment(this.relatorioAtendimento.dataHoraSolucao).format('HH:mm'));
      this.stepperForm.get('step1').patchValue(this.relatorioAtendimento);
    }

    console.log(this.relatorioAtendimento.relatorioAtendimentoDetalhes)
    this.obterStatusServicos();
    this.obterTecnicos(this.relatorioAtendimento?.tecnico?.nome);
  }

  obterTecnicos(filter: string = ''): Promise<TecnicoData> {
    return new Promise((resolve, reject) => {
      this._tecnicoService.obterPorParametros({
        filter: filter,
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
    const dialogRef = this._dialog.open(RelatorioAtendimentoDetalheFormComponent, {});

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

  adicionarPeca(detalhe: RelatorioAtendimentoDetalhe): void {
    console.log(detalhe);
    
  }

  private inicializarForm(): void {
    this.stepperForm = this._formBuilder.group({
      step1: this._formBuilder.group({
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
            value: moment(),
            disabled: false,
          }, [Validators.required]
        ],
        horaInicio: [moment().format('HH:mm'), [Validators.required]],
        horaFim: [undefined, [Validators.required]],
        obsRAT: [undefined],
      })
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
    this.stepperForm.disable();
    this.showAlert = false;

    const stepperForm: any = this.stepperForm.getRawValue();
    let obj = {
      ...this.relatorioAtendimento,
      ...stepperForm.step1,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.usuario.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._relatorioAtendimentoService.atualizar(obj).subscribe(() => {
      this.stepperForm.enable();

      this.alert = {
        type: 'success',
        message: 'Relatório de atendimento atualizado com sucesso'
      };

      this.showAlert = true;
    }, e => {
      this.stepperForm.enable();

      this.alert = {
        type: 'error',
        message: e?.error
      };

      this.showAlert = true;
    });
  }

  private criar(): void {
    const form: any = {};

    Object.keys(form).forEach(key => {
      typeof form[key] == "boolean"
        ? this.relatorioAtendimento[key] = +form[key]
        : this.relatorioAtendimento[key] = form[key];
    });

    this.relatorioAtendimento.codOS = this.codOS;
    this.relatorioAtendimento.dataHoraInicio = moment(`${form.data.format('YYYY-MM-DD')} ${form.horaInicio}`).format('YYYY-MM-DD HH:mm:ss');
    this.relatorioAtendimento.dataHoraInicioValida = this.relatorioAtendimento.dataHoraInicio;
    this.relatorioAtendimento.dataHoraSolucao = moment(`${form.data.format('YYYY-MM-DD')} ${form.horaFim}`).format('YYYY-MM-DD HH:mm:ss');
    this.relatorioAtendimento.dataHoraSolucaoValida = this.relatorioAtendimento.dataHoraSolucao;
    this.relatorioAtendimento.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
    this.relatorioAtendimento.codUsuarioCad = this.usuario.codUsuario;
    this.relatorioAtendimento.codUsuarioCadastro = this.usuario.codUsuario;

    this._relatorioAtendimentoService.criar(this.relatorioAtendimento).subscribe(() => {
      this._snack.exibirToast('Chamado adicionado com sucesso!', 'success');
      this._router.navigate([`/ordem-servico/detalhe/${this.codOS}`]);
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
