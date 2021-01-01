import { Injectable } from '@angular/core';
import { asObservable } from '@raeffs/rxjs';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export interface UserInfo {
  readonly email: string;
  readonly picture: string;
}

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private readonly oAuth: OAuthService) {}

  public getLoggedInUser(): Observable<UserInfo> {
    return asObservable(this.oAuth.loadUserProfile()).pipe(
      map<any, UserInfo>(data => {
        return {
          email: data.email,
          picture: data.picture,
        };
      })
    );
  }
}
