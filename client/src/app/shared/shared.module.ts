import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationHeaderComponent } from './components/pagination-header/pagination-header.component';
import { PaginationBottomComponent } from './components/pagination-bottom/pagination-bottom.component'

@NgModule({
  declarations: [
    PaginationHeaderComponent,
    PaginationBottomComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
  ],
  exports: [
    PaginationModule,
    PaginationHeaderComponent,
    PaginationBottomComponent
  ]
})
export class SharedModule { }
