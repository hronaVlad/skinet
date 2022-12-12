import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-pagination-bottom',
  templateUrl: './pagination-bottom.component.html',
  styleUrls: ['./pagination-bottom.component.scss']
})
export class PaginationBottomComponent implements OnInit {

  @Input() totalItems: number;
  @Input() pageSize: number;
  @Output() indexChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  pageChanged(event: PageChangedEvent): void {
    this.indexChanged.emit(event.page);
  }
}
