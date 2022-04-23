import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  constructor(private api: ApiService, private router: Router) {}

  ngOnInit(): void {}

  async register(username: string, email: string, password: string) {
    let resp: any = await this.api.tryRegister(username, email, password);
    if (resp.success) {
      alert('Successfully Registered, please login');
      this.router.navigate(['login']);
    } else {
      // This should realistically handle the error conditions and notify the user
      // I've decided for the sake of time I won't and will just display the error
      alert('Error Registering see console for details');
      console.log(resp.data);
    }
  }
}
