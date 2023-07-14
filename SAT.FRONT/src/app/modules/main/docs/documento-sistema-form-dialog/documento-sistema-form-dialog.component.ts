import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/core/user/user.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { documentoCategoriasConst } from 'app/core/types/documento-sistema.types';
import { AppConfig } from 'app/core/config/app.config'

@Component({
  selector: 'app-documento-sistema-form-dialog',
  templateUrl: './documento-sistema-form-dialog.component.html'
})
export class DocumentoSistemaFormDialogComponent implements OnInit {
  userSession: UsuarioSessao;
  loading: boolean;
  isAddMode: boolean;
  codDocumentoSistema: number;
  form: FormGroup;
  quillModules: any = {};
  categorias: string[] = [];
  protected _onDestroy = new Subject<void>();
  AppConfig: any;

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();

    this.categorias = documentoCategoriasConst;
    this.quillModules = this.AppConfig.quillModules;
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      titulo: [undefined, [Validators.required]],
      conteudo: [undefined, [Validators.required]],
      categoria: [undefined, [Validators.required]],
      indAtivo: [true]
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

