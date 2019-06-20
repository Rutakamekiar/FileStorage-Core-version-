import { Component, OnInit, Inject } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { HeaderComponent } from '../header/header.component';

@Component({
  selector: 'app-logout-user',
  templateUrl: './logout-user.component.html',
  styleUrls: ['./logout-user.component.css']
})
export class LogoutUserComponent {

  constructor(private service: UserService,
              @Inject(HeaderComponent) private header: HeaderComponent) {}

  onClick() {
    this.service.logout();
    this.header.isLogIn = false;
  }

}
