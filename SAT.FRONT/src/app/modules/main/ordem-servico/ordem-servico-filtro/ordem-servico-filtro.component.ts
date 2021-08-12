import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilialService } from 'app/core/services/filial.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { TipoIntervencao } from 'app/core/types/tipo-intervencao.types';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-ordem-servico-filtro',
  templateUrl: './ordem-servico-filtro.component.html'
})
export class OrdemServicoFiltroComponent implements OnInit {
  filtro: any;
  @Input() sidenav: MatSidenav;
  form: FormGroup;
  filiais: Filial[] = [];
  tiposIntervencao: TipoIntervencao[] = [];
  filialFilterCtrl: FormControl = new FormControl();
  tipoIntervencaoFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _filialService: FilialService,
    private _usuarioService: UsuarioService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _formBuilder: FormBuilder
  ) {
    this.filtro = this._usuarioService.obterFiltro('ordem-servico');
  }

  ngOnInit(): void {
    this.obterFiliais();
    this.obterTiposIntervencao();
    this.registrarEmitters();
    this.inicializarForm();
  }

  async obterFiliais(filter: string = '') {
    let params: FilialParameters = {
      filter: filter,
      indAtivo: 1,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.filiais;
  }

  async obterTiposIntervencao(filter: string = '') {
    let params = {
      filter: filter,
      indAtivo: 1,
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc'
    }

    const data = await this._tipoIntervencaoService
      .obterPorParametros(params)
      .toPromise();

    this.tiposIntervencao = data.tiposIntervencao;
  }

  private registrarEmitters(): void {
    this.filialFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterFiliais(this.filialFilterCtrl.value);
      });

    this.tipoIntervencaoFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterTiposIntervencao(this.tipoIntervencaoFilterCtrl.value);
      });
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codFilial: [undefined],
      codTipoIntervencao: [undefined]
    });

    this.form.patchValue(this.filtro.parametros);
  }

  aplicar(): void {
    const form: any = this.form.getRawValue();

    const filtro: any = {
      nome: 'ordem-servico',
      parametros: form
    }

    this._usuarioService.registrarFiltro(filtro);
    this.sidenav.close();
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
