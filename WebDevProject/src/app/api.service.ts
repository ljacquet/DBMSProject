import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  readonly APIURL = '/api';

  token: { token: string; expiration: string } | null = null;
  username: string = '';

  constructor(private http: HttpClient, private router: Router) {
    let localData = localStorage.getItem('jwt');

    if (localData != null) {
      this.token = JSON.parse(localData);
    }

    let localUserName = localStorage.getItem('username');
    if (localUserName != null) {
      this.username = localUserName;
    }
  }

  private getAPIRequest(url: string, data: object) {
    return new Promise((resolve, reject) => {
      if (this.token == null) {
        this.router.navigate(['login']);
      } else {
        let headers = new Headers();
        headers.append('Authorization', this.token?.token);

        let response = this.http.get(url, { headers: headers})
        );
      }
    });
  }

  isLoggedIn() {
    // Token valid if we have one and has not expired.
    return this.token != null && new Date(this.token.expiration) > new Date();
  }

  trySignIn(username: string, password: string) {
    return new Promise((resolve, reject) => {
      let response = this.http
        .post(
          this.APIURL + '/account/login',
          { username, password },
          { responseType: 'json' }
        )
        .toPromise();
      response
        .then((data) => {
          console.log('set token');
          console.log(data);
          this.token = data as { token: string; expiration: string };
          localStorage.setItem('jwt', JSON.stringify(this.token));
          localStorage.setItem('username', username);

          resolve({
            success: true,
            data: data,
          });
          resolve(data);
        })
        .catch((err) => {
          resolve({
            success: false,
            data: err,
          });
        });
    });
  }

  tryRegister(username: string, email: string, password: string) {
    return new Promise((resolve, reject) => {
      let response = this.http
        .post(
          '/api/account/register',
          {
            username,
            email,
            password,
          },
          { responseType: 'json' }
        )
        .toPromise()
        .then((data) => {
          resolve({ success: true, data });
        })
        .catch((err) => {
          resolve({ success: false, data: err });
        });
    });
  }

  getUserHouse(): any {
    throw new Error('Method not implemented.');
  }
}
