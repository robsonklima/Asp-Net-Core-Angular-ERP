import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { CidadeService } from 'app/core/services/cidade.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { FilialService } from 'app/core/services/filial.service';
import { GoogleGeolocationService } from 'app/core/services/google-geolocation.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { PaisService } from 'app/core/services/pais.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { GoogleGeolocation } from 'app/core/types/google-geolocation.types';
import { LocalAtendimento } from 'app/core/types/local-atendimento.types';
import { Pais, PaisParameters } from 'app/core/types/pais.types';
import { Regiao } from 'app/core/types/regiao.types';
import { TipoRota, TipoRotaEnum } from 'app/core/types/tipo-rota.types';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-local-atendimento-form',
  templateUrl: './local-atendimento-form.component.html'
})
export class LocalAtendimentoFormComponent implements OnInit, OnDestroy {
  codPosto: number;
  local: LocalAtendimento;
  form: FormGroup;
  isAddMode: boolean;
  paises: Pais[] = [];
  ufs: UnidadeFederativa[] = [];
  cidades: Cidade[] = [];
  cidadesFiltro: FormControl = new FormControl();
  clientes: Cliente[] = [];
  regioes: Regiao[] = [];
  regioesFiltro: FormControl = new FormControl();
  autorizadas: Autorizada[] = [];
  autorizadasFiltro: FormControl = new FormControl();
  tiposRota: TipoRota[] = [];
  filiais: Filial[] = [];
  equipamentosContrato: EquipamentoContrato[] = []; 
  userSession: UsuarioSessionData;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _router: Router,
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _paisService: PaisService,
    private _ufService: UnidadeFederativaService,
    private _cidadeService: CidadeService,
    private _localService: LocalAtendimentoService,
    private _autorizadaService: AutorizadaService,
    private _clienteService: ClienteService,
    private _filialService: FilialService,
    private _googleGeolocationService: GoogleGeolocationService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _equipamentoContratoService: EquipamentoContratoService,
    private _dialog: MatDialog
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codPosto = +this._route.snapshot.paramMap.get('codPosto');
    this.isAddMode = !this.codPosto;
    this.inicializarForm();

    this.obterPaises();
    this.obterClientes();
    this.obterFiliais();
    this.obterTiposRota();
    this.obterEquipamentosContrato();
    
    this.form.controls['codPais'].valueChanges.subscribe(async () => {
      this.obterUFs();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async () => {
      this.obterCidades();
    });

    this.form.controls['codCidade'].valueChanges.subscribe(async () => {
      this.form.controls['cep'].enable();
    });

    this.form.controls['codFilial'].valueChanges.subscribe(async () => {
      this.obterAutorizadas();
    });

    this.form.controls['codAutorizada'].valueChanges.subscribe(async () => {
      this.obterRegioes();
    });

    this.form.controls['cep'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text => { 
        if (text.length === 9) {
          const cep = this.form.controls['cep']?.value || '';
          
          this.obterLatLngPorEndereco(cep);
        }
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['numeroEnd'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text => { 
        const endereco = this.form.controls['endereco']?.value || '';
        const numero = this.form.controls['numeroEnd']?.value || '';
        const codCidade = this.form.controls['codCidade'].value;
        const cidade = (await this._cidadeService.obterPorCodigo(codCidade).toPromise());
        const query = `${endereco}, ${numero}, ${cidade.nomeCidade}`;
        this.obterLatLngPorEndereco(query);
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.cidadesFiltro.valueChanges.pipe(
      filter(filtro => !!filtro),
      tap(() => { }),
      debounceTime(700),
      map(async filtro => { this.obterCidades(filtro) }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    if (!this.isAddMode) {
      this.obterLocal();
    }
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codPosto: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      nomeLocal: [undefined, [Validators.required]],
      codPais: [undefined, Validators.required],
      codUF: [undefined, Validators.required],
      codCidade: [undefined, Validators.required],
      codCliente: [undefined, Validators.required],
      dcPosto: [undefined, [Validators.required, Validators.maxLength(2)]],
      numAgencia: [undefined, [Validators.required, Validators.maxLength(5)]],
      cnpj: [undefined, Validators.required],
      inscricaoEstadual: [undefined],
      cep: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      endereco: [undefined, Validators.required],
      enderecoComplemento: [undefined],
      bairro: [undefined, Validators.required],
      email: [undefined],
      site: [undefined],
      fone: [undefined],
      descTurno: [undefined],
      distanciaKmPatRes: [undefined],
      observacao: [undefined],
      numeroEnd: [undefined, Validators.required],
      cnpjFaturamento: [undefined],
      codRegiao: [undefined, Validators.required],
      codAutorizada: [
        {
          value: undefined,
          disabled: true,
        }, Validators.required
      ],
      codFilial: [undefined, Validators.required],
      codTipoRota: [undefined],
      latitude: [
        {
          value: undefined,
          disabled: true,
        }, Validators.required
      ],
      longitude: [
        {
          value: undefined,
          disabled: true,
        }, Validators.required
      ],
      indAtivo: [undefined]
    });
  }

  private async obterLocal() {
    this.local = await this._localService.obterPorCodigo(this.codPosto).toPromise();
    this.form.patchValue(this.local);
    this.form.controls['codPais'].setValue(this.local.cidade?.unidadeFederativa?.codPais);
    this.form.controls['codUF'].setValue(this.local.cidade?.unidadeFederativa?.codUF);
  }

  private async obterPaises() {
    const params: PaisParameters = {
      sortActive: 'nomePais',
      sortDirection: 'asc',
      pageSize: 200
    }

    const data = await this._paisService.obterPorParametros(params).toPromise();
    this.paises = data.paises;
  }

  private async obterUFs() {
    const codPais = this.form.controls['codPais'].value;

    const params: UnidadeFederativaParameters = {
      sortActive: 'nomeUF',
      sortDirection: 'asc',
      codPais: codPais,
      pageSize: 50
  }

    const data = await this._ufService.obterPorParametros(params).toPromise();
    this.ufs = data.unidadesFederativas;
  }

  private async obterCidades(filtro: string = '') {
    const codUF = this.form.controls['codUF'].value;

    const params: CidadeParameters = {
      sortActive: 'nomeCidade',
      sortDirection: 'asc',
      indAtivo: 1,
      codUF: codUF,
      pageSize: 1000,
      filter: filtro
    }

    const data = await this._cidadeService.obterPorParametros(params).toPromise();
    this.cidades = data.cidades;
  }

  private async obterClientes() {
    const params: ClienteParameters = {
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      indAtivo: 1,
      pageSize: 50
    }

    const data = await this._clienteService.obterPorParametros(params).toPromise();
    this.clientes = data.clientes;
  }

  private async obterFiliais() {
    const params: FilialParameters = {
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      indAtivo: 1,
      pageSize: 50
    }

    const data = await this._filialService.obterPorParametros(params).toPromise();
    this.filiais = data.filiais;
  }

  private async obterAutorizadas() {
    const params: AutorizadaParameters = {
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      indAtivo: 1,
      codFilial: this.form.controls['codFilial'].value,
      pageSize: 50
    }

    const data = await this._autorizadaService.obterPorParametros(params).toPromise();
    this.autorizadas = data.autorizadas;
  }

  private async obterRegioes() {
    const codAutorizada = this.form.controls['codAutorizada'].value;

    const data = await this._regiaoAutorizadaService.obterPorParametros({
      codAutorizada: codAutorizada,
      pageSize: 100
    }).toPromise();

    this.regioes = data.regioesAutorizadas
      .filter(ra => ra.codAutorizada === codAutorizada)
      .map(ra => ra.regiao);
  }

  private async obterEquipamentosContrato() {
    const data = await this._equipamentoContratoService.obterPorParametros({codPosto: this.codPosto}).toPromise();
    this.equipamentosContrato = data.equipamentosContrato;
  }

  private async obterLatLngPorEndereco(end: string) {
    this._googleGeolocationService.buscarPorEnderecoOuCEP(end.trim()).subscribe((data: GoogleGeolocation ) => {
      if (data && data.results.length > 0) {
        const res = data.results.shift();

        this.form.controls['endereco'].setValue(res.formatted_address);
        this.form.controls['latitude'].setValue(res.geometry.location.lat);
        this.form.controls['longitude'].setValue(res.geometry.location.lng);
        
        const bairros: any = res.address_components.filter(ac => ac.types.includes('sublocality'));
        this.form.controls['bairro'].setValue(bairros.shift()?.long_name);
      }            
    });
  }

  private obterTiposRota(): void {
    const tiposRota = Object.keys(TipoRotaEnum).filter((element) => {
      return isNaN(Number(element));
    });

    tiposRota.forEach((tr, i) => {
      this.tiposRota.push({
        codTipoRota: i + 1,
        nomeTipoRota: tr
      })
    });
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  async deletar() {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja excluir este local?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        if (this.equipamentosContrato.length) {
          this._snack.exibirToast('Este local possui equipamentos cadastrados', 'error');
          return;
        }

        this._localService.deletar(this.codPosto).subscribe(() => {
          this._snack.exibirToast('Local removido com sucesso!', 'success');
          this._router.navigate(['/local-atendimento']);
        }, e => {
          this._snack.exibirToast('Erro ao remover local', 'error');
        })
      }
    });
  }

  private atualizar(): void {
    this.form.disable();

    const form: any = this.form.getRawValue();
    let obj = {
      ...this.local,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        cep: form.cep.replace('-', ''),
        indAtivo: form.indAtivo ? 1 : 0
      }
    };

    this._localService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Chamado atualizado com sucesso!", "success");
      this._router.navigate(['local-atendimento']);
    });
  }

  private criar(): void {
    const form: any = this.form.getRawValue();
    let obj = {
      ...this.local,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        cep: form.cep.replace('-', ''),
        indAtivo: form.indAtivo ? 1 : 0
      }
    };

    this._localService.criar(obj).subscribe((os) => {
      this._snack.exibirToast("Registro adicionado com sucesso!", "success");
      this._router.navigate(['local-atendimento']);
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
