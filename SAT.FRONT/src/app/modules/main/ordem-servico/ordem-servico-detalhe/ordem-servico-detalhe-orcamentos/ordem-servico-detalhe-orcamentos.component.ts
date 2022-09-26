import { AfterViewInit, Component, Input, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { OrcamentoOSBuilder } from 'app/core/builders/implementations/orcamento-os.builder';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Orcamento } from 'app/core/types/orcamento.types';
import { OrdemServico, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
  selector: 'app-ordem-servico-detalhe-orcamentos',
  templateUrl: './ordem-servico-detalhe-orcamentos.component.html',
  styles: [`
        .list-grid-orcamentos {
            grid-template-columns: 100px 100px auto 130px 130px;
            
            /* @screen sm {
                grid-template-columns: 100px 100px auto 130px 130px;
            } */
        }
    `],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'pt'
    }
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoDetalheOrcamentosComponent implements OnInit {

  isLoading: boolean = false;
  @Input() orcamentos: Orcamento[] = [];
  @Input() codOS;
  os: OrdemServico;
  userSession: UserSession;

  constructor(private _dialog: MatDialog,
    private _router: Router,
    private _userService: UserService,
    private _osService: OrdemServicoService,
    private _orcamentoOSBuilder: OrcamentoOSBuilder,
    private _snack: CustomSnackbarService) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.os = (await this._osService.obterPorParametros(
      {
        codOS: this.codOS.toString(),
        include: OrdemServicoIncludeEnum.OS_ORCAMENTO
      }).toPromise()).items.shift();
  }

  criarNovoOrcamento() {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja criar um novo orçamento?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao)
        this._orcamentoOSBuilder.create(this.os, this.userSession)
          .then(orc => {
            this._router.navigateByUrl('/orcamento/detalhe/' + orc.codOrc);
          })
          .catch((e) => {
            this._snack.exibirToast(`Erro ao criar orçamento ${ e?.error?.message }`);
          });
    });
  }
}