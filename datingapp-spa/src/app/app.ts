import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Nav } from "../layout/nav/nav";
import { lastValueFrom } from 'rxjs';
import { AccountService } from '../core/services/account-service';
import { Home } from "../features/home/home";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Nav, Home]
})
export class App implements OnInit {

  protected http = inject(HttpClient);
  private accountService = inject(AccountService);
  protected readonly title = signal('datingapp-spa');
  protected members = signal<any>([]);

  async ngOnInit() {
    this.members.set(await this.getMembers());
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  async getMembers() {
    try {
      return lastValueFrom(this.http.get('https://localhost:5001/api/members'))
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}
