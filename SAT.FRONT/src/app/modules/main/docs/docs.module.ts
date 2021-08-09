import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ChangelogComponent } from './changelog/changelog.component';
import { docsRoutes } from './docs.routing';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    ChangelogComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(docsRoutes),
    MatIconModule
  ]
})
export class DocsModule { }
