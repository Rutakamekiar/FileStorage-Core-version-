import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterBindingModel } from 'src/app/models/requests/register-binding-model';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  public registerUser: RegisterBindingModel = new RegisterBindingModel();
  constructor(private userService: UserService,
              private router: Router) { }

  ngOnInit() {
  }

  registration() {
    this.userService.registerUser(this.registerUser).subscribe(() => {
      this.router.navigate(['/login']);
    });
  }
}
