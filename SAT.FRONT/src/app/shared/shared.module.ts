import { ErrorHandler, NgModule } from '@angular/core';
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
import { CNPJPipe } from './pipes/cnpj.pipe';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { EmailDialogComponent } from './email-dialog/email-dialog.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpErrorInterceptor } from 'app/core/interceptors/http-error.interceptor';
import { GlobalErrorInterceptor } from 'app/core/interceptors/global-error.interceptor';

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
        ConfirmacaoDialogComponent,
        EmailDialogComponent,
        CNPJPipe,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatButtonModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatSnackBarModule,
        MatSelectModule
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        EmailDialogComponent,
        CNPJPipe
    ],
    providers: [
        { provide: MatPaginatorIntl, useValue: getPortugueseIntl() },
        { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
        { provide: MAT_DATE_FORMATS, useValue: FORMATO_DATA },
        { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
        {
            provide: ErrorHandler,
            useClass: GlobalErrorInterceptor,
        },
        {
            provide : HTTP_INTERCEPTORS,
            useClass: HttpErrorInterceptor,
            multi   : true
        }
    ]
})
export class SharedModule
{
}
