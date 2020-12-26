import { InjectionToken } from '@angular/core';

export const SecurityOptionsToken: InjectionToken<SecurityOptions> = new InjectionToken<SecurityOptions>(
  'SecurityOptions'
);

export interface SecurityOptions {
  readonly apiEndpoint: string;
  readonly issuer: string;
  readonly clientId: string;
  readonly redirectUri: string;
  readonly silentRedirectUri: string;
  readonly scope: string;
}
