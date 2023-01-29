import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './product-details/product-details.component';

const routes: Routes = [
  {path: '', component: ShopComponent, data:  { breadcrumb: null }},
  {path: ':id', component: ProductDetailsComponent, data:  {alias: 'ProductDetails', breadcrumb: (product: any) => `${product.name} - ${product.id}` }},
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ShopRoutingModule { }
