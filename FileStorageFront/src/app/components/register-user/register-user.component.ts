import { LoginUserComponent } from './../login-user/login-user.component';
import { UserService } from '../../services/user.service';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { UserRegister } from '../../models/user-register';

@Component({
  selector: 'app-register-user',
  templateUrl: 'register-user.component.html',
  styleUrls: ['register-user.component.css']
})
export class RegisterUserComponent {
  userRegister: UserRegister = new UserRegister('vlad12@mail.ru', 'vlad10', 'vlad10');
  isLoading = false;
  errMessage?: string;
  constructor(private service: UserService,
              private router: Router) { }

  onCreateClick() {
    console.log('register');
    this.isLoading = true;
    this.service.registerUser(this.userRegister).subscribe(m => {
      this.isLoading = false;
      this.router.navigate(['/login']);
    },
    error => {
      this.isLoading = false;
      // this.errMessage = this.service.handleError(error);
    });
    this.errMessage = null;
  }
}
