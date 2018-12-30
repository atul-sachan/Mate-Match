import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';

import { JwtModule } from '@auth0/angular-jwt';
import { FileUploadModule } from 'ng2-file-upload';
import { NgxGalleryModule } from 'ngx-gallery';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HeaderInterceptorProvider } from './_interceptors/header.interceptor';
import { ErrorInterceptorProvider } from './_interceptors/error.interceptor';
import { AuthGuard } from './_guards/auth.guard';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { UserService } from './_services/user.service';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { RegisterComponent } from './register/register.component';
import { Constants } from './_global/constants';


export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MemberListComponent,
    MemberDetailComponent,
    MemberEditComponent,
    MemberCardComponent,
    NavbarComponent,
    PhotoEditorComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    AppRoutingModule,
    NgxGalleryModule,
    FileUploadModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: Constants.whiteListDomains,
        blacklistedRoutes: Constants.blacklistedRoutes
      }
    })
  ],
  providers: [
    AuthService,
    UserService,
    AlertifyService,

    // HeaderInterceptorProvider,
    ErrorInterceptorProvider,

    AuthGuard,

    MemberListResolver,
    MemberDetailResolver,
    MemberEditResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
