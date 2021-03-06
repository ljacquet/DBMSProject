import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(private api: ApiService, private router: Router) {}

  ngOnInit(): void {}

  async signIn(username: string, password: string) {
    let resp: any = await this.api.trySignIn(username, password);

    if (resp.success) {
      console.log('navigated?');
      this.router.navigate(['home']);
    } else {
      alert('Login failed');
    }

    console.log(resp);
  }
}
