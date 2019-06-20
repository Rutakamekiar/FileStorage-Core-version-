import { GetTxtFileSymbolsComponent } from './components/get-txt-file-symbols/get-txt-file-symbols.component';
import { GetFileTextComponent } from './components/get-file-text/get-file-text.component';
import { BlackoutFileComponent } from './components/blackout-file/blackout-file.component';
import { ChangeUserMemorySizeComponent } from './components/admin/change-user-memory-size/change-user-memory-size.component';
import { BlockFileComponent } from './components/admin/block-file/block-file.component';
import { RootFoldersComponent } from './components/admin/root-folders/root-folders.component';
import { DownloadFileComponent } from './components/download-file/download-file.component';
import { UpdateFileComponent } from './components/update-file/update-file.component';
import { DeleteFileComponent } from './components/delete-file/delete-file.component';
import { UpdateFolderComponent } from './components/update-folder/update-folder.component';
import { DeleteFolderComponent } from './components/delete-folder/delete-folder.component';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import { CreateFolderComponent } from './components/create-folder/create-folder.component';
import { GetFolderContentComponent } from './components/get-folder-content/get-folder-content.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { GlobalErrorHandler } from './error-handler';
import { HeaderComponent } from './components/header/header.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginUserComponent } from './components/login-user/login-user.component';
import { LogoutUserComponent } from './components/logout-user/logout-user.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginUserComponent,
    LogoutUserComponent,
    GetFolderContentComponent,
    RegisterUserComponent,
    CreateFolderComponent,
    UploadFileComponent,
    DeleteFolderComponent,
    UpdateFolderComponent,
    DeleteFileComponent,
    UpdateFileComponent,
    DownloadFileComponent,
    HeaderComponent,
    RootFoldersComponent,
    BlockFileComponent,
    ChangeUserMemorySizeComponent,
    BlackoutFileComponent,
    GetFileTextComponent,
    GetTxtFileSymbolsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    }
  ],  bootstrap: [AppComponent]
})
export class AppModule { }
