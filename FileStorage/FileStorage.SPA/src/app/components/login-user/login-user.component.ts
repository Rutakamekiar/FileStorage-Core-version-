import { Component, OnInit } from '@angular/core';
import { UserService } from './../../services/user.service';
import { Router } from '@angular/router';
import { LoginBindingModel } from 'src/app/models/requests/login-binding-model';

@Component({
  selector: 'app-login-user',
  templateUrl: './login-user.component.html',
  styleUrls: ['./login-user.component.css']
})
export class LoginUserComponent implements OnInit {

  loginModel: LoginBindingModel = new LoginBindingModel();
  constructor(private userService: UserService,
              private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.userService.login(this.loginModel).subscribe(r => {
      console.log(r);
      localStorage.setItem('token', r.token);
      localStorage.setItem('roles', r.roles);

      this.router.navigate(['/home']);
    });
  }
}
