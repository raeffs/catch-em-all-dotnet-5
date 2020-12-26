import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { OAuthStorage } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';
import { SecurityOptions, SecurityOptionsToken } from '../security-options.interface';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
  constructor(
    @Inject(SecurityOptionsToken) private readonly options: SecurityOptions,
    private readonly oAuth: OAuthStorage
  ) {}

  public intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (!this.shouldIntercept(request)) {
      return next.handle(request);
    }

    const token = this.oAuth.getItem('id_token');
    const requestCopy = request.clone({
      url: `${this.options.apiEndpoint}${request.url}`,
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });

    return next.handle(requestCopy);
  }

  private shouldIntercept(request: HttpRequest<unknown>): boolean {
    return request.url.startsWith('/') && !request.url.startsWith('//');
  }
}
