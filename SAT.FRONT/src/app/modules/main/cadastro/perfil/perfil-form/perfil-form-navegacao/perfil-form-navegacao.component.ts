import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { NavegacaoConfiguracaoService } from 'app/core/services/navegacao-configuracao.service';
import { NavegacaoService } from 'app/core/services/navegacao.service';
import { toastTypesConst } from 'app/core/types/generic.types';
import { NavegacaoConfiguracao, NavegacaoConfiguracaoData } from 'app/core/types/navegacao-configuracao.types';
import { Navegacao, NavegacaoData } from 'app/core/types/navegacao.types';
import { PerfilSetor } from 'app/core/types/perfil-setor.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-perfil-form-navegacao',
  templateUrl: './perfil-form-navegacao.component.html',
})
export class PerfilFormNavegacaoComponent implements OnInit {
  @Input() perfilSetor: PerfilSetor;
  userSession: UsuarioSessao;
  navegacao: NavegacaoData;
  navegacaoConf: NavegacaoConfiguracaoData;
  form: FormGroup;
  loading: boolean = true;
  nav: any = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _navService: NavegacaoService,
    private _navCfService: NavegacaoConfiguracaoService,
    private _snack: CustomSnackbarService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit() {
    this.obterDados();
  }

  async obterDados() {
    this.navegacaoConf = await this.obterNavConfiguracao();
    this.nav = await this.obterNavegacoes();
    this.loading = false;
  }

  async onChange($event: MatSlideToggleChange,codigo: number) {
    
    let obj: NavegacaoConfiguracao = {
        codNavegacao: codigo,
        codPerfil: this.perfilSetor.codPerfil,
        codSetor: this.perfilSetor.codSetor
      };

    if ($event.checked)
      this._navCfService.criar(obj).subscribe();
   
    else
      this._navCfService.deletar(codigo).subscribe();
  }

  private isChecked(n: Navegacao): boolean {
    return this.navegacaoConf.items
      .filter(c => c.codNavegacao == n.codNavegacao)
      .shift() != null;
  }

  private async obterNavConfiguracao(): Promise<any> {
    return await this._navCfService
      .obterPorParametros({
        codPerfil: this.perfilSetor.codPerfil,
        codSetor: this.perfilSetor.codSetor
      })
      .toPromise();
  }

  private async obterNavegacoes(): Promise<any> {
    return (await this._navService
      .obterPorParametros({})
      .toPromise()
    ).items
      .filter(f => !f.codNavegacaoPai)
      .map(n => ({
        codigo: n.codNavegacao,
        title: n.title,
        checked: this.isChecked(n),
        color: 'primary',
        children:
          n.children?.map(c => ({
            codigo: c.codNavegacao,
            title: c.title,
            checked: this.isChecked(c),
            color: 'primary',
            children: c.children?.map(cc => ({
              codigo: cc.codNavegacao,
              title: cc.title,
              checked: this.isChecked(cc),
              color: 'primary',
            }))
          }))
      }));;
  }
}
