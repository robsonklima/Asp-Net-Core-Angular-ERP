import { Component } from '@angular/core';
import { LoadingController, ToastController } from 'ionic-angular';
import { NgForm } from '@angular/forms';

import { Usuario } from '../../models/usuario';
import { DadosGlobaisService } from '../../services/dados-globais';
import { UsuarioService } from '../../services/usuario';

@Component({
  selector: 'senha-alteracao-page',
  templateUrl: 'senha-alteracao.html'
})
export class SenhaAlteracaoPage {
  private usuario: Usuario;
  private forcaSenha: number;
  private senhaEForte: boolean;
  public senhaAntiga: string;
  public novaSenha: string;
  public confirmacaoSenha: string;
  public corForcaSenha: string;
  public descricaoCorForcaSenha: string;

  constructor(
    private toastCtrl: ToastController,
    private loadingCtrl: LoadingController,
    private dgService: DadosGlobaisService,
    private usuarioService: UsuarioService
  ) { }

  ionViewWillEnter() {
    this.carregarDadosGlobais();
  }

  public alterarSenha(form: NgForm) {
    this.senhaAntiga = form.value.senhaAntiga;
    this.novaSenha = form.value.novaSenha;
    this.confirmacaoSenha = form.value.confirmacaoSenha;

    if (this.novaSenha != this.confirmacaoSenha) {
      this.exibirToast('A confirmação de senha deve ser igual à nova senha');
      return
    }

    const loading = this.loadingCtrl.create({ 
      content: 'Aguarde...' 
    });
    loading.present();

    this.usuarioService.alterarSenha(
      this.usuario,
      this.senhaAntiga,
      this.novaSenha
    )
      .subscribe((res) => {
        if (res)
          loading.dismiss().then(() => {
            this.exibirToast(res);
            form.reset();
            this.limparCampos();
          });
      }),
      err => {
        loading.dismiss().then(() => {
          this.exibirToast('Não foi possível alterar a senha');
        });
      }
  }

  public capturarDigitacao(ev: any) {
    this.senhaEForte = false;
    this.novaSenha = ev.target.value;

    if (this.novaSenha && this.novaSenha.trim() != '') {
      this.forcaSenha = this.medirForcaSenha(this.novaSenha);

      if (this.forcaSenha >= 40) {
        this.senhaEForte = true;
      }

      this.corForcaSenha = this.obterCorForcaSenha(this.forcaSenha);
      this.descricaoCorForcaSenha = this.obterDescricaoCorForcaSenha(this.forcaSenha);
    }
  }

  private carregarDadosGlobais() {
    this.dgService.buscarDadosGlobaisStorage()
      .then((dg) => {
        if (dg)
          this.usuario = dg.usuario;
      })
      .catch((err) => {});
  }

  private medirForcaSenha(senha: string): number {
    var forca: number = 0;

    var minusculas = /[a-z]+/.test(senha);
    var maiusculas = /[A-Z]+/.test(senha);
    var numeros = /[0-9]+/.test(senha);
    var simbolos = /[-!$%^&*@#()_+|~=`{}\[\]:";'<>?,.\/]/.test(senha);

    var tipos = [minusculas, maiusculas, numeros, simbolos];

    var combinacoes = 0;
    for (let tipo of tipos) {
      combinacoes += tipo === true ? 1 : 0;
    }

    forca += 2 * senha.length + ((senha.length >= 10) ? 1 : 0);
    forca += combinacoes * 10;
    forca = (senha.length < 8) ? Math.min(forca, 10) : forca;
    forca = (combinacoes === 1) ? Math.min(forca, 10) : forca;
    forca = (combinacoes === 2) ? Math.min(forca, 20) : forca;
    forca = (combinacoes === 3) ? Math.min(forca, 30) : forca;
    forca = (combinacoes === 4) ? Math.min(forca, 40) : forca;

    return forca;
  }

  private obterCorForcaSenha(forcaSenha: number): string {
    let cor: string;

    if (forcaSenha <= 10) {
      cor = 'vermelho';
    }
    else if (forcaSenha <= 20) {
      cor = 'laranja-forte';
    }
    else if (forcaSenha <= 30) {
      cor = 'laranja-fraco';
    }
    else if (forcaSenha <= 40) {
      cor = 'verde';
    }
    else {
      cor = 'verde';
    }

    return cor;
  }

  private obterDescricaoCorForcaSenha(forcaSenha: number): string {
    let descricao: string;

    if (forcaSenha <= 10) {
      descricao = 'Muito Fraca';
    }
    else if (forcaSenha <= 20) {
      descricao = 'Fraca';
    }
    else if (forcaSenha <= 30) {
      descricao = 'Média';
    }
    else if (forcaSenha <= 40) {
      descricao = 'Forte';
    }
    else {
      descricao = 'Forte';
    }

    return descricao;
  }

  private limparCampos() {
    this.senhaAntiga = null;
    this.novaSenha = null;
    this.confirmacaoSenha = null;
  }

  private exibirToast(mensagem: string): Promise<any> {
    return new Promise((resolve, reject) => {
      const toast = this.toastCtrl.create({
        message: mensagem, duration: 2500, position: 'bottom'
      });

      resolve(toast.present());
    });
  }
}