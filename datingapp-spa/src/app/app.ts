
import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Nav } from "../layout/nav/nav";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Nav, RouterOutlet]
})
export class App {

  // protected http = inject(HttpClient);
  protected router = inject(Router);
  // protected readonly title = signal('datingapp-spa');
  // protected members = signal<any>([]);

  // async ngOnInit() {
  //   this.members.set(await this.getMembers());
  //   //this.setCurrentUser();
  // }

  // setCurrentUser() {
  //   const userString = localStorage.getItem('user');
  //   if (!userString) return;
  //   const user = JSON.parse(userString);
  //   this.accountService.currentUser.set(user);
  // }

  // async getMembers() {
  //   try {
  //     return lastValueFrom(this.http.get('https://localhost:5001/api/members'))
  //   } catch (error) {
  //     console.log(error);
  //     throw error;
  //   }
  // }
}
