import { Component, Inject, OnInit } from '@angular/core';
import { UserService } from 'app/core/user/user.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DocumentoSistema, documentoCategoriasConst } from 'app/core/types/documento-sistema.types';
import { DocumentoSistemaService } from 'app/core/services/documentos-sistema.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Subject } from 'rxjs';
import moment from 'moment';
import { DocsComponent } from '../docs.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-documento-sistema-form-dialog',
  templateUrl: './documento-sistema-form-dialog.component.html'
})
export class DocumentoSistemaFormDialogComponent implements OnInit {
  userSession: UsuarioSessao;
  documento: DocumentoSistema;
  loading: boolean;
  isAddMode: boolean;
  codDocumentoSistema: number;
  form: FormGroup;
  quillModules: any = {};
  categorias: string[] = [];
  protected _onDestroy = new Subject<void>();
  AppConfig: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<DocsComponent>,
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _docSistemaService: DocumentoSistemaService,
    private _snack: CustomSnackbarService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();

    this.categorias = documentoCategoriasConst;
    this.quillModules = {
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
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      titulo: [undefined, [Validators.required]],
      conteudo: [undefined, [Validators.required]],
      categoria: [undefined, [Validators.required]],
      indAtivo: [true]
    });
  }

  public salvar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.documento,
      ...form,
      ...{
        indAtivo: +form.indAtivo,
      }
    };

    if (this.isAddMode) {
      obj.codUsuarioCad = this.userSession.usuario.codUsuario;
      obj.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');

      this._docSistemaService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Registro adicionada com sucesso!`, "success");
      });
    } else {
      obj.codUsuarioManut = this.userSession.usuario.codUsuario;
      obj.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');

      this._docSistemaService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast(`Registro atualizado com sucesso!`, "success");
      });
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

