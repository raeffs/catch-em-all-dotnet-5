import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ButtonComponent } from './button/button.component';
import { CenteredLayoutComponent } from './centered-layout/centered-layout.component';
import { ColumnLayoutDirective } from './centered-layout/column-layout.directive';
import { InputComponent } from './input/input.component';

@NgModule({
  imports: [CommonModule],
  declarations: [ButtonComponent, InputComponent, CenteredLayoutComponent, ColumnLayoutDirective],
  exports: [ButtonComponent, InputComponent, CenteredLayoutComponent, ColumnLayoutDirective],
})
export class UiControlsModule {}
