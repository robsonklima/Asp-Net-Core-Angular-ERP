import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServicoSTN } from 'app/core/types/ordem-servico-stn.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { FormGroup } from '@angular/forms';
import { statusConst } from 'app/core/types/status-types';
import { Laudo } from 'app/core/types/laudo.types';
import { LaudoService } from 'app/core/services/laudo.service';
@Component({
  selector: 'app-ordem-servico-stn-form',
  templateUrl: './ordem-servico-stn-form.component.html'
})
export class OrdemServicoStnFormComponent implements AfterViewInit {
  codAtendimento: number;
  os: OrdemServico;
  atendimento: OrdemServicoSTN;
  ultimoAtendimento: OrdemServicoSTN;
  ordemServicoSTN: OrdemServicoSTN;
  laudo: Laudo;
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
    private _laudoService: LaudoService,
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _router: Router
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.codAtendimento = +this._route.snapshot.paramMap.get('codAtendimento');
    this._cdr.detectChanges();

    this.isAddMode = !this.codAtendimento;
    this.atendimento = (await this._ordemServicoSTNService.obterPorParametros({ codAtendimento: this.codAtendimento }).toPromise()).items.shift();
    this.registrarEmitters();
    this.obterDados(this.atendimento.codOS);
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
    if(this.isAddMode)
      this.atendimento = (await this._ordemServicoSTNService.obterPorParametros({ codOS: codOS }).toPromise()).items.shift();
    
    this.obterLaudo();
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  async obterLaudo() {
    this.laudo = (await this._laudoService.obterPorParametros({
      codOS: this.atendimento.codOS
    }).toPromise()).items.shift();
  }

  criar(): void {
    let obj: OrdemServicoSTN = {
      ...this.ordemServicoSTN,
      ...{
        CodOS: this.os.codOS,
        DataHoraAberturaSTN: moment().format('YYYY-MM-DD HH:mm:ss'),
        CodStatusSTN: 1,
        codTecnico: this.userSession.usuario.codUsuario,
        CodUsuarioCad: this.userSession.usuario.codUsuario,
        IndAtivo: statusConst.ATIVO,
      }
    };

    this._ordemServicoSTNService.criar(obj).subscribe((atendimento) => {
      this._snack.exibirToast(`Chamado STN adicionado com sucesso!`, "success");
      this._router.navigate(['/suporte-stn/form/' + atendimento.codAtendimento]);
    });
  }

  validarStatus() {
    if (this.atendimento?.codStatusSTN == (1 || 2))
      return true;

    return false;
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}