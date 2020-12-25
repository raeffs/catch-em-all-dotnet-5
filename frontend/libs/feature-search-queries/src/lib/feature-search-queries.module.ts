import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UiControlsModule } from '@cea/ui-controls';
import { DetailComponent } from './detail/detail.component';
import { IndexComponent } from './index/index.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {
        path: '',
        component: IndexComponent,
      },
      {
        path: ':id',
        component: DetailComponent,
      },
    ]),
    UiControlsModule,
  ],
  declarations: [IndexComponent, DetailComponent],
})
export class FeatureSearchQueriesModule {}
