import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FilterBase } from 'app/core/filters/filter-base';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FotoService } from 'app/core/services/foto.service';

@Component({
  selector: 'app-dialog-alterar-foto-perfil',
  templateUrl: './dialog-alterar-foto-perfil.component.html'
})
export class DialogAlterarFotoPerfilComponent implements OnInit {

  @Input() public codUsuario: string;
  @Input() public fotoUsuario: string;

  loading: boolean = false;
  extensoes: string[] = ['image/png', 'image/jpeg'];
  tamanhoMaximo: number = 2097152; //2mb

  constructor(
    private _snack: CustomSnackbarService,
    private _fotoService: FotoService,
    public dialogRef: MatDialogRef<FilterBase>,
    private _cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.buscaFotoUsuario();
  }

  async buscaFotoUsuario() {
    let dadosFotoUsuario = (await this._fotoService.buscarFotoUsuario(this.codUsuario).toPromise());
    this.fotoUsuario = dadosFotoUsuario.base64;
    this._cdr.detectChanges();
  }

  private delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  async onFileSelect(fileInput: any) {
    this.loading = true;
    this._cdr.detectChanges();
    await this.delay(1000);

    let me = this;
    let arquivo = fileInput.target.files[0];

    if (arquivo) {

      let error = false;
      if (arquivo.size > this.tamanhoMaximo) {
        this._snack.exibirToast("Tamanho máximo permitido de 2Mb", "error");
        error = true;
      }

      if (!this.extensoes.find(f => f == arquivo.type)) {
        this._snack.exibirToast("O arquivo deve conter as extensões .png ou .jpeg", "error");
        error = true;
      }

      if (error) {
        fileInput.target.value = null
        this.loading = false;
        return;
      }

      let reader = new FileReader();
      reader.readAsDataURL(arquivo);
      reader.onload = function () {
        if (reader.result) {
          let model = new ImagemPerfilModel();
          model.codUsuario = me.codUsuario;
          model.base64 = reader.result.toString();
          model.mime = arquivo.type.split('/')[1];
          me._fotoService.alterarFotoPerfil(model).toPromise().then(async () => {
            me._snack.exibirToast("Foto atualizada com sucesso!", "success");
            fileInput.target.value = null
            me.loading = false;
            me.dialogRef.close();
          });
        };
        reader.onerror = function (error) {
          me._snack.exibirToast("Erro interno ao salvar a foto : " + error, "error");
        };
      }
    }
  }
}

export class ImagemPerfilModel {
  base64: string;
  codUsuario: string;
  mime: string;
}
