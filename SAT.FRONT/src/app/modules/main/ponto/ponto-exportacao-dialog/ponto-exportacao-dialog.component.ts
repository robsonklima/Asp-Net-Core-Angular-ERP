import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { FilialService } from 'app/core/services/filial.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { Filial } from 'app/core/types/filial.types';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { PontoPeriodoListaComponent } from '../ponto-periodo-lista/ponto-periodo-lista.component';

@Component({
  selector: 'app-ponto-exportacao-dialog',
  templateUrl: './ponto-exportacao-dialog.component.html',
})
export class PontoExportacaoDialogComponent implements OnInit {
  pontoPeriodo: PontoPeriodo;
  usuarios: Usuario[] = [];
  filiais: Filial[] = [];
  form: FormGroup;
  loading: boolean = true;
  usuarioSessao: UserSession;
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<PontoPeriodoListaComponent>,
    private _usuarioService: UsuarioService,
    private _formBuilder: FormBuilder,
    private _filialService: FilialService,
    private _exportacaoService: ExportacaoService
  ) {
    this.pontoPeriodo = data.periodo;
  }

  async ngOnInit() {
    this.criarForm();
    this.registrarEmitters();
    this.obterFiliais();
  }

  private criarForm() {
    this.form = this._formBuilder.group({
      codFiliais: [undefined],
      codUsuarios: [undefined],
    });
  }

  registrarEmitters() {
    this.form.controls['codFiliais'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe((value) => {
        this.obterUsuarios(value.join(','));
			});
  }

  private async obterFiliais() {
    const data = await this._filialService
      .obterPorParametros({ indAtivo: 1, sortActive: 'NomeFilial', sortDirection: 'asc' })
      .toPromise();

    this.filiais = data.items;
    this.loading = false;
  }

  private async obterUsuarios(codFiliais: string='') {
    this.loading = true;

    this.usuarios = (await this._usuarioService.obterPorParametros({ 
      indAtivo: 1,
      sortDirection: 'asc',
      sortActive: 'NomeUsuario',
      codPerfis: PerfilEnum.FILIAL_TECNICO_DE_CAMPO.toString(),
      codFiliais: codFiliais
    }).toPromise()).items;

    this.loading = false;
  }

  async exportar() {
    this.loading = true;
    const usuarios = this.form.controls['codUsuarios'].value.join(',');
    
		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.TXT,
			tipoArquivo: ExportacaoTipoEnum.PONTO_USUARIO,
			entityParameters: {
        codUsuarios: usuarios,
        pontoInicio: this.pontoPeriodo.dataInicio,
        pontoFim: this.pontoPeriodo.dataFim,
      }
		}

		await this._exportacaoService.exportar(FileMime.TXT, exportacaoParam);
		this.loading = false;
  }

  selectAll(select: AbstractControl, values, propertyName) {
    if (select.value[0] == 0 && propertyName != '')
        select.patchValue([...values.map(item => item[`${propertyName}`]), 0]);
    else if (select.value[0] == 0 && propertyName == '')
        select.patchValue([...values.map(item => item), 0]);
    else
        select.patchValue([]);
  }

  fechar() {
    this.dialogRef.close();
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
