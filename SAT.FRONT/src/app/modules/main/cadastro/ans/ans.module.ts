import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
import { SharedModule } from 'app/shared/shared.module';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSidenavModule } from '@angular/material/sidenav';
import { AnsFiltroComponent } from './ans-filtro/ans-filtro.component';
import { AnsListaComponent } from './ans-lista/ans-lista.component';
import { AnsFormComponent } from './ans-form/ans-form.component';
import { ansRoutes } from './ans.routing';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatDividerModule } from '@angular/material/divider';
import { MatRadioModule } from '@angular/material/radio';
import { MAT_DATE_FORMATS, MatRippleModule } from '@angular/material/core';
import { FuseFindByKeyPipeModule } from '@fuse/pipes/find-by-key';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import moment from 'moment';

@NgModule({
  declarations: [
    AnsFiltroComponent,
    AnsListaComponent,
    AnsFormComponent
  ],
  imports: [
    RouterModule.forChild(ansRoutes),
    DragDropModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatMomentDateModule,
    MatProgressBarModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatTooltipModule,
    FuseFindByKeyPipeModule,
    SharedModule
  ],
  providers: [
    {
      provide: MAT_DATE_FORMATS,
      useValue: {
        parse: {
          dateInput: moment.ISO_8601
        },
        display: {
          dateInput: 'll',
          monthYearLabel: 'MMM YYYY',
          dateA11yLabel: 'LL',
          monthYearA11yLabel: 'MMMM YYYY'
        }
      }
    }
  ]

})
export class AnsModule { }
