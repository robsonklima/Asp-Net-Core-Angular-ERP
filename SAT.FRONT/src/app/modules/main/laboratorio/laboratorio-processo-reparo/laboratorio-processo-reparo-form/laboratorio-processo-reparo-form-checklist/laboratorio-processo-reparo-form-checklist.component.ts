import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORCheckList, ORCheckListParameters } from 'app/core/types/or-checklist.types';
import { ORItem } from 'app/core/types/or-item.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-laboratorio-processo-reparo-form-checklist',
  templateUrl: './laboratorio-processo-reparo-form-checklist.component.html'
})
export class LaboratorioProcessoReparoFormChecklistComponent implements OnInit {
  @Input() codORItem: number;
  item: ORItem;
  loading: boolean = true;
  userSession: UserSession;
  orCheckList: ORCheckList;
  form: FormGroup;

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _snack: CustomSnackbarService,
    private _orCheckList: ORCheckListService,
    private _orItemService: ORItemService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.item = await this._orItemService
    .obterPorCodigo(this.codORItem)
    .toPromise();
    
    this.obterORChecklist(this.item);
  }
        
  private async obterORChecklist(item: ORItem) {
		let params: ORCheckListParameters = {
      codPeca: item.codPeca,
			sortActive: 'descricao',
			sortDirection: 'asc',
      pageSize: 120
		};

		const data = await this._orCheckList
			.obterPorParametros(params)
			.toPromise();

    this.orCheckList = data.items.shift();  

    console.log(this.orCheckList);
  	
  }

}
