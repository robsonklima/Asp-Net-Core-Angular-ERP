import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/core/user/user.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';

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
  categorias: string[] = ['MANUAL', 'SISTEMA'];
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.inicializarForm();


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

