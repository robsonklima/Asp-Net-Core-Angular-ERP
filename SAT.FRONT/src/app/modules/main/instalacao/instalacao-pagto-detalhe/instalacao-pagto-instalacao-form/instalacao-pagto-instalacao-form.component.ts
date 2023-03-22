  import { Component, Input, OnInit } from '@angular/core';
  import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
  import { Location } from '@angular/common';
  import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
  import { Subject } from 'rxjs';
  import moment from 'moment';
  import { UsuarioSessao } from 'app/core/types/usuario.types';
  import { UserService } from 'app/core/user/user.service';
  import { InstalacaoTipoParcela } from 'app/core/types/instalacao-tipo-parcela.types';
  import { InstalacaoTipoParcelaService } from 'app/core/services/instalacao-tipo-parcela.service';
import { InstalacaoPagtoInstal } from 'app/core/types/instalacao-pagto-instal.types';
import { InstalacaoMotivoMulta } from 'app/core/types/instalacao-motivo-multa.types';
import { InstalacaoMotivoMultaService } from 'app/core/services/instalacao-motivo-multa.service';
  
  @Component({
    selector: 'app-instalacao-pagto-instalacao-form',
    templateUrl: './instalacao-pagto-instalacao-form.component.html'
  })
  export class InstalacaoPagtoInstalacaoFormComponent implements OnInit {
    form: FormGroup;
    isAddMode: boolean;
    instalPagtoInstal: InstalacaoPagtoInstal;
    tiposParcela: InstalacaoTipoParcela[] = [];
    motivosMulta: InstalacaoMotivoMulta[] = [];
    userSession: UsuarioSessao;
    protected _onDestroy = new Subject<void>();
  
    constructor(
      private _formbuilder: FormBuilder,
      private _instalacaoTipoParcelaService: InstalacaoTipoParcelaService,
      private _instalMotivoMultaService: InstalacaoMotivoMultaService,
      private _userService: UserService
    ) {
      this.userSession = JSON.parse(this._userService.userSession);
    }
  
    ngOnInit(): void {
      this.isAddMode = !this.instalPagtoInstal;
      this.obterTiposParcela();
      this.obterMotivosMulta();
      this.inicializarForm();
    }
      
    async obterTiposParcela() {
      const data = await this._instalacaoTipoParcelaService
        .obterPorParametros({
          sortActive: "NomeTipoParcela",
          sortDirection: "asc"
        })
        .toPromise();
  
      this.tiposParcela = data?.items;
    }

    async obterMotivosMulta() {
      const data = await this._instalMotivoMultaService
        .obterPorParametros({
          sortActive: "NomeMotivoMulta",
          sortDirection: "asc"
        })
        .toPromise();
  
      this.motivosMulta = data?.items;
    }
  
    inicializarForm() {
      this.form = this._formbuilder.group({
        codInstalacao: [undefined, Validators.required],
        numserie: [undefined, Validators.required],
        CodInstalPagto: [undefined, Validators.required],
        CodInstalTipoParcela: [undefined, Validators.required],
        VlrParcela: [undefined, Validators.required],
        CodInstalMotivoMulta: [undefined, Validators.required],
        VlrMulta: [undefined, Validators.required],
        IndEndossarMulta: [undefined, Validators.required]
      });
    }
   
    salvar(): void {
      const form = this.form.getRawValue();
  
      let obj = {
        ...this.instalPagtoInstal,
        ...form,
        ...{
          dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
          codUsuarioCad: this.userSession.usuario.codUsuario
        }
      };
  
      // this._instalPagtoInstalService.criar(obj).subscribe(() => {
      //   this._snack.exibirToast(`PagtoInstal ${obj.nomePagtoInstal} adicionado com sucesso!`, "success");
      //   this._location.back();
      // });
    }
  
    ngOnDestroy() {
      this._onDestroy.next();
      this._onDestroy.complete();
    }
  }
  