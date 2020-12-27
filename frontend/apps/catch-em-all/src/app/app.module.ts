import {
  HttpClientModule,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppShellModule } from '@cea/app-shell';
import { SecurityModule } from '@cea/util-security';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { AppComponent } from './app.component';

export class TestInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.startsWith('/')) {
      return next.handle(req.clone({ url: `http://localhost:5000${req.url}` }));
    }
    return next.handle(req);
  }
}

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
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: TestInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
