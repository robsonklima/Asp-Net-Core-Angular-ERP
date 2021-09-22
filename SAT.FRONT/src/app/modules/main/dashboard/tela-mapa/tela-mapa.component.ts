import { AfterViewInit, ChangeDetectorRef, Component, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-tela-mapa',
  templateUrl: './tela-mapa.component.html',
  
})
export class TelaMapaComponent implements AfterViewInit {
  usuarioSessao: UsuarioSessao;
  
  public myData = [
  ['São Paulo', 8136000],
  ['Rio de Janeiro', 8538000],
  ['Rio Grande do Sul', 8538000],
  ['Mato Grosso', 8538000],
  ['Paraná', 8538000],
  ['Santa Catarina', 8538000],
  ['Rio Grande do Norte', 8538000]];

  public options = {
         region: 'BR',
         displayMode: 'regions',
         resolution: 'provinces',
         colorAxis: {colors: ['#00853f', 'black', '#e31b23']},
         showLegend: true,
         tooltip: {
            isHtml: true,
            textStyle: {
              color: 'black'
            }
         }
       };

  constructor(
    private _userService: UserService,
    private _cdr: ChangeDetectorRef
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
 
  }
}
