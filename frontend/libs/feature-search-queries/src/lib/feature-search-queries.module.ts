import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthenticationRequiredGuard } from '@cea/util-security';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: 'start',
        loadChildren: () => import('./index/index.module').then(m => m.IndexModule),
      },
      {
        path: 'list',
        loadChildren: () => import('./list/list.module').then(m => m.ListModule),
        canActivate: [AuthenticationRequiredGuard],
      },
      {
        path: 'detail/:id',
        loadChildren: () => import('./detail/detail.module').then(m => m.DetailModule),
      },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'start',
      },
    ]),
  ],
})
export class FeatureSearchQueriesModule {}
