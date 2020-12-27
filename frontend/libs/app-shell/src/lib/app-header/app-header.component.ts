import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '@cea/util-security';

@Component({
  selector: 'cea-app-header',
  templateUrl: 'app-header.component.html',
  styleUrls: ['app-header.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppHeaderComponent {
  public isLoggedIn: boolean;

  constructor(private readonly auth: AuthenticationService, private readonly router: Router) {
    this.isLoggedIn = this.auth.isLoggedIn();
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
