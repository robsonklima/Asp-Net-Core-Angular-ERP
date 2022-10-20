import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServicoSTN } from 'app/core/types/ordem-servico-stn.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { fromEvent, Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { OrdemServicoStnHistoricoComponent } from '../ordem-servico-stn-historico/ordem-servico-stn-historico.component';
import { Location } from '@angular/common';
import { FormGroup } from '@angular/forms';
@Component({
  selector: 'app-ordem-servico-stn-form',
  templateUrl: './ordem-servico-stn-form.component.html'
})
export class OrdemServicoStnFormComponent implements AfterViewInit {
  codAtendimento: number;
  os: OrdemServico;
  rat: RelatorioAtendimento;
  atendimentos: OrdemServicoSTN[] = [];
  ordemServicoSTN: OrdemServicoSTN;
  isAddMode: boolean;
  isLoading: boolean = false;
  userSession: UsuarioSessao;
  form: FormGroup;
  @ViewChild('codOSInputControl') codOSInputControl: ElementRef;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _ordemServicoService: OrdemServicoService,
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _dialog: MatDialog,
    private _location: Location
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.codAtendimento = +this._route.snapshot.paramMap.get('codAtendimento');
    this.isAddMode = !this.codAtendimento;

    //this.obterDados(6928411);

    this.registrarEmitters();
    this._cdr.detectChanges();

    if (!this.isAddMode) {
      this._ordemServicoSTNService.obterPorCodigo(this.codAtendimento)
        .pipe(first())
        .subscribe(data => {
          
        });
    }
  }

  private registrarEmitters() {
    fromEvent(this.codOSInputControl.nativeElement, 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(1000)
			, distinctUntilChanged()
		).subscribe((query: number) => {
			this.obterDados(query);
		});
  }

  private async obterDados(codOS: number) {
    this.isLoading = true;
    this.os = await this._ordemServicoService.obterPorCodigo(codOS).toPromise();
    this.rat = this.os?.relatoriosAtendimento[this.os.relatoriosAtendimento.length-1];
    this.atendimentos = (await this._ordemServicoSTNService.obterPorParametros({ codOS: this.os.codOS }).toPromise()).items;
    this.isLoading = false;
  }

  abrirHistorico() {
    const dialogRef = this._dialog.open(OrdemServicoStnHistoricoComponent, {
      data: {}
    });

    dialogRef.afterClosed().subscribe(() => {
      
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
   

    // let obj = {
    //   ...this.turno,
    //   ...form,
    //   ...{
    //     horaInicio1: moment().format(`yyyy-MM-DD ${form.horaInicio1}`),
    //     horaFim1: moment().format(`yyyy-MM-DD ${form.horaFim1}`),
    //     horaInicio2: moment().format(`yyyy-MM-DD ${form.horaInicio2}`),
    //     horaFim2: moment().format(`yyyy-MM-DD ${form.horaFim2}`),
    //     dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
    //     codUsuarioManut: this.userSession.usuario.codUsuario,
    //     indAtivo: +form.indAtivo
    //   }
    // };
    
    // this._turnoService.atualizar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Turno atualizado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  criar(): void {
        //const form: any = this.form.getRawValue();

        let obj: OrdemServicoSTN = {
          ...this.ordemServicoSTN,
          //...form,
          ...{
            codAtendimento: 0,
            CodOS: this.os.codOS,
            DataHoraAberturaSTN: moment().format('YYYY-MM-DD HH:mm:ss'),
            DataHoraFechamentoSTN: null,
            CodStatusSTN: 2,
            CodTipoCausa: null,
            CodGrupoCausa: null,
            CodDefeito: null,
            CodCausa: null,
            CodAcao: null,
            CodTecnico: 'SEM TRANSFERENCIA',
            CodUsuarioCad: 'ealmanca',
            CodUsuarioManut: null,
            CodOrigemChamadoSTN: null,
            IndAtivo: 1,
            NumReincidenciaAoAssumir: 0,
            DataHoraManut: null,
            NumTratativas: 0,
            IndEvitaPendencia: null,
            IndPrimeiraLigacao: null,
            NomeSolicitante: null,
            ObsSistema: null
          }
        };    

        this._ordemServicoSTNService.criar(obj).subscribe(() => {

          this._snack.exibirToast(`Chamado STN adicionado com sucesso!`, "success");
          this._location.back();
        });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}