import { Component, OnInit } from '@angular/core';
import { NavController, LoadingController, MenuController, ToastController, AlertController, Events } from 'ionic-angular';
import { NgForm } from '@angular/forms';

import { Config } from "../../models/config";
import { AppVersion } from '@ionic-native/app-version';

import { DadosGlobais } from '../../models/dados-globais';
import { HomePage } from '../home/home';
import { Login } from '../../models/login';
import { Usuario } from '../../models/usuario';

import { DadosGlobaisService } from '../../services/dados-globais';
import { UsuarioService } from '../../services/usuario';


@Component({
  selector: 'login-page',
  templateUrl: 'login.html'
})
export class LoginPage implements OnInit {
  dadosGlobais: DadosGlobais;
  usuario: Usuario;
  nomeApp: string; 
  versaoApp: string;
  
  constructor(
    private navCtrl: NavController,
    private alertCtrl: AlertController,
    private loadingCtrl: LoadingController,
    private menuCtrl: MenuController,
    private usuarioService: UsuarioService,
    private dadosGlobaisService: DadosGlobaisService,
    private toastCtrl: ToastController,
    private events: Events,
    private appVersion: AppVersion
  ) {}

  ngOnInit() {
    this.menuCtrl.enable(false);

    this.appVersion.getVersionNumber().then((versaoApp) => {
      this.versaoApp = versaoApp;
    }).catch(() => {
      this.versaoApp = Config.VERSAO_APP;
    });
  }

  public login(form: NgForm) {
    const loading = this.loadingCtrl.create({ 
      content: 'Autenticando...' 
    });
    loading.present();

    let usuario = new Usuario();
    usuario.codUsuario = form.value.codUsuario;
    usuario.senha = form.value.senha;

    let login = new Login();
    login.usuario = usuario;
    login.versaoAplicativo = this.versaoApp;

    this.usuarioService.login(login).subscribe((login) => {
      console.log(login)

      if(login && !login.erro) {
        loading.dismiss().then(() => {
          this.usuario = login.usuario;
          this.salvarDadosGlobais();
          this.usuarioService.salvarCredenciais(this.usuario);
          this.events.publish('login:efetuado', this.dadosGlobais);
          this.menuCtrl.enable(true);
          this.navCtrl.setRoot(HomePage);
        });
      } else {
        loading.dismiss().then(() => {
          this.exibirAlerta(login.mensagem);
        });
      }
    },
    err => {
      loading.dismiss().then(() => {
        console.log(err)
        this.exibirAlerta(err);
      });
    });
  }
 
  public recuperarSenha() {
    let prompt = this.alertCtrl.create({
      title: 'Recuperação de Senha',
      message: "Digite seu usuário",
      inputs: [
        {
          name: 'usuario',
          placeholder: 'Usuário'
        },
      ],
      buttons: [
        {
          text: 'Cancelar',
          handler: data => {}
        },
        {
          text: 'Enviar',
          handler: data => {
            if (data.usuario) {
              const loading = this.loadingCtrl.create({ content: 'Aguarde...' });
              loading.present();

              this.usuarioService.recuperarSenha(data.usuario)
                .subscribe((res) => {
                  if(res) {
                    loading.dismiss().then(() => {
                      this.exibirAlerta(res);
                    });
                  } else {
                    loading.dismiss().then(() => {
                      this.exibirAlerta(res);
                    });
                  }
                },
                err => {
                  loading.dismiss().then(() => {
                    this.exibirToast(err);
                  });
                });
            } else {
              this.exibirToast("Informe seu usuário")
            }
          }
        }
      ]
    });
    prompt.present();
  }

  private salvarDadosGlobais() {
    this.dadosGlobais = new DadosGlobais();
    this.dadosGlobais.usuario = this.usuario;
    
    this.dadosGlobaisService.insereDadosGlobaisStorage(this.dadosGlobais);
  }
  
  public exibirToast(message: string): Promise<any> {    
    return new Promise((resolve, reject) => {
      const toast = this.toastCtrl.create({
        message: message, duration: 2000, position: 'bottom'
      });

      resolve(toast.present());
    });
  }

  private exibirAlerta(msg: string) {
    const alerta = this.alertCtrl.create({
      title: 'Alerta!',
      subTitle: msg,
      buttons: ['OK']
    });

    alerta.present();
  }
}