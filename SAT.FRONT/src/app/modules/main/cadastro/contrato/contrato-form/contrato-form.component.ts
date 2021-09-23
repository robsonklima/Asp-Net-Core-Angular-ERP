import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { ContratoSLAService } from 'app/core/services/contrato-sla.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { FilialService } from 'app/core/services/filial.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Autorizada } from 'app/core/types/autorizada.types';
import { Cliente } from 'app/core/types/cliente.types';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { Filial } from 'app/core/types/filial.types';
import { LocalAtendimento } from 'app/core/types/local-atendimento.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { RegiaoAutorizada } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { TipoIntervencao, tipoIntervencaoConst } from 'app/core/types/tipo-intervencao.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { filter, tap, map, delay } from 'lodash';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-contrato-form',
  templateUrl: './contrato-form.component.html',
  styleUrls: ['./contrato-form.component.scss']
})
export class ContratoFormComponent implements OnInit {codContrato: number;
  contratoServico: ContratoService;
  form: FormGroup;
  isAddMode: boolean;
  perfis: any;
  userSession: UsuarioSessao;
  clientes: Cliente[] = [];
  searching: boolean;
  protected _onDestroy = new Subject<void>();
  tipoContrato = [
    "Manutenção",
    "Garantia",
    "Locação",
    "Ext Garantia",
    "Locação Outros",
    "Demonstração"
  ];
  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _userService: UserService,
    private _contratoService: ContratoService,
    private _snack: CustomSnackbarService,
    private _clienteService: ClienteService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
    this.isAddMode = !this.codContrato;
    this.inicializarForm();

    this.perfis = PerfilEnum;
    
    // Init
    this.obterClientes();

    // Main Obj
    await this.obterContrato();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codContrato: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      numOSCliente: [undefined],
      numOSQuarteirizada: [undefined],
      nomeSolicitante: [undefined],
      nomeContato: [undefined],
      telefoneSolicitante: [undefined],
      codCliente: [undefined, Validators.required],
      codTipoIntervencao: [undefined, Validators.required],
      codPosto: [undefined, Validators.required],
      defeitoRelatado: [undefined, Validators.required],
      codEquipContrato: [undefined],
      codEquip: [undefined],
      codFilial: [undefined, Validators.required],
      codRegiao: [undefined, Validators.required],
      codAutorizada: [undefined, Validators.required],
      indLiberacaoFechaduraCofre: [undefined],
      indIntegracao: [undefined],
      observacaoCliente: [undefined],
      descMotivoMarcaEspecial: [undefined],
      agenciaPosto: [undefined]
    });
  }
 
  private async obterContrato() {
    if (!this.isAddMode) {
      //this.contratoServico = await this._contratoService.obterPorCodigo(this.codContrato).toPromise();
      this.form.patchValue(this.contratoServico);
    } 
  }

  private async obterClientes(filter: string = '') {
    this.clientes = (await this._clienteService.obterPorParametros({
      filter: filter,
      indAtivo: 1,
      pageSize: 500,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc'
    }).toPromise()).items;
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private atualizar(): void {
    this.form.disable();

    const form: any = this.form.getRawValue();
    let obj = {
      ...this.contratoServico,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario?.codUsuario
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._contratoService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Chamado atualizado com sucesso!", "success");
      this._router.navigate(['ordem-servico/detalhe/'+this.codContrato]);
    });
  }

  private criar(): void {
    const form: any = this.form.getRawValue();
    let obj = {
      ...this.contratoServico,
      ...form,
      ...{
        dataHoraSolicitacao: moment().format('YYYY-MM-DD HH:mm:ss'),
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        dataHoraAberturaOS: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario,
        indStatusEnvioReincidencia: -1,
        indRevisaoReincidencia: 1,
        codStatusServico: 1
      }
    };

    Object.keys(obj).forEach((key) => {
      typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
    });

    this._contratoService.criar(obj).subscribe((os) => {
      this._snack.exibirToast("Registro adicionado com sucesso!", "success");
      this._router.navigate(['ordem-servico/detalhe/' + os.codContrato]);
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
