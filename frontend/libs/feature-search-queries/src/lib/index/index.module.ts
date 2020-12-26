import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UiControlsModule } from '@cea/ui-controls';
import { IndexComponent } from './index.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UiControlsModule,
    RouterModule.forChild([
      {
        path: '',
        component: IndexComponent,
      },
    ]),
  ],
  declarations: [IndexComponent],
})
export class IndexModule {}
