import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  protected http = inject(HttpClient);
  protected readonly title = signal('datingapp-spa');
  protected members = signal<any>([]);

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/members')
      .subscribe({
        next: response => this.members.set(response),
        error: error => console.log(error),
        complete: () => console.log(`completed`)
      });
  }
}
