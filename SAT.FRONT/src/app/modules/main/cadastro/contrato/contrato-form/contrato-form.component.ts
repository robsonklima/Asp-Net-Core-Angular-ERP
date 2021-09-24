import { Contrato } from './../../../../../core/types/contrato.types';
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
  contrato: Contrato;
  form: FormGroup;
  isAddMode: boolean;
  perfis: any;
  userSession: UsuarioSessao;
  clientes: Cliente[] = [];
  searching: boolean;
  protected _onDestroy = new Subject<void>();
  tipoContrato;
  tipoReajuste;

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
    this.obterTipoContrato();
    this.obterTipoReajuste();

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
      codContratoPai:[undefined],
      codCliente: [undefined, Validators.required],
      codTipoContrato: [undefined, Validators.required],
      codTipoReajuste: [undefined, Validators.required],
      percReajuste: [undefined, Validators.required],
      nroContrato: [undefined, Validators.required],
      nomeContrato: [undefined, Validators.required],
      dataContrato: [undefined, Validators.required],
      dataAssinatura:[undefined, Validators.required],
      dataInicioVigencia: [undefined, Validators.required],
      dataFimVigencia: [undefined, Validators.required],
      dataInicioPeriodoReajuste:[undefined, Validators.required],
      dataFimPeriodoReajuste: [undefined, Validators.required],
      nomeResponsavelPerto: [undefined, Validators.required],
      nomeResponsavelCliente: [undefined, Validators.required],
      objetoContrato: [undefined],
      semCobertura: [undefined],
      valTotalContrato: [undefined, Validators.required],
      indPermitePecaEspecifica: [undefined, Validators.required],
      numDiasSubstEquip: [undefined, Validators.required]

    });
  }
 
  private async obterTipoContrato() {
    this.tipoContrato = [
      {
        "codTipoContrato": 1,
        "nomeTipoContrato": "Manutenção"
      },
      {
        "codTipoContrato": 2,
        "nomeTipoContrato": "Garantia"
      },
      {
        "codTipoContrato": 3,
        "nomeTipoContrato": "Locação"
      },
      {
        "codTipoContrato": 4,
        "nomeTipoContrato": "Ext Garantia"
      },
      {
        "codTipoContrato": 5,
        "nomeTipoContrato": "Locação Outros"
      },
      {
        "codTipoContrato": 6,
        "nomeTipoContrato": "Demonstração"
      }
    ];
  }
  private async obterTipoReajuste() {
    this.tipoReajuste = [
      {
        "codTipoReajuste": 1,
        "nomeTipoReajuste": "IGPM-FGV"
      },
      {
        "codTipoReajuste": 2,
        "nomeTipoReajuste": "IPC-FIPE"
      },
      {
        "codTipoReajuste": 3,
        "nomeTipoReajuste": "INPC"
      },
      {
        "codTipoReajuste": 4,
        "nomeTipoReajuste": "IPCA-IBGE"
      },
      {
        "codTipoReajuste": 5,
        "nomeTipoReajuste": "Acorde entre as Partes"
      }
    ];
  }
  private async obterContrato() {
    if (!this.isAddMode) {
      let data = await this._contratoService.obterPorCodigo(this.codContrato).toPromise();
      console.log(data)
      this.contrato = data;
      this.form.patchValue(this.contrato);
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
      ...this.contrato,
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
      ...this.contrato,
      ...form,
      ...{
        dataHoraSolicitacao: moment().format('YYYY-MM-DD HH:mm:ss'),
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario?.codUsuario,
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
