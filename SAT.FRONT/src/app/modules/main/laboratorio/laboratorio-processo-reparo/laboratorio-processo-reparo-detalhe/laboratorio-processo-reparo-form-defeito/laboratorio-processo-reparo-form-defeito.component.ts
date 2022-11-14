import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { ItemDefeitoService } from 'app/core/services/item-defeito.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORDefeitoService } from 'app/core/services/orDefeito.service';
import { ItemDefeito } from 'app/core/types/item-defeito.types';
import { ORDefeito } from 'app/core/types/or-defeito.types';
import { ORItem } from 'app/core/types/or-item.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import moment from 'moment';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-defeito',
  templateUrl: './laboratorio-processo-reparo-form-defeito.component.html'
})
export class LaboratorioProcessoReparoFormDefeitoComponent implements AfterViewInit {
  @Input() codORItem: number;
  usuarioSessao: UsuarioSessao;
  defeitos: ORDefeito[] = [];
  itemsDefeitos: ItemDefeito[] = [];
  orItem: ORItem;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  @ViewChild('relatoInputControl') relatoInputControl: ElementRef;

  constructor(
    private _userService: UserService,
    private _oRDfeitoService: ORDefeitoService,
    private _itemDefeitoService: ItemDefeitoService,
    private _orItemService: ORItemService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.orItem = await this._orItemService.obterPorCodigo(this.codORItem).toPromise();
    this.registrarEmitters();
    this.obterDados();
  }

  private async obterDados(filter: string='') {
    this.defeitos = (await this._oRDfeitoService
        .obterPorParametros({ indAtivo: 1, filter: filter })
        .toPromise()).items;

    this.itemsDefeitos = (await this._itemDefeitoService
        .obterPorParametros({ codORItem: this.codORItem })
        .toPromise()).items;
  }

  private registrarEmitters() {
    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(1000)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      this.obterDados(text);
    });

    fromEvent(this.relatoInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(1000)
      , distinctUntilChanged()).subscribe((text: string) => {
        this.orItem.defeitoRelatado = text;
        this._orItemService.atualizar(this.orItem).subscribe();
      });
  }

  async onChange($event: MatSlideToggleChange, codigo) {
    if ($event.checked) {
      this._itemDefeitoService.criar({
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codTecnico: this.usuarioSessao.usuario.codUsuario,
        codORItem: this.codORItem,
        codDefeito: codigo
      }).subscribe();
    } else {
      const itemDefeito = (await this._itemDefeitoService.obterPorParametros({
        codORItem: this.codORItem,
        codDefeito: codigo
      }).toPromise()).items.shift();

      this._itemDefeitoService.deletar(itemDefeito.codItemDefeito).subscribe();
    }
  }

  public verificarDefeitoSelecionado(codDefeito: number): boolean {
    return _.find(this.itemsDefeitos, { codDefeito: codDefeito, codORItem: this.codORItem }) != null;
  }
}
