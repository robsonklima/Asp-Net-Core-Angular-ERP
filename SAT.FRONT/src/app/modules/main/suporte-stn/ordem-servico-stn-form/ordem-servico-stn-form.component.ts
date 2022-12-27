import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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
import { first } from 'rxjs/internal/operators/first';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { FormBuilder, FormGroup } from '@angular/forms';
import { StatusServicoSTN, StatusServicoSTNParameters } from 'app/core/types/status-servico-stn.types';
import { StatusServicoSTNService } from 'app/core/services/status-servico-stn.service';
import { ProtocoloSTN } from 'app/core/types/protocolo-stn.types';
import { OrdemServicoSTNOrigem, OrdemServicoSTNOrigemParameters } from 'app/core/types/ordem-servico-stn-origem.types';
import { OrdemServicoSTNOrigemService } from 'app/core/services/ordem-servico-stn-origem.service';
import { statusConst } from 'app/core/types/status-types';
import { Laudo } from 'app/core/types/laudo.types';
import { LaudoService } from 'app/core/services/laudo.service';
@Component({
  selector: 'app-ordem-servico-stn-form',
  templateUrl: './ordem-servico-stn-form.component.html'
})
export class OrdemServicoStnFormComponent implements AfterViewInit {
  codAtendimento: number;
  statusServicosSTN: StatusServicoSTN[] = [];
  os: OrdemServico;
  atendimentos: OrdemServicoSTN;
  ultimoAtendimento: OrdemServicoSTN;
  ordemServicoSTN: OrdemServicoSTN;
  ordemServicoSTNOrigens: OrdemServicoSTNOrigem[] = [];
  protocoloSTN: ProtocoloSTN;
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
    private _formBuilder: FormBuilder,
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _ordemServicoService: OrdemServicoService,
    private _statusServicoSTNService: StatusServicoSTNService,
    private _ordemServicoSTNOrigemService: OrdemServicoSTNOrigemService,
    private _laudoService: LaudoService,
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _router: Router
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.codAtendimento = +this._route.snapshot.paramMap.get('codAtendimento');
    this.isAddMode = !this.codAtendimento;
    this.atendimentos = (await this._ordemServicoSTNService.obterPorParametros({ codAtendimento: this.codAtendimento }).toPromise()).items.shift();
    this.inicializarForm();
    this.registrarEmitters();  
    this.obterDados(this.atendimentos.codOS);
    
    if (!this.isAddMode) {
      this._ordemServicoSTNService.obterPorCodigo(this.codAtendimento)
        .pipe(first())
        .subscribe(() => {
          
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
    this.ultimoAtendimento = this.atendimentos;   
    this.obterOrdemServicoSTNOrigem();
    this.obterStatusServicosSTN();
    this.obterLaudo();
    this.isLoading = false;
    this._cdr.detectChanges();    
  }

	private inicializarForm(): void {
		this.form = this._formBuilder.group({
			codStatusServicoSTN: [undefined],
      indPrimeiraLigacao: [undefined],
      nomeSolicitante: [undefined],
      CodOrigemChamadoSTN: [undefined]
		});
	}

  private async obterOrdemServicoSTNOrigem() {
    const params: OrdemServicoSTNOrigemParameters = {
      sortActive: 'descOrigemChamadoSTN',
      sortDirection: 'asc'
    }

    const data = await this._ordemServicoSTNOrigemService.obterPorParametros(params).toPromise();
    this.ordemServicoSTNOrigens = data.items;    
  }  

  private async obterStatusServicosSTN() {
    const params: StatusServicoSTNParameters = {
      sortActive: 'descStatusServicoSTN',
      sortDirection: 'asc'
    }

    const data = await this._statusServicoSTNService.obterPorParametros(params).toPromise();
    this.statusServicosSTN = data.items;    
  }  

  async obterLaudo(){
    this.laudo = (await this._laudoService.obterPorParametros({
      codOS: this.atendimentos.codOS
    }).toPromise()).items.shift();
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();  
  }

  atualizar(): void {
    this._snack.exibirToast(`Atualização!`, "success");
  }

  criar(): void {     
        let obj: OrdemServicoSTN = {
          ...this.ordemServicoSTN,
          ...{
            CodOS: this.os.codOS,
            DataHoraAberturaSTN: moment().format('YYYY-MM-DD HH:mm:ss'),
            CodStatusSTN: 2,
            CodTecnico: 'SEM TRANSFERENCIA',
            CodUsuarioCad: this.userSession.usuario.codUsuario,
            IndAtivo: statusConst.ATIVO,
            NumReincidenciaAoAssumir: 0,
            NumTratativas: 0,
          }
        };    

        this._ordemServicoSTNService.criar(obj).subscribe((atendimento) => {
          this._snack.exibirToast(`Chamado STN adicionado com sucesso!`, "success");
          this._router.navigate(['/suporte-stn/form/' + atendimento.codAtendimento]);
        });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}