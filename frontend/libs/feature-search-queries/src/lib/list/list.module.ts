import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UiControlsModule } from '@cea/ui-controls';
import { ListComponent } from './list.component';

@NgModule({
  imports: [
    CommonModule,
    UiControlsModule,
    RouterModule.forChild([
      {
        path: '',
        component: ListComponent,
      },
    ]),
  ],
  declarations: [ListComponent],
})
export class ListModule {}
