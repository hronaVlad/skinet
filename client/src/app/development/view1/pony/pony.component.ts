import { Component, Input, OnInit, Output, EventEmitter, ContentChild, ContentChildren } from '@angular/core';

@Component({
  selector: 'app-pony',
  templateUrl: './pony.component.html',
  styleUrls: ['./pony.component.scss']
})
export class PonyComponent implements OnInit {
  @Input() name: string;
  @Input() info: string;

  @Output() onSelected = new EventEmitter<string>();

  // @ContentChildren

  constructor() { }

  ngOnInit(): void {
  }

  onSelect(): void {
    this.onSelected.emit(this.name);
  }

}
