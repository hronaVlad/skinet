import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TestComponent } from './test/test.component';

const routes: Routes = [
  {path: '', component: TestComponent,  data: {breadcrumb: 'Test'}},
  {path: 'login', component: LoginComponent, data: {breadcrumb: ''}},
  {path: 'register', component: RegisterComponent, data: {breadcrumb: ''}},
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
