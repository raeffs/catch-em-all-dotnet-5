import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppShellModule } from '@cea/app-shell';
import { SecurityModule } from '@cea/util-security';
import { environment } from '../environments/environment';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      {
        path: 'queries',
        loadChildren: () => import('@cea/feature-search-queries').then(x => x.FeatureSearchQueriesModule),
      },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'queries',
      },
    ]),
    AppShellModule,
    SecurityModule.forRoot({
      apiEndpoint: environment.apiEndpoint,
      issuer: environment.authIssuer,
      clientId: environment.authClientId,
      redirectUri: `${window.location.origin}`,
      scope: 'openid profile email offline_access',
    }),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
