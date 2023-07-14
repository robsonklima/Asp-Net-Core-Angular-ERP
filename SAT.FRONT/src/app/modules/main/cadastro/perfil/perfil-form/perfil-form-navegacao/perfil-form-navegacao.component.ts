import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatTreeFlatDataSource,MatTreeFlattener } from '@angular/material/tree';
import { FlatTreeControl } from '@angular/cdk/tree';
import { NavegacaoConfiguracaoService } from 'app/core/services/navegacao-configuracao.service';
import { NavegacaoService } from 'app/core/services/navegacao.service';
import { NavegacaoConfiguracaoData } from 'app/core/types/navegacao-configuracao.types';
import { NavegacaoData, NavegacaoParameters } from 'app/core/types/navegacao.types';
import { PerfilSetor } from 'app/core/types/perfil-setor.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';

interface NavigationNode {
  name: string;
  children?: NavigationNode[];
  expanded?: boolean;
  checked?: boolean;
}

interface ExampleFlatNode{
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-perfil-form-navegacao',
  templateUrl: './perfil-form-navegacao.component.html',
})
export class PerfilFormNavegacaoComponent implements OnInit {

  @Input() perfilSetor: PerfilSetor;

  userSession: UsuarioSessao;
  navegacao: NavegacaoData;
  navegacaoConfiguracao: NavegacaoConfiguracaoData;
  form: FormGroup;
  isAddMode: boolean;
  searching: boolean;
  protected _onDestroy = new Subject<void>();

  treeData: NavigationNode[] = [
    {
      name: 'Home',
      children: [
        {
          name: 'About Us',
          children: [
            { name: 'Mission', checked: false },
            { name: 'Vision', checked: false },
            { name: 'Values', checked: false }
          ]
        },
        { name: 'Contact', checked: false }
      ],
      checked: false
    }
  ];

  // const menus = navegacoes.map((menu) => {
  //   name: menu.nome,
  //   children: menu.children.map((cd) => {
  //       name: menu.nome,
  //       children: cd.children.map((cd2) => {
  //           name: cd2.nome
  //       })
  //   })
//})

  treeControl: FlatTreeControl<NavigationNode> = new FlatTreeControl<ExampleFlatNode>(
    node=> node.level,
    node=> node.expandable
  );

  constructor(
    private _navegacaoSrv: NavegacaoService,
    private _navegacaoConfSrv: NavegacaoConfiguracaoService,
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit() {
    this.obterDados();
  }

  async obterDados() {
    await this.obterNavegacao();
  }

  async obterNavegacao() {
    let params: NavegacaoParameters = {
      sortActive: 'title',
      indAtivo: statusConst.ATIVO,
      sortDirection: 'asc',
      pageSize: 1000
    };
    const data = await this._navegacaoSrv
      .obterPorParametros(params)
      .toPromise();
    this.navegacao = data;

    console.log(this.navegacao);
  }

  toggleNode(node: NavigationNode): void {
    node.expanded = !node.expanded;
  }

  hasChild(_: number, node: NavigationNode): boolean {
    return !!node.children && node.children.length > 0;
  }

  salvar() {
    // Lógica de salvamento
  }
}
