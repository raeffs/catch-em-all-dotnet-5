import { ScrollingModule } from '@angular/cdk/scrolling';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UiControlsModule } from '@cea/ui-controls';
import { DetailComponent } from './detail.component';

@NgModule({
  imports: [
    CommonModule,
    ScrollingModule,
    UiControlsModule,
    RouterModule.forChild([
      {
        path: '',
        component: DetailComponent,
      },
    ]),
  ],
  declarations: [DetailComponent],
})
export class DetailModule {}
