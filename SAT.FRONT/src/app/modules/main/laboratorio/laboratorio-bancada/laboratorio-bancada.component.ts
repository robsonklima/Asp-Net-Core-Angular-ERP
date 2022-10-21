import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BancadaLaboratorioService } from 'app/core/services/bancada-laboratorio.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { BancadaLaboratorio, BancadaLaboratorioData, BancadaLaboratorioParameters } from 'app/core/types/bancada-laboratorio.types';
import { Usuario, UsuarioData, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
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
  bancadas: BancadaLaboratorio[];
  bancadaOrdenada: BancadaLaboratorio[];
  usuarios: Usuario[];

  public form: FormGroup;

  constructor(
    private _bancadaLaboratorioService: BancadaLaboratorioService,
    private _usuarioService: UsuarioService,
    private _dialog: MatDialog,
    private _userService: UserService,

  ){ 
    this.userSession = JSON.parse(this._userService.userSession); 
}

  async ngOnInit() {
    this.obterBancadas();
    this.obterUsuarios();
    
  }

  private async obterBancadas(){
    const params: BancadaLaboratorioParameters = {
        sortActive: 'numBancada',
        sortDirection: 'asc'
    }
    const data: BancadaLaboratorioData = await this._bancadaLaboratorioService.obterPorParametros(params).toPromise();
    this.bancadas = data.items;
    console.log(this.bancadas);
  }

  private async obterUsuarios(){
    const data: UsuarioData = await this._usuarioService.obterPorParametros({
        indAtivo: 1,
        codPerfis: "63,61",
        sortActive: "nomeUsuario",
        sortDirection: "asc"
    }).toPromise();
    this.usuarios = data.items;
    console.log(this.usuarios);
  }

  drop(event: CdkDragDrop<string[]>){
    moveItemInArray(this.bancadas, event.previousIndex, event.currentIndex);

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
