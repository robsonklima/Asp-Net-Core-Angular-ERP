import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent, Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { OrdemServicoStnHistoricoComponent } from '../ordem-servico-stn-historico/ordem-servico-stn-historico.component';

@Component({
  selector: 'app-ordem-servico-stn-form',
  templateUrl: './ordem-servico-stn-form.component.html'
})
export class OrdemServicoStnFormComponent implements AfterViewInit {
  codAtendimento: number;
  os: OrdemServico;
  rat: RelatorioAtendimento;
  isAddMode: boolean;
  isLoading: boolean = false;
  userSession: UsuarioSessao;
  @ViewChild('codOSInputControl') codOSInputControl: ElementRef;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _ordemServicoService: OrdemServicoService,
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _dialog: MatDialog
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.codAtendimento = +this._route.snapshot.paramMap.get('codAtendimento');
    this.isAddMode = !this.codAtendimento;

    this.obterOS(6886015);

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
			this.obterOS(query);
		});
  }

  private async obterOS(codOS: number) {
    this.isLoading = true;
    this.os = await this._ordemServicoService.obterPorCodigo(codOS).toPromise();
    this.rat = this.os?.relatoriosAtendimento[this.os.relatoriosAtendimento.length-1];
    console.log(this.os);
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
    // const form: any = this.form.getRawValue();

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
    // const form = this.form.getRawValue();

    // let obj = {
    //   ...this.turno,
    //   ...form,
    //   ...{
    //     horaInicio1: moment().format(`yyyy-MM-DD ${form.horaInicio1}`),
    //     horaFim1: moment().format(`yyyy-MM-DD ${form.horaFim1}`),
    //     horaInicio2: moment().format(`yyyy-MM-DD ${form.horaInicio2}`),
    //     horaFim2: moment().format(`yyyy-MM-DD ${form.horaFim2}`),
    //     dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
    //     codUsuarioCad: this.userSession.usuario.codUsuario,
    //     indAtivo: +form.indAtivo
    //   }
    // };

    // this._turnoService.criar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Turno adicionado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}