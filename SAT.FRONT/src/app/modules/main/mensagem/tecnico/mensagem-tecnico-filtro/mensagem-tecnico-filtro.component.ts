import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { Filial } from 'app/core/types/filial.types';
import { Usuario } from 'app/core/types/usuario.types';

@Component({
  selector: 'app-mensagem-tecnico-filtro',
  templateUrl: './mensagem-tecnico-filtro.component.html'
})
export class MensagemTecnicoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;
  filiais: Filial[] = [];
  filialFilterCtrl: FormControl = new FormControl();
  usuarios: Usuario[] = [];
  usuariosFilterCtrl: FormControl = new FormControl();
  statsFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder
  ) {
    super(_userService, _formBuilder, 'mensagem-tecnico');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  async loadData() {
    this.registrarEmitters();
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      indAtivo: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  private registrarEmitters() {
    
  }

  limpar() {
    super.limpar();

  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}