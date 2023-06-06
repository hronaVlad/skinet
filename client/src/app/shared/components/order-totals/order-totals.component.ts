import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IOrderTotals } from '../../models/basket';

@Component({
  selector: 'order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {

  // basketTotals$: Observable<IBasketTotals>;

  @Input() orderTotals: IOrderTotals;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    // this.basketTotals$ = this.basketService.basketTotals$;
  }
}
