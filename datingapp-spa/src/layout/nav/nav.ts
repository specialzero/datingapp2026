import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { LoginCreds } from '../../models/user';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastService } from '../../core/services/toast-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {
  protected accountService = inject(AccountService);
  private toastService = inject(ToastService);
  private router = inject(Router);
  protected creds: LoginCreds = { email: '', password:  ''}

  login() {
    this.accountService.login(this.creds)
      .subscribe({
        next: result => {
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
