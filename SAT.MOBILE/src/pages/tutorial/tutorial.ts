import { Component } from '@angular/core';
import { NavController, MenuController } from 'ionic-angular';

import { LoginPage } from '../login/login';
import { Config } from '../../models/config';


@Component({
  templateUrl: 'tutorial.html'
})
export class TutorialPage {
  versao: string;
  slides: any;

  constructor(
      private navCtrl: NavController,
      private menuCtrl: MenuController
  ) {}

  ngOnInit() {
    this.menuCtrl.enable(false);
    this.versao = Config.VERSAO_APP;
    this.slides = [
      {
        title: "Checklist",
        description: `Na versão <b>${this.versao}</b> você deverá realizar o checklist das preventivas contratuais. 
                      Adicionamos um botão logo abaixo do laudo.`,
        image: "assets/imgs/list_1.png",
      },
      {
        title: "Smartphones Antigos",
        description: `Agora é possível utilizar o App Técnicos nos Moto G4s.`,
        image: "assets/imgs/old-phone.png",
      }
    ];
  }

  public telaLogin() {
    this.navCtrl.push(LoginPage);
  }
}