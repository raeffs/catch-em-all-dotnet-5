import { Injectable } from '@angular/core';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private readonly oAuth: OAuthService, private readonly config: AuthConfig) {}

  public isLoggedIn(): boolean {
    return this.oAuth.hasValidAccessToken() && this.oAuth.hasValidIdToken();
  }

  public async login(returnUrl: string): Promise<boolean> {
    if (this.isLoggedIn()) {
      return true;
    }

    this.oAuth.configure({
      ...this.config,
      redirectUri: `${this.config.redirectUri}?returnUrl=${encodeURIComponent(returnUrl)}`,
    });

    await this.oAuth.loadDiscoveryDocument();

    // this will do a redirect, so we always return false
    this.oAuth.initCodeFlow(undefined, {
      returnUrl,
    });
    return false;
  }

  public async logout(): Promise<void> {
    // todo
  }
}
