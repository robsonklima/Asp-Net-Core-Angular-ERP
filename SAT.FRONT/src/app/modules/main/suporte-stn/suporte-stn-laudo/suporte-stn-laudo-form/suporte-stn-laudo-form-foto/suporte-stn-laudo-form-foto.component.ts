import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { FormGroup } from '@angular/forms';
import { Foto, FotoModalidadeEnum } from 'app/core/types/foto.types';
import { MatDialog } from '@angular/material/dialog';
import { Laudo } from 'app/core/types/laudo.types';
import { LaudoService } from 'app/core/services/laudo.service';
import { SuporteStnLaudoFormFotoDialogComponent } from './suporte-stn-laudo-form-foto-dialog/suporte-stn-laudo-form-foto-dialog.component';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';
import { FotoService } from 'app/core/services/foto.service';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

@Component({
    selector: 'app-suporte-stn-laudo-form-foto',
    templateUrl: './suporte-stn-laudo-form-foto.component.html'
})
export class SuporteStnLaudoFormFotoComponent implements OnInit {
    snackConfigSuccess: MatSnackBarConfig = { duration: 2000, panelClass: 'success', verticalPosition: 'top', horizontalPosition: 'right' };

    codLaudo: number;
    laudo: Laudo;
    relatorioAtendimento: RelatorioAtendimento;
    userSession: UsuarioSessao;
    form: FormGroup;
    fotos: Foto[];

    constructor(
        private _route: ActivatedRoute,
        private _laudoService: LaudoService,
        private _fotoSvc: FotoService,
        private _relatorioAtendimentoService: RelatorioAtendimentoService,
        private _matSnackBar: MatSnackBar,
        private _userService: UserService,
        private _dialog: MatDialog
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {
        this.codLaudo = +this._route.snapshot.paramMap.get('codLaudo');
        this.laudo = await this._laudoService.obterPorCodigo(this.codLaudo).toPromise();
        this.relatorioAtendimento = await this._relatorioAtendimentoService.obterPorCodigo(this.laudo.codRAT).toPromise();
        this.obterFotos();
    }

    private async obterFotos() {
        const data = await this._fotoSvc.obterPorParametros({
            numRAT: this.relatorioAtendimento.numRAT,
            codOS: this.relatorioAtendimento.codOS,
            modalidade: "LAUDO"
        }).toPromise();

        this.relatorioAtendimento.fotos = data.items;
    }

    ampliarFoto(foto: Foto) {
        this._dialog.open(SuporteStnLaudoFormFotoDialogComponent, {
            data: { foto: foto }
        });
    }

    removerFoto(codRATFotoSmartphone: number) {
        const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
            data: {
                titulo: 'Confirmação',
                message: 'Deseja excluir esta foto?',
                buttonText: {
                    ok: 'Sim',
                    cancel: 'Não'
                }
            }
        });

        dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
            if (confirmacao) {
                this._fotoSvc.deletar(codRATFotoSmartphone).subscribe(() => {
                    this.obterFotos();
                });
            }
        });
    }

    selecionarImagem(ev: any) {
        var files = ev.target.files;
        var file = files[0];

        if (files && file) {
            var reader = new FileReader();
            reader.onload = this.transformarBase64.bind(this);
            reader.readAsBinaryString(file);
        }
    }

    private transformarBase64(readerEvt) {
        var binaryString = readerEvt.target.result;
        var base64textString = btoa(binaryString);

        const foto: Foto = {
            codOS: this.relatorioAtendimento.codOS,
            numRAT: this.relatorioAtendimento.numRAT,
            modalidade: FotoModalidadeEnum.LAUDO_SIT_1,
            dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
            nomeFoto: moment().format('YYYYMMDDHHmmss') + "_" + this.relatorioAtendimento.codOS + '_' + 'LAUDO.jpg',
            base64: base64textString
        }

        this._fotoSvc.criar(foto).subscribe(() => {
            this.obterFotos();
            this._matSnackBar.open("Imagem adicionada com sucesso!", null, this.snackConfigSuccess);
        });
    }

}
