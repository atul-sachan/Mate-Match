import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'mate-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.alertify.success('Hello');
  }

  login(): void {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Login Successful');
    }, error => {
      this.alertify.error('Error in Login');
    }, () => {
      this.model = { username: '', password: '' };
      this.router.navigate(['/members']);
    });

  }

  logout(): void { }

}
