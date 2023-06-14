import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PlantaoTecnicoClienteService } from 'app/core/services/plantao-tecnico-cliente.service';
import { PlantaoTecnicoRegiaoService } from 'app/core/services/plantao-tecnico-regiao.service';
import { PlantaoTecnicoService } from 'app/core/services/plantao-tecnico.service';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { Location } from '@angular/common';
import { Subject } from 'rxjs';
import { Regiao } from 'app/core/types/regiao.types';
import { Cliente } from 'app/core/types/cliente.types';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { RegiaoService } from 'app/core/services/regiao.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { Tecnico } from 'app/core/types/tecnico.types';
import { debounceTime, delay, distinctUntilChanged, filter, map, takeUntil, tap } from 'rxjs/operators';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { statusConst } from 'app/core/types/status-types';
import Enumerable from 'linq';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { Autorizada } from 'app/core/types/autorizada.types';

@Component({
  selector: 'app-tecnico-plantao-form',
  templateUrl: './tecnico-plantao-form.component.html'
})
export class TecnicoPlantaoFormComponent implements OnInit {
  userSession: UserSession;
  loading: boolean;
  form: FormGroup;
  regioes: Regiao[] = [];
  clientes: Cliente[] = [];
  tecnicos: Tecnico[] = [];
	autorizadas: Autorizada[] = [];  
  tecnicosFiltro: FormControl = new FormControl();
  clientesFiltro: FormControl = new FormControl();
  regioesFiltro: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _plantaoTecnicoService: PlantaoTecnicoService,
    private _plantaoTecnicoRegiaoService: PlantaoTecnicoRegiaoService,
    private _plantaoTecnicoClienteService: PlantaoTecnicoClienteService,
    private _tecnicoService: TecnicoService,
    private _regiaoService: RegiaoService,
    private _clienteService: ClienteService,
		private _autorizadaService: AutorizadaService,    
		private _regiaoAutorizadaService: RegiaoAutorizadaService,    
    private _formBuilder: FormBuilder,
    private _location: Location,
    private _snack: CustomSnackbarService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();
    this.registrarEmitters();
    this.obterDados();
  }

  async obterDados()
  {
    this.loading = true;
    
    await this.obterTecnicos();
    await this.obterClientes();
    await this.obterRegioes();
    
    this.loading = false;
  }

  private inicializarForm()
  {
    this.form = this._formBuilder.group({
      codTecnico: [undefined, Validators.required],
      dataPlantao: [undefined, Validators.required],
      codRegioes: [undefined, Validators.required],
      codClientes: [undefined, Validators.required]
    });
  }

  private async obterTecnicos(filtro: string='') {
    const data = await this._tecnicoService.obterPorParametros({
      indAtivo: 1,
      codFiliais: this.userSession.usuario.codFilial?.toString(),
      sortActive: 'Nome',
      sortDirection: 'asc',
      filter: filtro,
      pageSize: 50
    }).toPromise();
    
    this.tecnicos = data.items;
  }

  private async obterClientes(filtro: string='') {
    const data = await this._clienteService.obterPorParametros({
      indAtivo: 1,
      sortActive: 'NomeFantasia',
      sortDirection: 'asc',
      filter: filtro,
      pageSize: 50
    }).toPromise();
    this.clientes = data.items;
  }

	private async obterAutorizadas() {
		this.autorizadas = (await this._autorizadaService
			.obterPorParametros({
				indAtivo: statusConst.ATIVO,
        indFilialPerto: 1,
				pageSize: 500,
				sortActive: 'codAutorizada',
				sortDirection: 'asc',
				codFilial: this.userSession.usuario?.filial?.codFilial
			}).toPromise()).items;
	}  

	private async obterRegioes(filtro: string='') {
    if (this.userSession.usuario?.filial?.codFilial) {
        await this.obterAutorizadas();
    		var codAutorizada = this.autorizadas[0].codAutorizada;
    }
        
		const data = await this._regiaoAutorizadaService.obterPorParametros({
			indAtivo: statusConst.ATIVO,
			codAutorizada: codAutorizada,
      codFiliais: this.userSession.usuario?.filial?.codFilial.toString(),
      filter: filtro,      
			pageSize: 50
		}).toPromise();

		this.regioes = Enumerable.from(data.items)
			.where(ra => ra.codAutorizada === codAutorizada && ra.indAtivo == statusConst.ATIVO && ra.regiao?.indAtivo == statusConst.ATIVO)
			.select(ra => ra.regiao).orderBy(ra => ra.nomeRegiao).toArray();

	}

  registrarEmitters() {
    this.tecnicosFiltro.valueChanges
      .pipe(
        filter(q => q != ''),
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (filtro) =>
      {
        this.obterTecnicos(filtro);
      });

    this.clientesFiltro.valueChanges
      .pipe(
        filter(q => q != ''),
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (filtro) =>
      {
        this.obterClientes(filtro);
      });

    this.regioesFiltro.valueChanges
      .pipe(
        filter(q => q != ''),
        takeUntil(this._onDestroy),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(async (filtro) =>
      {
        this.obterRegioes(filtro);
      });
  }

  public async criar()
  {
    this.form.disable();

    const form = this.form.getRawValue();

    const plantaoTecnico = await this._plantaoTecnicoService.criar({
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: 1
      }
    }).toPromise();

    for (const codCliente of form.codClientes) {
      await this._plantaoTecnicoClienteService.criar({
        codCliente: codCliente,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: 1,
        codPlantaoTecnico: plantaoTecnico.codPlantaoTecnico
      }).toPromise();
    }

    for (const codRegiao of form.codRegioes) {
      await this._plantaoTecnicoRegiaoService.criar({
        codRegiao: codRegiao,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indAtivo: 1,
        codPlantaoTecnico: plantaoTecnico.codPlantaoTecnico
      }).toPromise();
    }

    this._snack.exibirToast("Plantão cadastrado com sucesso", "success");
    this._location.back();
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
