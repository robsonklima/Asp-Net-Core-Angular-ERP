import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { CidadeService } from 'app/core/services/cidade.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaCartaoCombustivelService } from 'app/core/services/despesa-cartao-combustivel.service';
import { FilialService } from 'app/core/services/filial.service';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { PaisService } from 'app/core/services/pais.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { DespesaCartaoCombustivel, DespesaCartaoCombustivelParameters } from 'app/core/types/despesa-cartao-combustivel.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { Geolocalizacao, GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';
import { Pais, PaisParameters } from 'app/core/types/pais.types';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { statusConst } from 'app/core/types/status-types';
import { FrotaCobrancaGaragemEnum, FrotaFinalidadeUsoEnum, Tecnico } from 'app/core/types/tecnico.types';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';
import { debounceTime, delay, filter, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-tecnico-form',
  templateUrl: './tecnico-form.component.html'
})
export class TecnicoFormComponent implements OnInit, OnDestroy
{
  codTecnico: number;
  tecnico: Tecnico;
  isAddMode: boolean;
  form: FormGroup;
  filiais: Filial[] = [];
  autorizadas: Autorizada[] = [];
  regioes: Regiao[] = [];
  paises: Pais[] = [];
  ufs: UnidadeFederativa[] = [];
  cidades: Cidade[] = [];
  cidadesFiltro: FormControl = new FormControl();
  userSession: UsuarioSessao;
  frotaFinalidadesUso: any[] = [];
  frotaCobrancasGaragem: any[] = [];
  despesaCartoesCombustivel: DespesaCartaoCombustivel[] = [];
  protected _onDestroy = new Subject<void>();

  constructor (
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _route: ActivatedRoute,
    private _tecnicoService: TecnicoService,
    private _autorizadaService: AutorizadaService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _filialService: FilialService,
    private _paisService: PaisService,
    private _ufService: UnidadeFederativaService,
    private _cidadeService: CidadeService,
    private _location: Location,
    private _userService: UserService,
    private _despesaCartaoCombustivelService: DespesaCartaoCombustivelService,
    private _googleGeolocationService: GeolocalizacaoService
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit()
  {
    this.codTecnico = +this._route.snapshot.paramMap.get('codTecnico');
    this.isAddMode = !this.codTecnico;
    this.inicializarForm();
    this.obterFiliais();
    this.obterPaises();
    this.obterFrotaFinalidadesUso();
    this.obterFrotaCobrancasGaragem();
    this.obterCartoesCombustivel();

    this.form.controls['codFilial'].valueChanges.subscribe(async () =>
    {
      this.obterAutorizadas();
    });

    this.form.controls['codAutorizada'].valueChanges.subscribe(async () =>
    {
      this.obterRegioes();
    });

    this.form.controls['codPais'].valueChanges.subscribe(async () =>
    {
      this.obterUFs();
    });

    this.form.controls['codUF'].valueChanges.subscribe(async () =>
    {
      this.obterCidades();
    });

    this.form.controls['cep'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text =>
      {
        if (text.length === 9)
        {
          const cep = this.form.controls['cep']?.value || '';

          this.obterLatLngPorEndereco(cep);
        }
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    this.form.controls['numero'].valueChanges.pipe(
      filter(text => !!text),
      tap(() => { }),
      debounceTime(700),
      map(async text =>
      {
        const endereco = this.form.controls['endereco']?.value || '';
        const numero = this.form.controls['numero']?.value || '';
        const codCidade = this.form.controls['codCidade'].value;
        const cidade = (await this._cidadeService.obterPorCodigo(codCidade).toPromise());
        const query = `${endereco}, ${numero}, ${cidade.nomeCidade}`;
        this.obterLatLngPorEndereco(query);
      }),
      delay(500),
      takeUntil(this._onDestroy)
    ).subscribe(() => { });

    if (!this.isAddMode)
    {
      this._tecnicoService.obterPorCodigo(this.codTecnico)
        .pipe(first())
        .subscribe(data =>
        {
          this.form.patchValue(data);
          this.form.controls['codPais'].setValue(data?.cidade?.unidadeFederativa?.codPais)
          this.form.controls['codUF'].setValue(data?.cidade?.unidadeFederativa?.codUF)
          this.tecnico = data;
        });
    }
  }

  private inicializarForm()
  {
    this.form = this._formBuilder.group({
      codTecnico: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      nome: [undefined, Validators.required],
      codFilial: [undefined, Validators.required],
      codAutorizada: [undefined, Validators.required],
      codRegiao: [undefined, Validators.required],
      rg: [undefined, Validators.required],
      cpf: [undefined, Validators.required],
      apelido: [undefined],
      numCrea: [undefined],
      dataAdmissao: [undefined],
      dataNascimento: [undefined],
      codDespesaCartaoCombustivel: [undefined],
      codPais: [undefined, Validators.required],
      codUF: [undefined, Validators.required],
      codCidade: [undefined, Validators.required],
      endereco: [undefined, Validators.required],
      bairro: [undefined, Validators.required],
      numero: [undefined],
      enderecoComplemento: [undefined],
      cep: [undefined, Validators.required],
      email: [undefined],
      codFrotaFinalidadeUso: [undefined],
      codFrotaCobrancaGaragem: [undefined],
      latitude: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      longitude: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      fone: [undefined],
      foneParticular: [undefined],
      fonePerto: [undefined],
      indTecnicoBancada: [undefined],
      indFerias: [undefined],
      indAtivo: [undefined],
    });
  }

  private async obterFiliais()
  {
    const params: FilialParameters = {
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      indAtivo: statusConst.ATIVO,
      pageSize: 100
    }

    const data = await this._filialService.obterPorParametros(params).toPromise();
    this.filiais = data.items;
  }

  private async obterAutorizadas()
  {
    const codFilial = this.form.controls['codFilial'].value;

    const params: AutorizadaParameters = {
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      indAtivo: statusConst.ATIVO,
      codFilial: codFilial,
      pageSize: 100
    }

    const data = await this._autorizadaService.obterPorParametros(params).toPromise();
    this.autorizadas = data.items;
  }

  private async obterRegioes()
  {
    const codAutorizada = this.form.controls['codAutorizada'].value;

    const params: RegiaoAutorizadaParameters = {
      codAutorizada: codAutorizada,
      indAtivo: statusConst.ATIVO,
      pageSize: 100
    }

    const data = await this._regiaoAutorizadaService.obterPorParametros(params).toPromise();
    this.regioes = data.items.map(ra => ra.regiao);
  }

  private async obterPaises()
  {
    const params: PaisParameters = {
      sortActive: 'nomePais',
      sortDirection: 'asc',
      pageSize: 200
    }

    const data = await this._paisService.obterPorParametros(params).toPromise();
    this.paises = data.items;
  }

  private async obterUFs()
  {
    const codPais = this.form.controls['codPais'].value;

    const params: UnidadeFederativaParameters = {
      sortActive: 'nomeUF',
      sortDirection: 'asc',
      codPais: codPais,
      pageSize: 50
    }

    const data = await this._ufService.obterPorParametros(params).toPromise();
    this.ufs = data.items;
  }

  private async obterCidades(filtro: string = '')
  {
    const codUF = this.form.controls['codUF'].value;

    const params: CidadeParameters = {
      sortActive: 'nomeCidade',
      sortDirection: 'asc',
      indAtivo: statusConst.ATIVO,
      codUF: codUF,
      pageSize: 1000,
      filter: filtro
    }

    const data = await this._cidadeService.obterPorParametros(params).toPromise();
    this.cidades = data.items;
  }

  private async obterLatLngPorEndereco(end: string)
  {
    this._googleGeolocationService.obterPorParametros({ enderecoCep: end.trim(), geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE }).subscribe((data: Geolocalizacao) =>
    {
      if (data)
      {
        const res = data;
        this.form.controls['endereco'].setValue(res.endereco);
        this.form.controls['latitude'].setValue(res.latitude);
        this.form.controls['longitude'].setValue(res.longitude);
        this.form.controls['bairro'].setValue(res.bairro);

        this._cidadeService.obterCidades(null, res.cidade).then(c =>
        {
          const data = c[0];
          if (data)
          {
            this.form.controls['codUF'].setValue(data.codUF);
            this.form.controls['codCidade'].setValue(data.codCidade);
          }
        });
      }
    });
  }

  private obterFrotaFinalidadesUso(): void
  {
    const data = Object.keys(FrotaFinalidadeUsoEnum).filter((element) =>
    {
      return isNaN(Number(element));
    });

    data.forEach((tr, i) =>
    {
      this.frotaFinalidadesUso.push({
        codFrotaFinalidadeUso: i + 1,
        nome: tr
      })
    });
  }

  private obterFrotaCobrancasGaragem(): void
  {
    const data = Object.keys(FrotaCobrancaGaragemEnum).filter((element) =>
    {
      return isNaN(Number(element));
    });

    data.forEach((tr, i) =>
    {
      this.frotaCobrancasGaragem.push({
        codFrotaCobrancaGaragem: i + 1,
        nome: tr
      })
    });
  }

  private async obterCartoesCombustivel()
  {
    const params: DespesaCartaoCombustivelParameters = {
      sortActive: 'numero',
      sortDirection: 'asc',
      pageSize: 1000,
      indAtivo: statusConst.ATIVO
    }

    const data = await this._despesaCartaoCombustivelService.obterPorParametros(params).toPromise();
    this.despesaCartoesCombustivel = data.items;
  }

  salvar(): void
  {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void
  {
    const form: any = this.form.getRawValue();


    let obj = {
      ...this.tecnico,
      ...form,
      ...{
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario,
        indTecnicoBancada: +form.indTecnicoBancada,
        indFerias: +form.indFerias,
        indAtivo: +form.indAtivo
      }
    };

    this._tecnicoService.atualizar(obj).subscribe(() =>
    {
      this._snack.exibirToast(`Técnico ${obj.nome} atualizado com sucesso!`, "success");
      this._location.back();
    });
  }

  criar(): void
  {
    const form = this.form.getRawValue();

    let obj = {
      ...this.tecnico,
      ...form,
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indTecnicoBancada: +form.indTecnicoBancada,
        indFerias: +form.indFerias,
        indAtivo: +form.indAtivo
      }
    };

    this._tecnicoService.criar(obj).subscribe(() =>
    {
      this._snack.exibirToast(`Técnico ${obj.nome} adicionado com sucesso!`, "success");
      this._location.back();
    });
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
