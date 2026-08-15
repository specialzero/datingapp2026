import { Component, inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterCreds } from '../../../models/user';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private accountService = inject(AccountService);
  protected creds = {} as RegisterCreds;
  cancelRegister = output<boolean>();

  register() {
    this.accountService.register(this.creds).subscribe(
      {
        next: res => {
          console.log(res)
          this.close();
        },
        error: err => console.log(err)
      })
  }

  close() {
    this.cancelRegister.emit(false);
  }
}
