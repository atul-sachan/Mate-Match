import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'mate-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  constructor(private authService: AuthService, private alertify: AlertifyService, private route: Router) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
      confirmPassword: new FormControl('', [Validators.required])
    }, this.passwordMatchValidator);
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword') ? null : { 'mismatch': true };
  }

  register() {
    // this.authService.register(this.model).subscribe(() => {
    //   this.alertify.success("register succesfully");
    // });
    console.log(this.registerForm.value);
  }

  cancel() {
    this.alertify.message('cancelled');
    this.route.navigate(['/']);
  }

}
