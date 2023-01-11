import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { PaginationBottomComponent } from './components/pagination-bottom/pagination-bottom.component'
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    PaginationHeaderComponent,
    PaginationBottomComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    PaginationModule.forRoot()
  ],
  exports: [
    RouterModule,
    PaginationModule,
    PaginationHeaderComponent,
    PaginationBottomComponent
  ]
})
export class SharedModule { }