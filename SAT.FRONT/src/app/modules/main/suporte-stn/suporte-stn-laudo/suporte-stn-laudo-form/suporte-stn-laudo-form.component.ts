import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { LaudoService } from 'app/core/services/laudo.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { Laudo } from 'app/core/types/laudo.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-suporte-stn-laudo-form',
  templateUrl: './suporte-stn-laudo-form.component.html'
})
export class SuporteStnLaudoFormComponent implements OnInit, OnDestroy {
  codLaudo: number;
  laudo: Laudo;
  form: FormGroup;
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _laudoService: LaudoService,
    private _snack: CustomSnackbarService,
    private _exportacaoService: ExportacaoService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codLaudo = +this._route.snapshot.paramMap.get('codLaudo');

    this.inicializarForm();
    this.laudo = await this._laudoService.obterPorCodigo(this.codLaudo).toPromise();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
    });
  }

  exportar(laudo) {
    let exportacaoParam: Exportacao = {
      formatoArquivo: ExportacaoFormatoEnum.PDF,
      tipoArquivo: ExportacaoTipoEnum.LAUDO,
      entityParameters: {
        codLaudo: laudo.codLaudo
      }
    }

    this._exportacaoService
      .exportar(FileMime.PDF, exportacaoParam)
      .catch(e => { this._snack.exibirToast(`Não foi possível realizar o download ${e.message}`) });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}