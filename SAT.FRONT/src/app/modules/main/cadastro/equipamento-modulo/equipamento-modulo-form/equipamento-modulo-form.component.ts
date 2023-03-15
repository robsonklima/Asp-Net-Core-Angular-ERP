import { ActivatedRoute } from '@angular/router';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup } from '@angular/forms';
import { fromEvent, Subject } from 'rxjs';
import { UserService } from 'app/core/user/user.service';
import { CausaService } from 'app/core/services/causa.service';
import { Causa } from 'app/core/types/causa.types';
import { EquipamentoModulo } from 'app/core/types/equipamento-modulo.types';
import { EquipamentoModuloService } from 'app/core/services/equipamento-modulo.service';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { statusConst } from 'app/core/types/status-types';
import moment from 'moment';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import _ from 'lodash';

@Component({
  selector: 'app-equipamento-modulo-form',
  templateUrl: './equipamento-modulo-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class EquipamentoModuloFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codEquip: number;
  nomeEquip: string;
  form: FormGroup;
  equipamentoModulo: EquipamentoModulo;
  equipamentosModulos: EquipamentoModulo[] = [];
  public isLoading: Boolean = true;
  public causas: Causa[] = [];
  public equipamentoCausas: Causa[] = [];
  @ViewChild('searchInputControl') searchInputControl: ElementRef;

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _equipamentoModuloService: EquipamentoModuloService,
    private _route: ActivatedRoute,
    private _causaService: CausaService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codEquip = +this._route.snapshot.paramMap.get('codEquip');
    this.obterEquipamentoModulos();
    this.obterCausas();
  }

  private async obterEquipamentoModulos() {
    this.equipamentosModulos = (await this._equipamentoModuloService.obterPorParametros({
      indAtivo: statusConst.ATIVO,
      codEquip: this.codEquip
    }).toPromise()).items

    this.nomeEquip = this.equipamentosModulos[0].equipamento.nomeEquip;
  }

  private async obterCausas(filtro: string = '') {
    const data = await this._causaService.obterPorParametros({
      indAtivo: 1,
      sortActive: 'codECausa',
      sortDirection: 'asc',
      filter: filtro,
      apenasModulos: 1
    }).toPromise();
    this.causas = data.items;
  }

  verificarDefeitoSelecionado(codECausa: string): boolean {
    return _.find(this.equipamentosModulos, { codECausa: codECausa }) != null;
  }

  async onChange($event: MatSlideToggleChange, codigo) {
    if ($event.checked) {
      this._equipamentoModuloService.criar({
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.usuarioSessao.usuario.codUsuario,
        codEquip: this.codEquip,
        codECausa: codigo,
        indAtivo: statusConst.ATIVO
      }).subscribe();
    } else {
      const equipModulo = (await this._equipamentoModuloService.obterPorParametros({
        codEquip: this.codEquip,
        codECausa: codigo
      }).toPromise()).items.shift();

      console.log(equipModulo.codConfigEquipModulos);
      
      this._equipamentoModuloService.deletar(equipModulo.codConfigEquipModulos).toPromise();
    }
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
