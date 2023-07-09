import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket.component';
import { TestComponent } from './test/test.component';

const routes: Routes = [
  {path: '', component:  BasketComponent,  data: { breadcrumb: 'Basket'}},
  {path: 'test', component: TestComponent,  data: {breadcrumb: 'Test'}},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class BasketRoutingModule { }
