import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaProtocolo } from 'app/core/types/despesa-protocolo.types';
import { DespesaProtocoloService } from 'app/core/services/despesa-protocolo.service';
import { UserService } from 'app/core/user/user.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { Subject } from 'rxjs';
import { Location } from '@angular/common';
import moment from 'moment';

@Component({
  selector: 'app-despesa-protocolo-form',
  templateUrl: './despesa-protocolo-form.component.html'
})
export class DespesaProtocoloFormComponent implements OnInit, OnDestroy {

  protected _onDestroy = new Subject<void>();
  public protocolo: DespesaProtocolo;
  public loading: boolean = true;
  public codAuditoriacodDespesaProtocolo: number;
  userSession: UsuarioSessao;
  public form: FormGroup;
  searching: boolean;

  constructor(
    private _formBuilder: FormBuilder,
    private _snack: CustomSnackbarService,
    private _despesaProtocoloService: DespesaProtocoloService,
    private _userService: UserService,
    public _location: Location,
  ) {
        this.userSession = JSON.parse(this._userService.userSession);
   }

  async ngOnInit() {

    this.inicializarForm();
    this.registrarEmitters();
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
        codAuditoriacodDespesaProtocolo: [
        {
          value: undefined,
          disabled: true
        }
      ],
      nomeProtocolo: [{value: undefined,disabled: true}],
      obsProtocolo: [undefined, Validators.required],
    });
  }

  private registrarEmitters() {}

  public salvar(): void {

    const form = this.form.getRawValue();

    let obj = {
      ...this.protocolo,
      ...form,
      ...{
            dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
            codUsuarioCad:this.userSession.usuario.codUsuario,
            nomeProtocolo: this.userSession.usuario.filial.nomeFilial,
            codFilial: this.userSession.usuario.filial.codFilial,
            indAtivo: 1,
            indIntegracao: 0,
            indImpresso: 0,
            indFechamento: 0,
      }
    };

      this._despesaProtocoloService.criar(obj).subscribe(() => {
        this._snack.exibirToast(`Protocolo criado com sucesso!`, "success");
        this._location.back();
      });
    } 
    
  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

}