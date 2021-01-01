import { Injectable } from '@angular/core';
import { asObservable } from '@raeffs/rxjs';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

export interface UserInfo {
  readonly email: string;
  readonly picture: string;
}

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private readonly oAuth: OAuthService) {}

  public getLoggedInUser(): Observable<UserInfo> {
    try {
      return asObservable(this.oAuth.loadUserProfile()).pipe(
        map<any, UserInfo>(data => {
          return {
            email: data.email,
            picture: data.picture,
          };
        }),
        catchError(() => of({ email: '', picture: '' }))
      );
    } catch {
      return of({ email: '', picture: '' });
    }
  }
}
