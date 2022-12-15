import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CausaService } from 'app/core/services/causa.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { ProtocoloChamadoSTNService } from 'app/core/services/protocolo-chamado-stn.service';
import { StatusServicoSTNService } from 'app/core/services/status-servico-stn.service';
import { TipoServicoService } from 'app/core/services/tipo-servico.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { Causa } from 'app/core/types/causa.types';
import { OrdemServicoSTN } from 'app/core/types/ordem-servico-stn.types';
import { ProtocoloChamadoSTN } from 'app/core/types/protocolo-chamado-stn.types';
import { StatusServicoSTN } from 'app/core/types/status-servico-stn.types';
import { TipoServico } from 'app/core/types/tipo-servico.types';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-ordem-servico-stn-form-atendimento',
  templateUrl: './ordem-servico-stn-form-atendimento.component.html'
})
export class OrdemServicoStnFormAtendimentoComponent implements OnInit {
    @Input() codAtendimento: number;
    atendimento: OrdemServicoSTN;
    status: StatusServicoSTN;
    protocolo: ProtocoloChamadoSTN;
    tipoCausa: TipoServico;
    tipoServico: string;
    modulo: Causa;
    subModulo: Causa;
    componente: Causa;
    usuario: Usuario;
    userSession: UsuarioSessao;

  constructor(
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _statusSTNService: StatusServicoSTNService,
    private _protocoloChamadoSTNService: ProtocoloChamadoSTNService,
    private _tipoServicoService: TipoServicoService,
    private _causaService: CausaService,
    private _usuarioService: UsuarioService,
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _dialog: MatDialog,
    private _router: Router
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit(){
    this.atendimento = await this._ordemServicoSTNService.obterPorCodigo(this.codAtendimento).toPromise();
    this.status = await this._statusSTNService.obterPorCodigo(this.atendimento.codStatusSTN).toPromise();
    this.protocolo = (await this._protocoloChamadoSTNService.obterPorParametros({codAtendimento: this.codAtendimento}).toPromise()).items.shift();
    this.usuario = (await this._usuarioService.obterPorParametros({codUsuario: this.protocolo.codUsuarioCad}).toPromise()).items.shift();
    this.tipoCausa = (await this._tipoServicoService.obterPorParametros({codServico:+this.atendimento.codTipoCausa}).toPromise()).items.shift();
    console.log((await this._tipoServicoService.obterPorParametros({codServico:+this.atendimento.codTipoCausa}).toPromise()).items.shift());
    console.log(await this._ordemServicoSTNService.obterPorCodigo(this.codAtendimento).toPromise());
    
    
    if(+this.atendimento.codTipoCausa >= 200){
      this.tipoServico = "MÁQUINA";
    }
    else{
      this.tipoServico = "EXTRA-MÁQUINA";
    }
    this.obterCausas();
  }

  async obterCausas(){
    this.modulo = (await this._causaService.obterPorParametros({codECausa: this.atendimento.codGrupoCausa + "000"}).toPromise()).items.shift();  
    this.subModulo = (await this._causaService.obterPorParametros({codECausa: this.atendimento.codCausa + "00"}).toPromise()).items.shift();  
    this.componente = (await this._causaService.obterPorParametros({codECausa: this.atendimento.codDefeito}).toPromise()).items.shift();  
  }

  onChange(){}
}
