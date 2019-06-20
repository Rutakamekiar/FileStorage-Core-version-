import { RootFoldersComponent } from './components/admin/root-folders/root-folders.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginUserComponent } from './components/login-user/login-user.component';
import { GetFolderContentComponent } from './components/get-folder-content/get-folder-content.component';

const routes: Routes = [
  { path: '', component: RegisterUserComponent},
  { path: 'login', component: LoginUserComponent},
  { path: 'register', component: RegisterUserComponent},
  { path: 'folders', component: GetFolderContentComponent},
  { path: 'folders/:id', component: GetFolderContentComponent},
  { path: 'admin', component: RootFoldersComponent},
  { path: 'admin/:id', component: RootFoldersComponent},
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
