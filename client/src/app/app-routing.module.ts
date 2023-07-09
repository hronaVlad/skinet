import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { HomeComponent } from './home/home.component';
import { TestErrorComponent } from './test-error/test-error.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent,  data: { breadcrumb: 'Home'} },
  {path: 'test-error', component: TestErrorComponent, data: {breadcrumb: 'Test Error'} },
  {path: 'not-found', component: NotFoundComponent, data: {breadcrumb: 'Not Found' }},
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'Server Error' }},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule), data: {breadcrumb: 'Shop'}},
  {path: 'basket', loadChildren: () => import('./basket/basket.module').then(_ => _.BasketModule), data: {breadcrumd: 'Basket'}},
  {path: 'checkout', loadChildren: () => import('./checkout/checkout.module').then(_ => _.CheckoutModule), data: {breadcrumb: 'Checkout'},
    canActivate : [AuthGuard]},
  {path: 'account', loadChildren: () => import('./account/account.module').then(_ => _.AccountModule), data: {breadcrumb: 'Account'}},
  {path: 'orders', loadChildren: () => import('./orders/orders.module').then(_ => _.OrderModule), data: {breadcrumb: 'Orders'},
    canActivate: [AuthGuard]},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
