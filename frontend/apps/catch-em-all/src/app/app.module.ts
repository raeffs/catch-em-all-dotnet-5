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
import { Observable } from 'rxjs';
import { AppComponent } from './app.component';

export class TestInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req.clone({ url: `http://localhost:5000${req.url}` }));
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
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: TestInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
