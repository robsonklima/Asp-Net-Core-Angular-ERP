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
import { Cargo } from 'app/core/types/cargo.types';
import { Cidade } from 'app/core/types/cidade.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Contrato } from 'app/core/types/contrato.types';
import { Filial } from 'app/core/types/filial.types';
import { GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';
import { Pais } from 'app/core/types/pais.types';
import { Perfil } from 'app/core/types/perfil.types';
import { statusConst } from 'app/core/types/status-types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { Transportadora } from 'app/core/types/transportadora.types';
import { UnidadeFederativa } from 'app/core/types/unidade-federativa.types';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, filter, map, takeUntil, tap } from 'rxjs/operators';
import { Location } from '@angular/common';
import Enumerable from 'linq';

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
  protected _onDestroy = new Subject<void>();

  public codUsuarioExiste: boolean = false;
  public codUsuarioValidado: boolean = false;
  public verificandoUsuarioExiste: boolean = false;

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

  cidadeFiltro: FormControl = new FormControl();
  clienteFilterCtrl: FormControl = new FormControl();
  contratosFilterCtrl: FormControl = new FormControl();

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
    private _unidadeFederativaService: UnidadeFederativaService,
    private _usuarioService: UserService,
    private _cdr: ChangeDetectorRef,
    private _googleGeolocationService: GeolocalizacaoService,
    private _cargoService: CargoService,
    private _perfilService: PerfilService,
    private _location: Location
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.senhaNaoConfere = false;
    this.codUsuario = this._route.snapshot.paramMap.get('codUsuario');
    this.isAddMode = !this.codUsuario;
    this.buscandoCEP = false;
    this.inicializarForm();


    // if (!this.isAddMode) {
    //   this._userService.obterPorCodigo(this.codUsuario)
    //     .pipe(first())
    //     .subscribe(data => {
    //       this.form.patchValue(data);
    //       this.usuario = data;
    //     });
    // }

    this.paises = await this._paisService.obterPaises();
    this.cargos = await this._cargoService.obterCargos();
    this.perfis = await this._perfilService.obterPerfis();
    this.filiais = await this._filialService.obterFiliais();
    this.tecnicos = (await this._tecnicoService.obterPorParametros({ indAtivo: 1, naoVinculados: 1 }).toPromise()).items;
    this.transportadoras = (await ((this._transportadoraService.obterPorParametros({ indAtivo: 1 })).toPromise())).items;
    this.obterClientes();

    this.form.controls['indAtivo'].setValue(true);
    // Forçar o chrome a limpar os campos que coloca quando tem preenchimento automático
    // this.form.controls['codUsuario'].setValue('');
    this.form.controls['senha'].setValue(null);
  }

  async obterClientes(filtro: string = '') {
    let params: ClienteParameters = {
      filter: filtro,
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._clienteService
      .obterPorParametros(params)
      .toPromise();

    this.clientes = data.items;
  }

  private inicializarForm() {

    this.form = this._formBuilder.group({
      codUsuario: [undefined, Validators.required],
      nomeUsuario: [undefined, Validators.required],
      cpf: [undefined, Validators.required],
      endereco: [undefined, [Validators.required]],
      bairro: [undefined, [Validators.required]],
      cep: [undefined, [Validators.required]],
      codUF: [undefined, [Validators.required]],
      codPais: [undefined, [Validators.required]],
      codCidade: [undefined, Validators.required],
      senha: [undefined, Validators.required],
      confirmarSenha: [undefined, Validators.required],
      indAtivo: [undefined],
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
      indPermiteRegistrarEquipPOS: [undefined]
    });

    this.form.controls['nomeUsuario'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text => {
        let splitNome = text.split(' ');
        if (splitNome.length > 1) {
          this.form.controls['codUsuario'].disable();
          this.form.controls['codUsuario'].setValue(splitNome[0] + "." + splitNome[splitNome.length - 1])
          this.form.controls['codUsuario'].enable();
        }
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['codUsuario'].valueChanges.pipe(
      tap(() => { }),
      debounceTime(700),
      map(async text => {
        if (!text) {
          this.codUsuarioValidado = false;
          return;
        }
        let validaCodigo = await this._usuarioService.obterPorCodigo(text).toPromise();
        this.codUsuarioExiste = validaCodigo != null;
        this.codUsuarioValidado = true;
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['codPais'].valueChanges.subscribe(async () => {
      this.unidadesFederativas = [];
      this.unidadesFederativas = await this._unidadeFederativaService.obterUnidadesFederativas(this.form.controls['codPais'].value);
      this._cdr.detectChanges();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async () => {
      this.cidades = [];
      this.cidades = await this._cidadeService.obterCidades(this.form.controls['codUF'].value);
      this._cdr.detectChanges();
    });

    this.cidadeFiltro.valueChanges.pipe(
      filter(filtro => !!filtro),
      debounceTime(700),
      delay(500),
      takeUntil(this._onDestroy),
      map(async filtro => {
        this.cidades = await this._cidadeService.obterCidades(this.form.controls['codUF'].value, filtro);
        this._cdr.detectChanges();
      })
    ).toPromise();

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
          codCliente: this.form.controls['codCliente'].value
        })
        .toPromise()).items;
      this._cdr.detectChanges();
    });
  }

  async buscaCEP(cepCmp: any) {
    if (cepCmp.target.value) {
      this.form.disable();
      // Google
      // Tenta pelo cep (nem sempre os endereços são corretos)
      let mapService = (await this._googleGeolocationService.obterPorParametros
        ({ enderecoCep: cepCmp.target.value.replace(/\D+/g, ''), geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE }).toPromise());

      if (mapService) {
        this.form.controls['endereco'].setValue(mapService.endereco);
        this.form.controls['bairro'].setValue(mapService.bairro);

        this._cidadeService.obterCidades(null, mapService.cidade).then(c => {
          const data = c[0];
          if (data) {
            this.form.controls['codUF'].setValue(data.codUF);
            this.form.controls['codCidade'].setValue(data.codCidade);
          }
        });
      }
      this.form.enable();
      this._cdr.detectChanges();
    }
  }

  desbloquear(): void { }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();


    let obj = {
      ...this.usuario,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo
      }
    };

    // this._userService.atualizar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Usuário ${obj.nomeUsuario} atualizado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      //  ...this.usuario,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: +form.indAtivo,
        codContrato: Enumerable.from(this.form.controls['codContrato'].value).count() > 0 ?
          Enumerable.from(this.form.controls['codContrato'].value).select(t => t).distinct().toArray().join(',')
          : null,
        indPermiteRegistrarEquipPOS: +form.indPermiteRegistrarEquipPOS,
        indPonto: +form.indPonto
      }
    };

    // this._userService.criar(obj).subscribe(() => {
    //   this._snack.exibirToast(`Usuário ${obj.nomeUsuario} adicionado com sucesso!`, "success");
    //   this._location.back();
    // });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
