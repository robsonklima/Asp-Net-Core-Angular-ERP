import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { InstalacaoMotivoResService } from 'app/core/services/instalacao-motivo-res.service';
import { InstalacaoRessalvaService } from 'app/core/services/instalacao-ressalva.service';
import { InstalacaoMotivoRes, InstalacaoMotivoResData, InstalacaoMotivoResParameters } from 'app/core/types/instalacao-ressalva-motivo.types';
import { InstalacaoRessalva, InstalacaoRessalvaData } from 'app/core/types/instalacao-ressalva.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { InstalacaoListaComponent } from '../instalacao-lista/instalacao-lista.component';
import moment from 'moment';

@Component({
  selector: 'app-instalacao-ressalva-dialog',
  templateUrl: './instalacao-ressalva-dialog.component.html'
})
export class InstalacaoRessalvaDialogComponent implements OnInit {
  codInstalacao: number;
  ressalvas: InstalacaoRessalva[] = [];
  ressalva: InstalacaoRessalva;
  motivos: InstalacaoMotivoRes[] = [];
  userSession: UsuarioSessao;
  formRessalva: FormGroup;
  dataSourceData: InstalacaoRessalvaData;

  constructor(
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<InstalacaoListaComponent>,
    private _ressalvaService: InstalacaoRessalvaService,
    private _motivoService: InstalacaoMotivoResService,
    private _formBuilder: FormBuilder,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
  ) { 
    this.codInstalacao = data?.codInstalacao;
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.criarForms();
    this.motivos = (await this.obterMotivos()).items;
    this.obterRessalvas();
  }

  private criarForms() {
    this.formRessalva = this._formBuilder.group({
      codInstalMotivoRes: [undefined, Validators.required],
      comentario: [undefined, Validators.required],
      dataOcorrencia: [undefined, Validators.required],
      indJustificativa: [1]
    });    
  }

  async salvar() {
    const formRes = this.formRessalva.getRawValue();

		let obj = {
      ...this.ressalva,
			...formRes,
			...{
        codInstalacao: this.codInstalacao,
        codUsuarioCad: this.userSession.usuario?.codUsuario,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        indAtivo: statusConst.ATIVO,
        indJustificativa: +formRes.indJustificativa
			}
		};

		this._ressalvaService.criar(obj).subscribe(() => {
			this._snack.exibirToast("Registro adicionado com sucesso!", "success");			
		});    
  }

  async obterRessalvas(){
    const data = await this._ressalvaService.obterPorParametros({
      codInstalacao: this.codInstalacao
    }).toPromise();

    this.dataSourceData = data;
  }

  async obterMotivos(): Promise<InstalacaoMotivoResData> {
    let params: InstalacaoMotivoResParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'DescMotivoRes',
      sortDirection: 'asc'
    };

    return await this._motivoService
      .obterPorParametros(params)
      .toPromise();
  }

  fechar() {
    this.dialogRef.close();
  }
}
