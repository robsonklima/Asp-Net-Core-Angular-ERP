import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tutorial',
  templateUrl: './tutorial.component.html'
})
export class TutorialComponent implements OnInit {
  tutoriais: any[] = [];

  constructor() { }

  ngOnInit(): void {
    this.tutoriais = [
      {
        id: '01',
        titulo: 'Logando no sistema',
        url: '01-logando-no-sistema.mp4'
      },
      {
        id: '02',
        titulo: 'Listando chamados',
        url: '02-lista-chamados.mp4'
      },
      {
        id: '03',
        titulo: 'Filtrando a lista de chamados',
        url: '03-lista-chamados-filtro.mp4'
      },
      {
        id: '04',
        titulo: 'Exportando a lista de chamados',
        url: '04-lista-chamados-exportacao.mp4'
      },
      {
        id: '05',
        titulo: 'Abrindo um chamado',
        url: '05-novo-chamado.mp4'
      },
      {
        id: '06',
        titulo: 'Autenticação em duas etapas',
        url: '06-autenticacao-duas-etapas.mp4'
      }
    ]
  }
}
