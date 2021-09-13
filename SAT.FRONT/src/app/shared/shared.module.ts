import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { getPortugueseIntl } from './pt-br.paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmacaoDialogComponent } from './confirmacao-dialog/confirmacao-dialog.component';

export const FORMATO_DATA = {
    parse: {
        dateInput: 'LL',
    },
    display: {
        dateInput: 'DD/MM/yyyy',
        monthYearLabel: 'YYYY',
        dateA11yLabel: 'LL',
        monthYearA11yLabel: 'YYYY',
    },
};

@NgModule({
    declarations: [
      ConfirmacaoDialogComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatButtonModule
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatSnackBarModule        
    ],
    providers: [
        { provide: MatPaginatorIntl, useValue: getPortugueseIntl() },
        { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
        { provide: MAT_DATE_FORMATS, useValue: FORMATO_DATA },
    ]
})
export class SharedModule {
}
