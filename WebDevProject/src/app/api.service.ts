import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly APIURL = "/api";

  token: { token: string, expiration: string } | null = null;
  username: string = "";

  constructor(private http: HttpClient) {
    let localData = localStorage.getItem('jwt');

    if (localData != null) {
      this.token = JSON.parse(localData);
    }

    let localUserName = localStorage.getItem('username');
    if (localUserName != null) {
      this.username = localUserName;
    }
  }

  isLoggedIn() {
    // Token valid if we have one and has not expired.
    return this.token != null && new Date(this.token.expiration) < new Date();
  }

  trySignIn(username: string, password: string) {
    return new Promise((resolve, reject) => {
      let response = this.http.post(this.APIURL + "/account/login", { username, password }, { responseType: 'json' }).toPromise();
      response.then((data) => {
        resolve(data);
      }).catch((err) => {
        resolve({
          'success': false,
          'data': err
        })
      })
    });
  }
}
