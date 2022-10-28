import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BancadaLaboratorioService } from 'app/core/services/bancada-laboratorio.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { BancadaLaboratorio, BancadaLaboratorioData, BancadaLaboratorioParameters } from 'app/core/types/bancada-laboratorio.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import _ from 'lodash';
import { Subject } from 'rxjs';
import { LaboratorioBancadaDialogComponent } from './laboratorio-bancada-dialog/laboratorio-bancada-dialog.component';

@Component({
  selector: 'app-laboratorio-bancada',
  templateUrl: './laboratorio-bancada.component.html',
  styleUrls: ['./laboratorio-bancada.component.scss']
})
export class LaboratorioBancadaComponent implements OnInit, OnDestroy {
  form: FormGroup;
  protected _onDestroy = new Subject<void>();
  codBancadaLaboratorio: number;
  isLoading: boolean = true;
  userSession: UserSession;
  bancadas: BancadaLaboratorio[] = [];

  constructor(
    private _bancadaLaboratorioService: BancadaLaboratorioService,
    private _snack: CustomSnackbarService,
    private _dialog: MatDialog,
    private _userService: UserService,

  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.bancadas = (await this.obterBancadas()).items;
    this.fix();
    this.isLoading = false;
  }

  private async obterBancadas(): Promise<BancadaLaboratorioData> {
    const params: BancadaLaboratorioParameters = {
      sortActive: 'numBancada',
      sortDirection: 'asc',
      indUsuarioAtivo: statusConst.ATIVO
    }
    return await this._bancadaLaboratorioService.obterPorParametros(params).toPromise();
  }

  fix() {
    const array = new Array(24);

    for (let i = 0; i < array.length; i++) {
      const bancada = _.find(this.bancadas, { numBancada: i + 1 });

      array[i] = bancada;
    }

    this.bancadas = array;
  }

  async drop(event: CdkDragDrop<string[]>) {
    const aux = this.bancadas[event.previousIndex];
    this.bancadas[event.previousIndex] = this.bancadas[event.currentIndex];
    this.bancadas[event.previousIndex].numBancada = event.previousIndex + 1;
    this.bancadas[event.currentIndex] = aux;
    this.bancadas[event.currentIndex].numBancada = event.currentIndex + 1;
    this.atualizar(this.bancadas[event.previousIndex]);
    this.atualizar(this.bancadas[event.currentIndex]);
  }

  novoTecnico(bancada) {
    const dialogRef = this._dialog.open(LaboratorioBancadaDialogComponent, {
      data: {
        numBancada: bancada,
      },
      width: '540px'
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao)
        this.ngOnInit();
    });
  }

  async atualizar(bancada: BancadaLaboratorio): Promise<BancadaLaboratorio> {
    return this._bancadaLaboratorioService.atualizar(bancada).toPromise();
  }

  excluir(codBancadaLaboratorio) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        mensagem: 'Deseja confirmar a exclusão deste técnico?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
      if (confirmacao) {
        this._bancadaLaboratorioService.deletar(codBancadaLaboratorio).subscribe(() => {
          this._snack.exibirToast('Técnico Excluído', 'sucess');
          this.ngOnInit();
        })
      }
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
