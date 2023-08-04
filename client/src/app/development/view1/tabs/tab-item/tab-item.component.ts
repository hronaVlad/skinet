import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-tab-item',
  templateUrl: './tab-item.component.html',
  styleUrls: ['./tab-item.component.scss']
})
export class TabItemComponent implements OnInit {

  @Input() title: string;

  constructor() { }

  ngOnInit(): void {
  }
}
