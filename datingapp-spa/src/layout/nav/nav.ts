import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { LoginCreds } from '../../models/user';
import { Router, RouterLink, RouterLinkActive} from '@angular/router';
import { ToastService } from '../../core/services/toast-service';
import { themes } from '../themes';
import { BusyService } from '../../core/services/busy-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav implements OnInit {
  protected busyService = inject(BusyService);
  protected accountService = inject(AccountService);
  private toastService = inject(ToastService);
  private router = inject(Router);
  protected creds: LoginCreds = { email: '', password:  ''}

  protected selectedTheme = signal<string>(localStorage.getItem('theme') || 'light');
  protected themes = themes;

  ngOnInit(): void {
    document.documentElement.setAttribute('data-theme', this.selectedTheme());
  }
  
  handleSelectTheme(theme: string) {
    this.selectedTheme.set(theme);
    localStorage.setItem('theme', theme);
    document.documentElement.setAttribute('data-theme', theme);
    const elem = document.activeElement as HTMLDivElement;
    if (elem) elem.blur();
  }

  login() {
    this.accountService.login(this.creds)
      .subscribe({
        next: () => {
          this.creds = { email: '', password:  ''};
          this.toastService.success('Logged in successfully');
          this.router.navigate(['/members']);
        },
        error: error => {
          console.log(error);
          this.toastService.error(error.error);
        }
      });
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(['/']);
  }

}
