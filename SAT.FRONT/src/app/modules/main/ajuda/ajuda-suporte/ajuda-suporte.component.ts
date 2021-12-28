import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { EmailService } from 'app/core/services/email.service';
import { Email } from 'app/core/types/email.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-ajuda-suporte',
  templateUrl: './ajuda-suporte.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class AjudaSuporteComponent implements OnInit
{
  @ViewChild('supportNgForm') supportNgForm: NgForm;
  userSession: UserSession;
  alert: any;
  supportForm: FormGroup;

  constructor (
    private _formBuilder: FormBuilder,
    private _userSvc: UserService,
    private _emailSvc: EmailService
  )
  {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngOnInit(): void
  {
    this.supportForm = this._formBuilder.group({
      nome: ['', Validators.required],
      assunto: ['', Validators.required],
      mensagem: ['', Validators.required]
    });
  }

  clearForm(): void
  {
    this.supportNgForm.resetForm();
  }

  async sendForm()
  {
    const form: any = this.supportForm.getRawValue();
    const usuario = this.userSession.usuario;
    const email: Email = {
      emailRemetente: usuario.email,
      nomeRemetente: usuario.nomeUsuario,
      emailCC: usuario.email,
      nomeCC: usuario.nomeUsuario,
      nomeDestinatario: 'Equipe SAT',
      emailDestinatario: 'equipe.sat@perto.com.br',
      assunto: `Contato via Suporte: ${form.assunto}`,
      corpo: form.mensagem
    }

    await this._emailSvc.enviarEmail(email).toPromise();

    this.alert = {
      type: 'success',
      message: 'Sua solicitação foi entregue! Um membro da nossa equipe de suporte responderá o mais rápido possível.'
    };

    setTimeout(() =>
    {
      this.alert = null;
    }, 7000);

    this.clearForm();
  }
}
