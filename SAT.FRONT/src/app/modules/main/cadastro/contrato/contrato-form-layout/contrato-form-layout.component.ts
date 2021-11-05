import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contrato-form-layout',
  templateUrl: './contrato-form-layout.component.html',
})
export class ContratoFormLayoutComponent implements OnInit {

  public codContrato: number;

  constructor(
    private _route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
  }

}
