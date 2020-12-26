import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BreadcrumbComponent } from './breadcrumbs/breadcrumb.component';
import { BreadcrumbsComponent } from './breadcrumbs/breadcrumbs.component';
import { ButtonComponent } from './button/button.component';
import { CardComponent } from './card/card.component';
import { CenteredLayoutComponent } from './centered-layout/centered-layout.component';
import { ColumnLayoutDirective } from './centered-layout/column-layout.directive';
import { InputComponent } from './input/input.component';

@NgModule({
  imports: [CommonModule],
  declarations: [
    ButtonComponent,
    InputComponent,
    CenteredLayoutComponent,
    ColumnLayoutDirective,
    BreadcrumbsComponent,
    BreadcrumbComponent,
    CardComponent,
  ],
  exports: [
    ButtonComponent,
    InputComponent,
    CenteredLayoutComponent,
    ColumnLayoutDirective,
    BreadcrumbsComponent,
    BreadcrumbComponent,
    CardComponent,
  ],
})
export class UiControlsModule {}
