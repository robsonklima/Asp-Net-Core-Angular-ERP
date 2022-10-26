import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BancadaLaboratorioService } from 'app/core/services/bancada-laboratorio.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { BancadaLaboratorio, BancadaLaboratorioData, BancadaLaboratorioParameters } from 'app/core/types/bancada-laboratorio.types';
import { statusConst } from 'app/core/types/status-types';
import { Usuario, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import moment from 'moment';
import { Subject } from 'rxjs';
import { LaboratorioBancadaDialogComponent } from './laboratorio-bancada-dialog/laboratorio-bancada-dialog.component';

@Component({
  selector: 'app-laboratorio-bancada',
  templateUrl: './laboratorio-bancada.component.html',
  styleUrls: ['./laboratorio-bancada.component.scss']
})
export class LaboratorioBancadaComponent implements OnInit, OnDestroy {
  private userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();
  codBancadaLaboratorio: number;
  bancadas: BancadaLaboratorio[];
  usuarios: Usuario[];

  public form: FormGroup;

  constructor(
    private _bancadaLaboratorioService: BancadaLaboratorioService,
    private _snack: CustomSnackbarService,
    private _dialog: MatDialog,
    private _userService: UserService,

  ){ 
    this.userSession = JSON.parse(this._userService.userSession); 
}

  async ngOnInit() {
    this.bancadas = (await this.obterBancadas()).items;
    this.fix();
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
      const bancada = _.find(this.bancadas, { numBancada: i+1 });

      array[i] = bancada;
    }    

    this.bancadas = array;
  }

  drop(event: CdkDragDrop<string[]>){
    const aux = this.bancadas[event.previousIndex];
    this.bancadas[event.previousIndex] = this.bancadas[event.currentIndex];
    this.bancadas[event.currentIndex] = aux;
    console.log(this.bancadas);
    
    
    // let obj = {
    //     ...this.bancadas,
    //     ...{
    //             numBancada: event.currentIndex + 1,
    //       }
    //   };
  
    // this._bancadaLaboratorioService.atualizar(obj).subscribe(() => {
    //   }, e => {
    //     this.obterBancadas();
    //   });
  }

  novoTecnico(){
    const dialogRef = this._dialog.open(LaboratorioBancadaDialogComponent, {});
	
	  dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
		if (confirmacao)
		  this.obterBancadas();
	  });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
