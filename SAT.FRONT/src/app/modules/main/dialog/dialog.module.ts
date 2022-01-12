import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogSaveFilterComponent } from './dialog-save-filter/dialog-save-filter.component';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { TranslocoModule } from '@ngneat/transloco';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatSelectModule } from '@angular/material/select';
import { SharedModule } from 'app/shared/shared.module';
import { MatStepperModule } from '@angular/material/stepper';
import { MatRadioModule } from '@angular/material/radio';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DialogAlterarFotoPerfilComponent } from './dialog-alterar-foto-perfil/dialog-alterar-foto-perfil.component';

@NgModule({
  declarations: [
    DialogSaveFilterComponent,
    DialogAlterarFotoPerfilComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    TranslocoModule,
    MatDatepickerModule,
    NgxMatSelectSearchModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    SharedModule,
    MatButtonModule,
    MatStepperModule,
    MatCheckboxModule,
    MatRadioModule,
    MatListModule,
    MatDialogModule,
    MatTooltipModule,
    MatProgressBarModule,
    MatProgressSpinnerModule    
  ]
})
export class DialogModule { }
