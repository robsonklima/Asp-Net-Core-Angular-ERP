import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, Inject, OnInit } from '@angular/core';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EmailService } from 'app/core/services/email.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { OrdemServicoDetalheOrcamentosComponent } from '../../ordem-servico/ordem-servico-detalhe/ordem-servico-detalhe-orcamentos/ordem-servico-detalhe-orcamentos.component';

@Component({
  selector: 'app-orcamento-cotacao-dialog',
  templateUrl: './orcamento-cotacao-dialog.component.html'
})
export class OrcamentoCotacaoDialogComponent implements OnInit {
  os: OrdemServico;
  userSession: UserSession;
  loading: boolean;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    public _dialogRef: MatDialogRef<OrdemServicoDetalheOrcamentosComponent>,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
    private _emailService: EmailService
  ) {
    this.os = data?.os;
    console.log(this.os);
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {

  }

  addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  emails: string[] = [];

  adicionar(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (!value)
      return

    const emailValido = this.validarEmail(value);

    if (!emailValido)
    {
      this._snack.exibirToast('Verifique o e-mail digitado', 'error');
      return
    }

    this.emails.push(value);
    event.chipInput!.clear();
  }

  remover(email: string): void {
    const index = this.emails.indexOf(email);

    if (index >= 0)
    {
      this.emails.splice(index, 1);
    }
  }

  cancelar() {
    this._dialogRef.close();
  }

  enviar() {
    this.loading = true;
    this._emailService.enviarEmail({
      emailDestinatarios: this.emails,
      assunto: "Perto - Solicitação de Abertura Técnica",
      corpo: `<!DOCTYPE html>
              <html>
              <head>
              <link href='http://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>                          
              <style>
                body {
                  font-family: 'Roboto', sans-serif;
                  font-size: 12px;
                }
              </style>
              </head>
              <body>       
                  <p>Solicito cotação para o atendimento abaixo</p>
              
                  <table>
                    <tr>
                      <td>Chamado Perto</td>
                      <td>${ this.os.codOS }</td>
                    </tr>

                    <tr>
                      <td>Cliente</td>
                      <td>${ this.os?.cliente?.nomeFantasia }</td>
                    </tr>

                    <tr>
                      <td>Local</td>
                      <td>${ this.os?.localAtendimento?.nomeLocal }</td>
                    </tr>

                    <tr>
                      <td>Cep</td>
                      <td>${ this.os?.localAtendimento?.cep }</td>
                    </tr>

                    <tr>
                      <td>Endereço</td>
                      <td>${ this.os?.localAtendimento?.endereco } ${ this.os?.localAtendimento?.numeroEnd }</td>
                    </tr>

                    <tr>
                      <td>Bairro</td>
                      <td>${ this.os?.localAtendimento?.bairro }</td>
                    </tr>

                    <tr>
                      <td>Cidade</td>
                      <td>${ this.os?.localAtendimento?.cidade?.nomeCidade }</td>
                    </tr>

                    <tr>
                      <td>UF</td>
                      <td>${ this.os?.localAtendimento?.cidade?.unidadeFederativa?.nomeUF }</td>
                    </tr>

                    <tr>
                      <td>Máquina</td>
                      <td>${ this.os?.equipamento?.nomeEquip }</td>
                    </tr>

                    <tr>
                      <td>Série</td>
                      <td>${ this.os?.equipamentoContrato?.numSerie }</td>
                    </tr>
                  </table>
              </body>
              </html>`
    }).subscribe(() => {
      this._snack.exibirToast('Email de cotação enviado com sucesso!', 'success')
      this._dialogRef.close();
      this.loading = false;
    }, () => {
      this._snack.exibirToast('Erro ao enviar email', 'error');
      this._dialogRef.close()
    }
    );
  }

  validarEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
  }
}
