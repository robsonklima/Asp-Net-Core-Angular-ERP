import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { DespesaProtocolo } from 'app/core/types/despesa-protocolo.types';
import { DespesaProtocoloService } from 'app/core/services/despesa-protocolo.service';

@Component({
  selector: 'app-despesa-protocolo-detalhe',
  templateUrl: './despesa-protocolo-detalhe.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class DespesaProtocoloDetalheComponent implements AfterViewInit
{
  codDespesaProtocolo: number;
  protocolo: DespesaProtocolo;
  userSession: UsuarioSessao;
  isLoading: boolean;

  constructor (
    private _route: ActivatedRoute,
    private _despesaProtocoloSvc: DespesaProtocoloService,
    private _userService: UserService,
    private _cdr: ChangeDetectorRef
  )
  {
    this.userSession = JSON.parse(this._userService.userSession);
    this.codDespesaProtocolo = +this._route.snapshot.paramMap.get('codDespesaProtocolo');
  }

  ngAfterViewInit(): void
  {
    this.obterDados();
    this._cdr.detectChanges();
  }


  private async obterDados()
  {
    await this.obterProtocolo();
  }

  private async obterProtocolo()
  {
    this.protocolo =
      (await this._despesaProtocoloSvc.obterPorCodigo(this.codDespesaProtocolo).toPromise());

    console.log(this.protocolo);
  }

  imprimir(): void
  {

  }
}