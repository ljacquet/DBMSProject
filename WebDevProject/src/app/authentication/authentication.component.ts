import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css'],
})
export class AuthenticationComponent implements OnInit {
  constructor(private api: ApiService) {}

  ngOnInit(): void {}

  loggedIn(): boolean {
    return this.api.isLoggedIn();
  }

  getUsername(): string {
    return this.api.username;
  }

  signOut() {
    this.api.signOut();
  }
}
