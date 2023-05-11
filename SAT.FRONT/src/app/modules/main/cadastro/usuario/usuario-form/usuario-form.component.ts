import { Location } from '@angular/common';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { CargoService } from 'app/core/services/cargo.service';
import { CidadeService } from 'app/core/services/cidade.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FilialService } from 'app/core/services/filial.service';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { PaisService } from 'app/core/services/pais.service';
import { PerfilService } from 'app/core/services/perfil.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { TransportadoraService } from 'app/core/services/transportadora.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Autorizada } from 'app/core/types/autorizada.types';
import { Cargo, CargoParameters } from 'app/core/types/cargo.types';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Contrato } from 'app/core/types/contrato.types';
import { Filial } from 'app/core/types/filial.types';
import { Geolocalizacao, GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';
import { Pais, PaisEnum } from 'app/core/types/pais.types';
import { Perfil, PerfilEnum, PerfilParameters } from 'app/core/types/perfil.types';
import { statusConst } from 'app/core/types/status-types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { Transportadora } from 'app/core/types/transportadora.types';
import { Turno } from 'app/core/types/turno.types';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Utils } from 'app/core/utils/utils';
import Enumerable from 'linq';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, filter, first, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html'
})
export class UsuarioFormComponent implements OnInit, OnDestroy {
  codUsuario: string;
  usuario: Usuario;
  isAddMode: boolean;
  form: FormGroup;
  userSession: UsuarioSessao;
  senhaNaoConfere: boolean;
  formErrors: string[] = [];
  protected _onDestroy = new Subject<void>();

  public codUsuarioExiste: boolean = false;
  public codUsuarioValidado: boolean = false;
  public verificandoUsuarioExiste: boolean = false;
  public loading: boolean = true;
  public usuarioBloqueado: boolean = false;

  public buscandoCEP: boolean = false;
  public paises: Pais[] = [];
  public unidadesFederativas: UnidadeFederativa[] = [];
  public cidades: Cidade[] = [];
  public cargos: Cargo[] = [];
  public perfis: Perfil[] = [];
  public filiais: Filial[] = [];
  public autorizadas: Autorizada[] = [];
  public tecnicos: Tecnico[] = [];
  public clientes: Cliente[] = [];
  public contratos: Contrato[] = [];
  public transportadoras: Transportadora[] = [];
  public turnos: Turno[] = [];

  clienteFilterCtrl: FormControl = new FormControl();
  contratosFilterCtrl: FormControl = new FormControl();
  cidadesFilterCtrl: FormControl = new FormControl();

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _autorizadaService: AutorizadaService,
    private _transportadoraService: TransportadoraService,
    private _filialService: FilialService,
    private _paisService: PaisService,
    private _contratoService: ContratoService,
    private _cidadeService: CidadeService,
    private _clienteService: ClienteService,
    private _userService: UserService,
    private _tecnicoService: TecnicoService,
    private _usuarioService: UserService,
    private _cdr: ChangeDetectorRef,
    private _googleGeolocationService: GeolocalizacaoService,
    private _cargoService: CargoService,
    private _perfilService: PerfilService,
    private _location: Location,
    private _ufService: UnidadeFederativaService,
    private _utils: Utils,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codUsuario = this._route.snapshot.paramMap.get('codUsuario');
    this.senhaNaoConfere = false;
    this.isAddMode = !this.codUsuario;
    this.buscandoCEP = false;
    this.inicializarForm();
    this.registrarEmitters();
        
    if (!this.isAddMode) {
      this.carregarDadosUsuario();
    } else {
      this.clientes = await this.obterClientes();
      this.cargos = await this.obterCargos();
      this.filiais = await this.obterFiliais();
      this.perfis = await this.obterPerfis();
      this.transportadoras = await this.obterTranspotadoras();
      this.tecnicos = await this.obterTecnicos();
      this.paises = await this.obterPaises();
    }

    this.loading = false;
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codUsuario: [undefined],
      nomeUsuario: [undefined, Validators.required],
      cpf: [undefined, Validators.required],
      endereco: [undefined],
      bairro: [undefined],
      cep: [undefined, Validators.required],
      codUF: [undefined],
      codPais: [undefined],
      codCidade: [undefined],
      senha: [undefined, Validators.required],
      confirmarSenha: [undefined, Validators.required],
      indAtivo: [true],
      codAutorizada: [undefined],
      codPerfil: [undefined],
      email: [undefined],
      fone: [undefined],
      ramal: [undefined],
      codCargo: [undefined],
      codFilial: [undefined],
      pat: [undefined],
      perfil: [undefined],
      codTecnico: [undefined],
      codCliente: [undefined],
      codTransportadora: [undefined],
      codPeca: [undefined],
      codContrato: [undefined],
      dataAdmissao: [undefined],
      indPonto: [undefined],
      indPermiteRegistrarEquipPOS: [undefined],
      numCracha: [undefined],
      codTurno: [undefined],
      codFilialPonto: [undefined]
    });
  }

  private registrarEmitters() {
    this.form.controls['nomeUsuario'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text => {
        if (!this.isAddMode) return;

        const splitNome = text.split(' ');

        if (splitNome.length > 1) {
          let codUsuario = this._utils.removerAcentos(splitNome[0]?.toLowerCase() + "." + splitNome[splitNome.length - 1]?.toLowerCase());
          this.form.controls['codUsuario'].disable();
          this.form.controls['codUsuario'].setValue(codUsuario)
          this.form.controls['codUsuario'].enable();
        }

        this.validaCodUsuario();
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['codUsuario'].valueChanges.pipe(
      tap(() => { }),
      debounceTime(700),
      map(async text => {
        this.validaCodUsuario();
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['codPais'].valueChanges.subscribe(async () => {
      this._cdr.detectChanges();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async () => {
      this.obterCidades();
      this._cdr.detectChanges();
    });

    this.form.controls['codFilial'].valueChanges.subscribe(async () => {
      this.autorizadas = [];
      this.autorizadas = await this._autorizadaService.obterAutorizadas(this.form.controls['codFilial'].value);
      this._cdr.detectChanges();
    });

    this.form.controls['senha'].valueChanges.pipe(
      filter(texto => !!texto),
      debounceTime(400),
      delay(200),
      takeUntil(this._onDestroy),
      map(async texto => {
        this.senhaNaoConfere = texto != this.form.controls['confirmarSenha'].value && this.form.controls['confirmarSenha'].value != null;
        this._cdr.detectChanges();
      })
    ).toPromise();

    this.form.controls['confirmarSenha'].valueChanges.pipe(
      filter(texto => !!texto),
      debounceTime(400),
      delay(200),
      takeUntil(this._onDestroy),
      map(async texto => {
        this.senhaNaoConfere = texto != this.form.controls['senha'].value && this.form.controls['senha'].value != null;
        this._cdr.detectChanges();
      })
    ).toPromise();

    this.clienteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterClientes(this.clienteFilterCtrl.value);
      });

    this.form.controls['codCliente'].valueChanges.subscribe(async () => {
      this.contratos = (await this._contratoService
        .obterPorParametros({
          codClientes: this.form.controls['codCliente'].value
        })
        .toPromise()).items;
      this._cdr.detectChanges();
    });

    this.cidadesFilterCtrl.valueChanges.pipe(
      filter(filtro => !!filtro),
      tap(() => { }),
      debounceTime(700),
      map(async filtro => { this.cidades = await this.obterCidades(filtro) }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(async (filtro) => { });
  }

  private async carregarDadosUsuario() {
    this._userService.obterPorCodigo(this.codUsuario)
      .pipe(first())
      .subscribe(async usuario => {
        if (usuario.codTecnico) {
          this._tecnicoService.obterPorParametros({ indAtivo: 1 }).subscribe(tecData => {
            if (tecData) { 
              this.tecnicos = tecData.items;
            }
          });
        }
        
        this.form.controls['codPais'].setValue(PaisEnum.BRASIL);
        this.unidadesFederativas = await this.obterUFs(usuario?.cidade?.unidadeFederativa?.nomeUF);
        this.form.controls['codUF'].setValue(usuario.cidade?.codUF);
        this.cidades = await this.obterCidades(usuario.cidade.nomeCidade);
        this.form.controls['codCidade'].setValue(usuario.codCidade);

        if (usuario.codContrato) {
          let codContratos = usuario.codContrato.split(',').map(i => Number(i));
          this.form.controls['codContrato'].setValue(codContratos);
        }

        this.form.get('dataAdmissao').setValue(new Date(usuario.dataAdmissao).toISOString().split('T')[0]);
        this.form.controls['confirmarSenha'].clearValidators();
        this.form.controls['confirmarSenha'].updateValueAndValidity();
        this.form.controls['senha'].clearValidators();
        this.form.controls['senha'].updateValueAndValidity();

        this.usuarioBloqueado = usuario.usuarioSeguranca?.senhaBloqueada == true;
        this.usuario = usuario;
        this.form.patchValue(usuario);

        this.clientes = await this.obterClientes(usuario.cliente.nomeFantasia);
        this.cargos = await this.obterCargos(usuario.cargo.nomeCargo);
        this.filiais = await this.obterFiliais(usuario.filial.nomeFilial);
        this.perfis = await this.obterPerfis(usuario.perfil.nomePerfil);
        this.transportadoras = await this.obterTranspotadoras(usuario.transportadora?.nomeTransportadora);
        this.tecnicos = await this.obterTecnicos(usuario.tecnico.nome);

      });
  }

  private async obterClientes(filtro: string = ''): Promise<Cliente[]> {
    let params: ClienteParameters = {
      filter: filtro,
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      pageSize: 1000
    };

    return (await this._clienteService.obterPorParametros(params).toPromise()).items;
  }

  private async validaCodUsuario() {
    const codUsuario = this.form.controls['codUsuario'].value;

    if (!codUsuario) {
        this.codUsuarioValidado = false;
        return;
      }

      let validaCodigo = await this._usuarioService.obterPorCodigo(codUsuario).toPromise();
      this.codUsuarioExiste = validaCodigo != null;
      this.codUsuarioValidado = true;
  }

  private async obterCargos(filtro: string=''): Promise<Cargo[]> {
    const params: CargoParameters = {
      sortActive: 'nomeCargo',
      sortDirection: 'asc',
      indAtivo: statusConst.ATIVO,
      pageSize: 1000,
      filter: filtro
    }

    return (await this._cargoService.obterPorParametros(params).toPromise()).items;
  }

  private async obterPerfis(filtro: string=''): Promise<Perfil[]> {
    const params: PerfilParameters = {
      sortActive: 'nomePerfil',
      sortDirection: 'asc',
      pageSize: 100,
      filter: filtro
    }

    this.perfis = (await this._perfilService.obterPorParametros(params).toPromise()).items; 
    this.validaPerfis();
    
    return this.perfis;
  }

  private async obterTranspotadoras(filtro: string=''): Promise<Transportadora[]> {
    const params: any  = {
      sortActive: 'nomeTransportadora',
      sortDirection: 'asc',
      filter: filtro
    }
    
    return (await this._transportadoraService.obterPorParametros(params).toPromise()).items;
  }

  private async obterFiliais(filtro: string=''): Promise<Filial[]> {
    const params: PerfilParameters = {
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      pageSize: 100,
      filter: filtro
    }
    return (await this._filialService.obterPorParametros(params).toPromise()).items;
  }

  private async obterTecnicos(filtro: string=''): Promise<Tecnico[]> {
    return (await this._tecnicoService.obterPorParametros({ 
      indAtivo: 1, 
      naoVinculados: 1, 
      filter: filtro
    }).toPromise()).items;
  }

  private async obterPaises(): Promise<Pais[]> {
    return (await this._paisService.obterPorParametros({
      sortActive: 'nomePais',
      sortDirection: 'asc'
    }).toPromise()).items;
  }

  private async validaPerfis() {
    const codPerfilUsuarioLogado = this.userSession.usuario.codPerfil;

    switch (codPerfilUsuarioLogado) {
      case PerfilEnum.FILIAIS_SUPERVISOR:
        this.perfis = this.perfis
          .filter(p => p.codPerfil != PerfilEnum.ADM_DO_SISTEMA)
          .filter(p => p.codPerfil != PerfilEnum.FILIAIS_SUPERVISOR)
        break;

      case PerfilEnum.FILIAL_COORDENADOR:
        this.perfis = this.perfis
          .filter(p => p.codPerfil != PerfilEnum.ADM_DO_SISTEMA)
          .filter(p => p.codPerfil != PerfilEnum.COORDENADOR_POS)
          .filter(p => p.codPerfil != PerfilEnum.FILIAL_COORDENADOR)
          .filter(p => p.codPerfil != PerfilEnum.PV_COORDENADOR_DE_CONTRATO)
          .filter(p => p.codPerfil != PerfilEnum.FILIAIS_SUPERVISOR)
        break

      case PerfilEnum.ADM_DO_SISTEMA:
        break

      default:
        this.perfis = this.perfis
          .filter(p => p.codPerfil != PerfilEnum.ADM_DO_SISTEMA)
          .filter(p => p.codPerfil != PerfilEnum.COORDENADOR_POS)
          .filter(p => p.codPerfil != PerfilEnum.FILIAL_COORDENADOR)
          .filter(p => p.codPerfil != PerfilEnum.PV_COORDENADOR_DE_CONTRATO)
          .filter(p => p.codPerfil != PerfilEnum.FILIAIS_SUPERVISOR)
          .filter(p => p.codPerfil != PerfilEnum.SUPORTE_DE_EXPORTAÇÃO)
          .filter(p => p.codPerfil != PerfilEnum.FILIAL_LIDER_DE_SETOR)
          .filter(p => p.codPerfil != PerfilEnum.FILIAL_LIDER_C_FUNCOES_COORDENADOR)
          .filter(p => p.codPerfil != PerfilEnum.LOGÍSTICA_COORDENADOR)
        break;
    }
  }

  private async obterCidades(filtro: string = ''): Promise<Cidade[]> {
    const codUF = this.form.controls['codUF'].value;

    const params: CidadeParameters = {
      sortActive: 'nomeCidade',
      sortDirection: 'asc',
      indAtivo: statusConst.ATIVO,
      codUF: codUF,
      filter: filtro
    }

    return (await this._cidadeService.obterPorParametros(params).toPromise()).items;
  }

  private async obterUFs(filtro: string = ''): Promise<UnidadeFederativa[]> {
    const params: UnidadeFederativaParameters = {
      sortActive: 'siglaUF',
      sortDirection: 'asc',
      codPais: PaisEnum.BRASIL,
      filter: filtro,
      pageSize: 50,
    }

    return (await this._ufService.obterPorParametros(params).toPromise()).items;
  }

  async buscaCEP(cepCmp: any) {
    const cep: string = cepCmp.target.value.replace(/\D+/g, '');

    if (cep)
    {
      this.form.disable();

      this._googleGeolocationService
        .obterPorParametros({ enderecoCep: cep, geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE })
        .subscribe(async (mapService: Geolocalizacao) => {
          if (mapService) {
            this.form.controls['endereco'].setValue(mapService.endereco);
            this.form.controls['bairro'].setValue(mapService.bairro);

            this.unidadesFederativas = await this.obterUFs(mapService.estado);
            this.cidades = await this.obterCidades(mapService.cidade);

            this.form.controls['codPais'].setValue(PaisEnum.BRASIL);
            this.form.controls['codUF'].setValue(this.unidadesFederativas[0]?.codUF);
            this.form.controls['codCidade'].setValue(this.cidades[0]?.codCidade);
          }

          this.form.enable();
          this._cdr.detectChanges();
        });
    }
  }

  public desbloquear(): void {
    this._userService.desbloquearAcesso(this.usuario?.codUsuario).subscribe(() => {
      this.usuarioBloqueado = false;
      this._snack.exibirToast(`Usuário ${this.usuario?.nomeUsuario} desbloqueado com sucesso!`, "success");
    });
  }

  public salvar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.usuario,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo,
        codContrato: Enumerable.from(this.form.controls['codContrato'].value).count() > 0 ?
          Enumerable.from(this.form.controls['codContrato'].value).select(t => t).distinct().toArray()?.join(',')
          : null,
        indPermiteRegistrarEquipPOS: +form.indPermiteRegistrarEquipPOS,
        indPonto: +form.indPonto,
        codLingua: 1,
        codFusoHorario: 19
      }
    };

    if (this.isAddMode)
    {
      this._userService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Usuário ${obj.nomeUsuario} adicionado com sucesso!`, "success");
        this._location.back();
      });
    } else
    {
      this._userService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Usuário ${obj.nomeUsuario} atualizado com sucesso!`, "success");
        this._location.back();
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
