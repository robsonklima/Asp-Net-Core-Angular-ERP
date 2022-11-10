import { Component, Input, OnInit } from '@angular/core';
import { ORSolucaoService } from 'app/core/services/or-solucao.service';
import { ItemSolucaoService } from 'app/core/services/item-solucao.service';
import { ItemSolucao } from 'app/core/types/item-Solucao.types';
import { ORSolucao } from 'app/core/types/or-solucao.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import moment from 'moment';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-solucao',
  templateUrl: './laboratorio-processo-reparo-form-solucao.component.html'
})
export class LaboratorioProcessoReparoFormSolucaoComponent implements OnInit {
  @Input() codORItem: number;
  usuarioSessao: UsuarioSessao;
  solucoes: ORSolucao[];
  itemSolucao: ItemSolucao;

  constructor(
    private _userService: UserService,
    private _orSolucaoService: ORSolucaoService,
    private _itemSolucaoService: ItemSolucaoService,
  ) { 
      this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.solucoes = (await this._orSolucaoService.obterPorParametros({indAtivo:1}).toPromise()).items;
  }

  async onChange($event: MatSlideToggleChange,codigo) {
    if($event.checked){
      let obj = {
        ...this.itemSolucao,
        ...{
              dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
              codTecnico: this.usuarioSessao.usuario.codUsuario,
              codORItem: this.codORItem,
              codSolucao: codigo
        }
      };
      this._itemSolucaoService.criar(obj).subscribe();
    }
    else{
        this.itemSolucao = (await this._itemSolucaoService.obterPorParametros({
          codORItem: this.codORItem,
          codSolucao: codigo
        }).toPromise()).items.shift();
        this._itemSolucaoService.deletar(this.itemSolucao.codItemSolucao).subscribe();
    }
  }

}
