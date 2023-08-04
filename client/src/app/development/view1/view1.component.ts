import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, QueryList, ViewChildren } from '@angular/core';
import { Pony } from './pony';
import { PonyComponent } from './pony/pony.component';

@Component({
  selector: 'app-view1',
  templateUrl: './view1.component.html',
  styleUrls: ['./view1.component.scss']
})
export class View1Component implements OnInit, AfterViewInit {

  @ViewChild('name') inputName: ElementRef<HTMLInputElement>;
  @ViewChildren(PonyComponent) ponyList: QueryList<PonyComponent>;

  ponies: Pony[] = [
    {name: 'Madam', info: 'mini pony'},
    {name: 'Pokemon', info: 'a very powerful guy'}
  ]

  selectedPony!: string;


  constructor() {

  }


  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    console.log("input value", this.inputName.nativeElement.value);

    this.ponyList.changes.subscribe({
      next: list => {
        console.log(list.length);
      }
    });
  }

  onPonySelected(name: string) {
    console.log(name);
    this.selectedPony = name;
  }

}
