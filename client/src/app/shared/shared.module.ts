import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { PaginationBottomComponent } from './components/pagination-bottom/pagination-bottom.component'
import { RouterModule } from '@angular/router';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';

@NgModule({
  declarations: [
    PaginationHeaderComponent,
    PaginationBottomComponent,
    OrderTotalsComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ],
  exports: [
    RouterModule,
    PaginationModule,
    PaginationHeaderComponent,
    PaginationBottomComponent,
    CarouselModule,
    OrderTotalsComponent
  ]
})
export class SharedModule { }
