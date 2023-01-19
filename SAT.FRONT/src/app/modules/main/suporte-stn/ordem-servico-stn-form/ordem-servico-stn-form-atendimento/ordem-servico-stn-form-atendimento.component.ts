import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Router } from '@angular/router';
import { CausaImprodutividadeService } from 'app/core/services/causa-improdutividade.service';
import { CausaService } from 'app/core/services/causa.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ImprodutividadeService } from 'app/core/services/improdutividade.service';
import { OrdemServicoSTNOrigemService } from 'app/core/services/ordem-servico-stn-origem.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { ProtocoloChamadoSTNService } from 'app/core/services/protocolo-chamado-stn.service';
import { StatusServicoSTNService } from 'app/core/services/status-servico-stn.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { TipoChamadoSTNService } from 'app/core/services/tipo-chamado-stn.service';
import { TipoServicoService } from 'app/core/services/tipo-servico.service';
import { CausaImprodutividade } from 'app/core/types/causa-improdutividade.types';
import { Causa } from 'app/core/types/causa.types';
import { Improdutividade } from 'app/core/types/improdutividade.types';
import { OrdemServicoSTNOrigem } from 'app/core/types/ordem-servico-stn-origem.types';
import { OrdemServicoSTN } from 'app/core/types/ordem-servico-stn.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { ProtocoloChamadoSTN } from 'app/core/types/protocolo-chamado-stn.types';
import { StatusServicoSTN } from 'app/core/types/status-servico-stn.types';
import { statusConst } from 'app/core/types/status-types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { TipoChamadoSTN } from 'app/core/types/tipo-chamado-stn.types';
import { TipoServico } from 'app/core/types/tipo-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import moment from 'moment';
import { forkJoin, Subject } from 'rxjs';
import { debounceTime, filter, map, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-ordem-servico-stn-form-atendimento',
  templateUrl: './ordem-servico-stn-form-atendimento.component.html'
})
export class OrdemServicoStnFormAtendimentoComponent implements OnInit {
  @Input() codAtendimento: number;
  atendimento: OrdemServicoSTN;
  protocolo: ProtocoloChamadoSTN;
  os: OrdemServico;
  checkFilial: Boolean;
  filialTecnicos: string;
  tecnicos: Tecnico[] = [];
  userSession: UsuarioSessao;
  form: FormGroup;
  isAddMode: boolean;
  searching: boolean;
  causas: Causa[] = [];
  tiposCausas: TipoServico[] = [];
  status: StatusServicoSTN[] = [];
  tipoChamadoSTN: TipoChamadoSTN[] = [];
  origens: OrdemServicoSTNOrigem[] = [];
  improdutividades: Improdutividade[] = [];
  causaImprodutividade: CausaImprodutividade[] = [];
  causasFiltro: FormControl = new FormControl();
  tipoCausaFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _statusSTNService: StatusServicoSTNService,
    private _protocoloChamadoSTNService: ProtocoloChamadoSTNService,
    private _tipoChamadoSTNService: TipoChamadoSTNService,
    private _ordemServicoSTNOrigemSrv: OrdemServicoSTNOrigemService,
    private _tipoServicoService: TipoServicoService,
    private _causaService: CausaService,
    private _causaImprodutividadeService: CausaImprodutividadeService,
    private _improdutividadeService: ImprodutividadeService,
    private _ordemServicoService: OrdemServicoService,
    private _tecnicoService: TecnicoService,
    private _formBuilder: FormBuilder,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
    private _router: Router
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();
    this.registrarEmitters();
   
    this.atendimento = await this._ordemServicoSTNService.obterPorCodigo(this.codAtendimento).toPromise();
    this.protocolo = (await this._protocoloChamadoSTNService.obterPorParametros({ codAtendimento: this.codAtendimento }).toPromise()).items.shift(); 
    this.improdutividades = (await this._improdutividadeService.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
    this.os = await this._ordemServicoService.obterPorCodigo(this.atendimento.codOS).toPromise();
    this.isAddMode = !this.protocolo;

    if(this.isAddMode)
      this.criarProtocolo();

    this.obterDados();
  }

  async obterDados(){
    this.causaImprodutividade = (await this._causaImprodutividadeService.obterPorParametros({ codProtocolo: this.protocolo.codProtocoloChamadoSTN }).toPromise()).items;    

    this.preencherForm();
    this.obterTecnicos();
    this.obterOrigens();
    this.obterTipoCausa();
    this.obterTipoChamados();
    this.obterStatus();
    this.obterCausas();
  }

  private preencherForm(): void {
    this.form.controls['codAtendimento'].setValue(this.atendimento?.codAtendimento);
    this.form.controls['dataHoraAberturaSTN'].setValue(this.atendimento?.dataHoraAberturaSTN);
    this.form.controls['codOrigemChamadoSTN'].setValue(this.atendimento?.codOrigemChamadoSTN);
    this.form.controls['codStatusSTN'].setValue(this.atendimento?.codStatusSTN);
    this.form.controls['codTipoChamadoSTN'].setValue(this.protocolo?.codTipoChamadoSTN);
    this.form.controls['nomeUsuario'].setValue(this.atendimento?.usuario?.nomeUsuario);
    this.form.controls['codTecnicos'].setValue(this.os?.tecnico?.nome);
    this.form.controls['codTipoCausa'].setValue(this.atendimento?.codTipoCausa);
    this.form.controls['codDefeito'].setValue(this.atendimento?.codDefeito);
    this.form.controls['acaoSTN'].setValue(this.protocolo?.acaoSTN);

  }
  
  inicializarForm() {
    this.form = this._formBuilder.group({
      codAtendimento: [undefined],
      dataHoraAberturaSTN: [undefined],
      codOrigemChamadoSTN: [undefined],
      codStatusSTN: [undefined],
      codTipoChamadoSTN:[undefined],
      nomeUsuario: [undefined],
      codTecnicos: [undefined],
      codTipoCausa: [undefined],
      acaoSTN: [undefined],
      codDefeito: [undefined]
    });
  }

  registrarEmitters() {
    this.tipoCausaFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          const data = await this._tipoServicoService.obterPorParametros({
            sortActive: 'codETipoServico',
            sortDirection: 'asc',
            filter: query,
            pageSize: 100,
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.tiposCausas = await data;
      });

    this.causasFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          const data = await this._causaService.obterPorParametros({
            sortActive: 'codECausa',
            sortDirection: 'asc',
            indAtivo: statusConst.ATIVO,
            filter: query,
            pageSize: 100,
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.causas = await data;
      });
  }

  criarProtocolo(){
    let obj: ProtocoloChamadoSTN = {
      ...{
        codAtendimento: this.atendimento.codAtendimento,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario,
        indAtivo: statusConst.ATIVO
      }
    };

    this._protocoloChamadoSTNService.criar(obj).subscribe((atendimento) => {
      this.ngOnInit();
    });
  }

  async obterTecnicos(){
    this.tecnicos = (await this._tecnicoService.obterPorParametros({
      codFiliais: this.filialTecnicos,
      codPerfil: PerfilEnum.FILIAL_TECNICO_DE_CAMPO,
      sortActive: 'nome',
      sortDirection: 'asc',
      indAtivo: statusConst.ATIVO
    }).toPromise()).items;
  }

  async obterCausas() {
    this.causas = (await this._causaService
      .obterPorParametros({ 
        sortActive: 'nomeCausa',
        sortDirection: 'asc',
        indAtivo: statusConst.ATIVO
       })
      .toPromise()).items;
  }

  async obterOrigens() {
    this.origens = (await this._ordemServicoSTNOrigemSrv.obterPorParametros({
      sortActive: 'descOrigemChamadoSTN',
      sortDirection: 'asc',
    }).toPromise()).items;
  }

  async obterTipoCausa() {
    this.tiposCausas = (await this._tipoServicoService.obterPorParametros({ 
        sortActive: 'nomeServico',
        sortDirection: 'asc',
        pageSize: 100,
     }).toPromise()).items;
  }

  async obterStatus() {
    this.status = (await this._statusSTNService.obterPorParametros({
      sortDirection: 'asc',
      pageSize: 100,
    }).toPromise()).items;
  }

  async obterTipoChamados() {
    this.tipoChamadoSTN = (await this._tipoChamadoSTNService.obterPorParametros({
      indAtivo: statusConst.ATIVO
    }).toPromise()).items;
  }

  async onChange($event: MatSlideToggleChange, codigo) {
    if ($event.checked) {
      this._causaImprodutividadeService.criar({
        codImprodutividade: codigo,
        codProtocolo: this.protocolo.codProtocoloChamadoSTN,
        indAtivo: statusConst.ATIVO
      }).subscribe();
    } else {
      const causaImprodutividade = (await this._causaImprodutividadeService.obterPorParametros({
        codProtocolo: this.protocolo.codProtocoloChamadoSTN,
        codImprodutividade: codigo
      }).toPromise()).items.shift();

      this._causaImprodutividadeService.deletar(causaImprodutividade.codCausaImprodutividade).subscribe();
    }
  }

  async onChangePendencia($event: MatSlideToggleChange) {
    if ($event.checked) {
      this.atendimento.indEvitaPendencia = 1;
    }
    else {
      this.atendimento.indEvitaPendencia = 0;
    }
  }

  async onChangeFilial($event: MatSlideToggleChange) {
    if ($event.checked) {
      this.filialTecnicos = this.os.codFilial.toString(); 
      this.checkFilial = true;
    }
    else {
      this.filialTecnicos = null;
      this.checkFilial = false;     
    }

    this.obterTecnicos();
  }

  public verificarSelecionado(codImprodutividade: number): boolean {
    return _.find(this.causaImprodutividade, { codImprodutividade: codImprodutividade, codProtocolo: this.protocolo?.codProtocoloChamadoSTN }) != null;
  }

  salvar(){

    const form: any = this.form.getRawValue();
		
    let atendimento: OrdemServicoSTN = {
			...this.atendimento,
			...form,
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario?.codUsuario,
        dataHoraFechamentoSTN:moment().format('YYYY-MM-DD HH:mm:ss')
			}
		};

    let protocolo: ProtocoloChamadoSTN = {
			...this.protocolo,
			...form,
			...{
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario?.codUsuario,
        indAtivo: statusConst.ATIVO,
        acaoSTN: this.form?.controls['acaoSTN']?.value +  moment().format(' DD/MM HH:mm') + ' * ',
        tecnicoCampo: this.form.controls['codTecnicos']?.value
			}
		};

    forkJoin([
      this._ordemServicoSTNService.atualizar(atendimento),
      this._protocoloChamadoSTNService.atualizar(protocolo),
    ]).subscribe(([result1, result2]) => {
        this._snack.exibirToast('Atendimento atualizado com sucesso','sucess');
        this._router.navigate(['suporte-stn/form/' + atendimento.codAtendimento]);
      });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
