import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptorInterceptor } from './interceptors/error-interceptor.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { SharedModule } from '../shared/shared.module';
import { JwtInterceptorInterceptor } from './interceptors/jwt-interceptor.interceptor';

@NgModule({
  declarations: [NavBarComponent, NotFoundComponent, ServerErrorComponent, SectionHeaderComponent, BreadcrumbComponent],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    }),
    SharedModule
  ],
  exports: [NavBarComponent, SectionHeaderComponent],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorInterceptor, multi: true}
  ]
})
export class CoreModule { }
