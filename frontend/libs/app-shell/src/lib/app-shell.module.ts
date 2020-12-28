import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UiControlsModule } from '@cea/ui-controls';
import { AppFooterComponent } from './app-footer/app-footer.component';
import { AppHeaderComponent } from './app-header/app-header.component';
import { AppShellComponent } from './app-shell/app-shell.component';

@NgModule({
  imports: [CommonModule, RouterModule, UiControlsModule],
  declarations: [AppShellComponent, AppHeaderComponent, AppFooterComponent],
  exports: [AppShellComponent],
})
export class AppShellModule {}
