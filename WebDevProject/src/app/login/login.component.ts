import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private api: ApiService) { }

  ngOnInit(): void {
  }

  async signIn(username: string, password: string) {
    let resp = await this.api.trySignIn(username, password);
    console.log(resp);
  }

}
