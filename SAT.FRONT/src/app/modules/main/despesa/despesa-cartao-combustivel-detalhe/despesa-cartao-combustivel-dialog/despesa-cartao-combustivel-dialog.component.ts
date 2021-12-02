import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaCartaoCombustivelTecnicoService } from 'app/core/services/despesa-cartao-combustivel-tecnico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { DespesaCartaoCombustivelTecnico } from 'app/core/types/despesa-cartao-combustivel.types';
import { Tecnico, TecnicoParameters } from 'app/core/types/tecnico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
  selector: 'app-despesa-cartao-combustivel-dialog',
  templateUrl: './despesa-cartao-combustivel-dialog.component.html'
})
export class DespesaCartaoCombustivelDialogComponent implements OnInit
{
  form: FormGroup;
  tecnicos: Tecnico[] = [];
  codDespesaCartaoCombustivel: number;
  userSession: UsuarioSessao;

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<DespesaCartaoCombustivelDialogComponent>,
    private _formBuilder: FormBuilder,
    private _tecnicoService: TecnicoService,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
    private _despesaCartaoTecnicoService: DespesaCartaoCombustivelTecnicoService) 
  {
    if (data)
      this.codDespesaCartaoCombustivel = data.codDespesaCartaoCombustivel;

    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void
  {
    this.obterTecnicos();
    this.createForm();
  }

  createForm(): void
  {
    this.form = this._formBuilder.group({
      codTecnico: [undefined, Validators.required],
      dataInicial: [undefined, Validators.required]
    });
  }

  async obterTecnicos()
  {
    let params: TecnicoParameters = {
      indAtivo: 1,
      sortActive: 'nome',
      sortDirection: 'asc',
      codPerfil: 35,
      pageSize: 1000
    };

    const data = await this._tecnicoService
      .obterPorParametros(params)
      .toPromise();

    this.tecnicos = data.items;
  }

  async confirmar(): Promise<void>
  {
    var despesaCartaoTecnico: DespesaCartaoCombustivelTecnico =
    {
      codDespesaCartaoCombustivel: this.codDespesaCartaoCombustivel,
      codTecnico: this.form.controls['codTecnico'].value,
      dataHoraInicio: this.form.controls['dataInicial'].value,
      codUsuarioCad: this.userSession?.usuario?.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss')
    }

    this._despesaCartaoTecnicoService.criar(despesaCartaoTecnico)
      .subscribe(r =>
      {
        this._snack.exibirToast('Técnico vinculado com sucesso!', 'success');
        this.dialogRef.close(true);
      },
        e =>
        {
          this._snack.exibirToast('Erro ao vincular técnico.', 'error');
          this.cancelar();
        });
  }

  cancelar(): void
  {
    this.dialogRef.close(false);
  }
}
