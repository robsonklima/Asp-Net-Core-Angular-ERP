import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { AuditoriaVeiculoTanque } from 'app/core/types/auditoria-veiculo-tanque.types';
import { Auditoria } from 'app/core/types/auditoria.types';
import { DespesaConfiguracaoCombustivel } from 'app/core/types/despesa-configuracao-combustivel.types';
import { DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';


@Component({
  selector: 'app-auditoria-utilizacao',
  templateUrl: './auditoria-utilizacao.component.html',
})
export class AuditoriaUtilizacaoComponent implements OnInit {
  codAuditoria: number;
  codAuditoriaVeiculo: number;
  auditoria: Auditoria;
  form: FormGroup;
  userSession: UsuarioSessao;
  qtdLitros: number;
  tanques: AuditoriaVeiculoTanque[] = [];
  despesasPeriodoTecnico: DespesaPeriodoTecnico[] = [];
  configuracaoCombustivel: DespesaConfiguracaoCombustivel;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _auditoriaService: AuditoriaService,
    private _snack: CustomSnackbarService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();
    this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
    this.codAuditoriaVeiculo = +this._route.snapshot.paramMap.get('codAuditoriaVeiculo');
    this.obterAuditoria();
    this.registrarEmitters();
  }

  obterAuditoria() {
    this._auditoriaService.obterPorCodigo(this.codAuditoria)
      .pipe(first())
      .subscribe(data => {
        this.form.patchValue(data);
        this.auditoria = data;
      });
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codAuditoria: [undefined],
      codAuditoriaVeiculoTanque: [undefined],
      dataHoraRetiradaVeiculo: [undefined],
      dataHoraCad: [undefined],
      totalDiasEmUso: [undefined],
      totalMesesEmUso: [undefined],
      odometroInicialRetirada: [undefined],
      odometroPeriodoAuditado: [undefined],
      despesasSAT: [undefined],
      kmFerias: [undefined],
      creditosCartao: [undefined],
      saldoCartao: [undefined],
      observacoes: [undefined],
      qtdLitros: [undefined],
      valorTanque: [undefined],
      kmPercorrido: [undefined],
      kmCompensado: [undefined],
      despesasCompensadasValor: [undefined],
      kmParticular: [undefined],
      kmParticularMes: [undefined],
      usoParticular: [undefined],

    });
  }

  private registrarEmitters() { 
    this.form.controls['observacoes'].valueChanges
			.pipe(
        debounceTime(700),
				distinctUntilChanged(),
				takeUntil(this._onDestroy)
			)
			.subscribe((obs) => {
        if (!this.auditoria) return;
				this._auditoriaService.atualizar({ ...this.auditoria, ...{ observacoes: obs } }).subscribe();
        this._snack.exibirToast('Observação atualizada', 'success');
			});
   }

  validarCorKMParticular(auditoria) {
    if (auditoria?.usuario?.Tecnico?.codFrotaFinalidadeUso == 1) {
      if (auditoria.kmParticularMes > 0) {
        return false;
      } else {
        return true;
      }
    } else {
      if (auditoria.kmParticularMes > 800) {
        return false;
      }
      else {
        return true;
      }
    }
  }

  validarcorUsoParticular(auditoria) {
    if (auditoria.usoParticular > 0) {
      return false;
    } else {
      return true;
    }
  }


  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}