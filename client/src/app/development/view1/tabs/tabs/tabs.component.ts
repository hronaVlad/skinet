import { AfterViewInit, Component, ContentChildren, OnInit, QueryList, ViewChildren } from '@angular/core';
import { TabItemComponent } from '../tab-item/tab-item.component';

@Component({
  selector: 'app-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.scss']
})
export class TabsComponent implements OnInit, AfterViewInit {

  @ContentChildren(TabItemComponent, {descendants: true}) items!: QueryList<TabItemComponent>;

  publictabItems: string[];

  constructor() { }


  ngOnInit(): void {
  }

  ngAfterViewInit(): void {

  }

}
