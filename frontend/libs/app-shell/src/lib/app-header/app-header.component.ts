import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService, UserInfo, UserService } from '@cea/util-security';
import { Observable } from 'rxjs';

@Component({
  selector: 'cea-app-header',
  templateUrl: 'app-header.component.html',
  styleUrls: ['app-header.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppHeaderComponent {
  public isLoggedIn: boolean;

  public readonly currentUser: Observable<UserInfo>;

  constructor(
    private readonly auth: AuthenticationService,
    private readonly router: Router,
    private readonly user: UserService
  ) {
    this.isLoggedIn = this.auth.isLoggedIn();
    this.currentUser = this.user.getLoggedInUser();
  }

  public logInOrOut(): void {
    if (this.isLoggedIn) {
      this.auth.logout();
      this.isLoggedIn = this.auth.isLoggedIn();
    } else {
      this.auth.login(this.router.url);
    }
  }
}
