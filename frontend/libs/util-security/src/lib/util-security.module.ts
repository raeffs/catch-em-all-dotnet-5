import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, ModuleWithProviders, NgModule } from '@angular/core';
import { AuthConfig, OAuthModule } from 'angular-oauth2-oidc';
import { authenticationInitializer } from './initializers/authentication.initializer';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import { SecurityOptions, SecurityOptionsToken } from './security-options.interface';

@NgModule()
export class SecurityModule {
  public static forRoot(options: SecurityOptions): ModuleWithProviders<SecurityModule> {
    return {
      ngModule: SecurityModule,
      providers: [
        OAuthModule.forRoot().providers ?? [],
        { provide: SecurityOptionsToken, useValue: options },
        { provide: APP_INITIALIZER, useFactory: authenticationInitializer, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
        {
          provide: AuthConfig,
          useValue: {
            responseType: 'code',
            issuer: options.issuer,
            clientId: options.clientId,
            redirectUri: options.redirectUri,
            //silentRefreshRedirectUri: options.silentRedirectUri,
            scope: options.scope,
          },
        },
      ],
    };
  }
}
