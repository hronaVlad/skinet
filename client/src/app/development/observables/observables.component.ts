import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, debounceTime, distinctUntilChanged, filter, switchMap, catchError, of, map } from 'rxjs';

@Component({
  selector: 'app-observables',
  templateUrl: './observables.component.html',
  styleUrls: ['./observables.component.scss']
})
export class ObservablesComponent implements OnInit {


  input: FormControl = new FormControl();

  ponies: string[] = [];

  constructor() { }

  ngOnInit(): void {
    this.input.valueChanges
    .pipe(
      filter((value:string) => value.length >= 3),
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap((value:string) => this.getItems(value).pipe(
        catchError(error => of([])),
        map( (values:string[], index) => {

          values.push(Math.random().toString());

          values =values.filter(item => item.indexOf(value)> -1);

          return values;
        })
      )),
    )
    .subscribe(value => {
      console.log(value);
      this.ponies = value;
    });
  }

  getItems(value:string): Observable<string[]> {
    console.log(`getItems value: ${value}`);
    return new Observable<string[]> (_ => _.next(['item1', 'item2', 'item3']));
  }

  customerObservable(): void {
    new Observable<number>(_ => {
      _.next(1);
       _.next(2);
      _.complete()
    }).subscribe({
        next: number => console.log(number)
    });
  }

}
