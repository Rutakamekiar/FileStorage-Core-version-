import { HeaderComponent } from './../header/header.component';
import { UserService } from '../../services/user.service';
import { Component, Inject } from '@angular/core';
import { UserLogin } from 'src/app/models/user-login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-user',
  templateUrl: 'login-user.component.html',
  styleUrls: ['login-user.component.css']
})
export class LoginUserComponent {
  public userLogin: UserLogin = new UserLogin('vlad22@ukr.net', '123456');
  isLoading = false;
  errMessage?: string;

  constructor(private service: UserService,
              private router: Router) {}

  onCreateClick() {
    this.isLoading = true;
    this.service.loginUser(this.userLogin).subscribe(m => {
      this.isLoading = false;
      localStorage.setItem('access_token', m.access_token);
      if(m.role == 'admin'){
        this.router.navigate(['/admin']);
      }
      else this.router.navigate(['/folders']);
      },
      error => {
        this.isLoading = false;
        // this.errMessage = this.service.handleError(error);
    });
    this.errMessage = null;
  }
}
