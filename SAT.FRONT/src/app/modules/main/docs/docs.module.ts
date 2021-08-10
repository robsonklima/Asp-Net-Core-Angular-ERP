import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ChangelogComponent } from './changelog/changelog.component';
import { docsRoutes } from './docs.routing';
import { MatIconModule } from '@angular/material/icon';
import { TranslocoModule } from '@ngneat/transloco';

@NgModule({
  declarations: [
    ChangelogComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(docsRoutes),
    MatIconModule,
    TranslocoModule
  ]
})
export class DocsModule { }
