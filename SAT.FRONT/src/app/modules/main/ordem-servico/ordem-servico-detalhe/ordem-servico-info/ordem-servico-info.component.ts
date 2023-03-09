import { Component, Input, OnInit } from '@angular/core';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-ordem-servico-info',
  templateUrl: './ordem-servico-info.component.html'
})
export class OrdemServicoInfoComponent implements OnInit {
  @Input() os: OrdemServico;
  perfilCliente: boolean;

  constructor(
    private _userService: UserService,
  ) { }

  ngOnInit(): void {
    this.perfilCliente = this._userService.isCustomer;
  }
}
