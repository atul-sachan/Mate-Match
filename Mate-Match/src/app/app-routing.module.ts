import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MemberListComponent } from './members/member-list/member-list.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { PreventUnsavedChange } from './_guards/prevent-unsaved-changes.guard';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'members', component: MemberListComponent, canActivate: [AuthGuard], resolve: { users: MemberListResolver }},

  // tslint:disable-next-line:max-line-length
  { path: 'members/edit', component: MemberEditComponent, canActivate: [AuthGuard], resolve: { user: MemberEditResolver }, canDeactivate: [PreventUnsavedChange] },
  { path: 'members/:id', component: MemberDetailComponent, canActivate: [AuthGuard], resolve: { user: MemberDetailResolver } },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
