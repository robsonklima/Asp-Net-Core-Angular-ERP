import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/core/user/user.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DocumentoSistema, documentoCategoriasConst } from 'app/core/types/documento-sistema.types';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { mensagensConst, toastTypesConst } from 'app/core/types/generic.types';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import moment from 'moment';

@Component({
  selector: 'app-docs-form',
  templateUrl: './docs-form.component.html'
})
export class DocsFormComponent implements OnInit {
  userSession: UsuarioSessao;
  documento: DocumentoSistema;
  loading: boolean;
  isAddMode: boolean;
  codDocumentoSistema: number;
  form: FormGroup;
  quillModules: any = {
    toolbar: [
      ['bold', 'italic', 'underline', 'strike'],
      ['blockquote', 'code-block'],
      [{ 'header': 1 }, { 'header': 2 }],
      [{ 'list': 'ordered' }, { 'list': 'bullet' }],
      [{ 'script': 'sub' }, { 'script': 'super' }],
      [{ 'indent': '-1' }, { 'indent': '+1' }],
      [{ 'size': ['small', false, 'large', 'huge'] }],
      [{ 'align': [] }],
      ['clean'],
      ['link', 'image']
    ]
  };
  categorias: string[] = [];
  protected _onDestroy = new Subject<void>();
  AppConfig: any;

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _docSistemaService: DocumentoSistemaService,
    private _snack: CustomSnackbarService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codDocumentoSistema = +this._route.snapshot.paramMap.get('codDocumentoSistema');
    this.isAddMode = !this.codDocumentoSistema;

    this.form = this._formBuilder.group({
      titulo: [undefined, [Validators.required]],
      conteudo: [undefined, [Validators.required]],
      categoria: [undefined, [Validators.required]],
      indAtivo: [true]
    });

    if (!this.isAddMode) {
      this._docSistemaService.obterPorCodigo(this.codDocumentoSistema)
        .subscribe(data => {
          this.documento = data;
          this.form.patchValue(this.documento);
        });
    }

    this.categorias = documentoCategoriasConst;
  }

  public salvar(): void {
    const form = this.form.getRawValue();

    let data = {
      ...this.documento,
      ...form,
      ...{
        indAtivo: +form.indAtivo,
      }
    };

    if (this.isAddMode) {
      data.codUsuarioCad = this.userSession.usuario.codUsuario;
      data.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');

      this._docSistemaService
        .criar(data)
        .subscribe(() => {
          this._snack.exibirToast(mensagensConst.SUCESSO_AO_CRIAR, toastTypesConst.SUCCESS);
        }, e => {
          this._snack.exibirToast(e.message, toastTypesConst.ERROR);
        });
    } else {
      data.codUsuarioManut = this.userSession.usuario.codUsuario;
      data.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

      this._docSistemaService
        .atualizar(data)
        .subscribe(() => {
          this._snack.exibirToast(mensagensConst.SUCESSO_AO_ATUALIZAR, toastTypesConst.SUCCESS);
        }, e => {
          this._snack.exibirToast(e.message, toastTypesConst.ERROR);
        });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

