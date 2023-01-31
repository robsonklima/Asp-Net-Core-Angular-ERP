import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, map } from 'rxjs/operators';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculoTanque } from 'app/core/types/auditoria-veiculo-tanque.types';
import { DespesaConfiguracaoCombustivel } from 'app/core/types/despesa-configuracao-combustivel.types';
import { DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';


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
  @ViewChild('obsInputControl') obsInputControl: ElementRef;

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
    private _auditoriaService: AuditoriaService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();

    this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
    this.codAuditoriaVeiculo = +this._route.snapshot.paramMap.get('codAuditoriaVeiculo');
    this.obterAuditoria();

    this.registrarEmitters();

    this.preencherForm();

    
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

  private preencherForm(): void {
    this.form.controls['valorTanque'].setValue(this.auditoria?.valorTanque);
    this.form.controls['observacoes'].setValue(this.auditoria?.observacoes);
    this.form.controls['dataHoraRetiradaVeiculo'].setValue(this.auditoria?.dataHoraRetiradaVeiculo);
    this.form.controls['totalDiasEmUso'].setValue(this.auditoria?.totalDiasEmUso);
  
  }

  private registrarEmitters() { 
    fromEvent(this.obsInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(1000)
      , distinctUntilChanged()).subscribe((text: string) => {
        this.auditoria.observacoes = text;
        this._auditoriaService.atualizar(this.auditoria).subscribe(() => {
          this._snack.exibirToast('Descrição da observação atualizada', 'success');
        },
        (error) => {
          this._snack.exibirToast('Erro ao atualizar a descrição da observação', 'danger');
        });
      });

      console.log(this.auditoria.observacoes);
      
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