import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketItem, IOrderTotals } from '../shared/models/basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  basket$: Observable<IBasket>;
  priceTotals$: Observable<IOrderTotals>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.priceTotals$ = this.basketService.priceTotals$;
  }

  increaseQuantity(item: IBasketItem): void {
    this.basketService.increaseItem(item);
  }

  decreaseQuantity(item: IBasketItem): void {
    this.basketService.decreaseItem(item);
  }

  remove(item: IBasketItem) {
    this.basketService.removeItem(item);
  }
}
