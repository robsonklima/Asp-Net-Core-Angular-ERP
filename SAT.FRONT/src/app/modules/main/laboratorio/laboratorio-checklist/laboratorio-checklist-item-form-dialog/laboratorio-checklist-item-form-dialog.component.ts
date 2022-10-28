import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORCheckListItemService } from 'app/core/services/or-checklist-item.service';
import { ORCheckListItem } from 'app/core/types/or-checklist-item.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import 'leaflet';
import 'leaflet-routing-machine';
import { Subject } from 'rxjs';
import { LaboratorioChecklistFormComponent } from '../laboratorio-checklist-form/laboratorio-checklist-form.component';

@Component({
  selector: 'app-laboratorio-checklist-item-form-dialog',
  templateUrl: './laboratorio-checklist-item-form-dialog.component.html'
})
export class LaboratorioChecklistItemFormDialogComponent implements OnInit {
  form: FormGroup;
  userSession: UserSession;
  codORCheckList: number;
  orCheckListItem: ORCheckListItem
  numBancada: number;
  isAddMode: boolean;
  protected _onDestroy = new Subject<void>();

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
    private _userSvc: UserService,
    private _orCheckListItemService: ORCheckListItemService,
    private _snack: CustomSnackbarService,
    private dialogRef: MatDialogRef<LaboratorioChecklistFormComponent>
  ) {
    if (data) {
      this.orCheckListItem = data.orCheckListItem;
      this.codORCheckList = data.codORCheckList;
    }
    
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  async ngOnInit() {
    this.isAddMode = !this.orCheckListItem;
    this.criarForm();
    if (!this.isAddMode)
      this.form.patchValue(this.orCheckListItem);

    console.log(this.orCheckListItem);

  }

  criarForm() {
    this.form = this._formBuilder.group({
      descricao: [undefined, Validators.required],
      acao: [undefined, Validators.required],
      nivel: [undefined, Validators.required],
      parametro: [undefined],
      realizacao: [undefined, Validators.required],
      codMagnus: [undefined, Validators.required],
      pn_Mei: [undefined, Validators.required],
      passoObrigatorio: [undefined],
    });
  }

  salvar() {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  private criar() {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.orCheckListItem,
      ...form,
      ...{
        passoObrigatorio: +form.passoObrigatorio,
        codORCheckList: this.codORCheckList,
        nivel: form.nivel.toString()
      }
    };

    this._orCheckListItemService.criar(obj).subscribe(() => {
      this._snack.exibirToast("Item inserido com sucesso!", "success");
      this.dialogRef.close(true);
    }, (e) => {
      this.form.enable();
    });

    this.dialogRef.close(obj);
  }

  private atualizar() {
    const form: any = this.form.getRawValue();

    let obj = {
      ...this.orCheckListItem,
      ...form,
      ...{
        passoObrigatorio: +form.passoObrigatorio,
        codORCheckList: this.codORCheckList,
        nivel: +form.nivel.toString()
      }
    };
    
    this._orCheckListItemService.atualizar(obj).subscribe(() => {
      this._snack.exibirToast("Item atualizado com sucesso!", "success");
      this.dialogRef.close(true);
    }, () => {
      this.form.enable();
    });

    this.dialogRef.close(obj);
  }
}