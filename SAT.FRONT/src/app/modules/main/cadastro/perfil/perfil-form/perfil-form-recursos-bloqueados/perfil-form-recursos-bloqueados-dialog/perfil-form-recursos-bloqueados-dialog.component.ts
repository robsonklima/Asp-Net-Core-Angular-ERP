import { AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import _ from 'lodash';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { PerfilFormRecursosBloqueadosComponent } from '../perfil-form-recursos-bloqueados.component';
import { PerfilSetor } from 'app/core/types/perfil-setor.types';
import { RecursoBloqueado } from 'app/core/types/recurso-bloqueado.types';
import { statusConst } from 'app/core/types/status-types';
import { RecursoBloqueadoService } from 'app/core/services/recurso-bloqueado.service';

@Component({
    selector: 'app-perfil-form-recursos-bloqueados-dialog',
    templateUrl: './perfil-form-recursos-bloqueados-dialog.component.html'
})
export class PerfilFormRecursosBloqueadosDialogComponent implements OnInit {
    perfilSetor: PerfilSetor;
    recursoBloqueado: RecursoBloqueado;
    claims: string;
    flagConsultar: Boolean;
    flagCriar: Boolean;
    flagEditar: Boolean;
    flagExcluir: Boolean;
    userSession: UserSession;
    public form: FormGroup;
    loading: boolean = false;
    protected _onDestroy = new Subject<void>();


    constructor(
        @Inject(MAT_DIALOG_DATA) data: any,
        private _formBuilder: FormBuilder,
        public _dialogRef: MatDialogRef<PerfilFormRecursosBloqueadosComponent>,
        private _recursoBloqueadoSrv: RecursoBloqueadoService,
        private _userService: UserService,
        private _snack: CustomSnackbarService,
        private dialogRef: MatDialogRef<PerfilFormRecursosBloqueadosComponent>
    ) {
        this.perfilSetor = data?.perfilSetor;
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit() {
        this.inicializarForm();
    }

    private inicializarForm() {
        this.form = this._formBuilder.group({
            url: [undefined],
        });
    }

    onChange($event: MatSlideToggleChange, acao: string) {
        if(!this.claims)
            this.claims = "";

        if ($event.checked) 
        {
            if(acao == "consultar")
                {
                    this.claims = this.claims + "CanReadResource,";
                    this.flagConsultar = true;
                }
            else if(acao == "criar")
                {
                    this.claims = this.claims + "CanAddResource,";
                    this.flagCriar = true;
                }
            else if(acao == "editar")
                {
                    this.claims = this.claims + "CanEditResource,";
                    this.flagEditar = true;
                }
            else if(acao == "excluir")
                {
                    this.claims = this.claims + "CanDeleteResource,";                    
                    this.flagExcluir = true;
                }
        }

        else {
            if(acao == "consultar")
                {
                    this.claims = this.claims.replace("CanReadResource,", '');
                    this.flagConsultar = true;
                }
            else if(acao == "criar")
                {
                    this.claims = this.claims.replace("CanAddResource,", '');
                    this.flagCriar = true;
                }
            else if(acao == "editar")
                {
                    this.claims = this.claims.replace("CanEditResource,", '');
                    this.flagEditar = true;
                }
            else if(acao == "excluir")
                {
                    this.claims = this.claims.replace("CanDeleteResource,", '');
                    this.flagExcluir = true;
                }
            }
    }

    cancelar() {
        this._dialogRef.close();
    }

    async salvar() {

        let obj: RecursoBloqueado = {
            codPerfil: this.perfilSetor.codPerfil,
            codSetor: this.perfilSetor.codSetor,
            claims: this.claims,
            url: this.form.controls['url'].value,
            indAtivo: statusConst.ATIVO,
        };

        this._recursoBloqueadoSrv.criar(obj).subscribe(() => {
            this._snack.exibirToast("Regra adicionada com sucesso!", "success");
            this.dialogRef.close(true);
        }, e => {
            this.form.enable();
        });

        this.dialogRef.close(true);
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}

